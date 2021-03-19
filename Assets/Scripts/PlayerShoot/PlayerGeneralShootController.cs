using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using static Gun;

public class PlayerGeneralShootController : NetworkBehaviour
{

    public Animator bodyAnimator;
    public GameObject muzzleFlash;
    public GameObject handgunSmoke;
    public Transform gunPosition;
    public GameObject handgunBullet;
    public Gun currGun;
    public TextMeshProUGUI ammoText;
    public CooldownBar cooldownBar;

    [SerializeField] private int numBulletsInGun; // server is authority on whether player needs to reload or not
    private bool isReloading;
    private float muzzleFlashTimer;
    private float muzzleFlashTime = .05f;
	private AudioManager audioManager;
    public Dictionary<GunType, int> ammoPerGunType = new Dictionary<GunType, int>();

    public float coolDownTimer = 100f;

    private void Awake()
    {
        currGun = new Gun(1f, Color.white, 12, 100, "Starter Gun", GunType.Pistol, 60f, 1f, .3f, 3, 6, Rarity.Common, new List<string>(new string[] { "INACCURATE" }));
        ammoPerGunType[GunType.Pistol] = 1000 - currGun.clipSize;
        numBulletsInGun = currGun.clipSize;
        CorrectAmmoText();
		audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (!hasAuthority) { return; }

        /* muzzle flash is aesthetic, less data sent to server if client handles validation 
         *  on updates and only sends command if it's time to toggle muzzle flash: 
         *  RemoveMuzzleFlashCmd called from this [Client] Update() and CreateSmokeCmd from ShootHandgun()
        */
        if (muzzleFlashTimer > muzzleFlashTime && muzzleFlash.activeSelf)
            RemoveMuzzleFlashCmd();
        else
            muzzleFlashTimer += Time.deltaTime;
        
        
        coolDownTimer += Time.deltaTime;
        if (cooldownBar != null)
        {
            cooldownBar.SetFill(coolDownTimer, currGun.cooldownTime);
        }
    }

    [Command]
    private void RemoveMuzzleFlashCmd()
    {
        RemoveMuzzleFlashRpc();
    }
    [ClientRpc]
    private void RemoveMuzzleFlashRpc()
    {
        muzzleFlash.SetActive(false);
    }

    [Client]
    private void CreateSmoke()
    {
        CreateSmokeCmd();
    }
    [Command]
    private void CreateSmokeCmd()
    {
        CreateSmokeRpc();
    }
    [ClientRpc]
    private void CreateSmokeRpc()
    {
        GameObject handgunSmokeObj = Instantiate(handgunSmoke);
        handgunSmokeObj.transform.position = gunPosition.position;
        handgunSmokeObj.transform.eulerAngles = new Vector3(transform.eulerAngles.z * -1, 90f, 0);
    }

    [Client]
    public void ShootHandgun()
    {
        ShootHandgunCmd();
    }
    [Command]
    public void ShootHandgunCmd()
    {
        if (numBulletsInGun > 0) // first example of server authority validation
            ShootHandgunRpc();
        else
            ReloadRpc(); // ***!!! NO LONGER VALID: calls [Command] instead of [ClientRpc] in case there is server validation required
    }
    [ClientRpc]
    public void ShootHandgunRpc()
    {        
        CreateSmoke();
        muzzleFlash.SetActive(true);
        muzzleFlashTimer = 0f;
        coolDownTimer = 0f;
        bodyAnimator.SetTrigger("FireHandgun");
        GameObject bullet = Instantiate(handgunBullet, gunPosition.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * currGun.bulletSpeed, ForceMode2D.Impulse);
        numBulletsInGun--;
        CorrectAmmoText();
        audioManager.Play("PistolShoot", 0);
    }

    [Client]
    public void Reload()
    {
        ReloadCmd();
    }
    [Command]
    public void ReloadCmd()
    {
        ReloadRpc();
    }
    [ClientRpc]
    public void ReloadRpc()
    {
        bodyAnimator.speed = 1 / currGun.reloadTime;
        bodyAnimator.SetTrigger("Reload");
        isReloading = true;
        audioManager.Play("PistolReload", 0);
    }

    public bool IsReloading()
    {
        return isReloading;
    }

    public void CorrectAmmoText()
    {
        if(ammoText != null)
        {
            ammoText.text = numBulletsInGun.ToString() + " / " + ammoPerGunType[currGun.gunType].ToString();
        }
    }

    public void SetNewGun(Gun newGun)
    {
        ammoPerGunType[currGun.gunType] += numBulletsInGun;
        currGun = newGun;
        numBulletsInGun = newGun.clipSize;
        ammoPerGunType[currGun.gunType] -= newGun.clipSize;
        CorrectAmmoText();
		audioManager.Play("WeaponSwitch", 0);
    }

    [Client]
    public void CompleteReload()
    {
        CompleteReloadCmd();
    }
    [Command] public void CompleteReloadCmd() { CompleteReloadRpc(); }
    [ClientRpc] public void CompleteReloadRpc()
    {
        isReloading = false;
        bodyAnimator.speed = 1; // body animator is really only data that needs to be broadcast to ALL clients
        ammoPerGunType[currGun.gunType] -= currGun.clipSize - numBulletsInGun;
        numBulletsInGun = currGun.clipSize;
        CorrectAmmoText();
    }
}


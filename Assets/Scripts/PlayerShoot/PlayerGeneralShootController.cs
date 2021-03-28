using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Gun;

public class PlayerGeneralShootController : MonoBehaviour
{

    public Animator bodyAnimator;
    public GameObject muzzleFlash;
    public GameObject handgunSmoke;
    public Transform gunPosition;
    public GameObject handgunBullet;
    public Gun currGun;
    public List<UIWeaponController> uiGunList;
    public TextMeshProUGUI ammoText;
    public CooldownBar cooldownBar;

    private int numBulletsInGun;
    private bool isReloading;
    private float muzzleFlashTimer;
    private float muzzleFlashTime = .05f;
	private AudioManager audioManager;
    public Dictionary<GunType, int> ammoPerGunType = new Dictionary<GunType, int>();
    public List<Sprite> gunSprites;
    public Image currGunSprite;
    public float coolDownTimer = 100f;

    private void Start()
    {
        uiGunList[0].isActive = true;
        int i = 1;
        /*foreach (UIWeaponController uiGun in uiGunList)
        {
            uiGun.playerGeneralShootController = this;
            uiGun.setGun(new Gun(1f, Color.white, 12, 100, "Starter Gun " + i.ToString(), GunType.Pistol, 60f, 2f, .45f, 3, 6, Rarity.Common, new List<string>(new string[] { "INACCURATE" })));
            i++;
        }*/
        uiGunList[0].playerGeneralShootController = this;
        uiGunList[0].setGun(new Gun(1f, Color.white, 12, 100, "Starter Pistol", GunType.Pistol, 60f, 2f, .45f, 3, 6, Rarity.Common, new List<string>(new string[] { "INACCURATE" })));
        uiGunList[1].playerGeneralShootController = this;
        uiGunList[1].setGun(new Gun(1f, Color.white, 20, 100, "Starter Rifle", GunType.Rifle, 60f, 3f, .1f, 1, 3, Rarity.Common, new List<string>(new string[] { "INACCURATE" })));

        currGun = uiGunList[0].getGun();
        ammoPerGunType[GunType.Pistol] = 1000 - currGun.clipSize;
        ammoPerGunType[GunType.Rifle] = 2000;
        numBulletsInGun = currGun.clipSize;
        CorrectAmmoText();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (muzzleFlashTimer > muzzleFlashTime && muzzleFlash.activeSelf)
        {
            muzzleFlash.SetActive(false);
        }
        else
        {
            muzzleFlashTimer += Time.deltaTime;
        }
        
        coolDownTimer += Time.deltaTime;
        if (cooldownBar != null)
        {
            cooldownBar.SetFill(coolDownTimer, currGun.cooldownTime);
        }
    }

    private UIWeaponController getActiveUIWeaponController()
    {
        foreach(UIWeaponController uiGun in uiGunList)
        {
            if(uiGun.isActive)
            {
                return uiGun;
            }
        }
        return uiGunList[0];
    }

    private void CreateSmoke()
    {
        GameObject handgunSmokeObj = Instantiate(handgunSmoke);
        handgunSmokeObj.transform.position = gunPosition.position;
        handgunSmokeObj.transform.eulerAngles = new Vector3(transform.eulerAngles.z * -1, 90f, 0);
    }

    public void ShootHandgun()
    {
		if (numBulletsInGun > 0) {
            CreateSmoke();
            muzzleFlash.SetActive(true);
            muzzleFlashTimer = 0f;
            coolDownTimer = 0f;
            bodyAnimator.SetTrigger("Shoot");
            GameObject bullet = Instantiate(handgunBullet, gunPosition.position, transform.rotation);
            bullet.GetComponent<BulletManager>().gun = currGun;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * currGun.bulletSpeed, ForceMode2D.Impulse);
            numBulletsInGun--;
            CorrectAmmoText();
			audioManager.Play("PistolShoot", 0);
		}
		else
      	    Reload();
    }

    public void Reload()
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
        getActiveUIWeaponController().setGun(newGun);
        numBulletsInGun = newGun.clipSize;
        ammoPerGunType[currGun.gunType] -= newGun.clipSize;
        CorrectAmmoText();
		audioManager.Play("WeaponSwitch", 0);
        bodyAnimator.SetInteger("CurrGunType", (int)currGun.gunType);
        currGunSprite.sprite = gunSprites[(int)currGun.gunType];
    }

    public void CompleteReload()
    {
        isReloading = false;
        bodyAnimator.speed = 1;
        ammoPerGunType[currGun.gunType] -= currGun.clipSize - numBulletsInGun;
        numBulletsInGun = currGun.clipSize;
        CorrectAmmoText();
    }
}


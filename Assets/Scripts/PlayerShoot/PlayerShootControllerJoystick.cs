using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Gun;

public class PlayerShootControllerJoystick : MonoBehaviour
{

    public Animator bodyAnimator;
    public GameObject muzzleFlash;
    public GameObject handgunSmoke;
    public Transform gunPosition;
    public GameObject handgunBullet;
    public Gun currGun;
    public TextMeshProUGUI ammoText;

    private int numBulletsInGun;

    private float muzzleFlashTimer;
    private float muzzleFlashTime = .05f;
    public Dictionary<GunType, int> ammoPerGunType = new Dictionary<GunType, int>();
    public Joystick joystickShooting;

    private float coolDownTimer;

    private void Awake()
    {
        currGun = new Gun(1f, Color.white, 12, 100, "Starter Gun", GunType.Pistol, 60f, 1f, .3f, 3, 6, Rarity.Common, new List<string>( new string[] { "INACCURATE" }));
        ammoPerGunType[GunType.Pistol] = 100 - currGun.clipSize;
        numBulletsInGun = currGun.clipSize;
        CorrectAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if((joystickShooting.Horizontal != 0 || joystickShooting.Vertical != 0) && !IsReloading() && coolDownTimer > currGun.cooldownTime)
        {
            muzzleFlash.SetActive(true);
            muzzleFlashTimer = 0f;
            CreateSmoke();
            ShootHandgun();
            coolDownTimer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.R) )
        {
            Reload();
        }
        if(muzzleFlashTimer > muzzleFlashTime && muzzleFlash.activeSelf)
        {
            muzzleFlash.SetActive(false);
        } else
        {
            muzzleFlashTimer += Time.deltaTime;
        }
        coolDownTimer += Time.deltaTime;
    }

    private void CreateSmoke()
    {
        GameObject handgunSmokeObj = Instantiate(handgunSmoke);
        handgunSmokeObj.transform.position = gunPosition.position;
        handgunSmokeObj.transform.eulerAngles = new Vector3(transform.eulerAngles.z * -1, 90f, 0);
    }

    private void ShootHandgun()
    {
        bodyAnimator.SetTrigger("FireHandgun");
        GameObject bullet = Instantiate(handgunBullet, gunPosition.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * currGun.bulletSpeed, ForceMode2D.Impulse);
        numBulletsInGun--;
        CorrectAmmoText();
        if (numBulletsInGun == 0)
        {
            Reload();
        }
        
    }

    private void Reload()
    {
        bodyAnimator.SetTrigger("Reload");
    }

    private bool IsReloading()
    {
        return bodyAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Reload");
    }

    public void CorrectAmmoText()
    {
        ammoText.text = numBulletsInGun.ToString() + " / " + ammoPerGunType[currGun.gunType].ToString();
    }

    public void SetNewGun(Gun newGun)
    {
        ammoPerGunType[currGun.gunType] += numBulletsInGun;
        currGun = newGun;
        numBulletsInGun = newGun.clipSize;
        ammoPerGunType[currGun.gunType] -= newGun.clipSize;
        CorrectAmmoText();
    }


    public void CompleteReload()
    {
        ammoPerGunType[currGun.gunType] -= currGun.clipSize - numBulletsInGun;
        numBulletsInGun = currGun.clipSize;
        CorrectAmmoText();
    }
}

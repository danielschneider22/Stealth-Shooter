using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponController : MonoBehaviour
{
    private Gun gun;
    public bool isActive;
    public Sprite activeImage;
    public Sprite inactiveImage;
    private Image image;
    public PlayerGeneralShootController playerGeneralShootController;
    public GameObject statsContainer;
    private InitializeWithGun initializeWithGun;
    public ButtonManager hideStatsButtonManager;
    public TextMeshProUGUI gunName;
    public Image weaponIcon;
    public List<Sprite> gunImages;

    private void Awake()
    {
        image = transform.GetChild(0).GetChild(0).GetComponent<Image>();
        hideStatsButtonManager.OnButtonClicked += HideStatsContainer;
        initializeWithGun = statsContainer.GetComponent<InitializeWithGun>();
    }

    public Gun getGun()
    {
        return gun;
    }

    public void setGun(Gun gun)
    {
        this.gun = gun;
        gunName.text = gun.gunName;
        initializeWithGun.Initialize(gun, null);
        weaponIcon.sprite = gunImages[(int)gun.gunType];
    }

    public void ToggleActive(bool isActive)
    {
        this.isActive = isActive;
        if(this.isActive)
        {
            image.sprite = activeImage;
            playerGeneralShootController.SetNewGun(gun);
        }
        if (!this.isActive)
        {
            image.sprite = inactiveImage;
        }
    }

    public void UiWeaponTouched()
    {
        foreach(Transform child in transform.parent)
        {
            child.gameObject.GetComponent<UIWeaponController>().ToggleActive(false);
        }
        ToggleActive(true);
    }

    public void ShowStatsContainer()
    {
        statsContainer.SetActive(true);
    }
    public void HideStatsContainer()
    {
        statsContainer.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeaponController : MonoBehaviour
{
    public Gun gun;
    public bool isActive;
    public Sprite activeImage;
    public Sprite inactiveImage;
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ToggleActive(bool isActive)
    {
        this.isActive = isActive;
        if(this.isActive)
        {
            image.sprite = activeImage;
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
}

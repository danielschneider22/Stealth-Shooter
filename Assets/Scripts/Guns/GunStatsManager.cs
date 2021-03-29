using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Gun;

public class GunStatsManager : MonoBehaviour
{
    public Gun gun;
    public MoveUpAndDown moveUpAndDown;
    public GameObject statsContainerSolo;
    public GameObject statsContainerPlayerGun;
    public GameObject statsContainerSolo2;
    private InitializeGunPickupColors initializeGunPickupColors;
    public bool isNotRandomGun;
    public List<Sprite> gunImages;
    public SpriteRenderer worldGunSprite;

    void Awake()
    {
        initializeGunPickupColors = GetComponent<InitializeGunPickupColors>();
        Rarity rarity = GetRandomRarity();
        int randomGunType = Random.Range(0, System.Enum.GetNames(typeof(GunType)).Length);
        switch (randomGunType)
        {
            case 0:
                gun = new Gun(1f, Color.white, GetRandomClipSize(), 100, GunNameGenerator.GeneratePistolName(rarity), GunType.Pistol, 60f, GetRandomReloadTime(), GetRandomCooldownTime(), GetRandomLowDamage(rarity), GetRandomHighDamage(rarity), rarity, new List<string>());
                break;
            case 1:
                gun = new Gun(1f, Color.white, GetRandomClipSizeRifle(), 100, GunNameGenerator.GenerateRifleName(rarity), GunType.Rifle, 60f, GetRandomReloadTimeRifle(), GetRandomCooldownTimeRifle(), GetRandomLowDamageRifle(rarity), GetRandomHighDamageRifle(rarity), rarity, new List<string>());
                break;
        }
        InitializeGunDrop(gun);
    }

    private int GetRandomHighDamage(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Random.Range(6, 8);
            case Rarity.Uncommon:
                return Random.Range(9, 13);
            case Rarity.Rare:
                return Random.Range(13, 17);
            case Rarity.UltraRare:
                return Random.Range(15, 22);
            case Rarity.God:
                return Random.Range(18, 25);
            default:
                return 1;
        }

    }

    private int GetRandomLowDamage(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Random.Range(2, 5);
            case Rarity.Uncommon:
                return Random.Range(4, 8);
            case Rarity.Rare:
                return Random.Range(7, 12);
            case Rarity.UltraRare:
                return Random.Range(9, 14);
            case Rarity.God:
                return Random.Range(12, 17);
            default:
                return 1;
        }  

    }

    private int GetRandomHighDamageRifle(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Random.Range(3, 5);
            case Rarity.Uncommon:
                return Random.Range(5, 8);
            case Rarity.Rare:
                return Random.Range(7, 9);
            case Rarity.UltraRare:
                return Random.Range(8, 12);
            case Rarity.God:
                return Random.Range(12, 16);
            default:
                return 1;
        }

    }

    private int GetRandomLowDamageRifle(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common:
                return Random.Range(1, 2);
            case Rarity.Uncommon:
                return Random.Range(3, 4);
            case Rarity.Rare:
                return Random.Range(5, 6);
            case Rarity.UltraRare:
                return Random.Range(7, 8);
            case Rarity.God:
                return Random.Range(9, 10);
            default:
                return 1;
        }

    }

    private int GetRandomClipSize()
    {
        return Random.Range(12, 20);
    }

    private int GetRandomClipSizeRifle()
    {
        return Random.Range(20, 40);
    }

    private float GetRandomReloadTime()
    {
        return Mathf.Round(Random.Range(.75f, 2f) * 100f) / 100f;
    }

    private float GetRandomReloadTimeRifle()
    {
        return Mathf.Round(Random.Range(1.25f, 4f) * 100f) / 100f;
    }

    private float GetRandomCooldownTime()
    {
        return Mathf.Round(Random.Range(.10f, .25f) * 100f) / 100f;
    }

    private float GetRandomCooldownTimeRifle()
    {
        return Mathf.Round(Random.Range(.06f, .2f) * 100f) / 100f;
    }

    private Rarity GetRandomRarity()
    {
        float randomNum = Random.Range(0f, 1f);
        if(randomNum <= .5f)
        {
            return Rarity.Common;
        } else if (randomNum <= .75f)
        {
            return Rarity.Uncommon;
        } else if (randomNum <= .9f)
        {
            return Rarity.Rare;
        } else if (randomNum <= .95f)
        {
            return Rarity.UltraRare;
        } else
        {
            return Rarity.God;
        }
    }

    private void Update()
    {
        if(!moveUpAndDown.enabled)
        {
            moveUpAndDown.enabled = true;
        }
    }

    public void InitializeGunDrop(Gun newGun)
    {
        gun = newGun;
        initializeGunPickupColors.InitializeColor(newGun);
        worldGunSprite.sprite = gunImages[(int)newGun.gunType];
    }

}

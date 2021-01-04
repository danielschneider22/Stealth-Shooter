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

    void Awake()
    {
        initializeGunPickupColors = GetComponent<InitializeGunPickupColors>();
        Rarity rarity = Rarity.Uncommon;
        gun = new Gun(1f, Color.white, 20, 100, GunNameGenerator.GeneratePistolName(rarity), GunType.Pistol, 60f, 1f, .1f, 2, 8, rarity, new List<string>());
        InitializeGunDrop(gun);
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
    }

}

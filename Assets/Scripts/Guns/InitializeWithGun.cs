using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitializeWithGun : MonoBehaviour
{
    public TextMeshProUGUI gunName;
    public TextMeshProUGUI damageLow;
    public TextMeshProUGUI damageHigh;
    public TextMeshProUGUI clipSize;
    public TextMeshProUGUI reloadTime;
    public TextMeshProUGUI specialStats;

    public Image bgWithColor;
    public Image innerCircle;
    public Image outerCircle;
    public Image bgName;
    public Image bgStats;

    public void Initialize(Gun gun, Gun playerGun)
    {
        gunName.text = gun.gunName;
        damageLow.text = gun.damageLow.ToString();
        damageHigh.text = gun.damageHigh.ToString();
        clipSize.text = gun.clipSize.ToString();
        reloadTime.text = gun.reloadTime.ToString() + " sec";

        if (playerGun != null)
        {
            damageLow.color = playerGun.damageLow > gun.damageLow ? Color.red : playerGun.damageLow < gun.damageLow ? Color.green : Color.yellow;
            damageHigh.color = playerGun.damageHigh > gun.damageHigh ? Color.red : playerGun.damageHigh < gun.damageHigh ? Color.green : Color.yellow;
            clipSize.color = playerGun.clipSize > gun.clipSize ? Color.red : playerGun.clipSize < gun.clipSize ? Color.green : Color.yellow;
            reloadTime.color = playerGun.reloadTime > gun.reloadTime ? Color.red : playerGun.clipSize < gun.reloadTime ? Color.green : Color.yellow;
        }

        specialStats.text = "";
        foreach (string specialStat in gun.specialStats)
        {
            specialStats.text += specialStat + " ";
        }

        switch (gun.rarity)
        {
            case Rarity.Common:
                bgWithColor.color = new Color(InitializeGunPickupColors.commonDarkerColor.r + .1f, InitializeGunPickupColors.commonDarkerColor.g + .1f, InitializeGunPickupColors.commonDarkerColor.b + .1f, .4f);
                bgName.color = new Color(InitializeGunPickupColors.commonDarkerColor.r - .1f, InitializeGunPickupColors.commonDarkerColor.g - .1f, InitializeGunPickupColors.commonDarkerColor.b - .1f, .4f);
                bgStats.color = new Color(InitializeGunPickupColors.commonPrimaryColor.r - .05f, InitializeGunPickupColors.commonPrimaryColor.g - .05f, InitializeGunPickupColors.commonPrimaryColor.b - .05f, .4f);
                innerCircle.color = InitializeGunPickupColors.commonDarkerColor;
                outerCircle.color = InitializeGunPickupColors.commonPrimaryColor;
                break;
            case Rarity.Uncommon:
                bgWithColor.color = new Color(0, 0.1719078f, 0.3867925f, .4f);
                bgName.color = new Color(0, 0.1283437f, 0.1981132f, .4f);
                bgStats.color = new Color(0, 0.2991122f, 0.4622642f, .4f);
                innerCircle.color = InitializeGunPickupColors.uncommonDarkerColor;
                outerCircle.color = InitializeGunPickupColors.uncommonPrimaryColor;
                break;
            case Rarity.Rare:
            case Rarity.UltraRare:
            case Rarity.God:
                break;
        }

    }
}

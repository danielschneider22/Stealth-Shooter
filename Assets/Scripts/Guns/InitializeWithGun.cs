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
    public TextMeshProUGUI cooldownTime;
    public TextMeshProUGUI specialStats;

    public Image bgWithColor;
    public Image innerCircle;
    public Image outerCircle;
    public Image bgName;
    public Image bgStats;

    public List<Sprite> gunImages;
    public Image gunImage;

    public void Initialize(Gun gun, Gun playerGun)
    {
        gunName.text = gun.gunName;
        damageLow.text = gun.damageLow.ToString();
        damageHigh.text = gun.damageHigh.ToString();
        clipSize.text = gun.clipSize.ToString();
        reloadTime.text = gun.reloadTime.ToString() + " sec";
        cooldownTime.text = gun.cooldownTime.ToString();
        gunImage.sprite = gunImages[(int)gun.gunType];

        if (playerGun != null)
        {
            damageLow.color = playerGun.damageLow > gun.damageLow ? Color.red : playerGun.damageLow < gun.damageLow ? Color.green : Color.yellow;
            damageHigh.color = playerGun.damageHigh > gun.damageHigh ? Color.red : playerGun.damageHigh < gun.damageHigh ? Color.green : Color.yellow;
            clipSize.color = playerGun.clipSize > gun.clipSize ? Color.red : playerGun.clipSize < gun.clipSize ? Color.green : Color.yellow;
            reloadTime.color = playerGun.reloadTime < gun.reloadTime ? Color.red : playerGun.reloadTime > gun.reloadTime ? Color.green : Color.yellow;
            cooldownTime.color = playerGun.cooldownTime < gun.cooldownTime ? Color.red : playerGun.cooldownTime > gun.cooldownTime ? Color.green : Color.yellow;
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
                bgWithColor.color = new Color(.678f, .68f, .2f);
                bgName.color = new Color(.17f, .17f, .17f);
                bgStats.color = new Color(.5f, .5f, .24f);
                innerCircle.color = InitializeGunPickupColors.rareDarkerColor;
                outerCircle.color = InitializeGunPickupColors.rarePrimaryColor;
                break;
            case Rarity.UltraRare:
                bgWithColor.color = new Color(InitializeGunPickupColors.ultraRareDarkerColor.r + .1f, InitializeGunPickupColors.ultraRareDarkerColor.g + .1f, InitializeGunPickupColors.ultraRareDarkerColor.b + .1f, .4f);
                bgName.color = new Color(InitializeGunPickupColors.ultraRareDarkerColor.r - .1f, InitializeGunPickupColors.ultraRareDarkerColor.g - .1f, InitializeGunPickupColors.ultraRareDarkerColor.b - .1f, .4f);
                bgStats.color = new Color(InitializeGunPickupColors.ultraRarePrimaryColor.r - .05f, InitializeGunPickupColors.ultraRarePrimaryColor.g - .05f, InitializeGunPickupColors.ultraRarePrimaryColor.b - .05f, .4f);
                innerCircle.color = InitializeGunPickupColors.ultraRareDarkerColor;
                outerCircle.color = InitializeGunPickupColors.ultraRarePrimaryColor;
                break;
            case Rarity.God:
                bgWithColor.color = new Color(InitializeGunPickupColors.godDarkerColor.r + .1f, InitializeGunPickupColors.godDarkerColor.g + .1f, InitializeGunPickupColors.godDarkerColor.b + .1f, .4f);
                bgName.color = new Color(InitializeGunPickupColors.godDarkerColor.r - .1f, InitializeGunPickupColors.godDarkerColor.g - .1f, InitializeGunPickupColors.godDarkerColor.b - .1f, .4f);
                bgStats.color = new Color(InitializeGunPickupColors.godPrimaryColor.r - .05f, InitializeGunPickupColors.godPrimaryColor.g - .05f, InitializeGunPickupColors.godPrimaryColor.b - .05f, .4f);
                innerCircle.color = InitializeGunPickupColors.godDarkerColor;
                outerCircle.color = InitializeGunPickupColors.godPrimaryColor;
                break;
        }

    }
}

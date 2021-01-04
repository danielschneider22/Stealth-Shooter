using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class InitializeGunPickupColors : MonoBehaviour
{
    public Light2D innerLight;
    public Light2D outerCircleLight;
    public ParticleSystem gunPickupParticleSystem;
    public static Color uncommonPrimaryColor = new Color(0, 0.7960784f, 1);
    public static Color uncommonDarkerColor = new Color(0, 0.5030503f, 1);
    public static Color commonDarkerColor = new Color(0.2862745f, 0.2039216f, 0.06666667f);
    public static Color commonPrimaryColor = new Color(0.4339623f, 0.352827f, 0.2108402f);
    public static Color taesad = new Color(0, 1, 1);

    public void InitializeColor(Gun gun)
    {
        var main = gunPickupParticleSystem.main;
        switch (gun.rarity)
        {
            case Rarity.Common:
                innerLight.color = commonPrimaryColor;
                outerCircleLight.color = commonDarkerColor;
                main.startColor = new ParticleSystem.MinMaxGradient(commonDarkerColor, commonPrimaryColor);
                break;
            case Rarity.Uncommon:
                innerLight.color = uncommonPrimaryColor;
                outerCircleLight.color = uncommonPrimaryColor;
                main.startColor = new ParticleSystem.MinMaxGradient(uncommonDarkerColor, uncommonPrimaryColor);
                break;
            case Rarity.Rare:
            case Rarity.UltraRare:
            case Rarity.God:
                innerLight.color = taesad;
                break;
        }
    }
}

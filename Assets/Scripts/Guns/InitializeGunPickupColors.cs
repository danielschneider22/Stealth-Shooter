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
    public static Color rareDarkerColor = new Color(0.8490566f, 0.8490566f, 0f);
    public static Color rarePrimaryColor = new Color(0.5294118f, 0.5294118f, 0f);
    public static Color ultraRareDarkerColor = new Color(0.8924528f, 0.33f, 0f);
    public static Color ultraRarePrimaryColor = new Color(0.5294118f, 0.05882353f, 0f);
    public static Color godDarkerColor = new Color(0.1926472f, 0, 0.22f);
    public static Color godPrimaryColor = new Color(0.3109777f, 0, 0.35f);

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
                innerLight.color = rarePrimaryColor;
                outerCircleLight.color = rareDarkerColor;
                main.startColor = new ParticleSystem.MinMaxGradient(rareDarkerColor, rarePrimaryColor);
                break;
            case Rarity.UltraRare:
                innerLight.color = ultraRarePrimaryColor;
                outerCircleLight.color = ultraRareDarkerColor;
                main.startColor = new ParticleSystem.MinMaxGradient(ultraRareDarkerColor, ultraRarePrimaryColor);
                break;
            case Rarity.God:
                innerLight.color = godPrimaryColor;
                outerCircleLight.color = godDarkerColor;
                main.startColor = new ParticleSystem.MinMaxGradient(new Color(.71f, 0f, .8f), new Color(.96f, .6f, 1f));
                break;
        }
    }
}

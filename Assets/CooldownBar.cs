using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    private Image cooldownBar;
    private Color green = new Color(0, .32f, .1f);
    private Color yellow = new Color(.32f, .3f, 0f);

    void Start()
    {
        cooldownBar = GetComponent<Image>();
    }

    public void SetFill(float currTime, float gunCooldown)
    {
        float fill = currTime / gunCooldown;
        if (fill > 1)
        {
            fill = 1f;
            cooldownBar.color = green;
        } else
        {
            cooldownBar.color = yellow;
        }
            
        cooldownBar.fillAmount = fill;

    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public Animator uiHitAnimator;
    public int health = 100;
    public SpriteRenderer playerSprite;
    public ScreenShakeController screenShakeController;

    private float invulnerabilityTime = .5f;
    public float invulnerabilityTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + health.ToString();
    }

    private void FixedUpdate()
    {
        invulnerabilityTimer += Time.deltaTime;
        if(!IsInvulnerable() && playerSprite.color == Color.red)
        {
            playerSprite.color = Color.white;
        }
    }

    public void HealthChange(int changeAmount)
    {
        
        if(changeAmount < 0 && !IsInvulnerable())
        {
            uiHitAnimator.SetTrigger("Hit");
            invulnerabilityTimer = 0f;
            screenShakeController.StartShake(.5f, 1);

            health = health + changeAmount;
            healthText.text = "Health: " + health.ToString();
            playerSprite.color = Color.red;
        } else if (changeAmount > 0)
        {
            health = health + changeAmount;
            healthText.text = "Health: " + health.ToString();
        }
    }

    public bool IsInvulnerable()
    {
        return invulnerabilityTimer < invulnerabilityTime;
    }
}

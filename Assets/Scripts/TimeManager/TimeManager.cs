using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLength = 5f;
    public float origTime = .02f;
    public float improvedRunSpeed = 4000f;

    public PlayerMovement playerMovement;

    private float origMovementSpeed;

    public void Start()
    {
        origTime = Time.fixedDeltaTime;
    }
    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        origMovementSpeed = playerMovement.runSpeed;
        playerMovement.runSpeed = improvedRunSpeed;
    }

    public void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            DoSlowMotion();
        }
        if(Time.timeScale != 1)
        {
            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            float slowdownCompleteProportion = Time.timeScale / 1f;
            playerMovement.runSpeed = Mathf.Max(improvedRunSpeed * (1 - slowdownCompleteProportion), 2000f);
            if (Time.timeScale == 1.0f)
            {
                Time.fixedDeltaTime = origTime;
                playerMovement.runSpeed = origMovementSpeed;
            }
        }*/
    }
}

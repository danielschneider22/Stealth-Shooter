using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Gun;

public class PlayerShootControllerJoystick : MonoBehaviour
{
    public Joystick joystickShooting;

    private PlayerGeneralShootController playerGeneralShootController;

    private void Awake()
    {
        playerGeneralShootController = GetComponent<PlayerGeneralShootController>();
    }

    // Update is called once per frame
    void Update()
    {
        if((joystickShooting.Horizontal != 0 || joystickShooting.Vertical != 0) && !playerGeneralShootController.IsReloading() && playerGeneralShootController.coolDownTimer > playerGeneralShootController.currGun.cooldownTime)
        {
            playerGeneralShootController.ShootHandgun();
        }
        if(Input.GetKeyDown(KeyCode.R) )
        {
            playerGeneralShootController.Reload();
        }
    }
}

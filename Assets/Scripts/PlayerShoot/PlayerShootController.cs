using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Gun;

public class PlayerShootController : MonoBehaviour
{
		public bool	canShootOverride;
    private PlayerGeneralShootController playerGeneralShootController;

    private void Awake()
    {
        playerGeneralShootController = GetComponent<PlayerGeneralShootController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") 
					&& ((!playerGeneralShootController.IsReloading() && playerGeneralShootController.coolDownTimer > playerGeneralShootController.currGun.cooldownTime) 
					|| (canShootOverride))
				)
        {
            playerGeneralShootController.ShootHandgun();
        }
        if(Input.GetKeyDown(KeyCode.R) )
        {
            playerGeneralShootController.Reload();
        }
    }
}

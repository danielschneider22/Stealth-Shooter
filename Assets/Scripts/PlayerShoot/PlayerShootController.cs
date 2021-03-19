using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using static Gun;

public class PlayerShootController : NetworkBehaviour
{
	public bool	canShootOverride;
    private PlayerGeneralShootController playerGeneralShootController;

    private void Awake()
    {
        playerGeneralShootController = GetComponent<PlayerGeneralShootController>();
    }

    // Update is called once per frame
    [Client]
    void Update()
    {
        if (!hasAuthority) { return;  }
        if (Input.GetButton("Fire1") 
					&& ((!playerGeneralShootController.IsReloading() && playerGeneralShootController.coolDownTimer > playerGeneralShootController.currGun.cooldownTime) 
					|| (Input.GetButtonDown("Fire1") && canShootOverride))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectAmmoOnRunover : MonoBehaviour
{
    public int ammoOnGun;
    private Gun gun;

    private void Start()
    {
        gun = gameObject.transform.parent.gameObject.GetComponent<GunStatsManager>().gun;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ammoOnGun > 0 && collision.gameObject.name == "Player 1")
        {
            PlayerGeneralShootController playerShootController = collision.gameObject.GetComponent<PlayerGeneralShootController>();
            playerShootController.ammoPerGunType[gun.gunType] += ammoOnGun;
            playerShootController.CorrectAmmoText();
            ammoOnGun = 0;
        }
    }
}

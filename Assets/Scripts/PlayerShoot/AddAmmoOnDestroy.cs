using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmmoOnDestroy : MonoBehaviour
{
    private PlayerGeneralShootController playerGeneralShootController;
    private ParticleSystem _particleSystem;

    public void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        playerGeneralShootController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGeneralShootController>();
    }

    private void OnDestroy()
    {
        // ParticleSystem.MainModule main = _particleSystem.main;
        playerGeneralShootController.ammoPerGunType[Gun.GunType.Pistol] += 100;
        playerGeneralShootController.CorrectAmmoText();
    }
}

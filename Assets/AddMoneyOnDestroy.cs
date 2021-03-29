using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoneyOnDestroy : MonoBehaviour
{
    private PlayerMoneyManager playerMoneyManager;
    private ParticleSystem _particleSystem;

    public void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        playerMoneyManager = GameObject.FindGameObjectWithTag("PlayerMoneyManager").GetComponent<PlayerMoneyManager>();
    }

    private void OnDestroy()
    {
        ParticleSystem.MainModule main = _particleSystem.main;
        playerMoneyManager.changeMoney(main.maxParticles);
    }
}

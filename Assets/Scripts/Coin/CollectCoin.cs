using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject coinParticleEffect;

    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            audioManager.Play("CollectCoin", 0);
            GameObject particleEffect = Instantiate(coinParticleEffect, transform.position, coinParticleEffect.transform.rotation);
            Destroy(gameObject);
        }
    }
}

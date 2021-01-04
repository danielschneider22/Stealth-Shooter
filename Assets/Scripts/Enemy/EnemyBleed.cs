using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBleed : MonoBehaviour
{
    private AudioManager audioManager;
    public GameObject enemyBleedEffect;

    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            GameObject particleEffect = Instantiate(enemyBleedEffect, transform.position, enemyBleedEffect.transform.rotation);
        }
    }
}

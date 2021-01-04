using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerTrigger : MonoBehaviour
{
    private HealthManager healthManager;
    public int damage = 10;
    public bool destroyOnHit = false;

    public void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            healthManager.HealthChange(damage * -1);
            if(destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }
}

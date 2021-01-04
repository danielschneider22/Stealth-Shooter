using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthManager : MonoBehaviour
{
    public int health = 40;
    public GameObject bloodPoolEffect;
    public GameObject bodyPartsEffect;
    // Start is called before the first frame update
    public void HealthChange(int changeAmount)
    {
        health = health + changeAmount;
        if(health <= 0)
        {
            GameObject bloodPool = Instantiate(bloodPoolEffect);
            bloodPool.transform.position = transform.position;
            GameObject bodyParts = Instantiate(bodyPartsEffect);
            bodyParts.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}

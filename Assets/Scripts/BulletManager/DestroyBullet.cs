using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private float instantiateTimer;
    public float deathTime = 3f;

    // Update is called once per frame
    void Update()
    {
        instantiateTimer += Time.deltaTime;
        if(instantiateTimer >= deathTime)
        {
            Destroy(gameObject);
        }
    }
}

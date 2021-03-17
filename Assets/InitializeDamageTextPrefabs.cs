using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeDamageTextPrefabs : MonoBehaviour
{
    public GameObject damageTextPrefab;

    public void Awake()
    {
        for(var i = 0; i <=100; i++)
        {
            GameObject damageText = Instantiate(damageTextPrefab);
            damageText.transform.SetParent(transform);
        }
    }
}

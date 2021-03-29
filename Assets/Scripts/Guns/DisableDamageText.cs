using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDamageText : MonoBehaviour
{
    private Vector3 initPosition;
    public void Start()
    {
        initPosition = transform.position;
    }
    public void DisableDamageTextObj()
    {
        gameObject.SetActive(false);
        transform.position = initPosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenAnimationComplete : MonoBehaviour
{
    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
}

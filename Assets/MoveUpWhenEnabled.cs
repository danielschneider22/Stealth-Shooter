using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpWhenEnabled : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += Vector3.up * 10f * Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallColliderManager : MonoBehaviour
{
    public bool willHitWall = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Wall") || collision.gameObject.name.Contains("Zombie"))
        {
            willHitWall = true;
        }
    }
}

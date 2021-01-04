using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    public LevelLoader levelLoader;

    //When the Primitive collides with the walls, it will reverse direction
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            levelLoader.LoadNextLevel();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    public Transform characterToFollow;
    public float yOffsetFromChar;

    void Update()
    {
        if(characterToFollow != null)
        {
            transform.position = new Vector3(characterToFollow.position.x, characterToFollow.position.y + yOffsetFromChar);
        }
    }
}

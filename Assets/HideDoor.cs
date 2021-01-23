using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideDoor : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer top;
    public SpriteRenderer bottom;
    private Color doorColor;
    public void Start()
    {
        doorColor = top.color;
    }
    public void MakeDoorHidden()
    {
        animator.enabled = false;
        top.color = Color.white;
        bottom.color = Color.white;

    }

    public void UnhideDoor()
    {
        animator.enabled = true;
        top.color = doorColor;
        bottom.color = doorColor;

    }
}

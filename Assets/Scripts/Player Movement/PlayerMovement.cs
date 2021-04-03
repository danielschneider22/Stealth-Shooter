using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    public PlayerController controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    float verticalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;
    }

    private void FixedUpdate()
    {
        if (!IsLocalPlayer) return;
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
        controller.RotateTowardsMouse();
    }
}

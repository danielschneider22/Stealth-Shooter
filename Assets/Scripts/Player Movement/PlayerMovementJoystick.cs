using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementJoystick : MonoBehaviour
{
    public PlayerController controller;
    public Joystick joystickMovement;
    public Joystick joystickShooting;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    float verticalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = joystickMovement.Horizontal * runSpeed;
        verticalMove = joystickMovement.Vertical * runSpeed;
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
        controller.RotateWithJoystick(joystickShooting);
    }
}

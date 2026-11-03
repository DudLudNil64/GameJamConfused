using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Class veriables
    [SerializeField] float MoveSpeed;

    // Player Components
    Rigidbody2D PlayerBody;

    // Needed inputs for movement -Lud
    InputAction MovementInput;
    InputAction MousePosition;

    private void Start()
    {
        // try to get player body -Lud
        if (gameObject.GetComponent<Rigidbody2D>() != null) PlayerBody = gameObject.GetComponent<Rigidbody2D>();
        else Debug.Log("Player Doesnt Have A Rigidbody2D");

        // Checks for inputs -Lud
        if (InputSystem.actions.FindAction("Main/Movement") != null) MovementInput = InputSystem.actions.FindAction("Main/Movement");
        else Debug.Log("Movement Input Not Found");
        // 
        if (InputSystem.actions.FindAction("Main/MousePosition") != null) MousePosition = InputSystem.actions.FindAction("Main/MousePosition");
        else Debug.Log("MousePosition Input Not Found");
    }

    private void FixedUpdate()
    {
        HandleMove();
        HandleRotate();
    }

    void HandleMove()
    {
        // Read input -Lud
        Vector2 MoveRead = MovementInput.ReadValue<Vector2>();

        // checks if input is doing something -Lud
        if (MoveRead.magnitude > 0)
        {
            // Normalize so that you dont go faster or slower -Lud
            MoveRead.Normalize();
            // Moves with MoveSpeed, might want it to get faster over time -Lud
            PlayerBody.linearVelocity = MoveRead * MoveSpeed;
        }
        else
        {
            // Set speed to zero if the player doesnt move
            PlayerBody.linearVelocity = Vector2.zero;
        }
    }

    void HandleRotate()
    {
        // Gets Mouse Position And Makes It To Player Space - Lud
        Vector3 MouseToWorldPosition = Camera.main.ScreenToWorldPoint(MousePosition.ReadValue<Vector2>());
        MouseToWorldPosition.z = 0;
        Vector2 OffsetVector = transform.position - MouseToWorldPosition;
        // Uses The OffsetVector To Rotate The Player Towards The Mouse
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);
    }
}

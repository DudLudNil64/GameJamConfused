using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Class veriables
    float MoveSpeed;

    // Player Components
    Rigidbody2D PlayerBody;

    // Needed inputs for movement -Lud
    InputAction MovementInput;

    private void Start()
    {
        // try to get player body -Lud
        if (gameObject.GetComponent<Rigidbody2D>() != null) PlayerBody = gameObject.GetComponent<Rigidbody2D>();
        else Debug.Log("Player Doesnt Have A Rigidbody2D");

        // Checks for inputs -Lud
        if (InputSystem.actions.FindAction("Main/Movement") != null) MovementInput = InputSystem.actions.FindAction("Main/Movement");
        else Debug.Log("Movement Input Not Found");

    }

    private void FixedUpdate()
    {
        // Read input
        Vector2 MoveRead = MovementInput.ReadValue<Vector2>();

        // Adds force to move the player
        PlayerBody.AddForce(MoveRead * MoveSpeed, ForceMode2D.Impulse);
    }
}

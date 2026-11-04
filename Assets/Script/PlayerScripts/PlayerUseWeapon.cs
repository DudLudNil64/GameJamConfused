using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUseWeapon : MonoBehaviour
{
   public WeaponBase Pistol;
   public WeaponBase Sword;

    InputAction LeftClick;
    InputAction RightClick;
    void Start()
    {
        if (InputSystem.actions.FindAction("Main/RightMouseButton") != null) LeftClick = InputSystem.actions.FindAction("Main/RightMouseButton");
        else Debug.Log("RightMouseButton Input Not Found");

        // Gets the mouses buttons -Lud
        if (InputSystem.actions.FindAction("Main/LeftMouseButton") != null) LeftClick = InputSystem.actions.FindAction("Main/LeftMouseButton");
        else Debug.Log("LeftMouseButton Input Not Found");
        //
        if (InputSystem.actions.FindAction("Main/RightMouseButton") != null) RightClick = InputSystem.actions.FindAction("Main/RightMouseButton");
        else Debug.Log("RightMouseButton Input Not Found");
    }
    void Update()
    {
        // Sword stuff
        if (RightClick.ReadValue<float>() > 0.5f && Sword.CheckForUseability()) Sword.UseWeapon();
        // Gun stuff
        if (LeftClick.ReadValue<float>() > 0.5f && Pistol.CheckForUseability()) Pistol.UseWeapon();

    }
}

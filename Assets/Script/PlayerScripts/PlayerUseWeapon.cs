using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUseWeapon : MonoBehaviour
{
   public WeaponBase pistol;
   public WeaponSword weaponSword;
   public WeaponBase Pistol;
   public WeaponSword Sword;

    InputAction LeftClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (RightClick.ReadValue<float>() > 0.5f && Sword.CheckForUseability()) Sword.UseWeapon();

        if (LeftClick.ReadValue<float>() > 0)
        {
            if (Pistol.CheckForUseability())
                Pistol.UseWeapon();
        }
        
    }
}

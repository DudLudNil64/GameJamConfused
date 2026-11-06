using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUseWeapon : MonoBehaviour
{
   public WeaponBase Shotgun;
   public WeaponBase Sword;

    InputAction LeftClick;
    InputAction RightClick;
    InputAction Reload;
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
        //
        if(InputSystem.actions.FindAction("Main/ReloadKey") != null) Reload = InputSystem.actions.FindAction("Main/ReloadKey");
        else Debug.Log("ReloadKey Wasn't Found");
    }
    void Update()
    {
        Sword.FirePosition = transform.position + transform.up;
        Shotgun.FirePosition = transform.position + transform.up;

        // Sword stuff -Lud
        if (RightClick.IsPressed() && Sword.CheckForUseability()) Sword.UseWeapon();
        // Gun stuff -Lud
        if (LeftClick.IsPressed() && Shotgun.CheckForUseability()) Shotgun.UseWeapon();
        // Reloads -Lud
        if (Reload.IsPressed()) Shotgun.Reload();
    }
}

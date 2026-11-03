using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerUseWeapon : MonoBehaviour
{
   public WeaponBase pistol;
   public WeaponSword weaponSword;

    InputAction LeftClick;
    InputAction RigthClick;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (InputSystem.actions.FindAction("Main/LeftMouseButton") != null) LeftClick = InputSystem.actions.FindAction("Main/LeftMouseButton");
        else Debug.Log("LeftMouseButton Input Not Found");
    }

    // Update is called once per frame
    void Update()
    {
      

        if (LeftClick.ReadValue<float>() > 0)        
        { 
          
            if(weaponSword.CheckForUseability())
            weaponSword.UseWeapon();
        }
    }
}

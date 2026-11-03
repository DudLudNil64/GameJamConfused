using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WeaponBase : MonoBehaviour
{
    // Used for fireing -Lud
    public Vector2 FirePosition;
    public Vector2 BulletDirection;
    public GameObject Bullet;
    

    // Main stats -Lud
    public int Damage;
    public int AttackCount;

    // For ammo usage can be ignored for swords
    public int Uses;
    public int MaxUses;

    // Delay Veriables for delay -Lud
    public float UseDelay;
    public bool InUseDelay;
    // Reload -Lud
    public float LoadDelay;
    public bool InLoadDelay;

    public virtual void UseWeapon()
    {
        StartCoroutine("StartUseDelay");

        // this functions shoot operation
        GameObject CurrentBullet = Instantiate(Bullet);
        CurrentBullet.transform.position = FirePosition;
    }

    public virtual void Reload()
    {
        StartCoroutine("StartLoadDelay");
    }

    public virtual bool CheckForUseability()
    {
        bool Check = true;

        if(InLoadDelay || InUseDelay) Check = false;

        if(Uses <= 0) Check = false;

        return Check;
    }

    public virtual IEnumerator StartUseDelay()
    {
        Uses--;

        InUseDelay = true;
        yield return new WaitForSeconds(UseDelay);
        InUseDelay = false;
    }
    public virtual IEnumerator StartLoadDelay()
    {
        Uses = MaxUses;

        InLoadDelay = true;
        yield return new WaitForSeconds(LoadDelay);
        InLoadDelay = false;
    }
}

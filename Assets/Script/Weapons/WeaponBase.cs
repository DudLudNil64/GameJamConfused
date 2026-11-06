using System.Collections;
using UnityEngine;

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
        // starts said delay -Lud
        StartCoroutine("StartLoadDelay");
    }

    public virtual bool CheckForUseability()
    {
        // simple if it can be used -Lud

        bool Check = true;

        if(InLoadDelay || InUseDelay) Check = false;

        if(Uses <= 0) Check = false;

        return Check;
    }

    public virtual IEnumerator StartUseDelay()
    {
        // remove use and start delay -Lud
        Uses--;

        InUseDelay = true;
        yield return new WaitForSeconds(UseDelay);
        InUseDelay = false;
    }
    public virtual IEnumerator StartLoadDelay()
    {
        // set uses to max and delay -Lud

        Uses = MaxUses;

        InLoadDelay = true;
        yield return new WaitForSeconds(LoadDelay);
        InLoadDelay = false;
    }
}

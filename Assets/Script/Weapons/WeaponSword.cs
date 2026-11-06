using System.Collections;
using UnityEngine;

public class WeaponSword : WeaponBase
{
    public float AreaAttack;
    public LayerMask mask;
    public override void UseWeapon()
    {
        // get the enemy heal code and trigger "damage" -Lud
        Collider2D[] colliders = Physics2D.OverlapCircleAll(FirePosition, AreaAttack, mask);

        StartCoroutine("StartUseDelay");

        foreach (Collider2D collider in colliders)
        {
            // Return if of same faction -Lud
            if(collider.CompareTag(tag)) continue;

            // Check for what todo since our code is so divided._. -Lud
            if(collider.CompareTag("RandomObject"))
            {
                // does nothing for now -Lud

                continue;
            }
            if (collider.CompareTag("Player"))
            {
                // Gets the players health data and deals damage -Lud
                PlayerData Health = collider.GetComponent<PlayerData>();

                Health.RemoveHealth(Damage);

                continue;
            }
            if (collider.CompareTag("Enemy")) 
            {
                // not shocking but it deals damage to any object tagged enemy -Lud

                EnemyHealthManager Health = collider.GetComponent<EnemyHealthManager>();

                Health.TakeDamage(Damage);

                continue;
            }
        }
    }

    public override bool CheckForUseability()
    {
        bool Check = true;

        if (InUseDelay) Check = false;

        return Check;
    }

    public override IEnumerator StartUseDelay()
    {
        InUseDelay = true;
        yield return new WaitForSeconds(UseDelay);
        InUseDelay = false;
    }

    // draw a shere to can see the attac while not sprite
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(FirePosition, AreaAttack);
    }
}

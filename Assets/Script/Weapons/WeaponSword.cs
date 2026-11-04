using System.Collections;
using UnityEngine;

public class WeaponSword : WeaponBase
{
    public LayerMask mask;
    public override void UseWeapon()
    {
        // get the enemy heal code and trigger "damage"
        Collider2D[] colliders = Physics2D.OverlapCircleAll(FirePosition, 1, mask);

        foreach (Collider2D collider in colliders)
        {
            // Return if of same faction
            if(collider.CompareTag(tag)) continue;

            // Check for what todo since our code is so divided._.
            if(collider.CompareTag("RandomObject"))
            {
                // does nothing for now

                continue;
            }
            if (collider.CompareTag("Player"))
            {
                // Gets the players health data and deals damage
                PlayerData Health = collider.GetComponent<PlayerData>();

                Health.health -= Damage;

                continue;
            }
            if (collider.CompareTag("Enemy"))
            {
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
        Gizmos.DrawSphere(FirePosition, 1);
    }
}

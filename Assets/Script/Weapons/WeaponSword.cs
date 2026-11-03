using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;
using System.Collections;

public class WeaponSword : WeaponBase
{
    public LayerMask mask;
    public override void UseWeapon()
    {
        StartCoroutine("StartUseDelay");
        // get the enemy heal code and trigger "damage"
        Collider2D[] colliders = Physics2D.OverlapCircleAll(FirePosition, 1, mask);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<EnemyHealthManager>().TakeDamage(Damage);
        }

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

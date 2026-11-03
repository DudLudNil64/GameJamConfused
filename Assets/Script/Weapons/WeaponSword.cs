using UnityEngine;
using UnityEngine.UI;

public class WeaponSword : WeaponBase
{
    public int Damage = 10;
    public LayerMask mask;
    public override void UseWeapon()
    {
        // get the enemy heal code and trigger "damage"
        Collider2D[] colliders = Physics2D.OverlapCircleAll(FirePosition, 1, mask);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<EnemyHealthManager>().TakeDamage(Damage); ;
        }

    }
    // draw a shere to can see the attac while not sprite
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(FirePosition, 1);
    }
}

using UnityEngine;

public class ZombieJester : ZombieKnigth
{
    [SerializeField] float attackRange = 3;
    [SerializeField] LayerMask mask;    
    [SerializeField, Range(1, 100)] float avoidRadius = 10;

    

    protected override void Update()
    {
        Collider2D playerBullet = Physics2D.OverlapCircle(transform.position, avoidRadius, mask);// detect certain things who enter in the mask
        if (playerBullet != null)
        {
            //gettt away from bullets

            EnemyBody.linearVelocity -= (Vector2)((playerBullet.transform.position - transform.position).normalized * speed * 3 * Time.deltaTime);
        }

        dir = (player.position - transform.position).normalized; //-1 - 1

        // follow the player 
        if (Vector2.Distance(player.position, transform.position) > attackRange)
        {
            EnemyBody.linearVelocity = dir * speed;

        }

        //if player to close get away from him
        else if (Vector2.Distance(player.position, transform.position) < attackRange - 1)
        {
            EnemyBody.linearVelocity = -dir * speed; 
        }

        Vector2 OffsetVector = transform.position - player.position;
        // Uses The OffsetVector To Rotate The Player Towards The Mouse
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);

        if(Sword.Uses <= 0) Sword.Reload();

        // when and where to shoot
        if (Vector2.Distance(player.position, transform.position) < attackRange && Sword.CheckForUseability())
        {
            Sword.FirePosition = transform.position;
            Sword.UseWeapon();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, avoidRadius);
    }
}

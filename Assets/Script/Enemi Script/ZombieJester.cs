using UnityEngine;

public class ZombieJester : ZombieKnigth
{
    [SerializeField] float attackRange = 3;
    [SerializeField] LayerMask mask;    
    [SerializeField, Range(1, 100)] float avoidRadius = 10;

    protected override void FixedUpdate()
    {
        // Uses The OffsetVector To Rotate The enemy Towards The Mouse
        Vector2 OffsetVector = transform.position - player.position;
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);

        
        Collider2D playerBullet = Physics2D.OverlapCircle(transform.position, avoidRadius, mask);// detect certain things who enter in the mask
        if (playerBullet != null)
        {
            //gettt away from bullets
            EnemyBody.linearVelocity -= (Vector2)((playerBullet.transform.position - transform.position).normalized * speed * 3 * Time.deltaTime);
        }
        

        
        dir = (player.position - transform.position).normalized;
        // follow the player 
        if ((player.position- transform.position).magnitude > attackRange)
        {
            EnemyBody.linearVelocity = dir * speed;
        }
        else if ((player.position- transform.position).magnitude < attackRange - 1)
        {
            EnemyBody.linearVelocity = -dir * speed; 
        }

        if(Sword.Uses <= 0) Sword.Reload();
        Sword.FirePosition = transform.position;


        // when and where to shoot
        if ((player.position- transform.position).magnitude < attackRange && Sword.CheckForUseability())
        {
            Sword.UseWeapon();
        }
    }
}

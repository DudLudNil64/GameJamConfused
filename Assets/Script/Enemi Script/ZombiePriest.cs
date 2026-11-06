using System.Collections;
using UnityEngine;

public class ZombiePriest : ZombieKnigth
{
    [SerializeField] float attackRange = 3;
    [SerializeField] LayerMask mask;
    [SerializeField, Range(1, 100)] float avoidRadius = 10;
    [SerializeField] GameObject summonEnemy;

    protected override void Start()
    {
        
    }

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
        if ((player.position - transform.position).magnitude > attackRange)
        {
            EnemyBody.linearVelocity = dir * speed;
            StartCoroutine(SummonEnemy());
        }
        else if ((player.position - transform.position).magnitude < attackRange - 1)
        {
            EnemyBody.linearVelocity = -dir * speed;
        }
    }

    IEnumerator SummonEnemy()
    {
        GameObject zombieobj = Instantiate(summonEnemy);

        zombieobj.transform.position = transform.position + (Vector3)(Random.insideUnitCircle*3);

        yield return new WaitForSeconds(0.5f);
    }
}

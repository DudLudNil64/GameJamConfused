using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ZombiePriest : ZombieKnigth
{
    [SerializeField] float attackRange = 3;
    [SerializeField] LayerMask mask;
    [SerializeField, Range(1, 100)] float avoidRadius = 10;
    [SerializeField] GameObject summonEnemy;

    protected override void Start()
    {
        // Get player taged object -Lud
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void FixedUpdate()
    {
        // Uses The OffsetVector To Rotate The enemy the player tagged object -Lud
        Vector2 OffsetVector = transform.position - player.position;
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);

        // old code from omar should detect and avoid bullets but doesnt work.. -Lud
        Collider2D playerBullet = Physics2D.OverlapCircle(transform.position, avoidRadius, mask);
        if (playerBullet != null)
        {
            //gettt away from bullets 
            EnemyBody.linearVelocity -= (Vector2)((playerBullet.transform.position - transform.position).normalized * speed * 3 * Time.deltaTime);
        }

        if(!InSpawnDelay)
        {
            // Summon enemy orb and then sets delay -Lud
            StartCoroutine("DelaySpawn");
            StartCoroutine("SummonEnemy");
        }
        else if(Sword.CheckForUseability())
        {
            // Just uses weapon and updates stats -Lud

            Sword.FirePosition = transform.position + transform.up;

            Sword.UseWeapon();
            Sword.Reload();
        }

        // get direction towards player -Lud
        dir = (player.position - transform.position).normalized;

        // follow the player -Lud
        if ((player.position - transform.position).magnitude > attackRange)
        {
            EnemyBody.linearVelocity = dir * speed;
            
        }
        else if ((player.position - transform.position).magnitude < attackRange - 4)
        {
            EnemyBody.linearVelocity = -dir * speed;
        }
    }

    public bool InSpawnDelay;
    public float SpawnDelay;
    IEnumerator DelaySpawn()
    {
        // Just delay -Lud
        InSpawnDelay = true;
        yield return new WaitForSeconds(SpawnDelay);
        InSpawnDelay = false;
    }

    IEnumerator SummonEnemy()
    {
        GameObject zombieobj = Instantiate(summonEnemy);

        zombieobj.transform.position = transform.position + (Vector3)(Random.insideUnitCircle*3);

        yield return new WaitForSeconds(0.5f);
    }
}

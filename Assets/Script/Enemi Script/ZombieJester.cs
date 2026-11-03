using UnityEngine;

public class ZombieJester : ZombieKnigth
{
    [SerializeField] float attackRange = 3;
    public float shootpoint;
    public float cooldown = 1.5f;
    float timer = 0;
    public float bulletForce = 10f;
    [SerializeField] LayerMask mask;    
    [SerializeField, Range(1, 100)] float avoidRadius = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    protected override void Update()
    {
        Collider2D playerBullet = Physics2D.OverlapCircle(transform.position, avoidRadius, mask);// detect certain things who enter in the mask
        if (playerBullet != null)
        {
            //gettt away from bullets

            EnemyBody.linearVelocity /= (playerBullet.transform.position - transform.position).normalized * speed * 3 * Time.deltaTime;
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

        // when and where to shoot
        if (timer >= cooldown && Vector2.Distance(player.position, transform.position) < attackRange)
        {
            print("shoot");
       
            timer = 0;
        }
        timer += Time.deltaTime;


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, avoidRadius);
    }
}

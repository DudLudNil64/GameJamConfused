using UnityEngine;

public class DeathOrb : ZombieJester
{
    // the refrence to a object with a player tag -Lud
    GameObject Player;

    protected override void Start()
    {
        // get a player tag to follow -Lud
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void FixedUpdate()
    {
        // OffsetVector to get direction towards the player -Lud
        Vector2 OffsetVector = Player.transform.position - transform.position;
        OffsetVector.Normalize();
        // goes to player to kill it :) -Lud
        transform.position += (Vector3)(speed * Time.deltaTime * OffsetVector);
        // Just making sure the linearVelocity is zero so it doesnt drift off into space -Lud
        transform.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // make sure it has the player tag
        if(collision.transform.CompareTag("Player"))
        {
            // check if it has the playerdata component so that we only try to deal damage to the player -Lud
            if(collision.gameObject.GetComponent<PlayerData>() != null)
            {
                // remove health -Lud
                collision.gameObject.GetComponent<PlayerData>().RemoveHealth(1);
                Destroy(gameObject);
            }
            else
            {
                // just deastroy -Lud
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

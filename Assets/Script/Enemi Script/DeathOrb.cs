using Unity.VisualScripting;
using UnityEngine;

public class DeathOrb : ZombieJester
{
    GameObject Player;

    protected override void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void FixedUpdate()
    {
        // OffsetVector
        Vector2 OffsetVector = Player.transform.position - transform.position;
        OffsetVector.Normalize();
        // goes to player to kill it :)
        transform.position += (Vector3)(speed * Time.deltaTime * OffsetVector);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if(collision.gameObject.GetComponent<PlayerData>() != null)
            {
                collision.gameObject.GetComponent<PlayerData>().health--;
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

using System.Diagnostics;
using UnityEngine;

public class BulletMoveForward : MonoBehaviour
{
    public int damage;
    public string TAG;

    private void FixedUpdate()
    {
        transform.position += transform.up/2;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Doesnt work....
        if(transform.CompareTag("Untagged")) return;
        // Return if of same faction
        else if(collision.transform.CompareTag(tag)) return;
        // Check for what todo since our code is so divided._.
        else if(collision.transform.CompareTag("RandomObject"))
        {
            // does nothing for now
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("Player"))
        {
            // Gets the players health data and deals damage
            PlayerData Health = collision.transform.GetComponent<PlayerData>();
            if(Health != null ) Health.RemoveHealth(damage);

            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("Enemy"))
        {
            EnemyHealthManager Health = collision.transform.GetComponent<EnemyHealthManager>();
            if(Health != null ) Health.TakeDamage(damage);

            Destroy(gameObject);
        }
        else if(collision.transform.CompareTag("Wall") || collision.transform.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}

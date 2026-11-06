using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int maxHealth = 80;
    public int currentHealth;

    // Should be in update ._. -Lud
    void Start()
    {
        //make curret Healt the same as max in the star-Omar
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }


    //code to take damage-omar
    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }


    //coode to die, yea die.-Omar
    void Die()
    {
        Destroy(gameObject);
    }
}

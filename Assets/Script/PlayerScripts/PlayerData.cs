using System.Collections;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private WeaponSword sword;
    public int maxhealth = 100;
    public int health = 90;
    public bool hasKey = false;

    public void GiveHealth(int amount)
    {
       if (maxhealth > 0 && health < maxhealth)
        {
            health += amount;
        }
    }

    public void RemoveHealth(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void GiveKey()
    {
        hasKey = true;
    }

    public void DamageBoost(int amount, float duration)
    {
        StartCoroutine(Apply(amount, duration));

        IEnumerator Apply(int amount, float duration)
        {
            sword.Damage += amount;
            yield return new WaitForSeconds(duration);
            sword.Damage -= amount;
        }
    }
}

using UnityEngine;

public class DamagePotion : MonoBehaviour
{
    // wasn't used in the end but would have bosted the damage -Lud
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData data = collision.gameObject.GetComponent<PlayerData>();
            GameObject.Destroy(gameObject);
            data.DamageBoost(10, 4f);
        }
    }
}

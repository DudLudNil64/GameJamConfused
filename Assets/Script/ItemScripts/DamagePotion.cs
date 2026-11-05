using UnityEngine;

public class DamagePotion : MonoBehaviour
{
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

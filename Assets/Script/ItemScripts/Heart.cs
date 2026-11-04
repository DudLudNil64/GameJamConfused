using UnityEngine;

public class Hearts : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData data = collision.gameObject.GetComponent<PlayerData>();
            data.GiveHealth(10);
            GameObject.Destroy(gameObject);
        }
    }
}
using UnityEngine;

public class Hearts : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // gives health only to the player -Lud
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData data = collision.gameObject.GetComponent<PlayerData>();
            if(data != null)
            {
                data.GiveHealth(1);
                GameObject.Destroy(gameObject);
            }
        }
    }
}
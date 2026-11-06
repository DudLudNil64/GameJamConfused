using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerData data = collision.gameObject.GetComponent<PlayerData>();
            if(data != null)
            {
                data.GiveKey();
                GameObject.Destroy(gameObject);
            }
        }
    }
}

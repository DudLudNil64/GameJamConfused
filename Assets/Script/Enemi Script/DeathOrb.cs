using Unity.VisualScripting;
using UnityEngine;

public class DeathOrb : MonoBehaviour
{
    GameObject Player;

    void Start()
    {
        GameObject[] FindPlayer = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject obj in FindPlayer)
        {
            if(obj.GetComponent<PlayerData>() != null) Player = obj;
        }

        if(Player == null) Debug.Break();
    }
    void FixedUpdate()
    {
        // Uses The OffsetVector To Rotate The enemy Towards The Mouse
        Vector2 OffsetVector = transform.position - Player.transform.position;
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);

        transform.position += transform.up / 4;
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

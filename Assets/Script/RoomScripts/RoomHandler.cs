using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class RoomHandler : MonoBehaviour
{
<<<<<<< HEAD
=======
     [SerializeField] GameObject[] zombiePrefabs;

    private bool firstEnter = false;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Entered");

        if (other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player entered room: " + gameObject.name);

            if (!firstEnter) 
            {
                firstEnter = true;
                Debug.Log("First enter - Spawning zombies");
                StartCoroutine(SpawnZombies(0.7f));
            }
        }
    }
        }
    

    IEnumerator SpawnZombies(float duration)
    {
        int amount = Random.Range(minZombies, maxZombies);
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(1, zombiePrefabs.Length);
            GameObject selectedZombie = zombiePrefabs[randomIndex];
            Vector3 randomPos = transform.position + new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2), 0f);

            GameObject zombieobj = Instantiate(selectedZombie, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(duration);
        }
    }
}

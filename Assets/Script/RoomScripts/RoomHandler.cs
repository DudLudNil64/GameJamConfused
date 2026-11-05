using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class RoomHandler : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float cameraSize;
    [SerializeField] int minZombies;
    [SerializeField] int maxZombies;
    [SerializeField] GameObject[] zombiePrefabs;
    bool moveCamera = false;
    private bool firstEnter = false;

    void LateUpdate()
    {
       if (moveCamera)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + new Vector3(0, 0, -10), speed);

            if (Vector3.Distance(Camera.main.transform.position, transform.position + new Vector3(0, 0 , -10)) < 0.01f)
            {
                Camera.main.transform.position = transform.position + new Vector3(0, 0, -10);
                moveCamera = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");
        if (other.gameObject.CompareTag("Player"))
        {
            moveCamera = true;
            Camera.main.orthographicSize = cameraSize;
            Debug.Log("Move camera to room" + gameObject.name);

            if (!firstEnter)
            {
                firstEnter = true;
                Debug.Log("First enter");

                StartCoroutine(SpawnZombies(0.7f));
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

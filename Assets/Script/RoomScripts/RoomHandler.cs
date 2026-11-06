using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomHandler : MonoBehaviour
{
    [SerializeField] GameObject[] zombiePrefabs;
    [SerializeField] GameObject bottomWall;
    [SerializeField] GameObject topWall;
    [SerializeField] TextMeshProUGUI TMPro;
    [SerializeField] int minenemies;
    [SerializeField] int maxenemies;

    private bool firstEnter = false;
    private bool haskey = false;

    void FixedUpdate()
    {
        //Check if player has key and if it's close enough - Wiktor 
        if (haskey && (Vector3.Distance(FindAnyObjectByType<PlayerMovement>().transform.position, transform.position + new Vector3(0, 4, 0)) < 1.75f))
        {
            TMPro.gameObject.SetActive(true);
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                haskey = false;
                topWall.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else //Else don't show anything - Wiktor
        {
            TMPro.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");

        //Check if the trigger is the player
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player entered room: " + gameObject.name);

            if (!firstEnter)
            {
                firstEnter = true;
                StartCoroutine(LockDoors());
                StartCoroutine(SpawnZombies(0.7f));
            }
        }
    }

    IEnumerator SpawnZombies(float duration)
    {
        int amount = Random.Range(minenemies, maxenemies);

        //Spawn random amount of zombies between minenemies and maxenemies. Get a random zombie model every instantiate. - Wiktor
        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(1, zombiePrefabs.Length);
            GameObject selectedZombie = zombiePrefabs[randomIndex];
            Vector3 randomPos = transform.position + new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2), 0f);

            GameObject zombieobj = Instantiate(selectedZombie, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(duration);
        }

        //Check if there is enemies inside the room else give key and finish the current dungeon/room - Wiktor
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return null;
        }

        bottomWall.GetComponent<BoxCollider2D>().enabled = false;
        haskey = true;
    }

    IEnumerator LockDoors()
    {
        yield return new WaitForSeconds(1f);
        bottomWall.GetComponent<BoxCollider2D>().enabled = true;
    }
}

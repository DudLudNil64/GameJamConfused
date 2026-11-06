using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

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

    void Update()
    {
        if (haskey && (Vector3.Distance(FindAnyObjectByType<PlayerMovement>().transform.position, transform.position + new Vector3(0, 4, 0)) < 1.75f))
        {
            TMPro.gameObject.SetActive(true);
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                haskey = false;
                topWall.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        else
        {
            TMPro.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered");

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

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(1, zombiePrefabs.Length);
            GameObject selectedZombie = zombiePrefabs[randomIndex];
            Vector3 randomPos = transform.position + new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), Random.Range(-transform.localScale.y / 2, transform.localScale.y / 2), 0f);

            GameObject zombieobj = Instantiate(selectedZombie, randomPos, Quaternion.identity);
            yield return new WaitForSeconds(duration);
        }

        while (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
        {
            GameObject.Destroy(bottomWall.GetComponent<BoxCollider2D>());
            haskey = true;
            yield return null;
        }
    }

    IEnumerator LockDoors()
    {
        yield return new WaitForSeconds(1f);
        bottomWall.GetComponent<BoxCollider2D>().enabled = true;
    }
}

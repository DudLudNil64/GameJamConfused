using System.Collections;
using UnityEngine;
using UnityEngine.WSA;

public class CameraRoom : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float cameraSize;
    bool moveCamera = false;
    private bool firstEnter = false;

    void LateUpdate()
    {
        if (moveCamera)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position + new Vector3(0, 0, -10), speed);

            if (Vector3.Distance(Camera.main.transform.position, transform.position + new Vector3(0, 0, -10)) < 0.01f)
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
            if(other.gameObject.GetComponent<PlayerData>() == null) return;

            moveCamera = true;
            Camera.main.orthographicSize = cameraSize;
            Debug.Log("Move camera to room" + gameObject.name);

            if (!firstEnter)
            {
                firstEnter = true;
                Debug.Log("First enter");
            }
        }
    }

}
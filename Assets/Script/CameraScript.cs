using UnityEngine;

public class CameraScript : MonoBehaviour
{
    bool moveCamera = false;
    [SerializeField] float speed;
    [SerializeField] float cameraSize;

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
        }
    }
}

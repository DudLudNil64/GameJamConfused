using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // The object the camera will follow (drag your player here)
    public Transform objetivo;

    //Separation distance between the camera and the object
    public Vector3 offset = new Vector3(0f, 5f, -50f);

    // Smoothing speed (values ​​between 0.1 and 0.3)
    public float suavizado = 0.125f;

    // Se usa LateUpdate para asegurar que el jugador ya se movió en este frame
    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Calculate the desired position by adding the displacement
            Vector3 Wishedposition = objetivo.position + offset;

            // Linearly interpolates between the current and desired positions
            Vector3 Smootposition = Vector3.Lerp(transform.position, Wishedposition, suavizado);

            // update camepra position
            transform.position = Smootposition;

        }
    }
}
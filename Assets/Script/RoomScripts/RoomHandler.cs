using UnityEngine;

public class RoomHandler : MonoBehaviour
{
    public float TimeToMoveCamera;
    public float SetSizeOnMove;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Vector3 Position = transform.position;
            Position.z = -10;

            Vector3.Lerp(Camera.main.transform.position, Position, TimeToMoveCamera);

            Camera.main.orthographicSize = SetSizeOnMove;
        }
    }
}

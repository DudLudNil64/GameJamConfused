using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ZombieKnigth : MonoBehaviour
{
    public Vector3 dir;
    [SerializeField] public float speed = 4;
    public Transform player;
    protected EnemyHealthManager healthManager;
    public float enemyheal = 2;
    public Rigidbody2D EnemyBody;

    // SwordWeapon
    public WeaponBase Sword;

    protected virtual void Start()
    {
        // find player
        player = FindAnyObjectByType<PlayerMovement>().transform;
        // find enemy healt
        healthManager = GetComponent<EnemyHealthManager>();

        if (gameObject.GetComponent<Rigidbody2D>() != null) EnemyBody = gameObject.GetComponent<Rigidbody2D>();
        else Debug.Log("Enemy Doesnt Have A Rigidbody2D");
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        // follow the player
        dir = (player.position - transform.position).normalized; //-1 - 1
        EnemyBody.linearVelocity = dir * speed;
        Sword.FirePosition = transform.position + transform.up;

        Vector2 OffsetVector = transform.position - player.position;
        // Uses The OffsetVector To Rotate The Player Towards The Mouse
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);

        if (Sword.CheckForUseability() && (player.position - transform.position).magnitude < 1)
        {
            Sword.UseWeapon();
        }
    }
}
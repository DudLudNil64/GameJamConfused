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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        healthManager = GetComponent<EnemyHealthManager>();
        if (gameObject.GetComponent<Rigidbody2D>() != null) EnemyBody = gameObject.GetComponent<Rigidbody2D>();
        else Debug.Log("Enemy Doesnt Have A Rigidbody2D");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        dir = (player.position - transform.position).normalized; //-1 - 1
        EnemyBody.linearVelocity = dir * speed;
    }

   
}

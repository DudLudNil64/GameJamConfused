using UnityEngine;
using static MazeGen;

public class RoomTrigger : MonoBehaviour
{
    // Uses Same Enum As MazeGen Since Uh I Can And It Aint Hard
    public enum TriggerType
    {
        // None				                    #---------------------------------------------------------------------------------------#
        NONE,               // None					[ Base Value Does Nothing ]
        Empty,              // Empty				[ Empty ]
                            // Utility			#---------------------------------------------------------------------------------------#
        Store,              // Store				[ Sells Random Items ]
        HealingFountain,    // Healing Fountain		[ Heals The Player ]
                            // Loot				#---------------------------------------------------------------------------------------#
        LowLoot,            // Low Loot				[ Example 25 Gold ]
        MediumLoot,         // Medium Loot			[ Example 65 Gold ]
        HighLoot,           // High Loot			[ Example 145 Gold ]
                            // Danger			#---------------------------------------------------------------------------------------#
        LowDanger,          // Low Danger			[ Example 5 Enemys ]
        MediumDanger,       // Medium Danger		[ Example 13 Enemys ]
        HighDanger,         // High Danger			[ Example 25 Enemys ]
                            // Level Managment	#---------------------------------------------------------------------------------------#
        Entrance,           // Entrance				[ Player Spawn ]
        Exit,               // Exit					[ Next Level ]
    };

    public TriggerType Type;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            switch (Type)
            {
                case TriggerType.NONE:
                    // Nothing
                    break;
                case TriggerType.Empty:
                    // Nothing
                    break;
                case TriggerType.Store:
                    // "Nothing" -> Might Be Removed
                    break;
                case TriggerType.HealingFountain:
                    // Code To Heal Player Per "Tick"
                    break;
                case TriggerType.LowLoot:
                    SpawnItem(transform.position, 1);
                    Destroy(gameObject);
                    break;
                case TriggerType.MediumLoot:
                    SpawnItem(transform.position, 3);
                    Destroy(gameObject);
                    break;
                case TriggerType.HighLoot:
                    SpawnItem(transform.position, 5);
                    Destroy(gameObject);
                    break;
                case TriggerType.LowDanger:
                    SpawnEnemy(transform.position, 3);
                    Destroy(gameObject);
                    break;
                case TriggerType.MediumDanger:
                    SpawnEnemy(transform.position, 7);
                    Destroy(gameObject);
                    break;
                case TriggerType.HighDanger:
                    SpawnEnemy(transform.position, 11);
                    Destroy(gameObject);
                    break;
                case TriggerType.Entrance:
                    Destroy(gameObject);
                    break;
                case TriggerType.Exit:
                    GameObject MazeManager = GameObject.FindGameObjectWithTag("Manager");
                    MazeGen Maze = MazeManager.GetComponent<MazeGen>();
                    Maze.UpdateMaze();
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public GameObject Enemy;
    // Spawns Enemy At Random Positions
    public void SpawnEnemy(Vector3 Position, int Amount)
    {
        // Reffrence To Maze
        GameObject MazeManager = GameObject.FindGameObjectWithTag("Manager");
        MazeGen Maze = MazeManager.GetComponent<MazeGen>();
        for (int i = 0; i < Amount; i++)
        {
            float Distance = UnityEngine.Random.Range(0.0f, Maze.RoomSize / 4);
            Vector2 Direction = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
            Direction = Direction.normalized * Distance;
            Instantiate(Enemy, new Vector3(Position.x + Direction.x, Position.y + Direction.y, 0), Quaternion.identity);
        }
    }

    public GameObject Item;
    // Spawns Items In Random Positions
    public void SpawnItem(Vector3 Position, int Amount)
    {
        GameObject MazeManager = GameObject.FindGameObjectWithTag("Manager");
        MazeGen Maze = MazeManager.GetComponent<MazeGen>();
      /*  for (int i = 0; i < Amount; i++)
        {
            float Distance = UnityEngine.Random.Range(0.0f, Maze.RoomSize / 4);
            Vector2 Direction = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
            Direction = Direction.normalized * Distance;
            GameObject NewItem = Instantiate(Item, new Vector3(Position.x + Direction.x, Position.y + Direction.y, 0), Quaternion.identity);
            ItemPickUp NewItemPick = NewItem.GetComponent<ItemPickUp>();

            float RandomLoot = UnityEngine.Random.Range(-1, 5);
            if (RandomLoot <= 1)
            {
                NewItemPick.Item = global::ItemPickUp.Items.Health;
            }
            else if (RandomLoot <= 2)
            {
                NewItemPick.Item = global::ItemPickUp.Items.Gold;
            }
            else if (RandomLoot <= 3)
            {
                NewItemPick.Item = global::ItemPickUp.Items.AttackSpeed;
            }
            else
            {
                NewItemPick.Item = global::ItemPickUp.Items.MaxHealth;
            }
        }*/
    }
}

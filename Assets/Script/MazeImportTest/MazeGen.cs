using System;
using System.Runtime.InteropServices;
using UnityEngine;
using static RoomTrigger;

public class MazeGen : MonoBehaviour
{
    public enum MazeRoomType
    {
        // None				#---------------------------------------------------------------------------------------#
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
    public class MazeImport
    {

        [StructLayout(LayoutKind.Sequential)]
        public struct Float2
        {
            public float x, y;
        };


        [StructLayout(LayoutKind.Sequential)]
        public struct CompiledMazeRoom
        {
            // Base Needed Values
            public Float2 Position; // Postion [ Vector2 Cant Be Moved From c++ ]
            public byte Group; // Used For Room Pairing
            public MazeRoomType Type; // Used To Spawn The Correct Room
            public byte RoomShape; // Shape Of The Room
        }

        [DllImport("MazeGen-SchoolProject")] // Imports The CompileMaze Function
        public static extern ulong CompileMaze(Float2 ExitDirection, float Distance);

        [DllImport("MazeGen-SchoolProject")] // Imports The GetCompiledMazeDataPointer Function
        public static extern IntPtr GetCompiledMazeDataPointer();
    }

    public static MazeImport.CompiledMazeRoom[] GenerateMaze(float Distance)
    {
        Vector2 ExitDirectionRandom = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
        MazeImport.Float2 ExitDirection;

        ExitDirection.x = 0.9f;
        ExitDirection.y = 0.1f;

        // Just Just Just In Case Its 0,0
        if ((ExitDirection.x == 0) && (ExitDirection.y == 0))
        {
            ExitDirection.x = 1;
        }

        float ExitDirectionNormalizer = ExitDirection.x + ExitDirection.y;
        if (ExitDirection.x != 0)
        {
            ExitDirection.x /= ExitDirectionNormalizer;
        }
        if(ExitDirection.y != 0)
        {
            ExitDirection.y /= ExitDirectionNormalizer;
        }

        // Randomizes It From Positive To Negetive
        if (UnityEngine.Random.value > 0.5f)
        {
            ExitDirection.x *= 1;
        }
        else
        {
            ExitDirection.x *= -1;
        }
        // Randomizes It From Positive To Negetive
        if (UnityEngine.Random.value > 0.5f)
        {
            ExitDirection.y *= 1;
        }
        else
        {
            ExitDirection.y *= -1;
        }

        ulong size = MazeImport.CompileMaze(ExitDirection, Distance);
        IntPtr Pointer = MazeImport.GetCompiledMazeDataPointer();

        MazeImport.CompiledMazeRoom[] NewMaze = new MazeImport.CompiledMazeRoom[size];
        int structSize = Marshal.SizeOf(typeof(MazeImport.CompiledMazeRoom));

        // Goes Trough And Copy's The Vector In Ram
        for (ulong i = 0; i < size; i++)
        {
            IntPtr elementPtr = IntPtr.Add(Pointer, (int)i * structSize);
            NewMaze[i] = Marshal.PtrToStructure<MazeImport.CompiledMazeRoom>(elementPtr);
        }

        return NewMaze;
    }

    // Gets The Room Positions
    static int[] GetRoomAtPositions(MazeImport.Float2[] RoomPositions, MazeImport.CompiledMazeRoom[] MazeRooms)
    {
        int[] Positions = new int[RoomPositions.Length];

        for (int j = 0; j < RoomPositions.Length; j++)
        {
            Positions[j] = -1;
        }

            // Compares Againts All Rooms That Are Needed
        for (int j = 0; j < RoomPositions.Length; j++)
        {
            // Goes Trough All Rooms
            for (int i = 0; i < MazeRooms.Length; i++)
            {
                // Checks If They Are The Same Or Not
                if ((MazeRooms[i].Position.x == RoomPositions[j].x) && (MazeRooms[i].Position.y == RoomPositions[j].y))
                {
                    // Room Index
                    Positions[j] = i;
                }
            }
        }


        return Positions;
    }

    public float RoomSize = 20;
    public Vector3 RoomPartOffset;

    public GameObject WallPreFab;
    public GameObject DoorPreFab;

    public GameObject Trigger;

    // Places All Maze Components Like Walls, Doors, Triggers And Items
    void PlaceMaze()
    {
        MazeImport.CompiledMazeRoom[] MazeRooms = GenerateMaze(CurrentLength);

        for (int i = 0; i < MazeRooms.Length; i++)
        {
            GameObject NewTrigger = Instantiate(Trigger, new Vector3(MazeRooms[i].Position.x * RoomSize, MazeRooms[i].Position.y * RoomSize, 0), Quaternion.identity);
            RoomTrigger RoomTrigger = NewTrigger.GetComponent<RoomTrigger>();
            SpriteRenderer Renderer = NewTrigger.GetComponent<SpriteRenderer>();

            // Noticed Here That I Could Use global ._.

            switch (MazeRooms[i].Type)
            {
                case MazeRoomType.NONE: // Sets Type As NONE
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.NONE;
                    break;
                case MazeRoomType.Empty: // Sets Type As Empty
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.Empty;
                    break;
                case MazeRoomType.Store:
                    // "Nothing" -> Is Removed
                    break;
                case MazeRoomType.HealingFountain:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.HealingFountain;
                    NewTrigger.transform.localScale = new Vector3(3, 3, 0);
                    Renderer.color = new Color(0, 1, 0.1f, 0.4f);
                    break;
                case MazeRoomType.LowLoot:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.LowLoot;
                    break;
                case MazeRoomType.MediumLoot:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.MediumLoot;
                    break;
                case MazeRoomType.HighLoot:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.HighLoot;
                    break;
                case MazeRoomType.LowDanger:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.LowDanger;
                    break;
                case MazeRoomType.MediumDanger:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.MediumDanger;
                    break;
                case MazeRoomType.HighDanger:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.HighDanger;
                    break;
                case MazeRoomType.Entrance:
                    // Nothing
                    break;
                case MazeRoomType.Exit:
                    RoomTrigger.Type = global::RoomTrigger.TriggerType.Exit;
                    NewTrigger.transform.localScale = new Vector3(3, 3, 0);
                    Renderer.color = new Color(1, 0, 1, 0.4f);
                    break;
            }

            MazeImport.Float2[] Positions = new MazeImport.Float2[4];
            // Up
            Positions[0].x = MazeRooms[i].Position.x;
            Positions[0].y = MazeRooms[i].Position.y + 1;
            // Down
            Positions[1].x = MazeRooms[i].Position.x;
            Positions[1].y = MazeRooms[i].Position.y - 1;
            // Right
            Positions[2].x = MazeRooms[i].Position.x + 1;
            Positions[2].y = MazeRooms[i].Position.y;
            // Left
            Positions[3].x = MazeRooms[i].Position.x - 1;
            Positions[3].y = MazeRooms[i].Position.y;

            int[] RoomPosition = GetRoomAtPositions(Positions, MazeRooms);
            if ((MazeRooms[i].RoomShape & 1) == 0) // up
            {
                if (RoomPosition[0] != -1)
                {
                    Instantiate(DoorPreFab, new Vector3(MazeRooms[i].Position.x * RoomSize, (MazeRooms[i].Position.y * RoomSize) + RoomPartOffset.y, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(WallPreFab, new Vector3(MazeRooms[i].Position.x * RoomSize, (MazeRooms[i].Position.y * RoomSize) + RoomPartOffset.y, 0), Quaternion.identity);
                }
            }

            if ((MazeRooms[i].RoomShape & 2) == 0) // down
            {
                if (RoomPosition[1] != -1)
                {
                    Instantiate(DoorPreFab, new Vector3(MazeRooms[i].Position.x * RoomSize, (MazeRooms[i].Position.y * RoomSize) - RoomPartOffset.y, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(WallPreFab, new Vector3(MazeRooms[i].Position.x * RoomSize, (MazeRooms[i].Position.y * RoomSize) - RoomPartOffset.y, 0), Quaternion.identity);
                }
            }

            if ((MazeRooms[i].RoomShape & 4) == 0) // right
            {
                if (RoomPosition[2] != -1)
                {
                    Instantiate(DoorPreFab, new Vector3((MazeRooms[i].Position.x * RoomSize) + RoomPartOffset.x, MazeRooms[i].Position.y * RoomSize, 0), Quaternion.Euler(0, 0, 90));
                }
                else
                {
                    Instantiate(WallPreFab, new Vector3((MazeRooms[i].Position.x * RoomSize) + RoomPartOffset.x, MazeRooms[i].Position.y * RoomSize, 0), Quaternion.Euler(0, 0, 90));
                }
            }

            if ((MazeRooms[i].RoomShape & 8) == 0) // left
            {
                if (RoomPosition[3] != -1)
                {
                    Instantiate(DoorPreFab, new Vector3((MazeRooms[i].Position.x * RoomSize) - RoomPartOffset.x, MazeRooms[i].Position.y * RoomSize, 0), Quaternion.Euler(0, 0, 90));
                }
                else
                {
                    Instantiate(WallPreFab, new Vector3((MazeRooms[i].Position.x * RoomSize) - RoomPartOffset.x, MazeRooms[i].Position.y * RoomSize, 0), Quaternion.Euler(0, 0, 90));
                }
            }
        }
    }

    public int CurrentLength = 5;
    public void UpdateMaze(int NewLength = 0)
    {
        if (NewLength != 0)
        {
            CurrentLength = NewLength;
        }

        GameObject[] Maze = GameObject.FindGameObjectsWithTag("MazeRoom");
        for (int i = 0; i < Maze.Length; i++)
        {
            Destroy(Maze[i]);
        }

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = Vector3.zero;

        PlaceMaze();
        CurrentLength++;
    }
}
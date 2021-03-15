using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BoardMaker : MonoBehaviour
{
    public float BoxX, BoxY;
    public float DistanceBetweenBlocks = 0.64f;
    public int[] WallLocationsX;
    public int[] WallLocationsY;
    public int Connections;
    public float Chance;
    public float ChanceForStrong;
    //0 is no idrection. 1 is up, 2 is right, 3 is left, 4 is down 
    public int Direction;
    public bool Condition;
    public bool CenterSpawn;
    public bool CornerStuff;
    public bool WillSpawn;
    public float SmallDistance, MaxDistance;
    public float SmallDistanceX, SmallDistanceY;

    public int Priority;

    [Space]
    public GameObject[] Tiles;
    public GameObject[] CornerTiles;
    public GameObject[] AdaptTiles;
    public GameObject[] WallEnvironment;
    public GameObject[] Walls;
    public GameObject Tower;
    public GameObject Decoration;
    public GameObject GameBoard;
    public GameObject BottomShadow;
    public GameObject SpawnEnemyParticle;

    public bool[] UnusedLocations;

    [Space]
    public float SpawnAddOnX;
    public float SpawnAddOnY;
    public float SpawnLocation;
    public float DecorationChance;
    public GameObject[] EnemiesToSpawn;
    public GameObject StronkEnemy;
    public Vector3[] SpawnLocations;
    public int AmountOfEnemiesMin, AmountOfEnemiesMax;

    private BoxCollider2D collider;
    [HideInInspector]
    public CreateBoard boardManager;
    private Vector2 BoardPosition;
    private SpawnBattle MemoryPillar;
    private bool StartedBattle;
    private PlayerController PlayerControl;
    GameObject Board;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        boardManager = FindObjectOfType<CreateBoard>();
        PlayerControl = FindObjectOfType<PlayerController>();
        collider.size = new Vector2(BoxX * 0.64f, BoxY * 0.64f);
        StartedBattle = false;

        GameObject Bottom = Instantiate(BottomShadow, transform.position, Quaternion.identity, transform);
        collider.size = new Vector2(BoxX * 0.64f, BoxY * 0.64f);
        if (CenterSpawn)
        {
            boardManager.BoardY[4].BoardX[4] = 1;
            BoardPosition = new Vector2(5, 5);
        }
        for (int x = 0; x < BoxX; x++)
        {
            for (int y = 0; y < BoxY; y++)
            {
                Vector3 Vector3Position = transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks);

                float RandomChance = Random.Range(0f, 1f);
                if(RandomChance <= DecorationChance && y > 0 && x > 0 && x < BoxX - 1 && y < BoxY - 1)
                {
                    Instantiate(Decoration, transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                }

                if (x == (BoxX / 2) + 0.5f && y == (BoxY / 2) + 0.5f)
                {
                    Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                    GameObject TowerPillar = Instantiate(Tower, transform.position, Quaternion.identity, transform);
                    MemoryPillar = TowerPillar.GetComponent<SpawnBattle>();
                }
                //Corners:right, left, up, down 
                /*else if (x == BoxX - 2 || y == BoxY - 2 || y == 1 && x == 1)
                {
                    Instantiate(AdaptTiles[Random.Range(0, AdaptTiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                */
                else if (x == BoxX - 1 && y == BoxY - 1)
                {
                    Instantiate(CornerTiles[4], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                }
                else if (x == BoxX - 1 && y == 0)
                {
                    Instantiate(CornerTiles[5], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                }
                else if (x == 0 && y == BoxY - 1)
                {
                    Instantiate(CornerTiles[6], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                }
                else if (x == 0 && y == 0)
                {
                    Instantiate(CornerTiles[7], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                }
                //For vector locations 

                else if (x == BoxX - SpawnLocation && y == BoxY - SpawnLocation)
                {
                    SpawnLocations[0] = Vector3Position;
                    if (CornerStuff)
                    {
                        Instantiate(Walls[Random.Range(0, Walls.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                    else
                    {
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }

                }
                else if (x == BoxX - SpawnLocation && y == SpawnLocation - 1)
                {
                    SpawnLocations[1] = Vector3Position;
                    if (CornerStuff)
                    {
                        Instantiate(Walls[Random.Range(0, Walls.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                    else
                    {
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                }
                else if (x == SpawnLocation - 1 && y == BoxY - SpawnLocation)
                {
                    SpawnLocations[2] = Vector3Position;
                    if (CornerStuff)
                    {
                        Instantiate(Walls[Random.Range(0, Walls.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                    else
                    {
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                }
                else if (x == SpawnLocation - 1 && y == SpawnLocation - 1)
                {
                    SpawnLocations[3] = Vector3Position;
                    if (CornerStuff)
                    {
                        Instantiate(Walls[Random.Range(0, Walls.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                    else
                    {
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                }

                //this is the rest of the stuff 
                else
                {
                    if (x == BoxX - 1)
                    {
                        Instantiate(CornerTiles[0], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                    }
                    else if (x == 0)
                    {
                        Instantiate(CornerTiles[1], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                    else if (y == BoxY - 1)
                    {
                        Instantiate(CornerTiles[2], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                    else if (y == 0)
                    {
                        Instantiate(CornerTiles[3], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);

                    }
                    else
                    {
                        Instantiate(Tiles[Random.Range(0, Tiles.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                    }
                }

                if (Condition && WillSpawn)
                {
                    for (int i = 0; i < WallLocationsX.Length; i++)
                    {
                        //second is less, third is greater than. what the heck is ths lazy code!
                        if (x == WallLocationsX[i] && x < WallLocationsX[1] && x > WallLocationsX[2])
                        {
                            Instantiate(WallEnvironment[Random.Range(0, WallEnvironment.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                        }
                    }
                    for (int i = 0; i < WallLocationsY.Length; i++)
                    {
                        //second is less, third is greater than. what the heck is ths lazy code!
                        if (y == WallLocationsX[i] && y < WallLocationsX[1] && y > WallLocationsX[2])
                        {
                            Instantiate(WallEnvironment[Random.Range(0, WallEnvironment.Length)], transform.position + new Vector3((x - (BoxX - 1) / 2) * DistanceBetweenBlocks, (y - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
                        }
                    }
                }

            }
        }
        if (!Condition && WillSpawn)
        {
            for (int i = 0; i < WallLocationsX.Length; i++)
            {
                Instantiate(WallEnvironment[Random.Range(0, WallEnvironment.Length)], transform.position + new Vector3((WallLocationsX[i] - (BoxX - 1) / 2) * DistanceBetweenBlocks, (WallLocationsY[i] - (BoxY - 1) / 2) * DistanceBetweenBlocks, 0), Quaternion.identity, transform);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Calls only one frame when it is called 
        if (!StartedBattle && MemoryPillar.CombatState == 1)
        {
            for (int i = 0; i < UnusedLocations.Length; i++)
            {
                UnusedLocations[i] = false;
            }
            int SpawnLocation = Random.Range(0, 4);
            for (int i = 0; i < Random.Range(AmountOfEnemiesMin, AmountOfEnemiesMax + 1); i++)
            {
                if (i <= 3)
                {
                    while (UnusedLocations[SpawnLocation] == true)
                    {
                        SpawnLocation = Random.Range(0, 4);
                    }

                    UnusedLocations[SpawnLocation] = true;
                }
                else
                {
                    SpawnLocation = Random.Range(0, 4);
                }
                Vector3 RandPos = new Vector3(Random.Range(-SpawnAddOnX, SpawnAddOnX), Random.Range(-SpawnAddOnY, SpawnAddOnY), 0);
                Vector3 RandomVector = SpawnLocations[SpawnLocation];

                float RandChance = Random.Range(0f, 1f);
                if (RandChance >= ChanceForStrong)
                {
                    Instantiate(EnemiesToSpawn[Random.Range(0, EnemiesToSpawn.Length)], RandomVector + RandPos, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(StronkEnemy, RandomVector + RandPos, Quaternion.identity, transform);
                }
                Instantiate(SpawnEnemyParticle, RandomVector + RandPos, Quaternion.identity, transform);


            }


            StartedBattle = true;
        }
        GameObject[] AllEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (AllEnemies.Length <= 0 && MemoryPillar.CombatState == 1)
        {
            MemoryPillar.CombatState = 2;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        string NameTag = collision.gameObject.tag;
        if (NameTag == "Player")
        {
            PlayerControl.OnPlatform = true;
            PlayerControl.BoardSpawnLocation = this.gameObject.transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string NameTag = collision.gameObject.tag;
        if (NameTag == "Player")
        {
            PlayerControl.OnPlatform = true;
            PlayerControl.BoardSpawnLocation = this.gameObject.transform;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        string NameTag = collision.gameObject.tag;
        if (NameTag == "Player")
        {
            PlayerControl.OnPlatform = false;

        }
    }


        /*
for (int boards = 0; boards <= Connections; boards++)
{
    float Rand = Random.Range(0f, 1f);
    if (CreateBoard.totalBoards < CreateBoard.MaxBoard)
    {

        int RandomNumber = Direction;
        while (RandomNumber == Direction)
        {
            RandomNumber = Random.Range(1, 5);

        }


        CreateBoards(RandomNumber, (int)BoardPosition.x, (int)BoardPosition.y);
        Priority = CreateBoard.totalBoards;

    }
}
*/
    
    /*
void CreateBoards(int Location, int PosX, int PosY)
{
    if (BoardPosition.x > 0 && BoardPosition.y > 0 && BoardPosition.x < boardManager.BoardY[PosY].BoardX.Length && BoardPosition.y < boardManager.BoardY.Length)
    {

        //0 is no idrection. 1 is right, 2 is down, 3 is left, 4 is up 

        switch (Location)
        {
            case 1:
                MakeObject(1, 0, Location, PosY, PosX);
                break;
            case 2:
                MakeObject(0, -1, Location, PosY, PosX);
                break;
            case 3:
                MakeObject(-1, 0, Location, PosY, PosX);
                break;
            case 4:
                MakeObject(0, 1, Location, PosY, PosX);
                break;

        }

    }
}

void MakeObject(int xMove, int yMove, int Location, int PosY, int PosX)
{
    //Direction == 1 and then the direciton 4 is invalid; 
    if (boardManager.BoardY[PosY + yMove].BoardX[PosX + xMove] == 0)
    {
        Board = Instantiate(GameBoard, transform.position + new Vector3((BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance)) * xMove, (BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance)) * yMove, 0), Quaternion.identity);
        Board.GetComponent<BoardMaker>().Direction = Location;
        boardManager.BoardY[PosY + yMove].BoardX[PosX + xMove] = 1;

        print(boardManager.BoardY[PosY + yMove].BoardX[PosX + xMove]);

        Board.GetComponent<BoardMaker>().BoardPosition = new Vector2(PosY + yMove, PosX + xMove);
        CreateBoard.totalBoards++;
    }
}

void SpawnBoards(int boards, int PosX, int PosY)
{
    if (BoardPosition.x > 0 && BoardPosition.y > 0 && BoardPosition.x < boardManager.BoardY[PosY].BoardX.Length && BoardPosition.y < boardManager.BoardY.Length)
    {
        bool MadeBoard = false;
        //0 is no idrection. 1 is up, 2 is right, 3 is left, 4 is down 
        if (Direction == 0)
        {
            //Because I am very lazy right now, Imma about to pull the meme here 
            switch (boards)
            {
                case 1:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 1;
                    MadeBoard = true;
                    break;
                case 2:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 2;
                    MadeBoard = true;

                    break;
                case 3:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 3;
                    MadeBoard = true;

                    break;
                case 4:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 4;
                    MadeBoard = true;

                    break;
            }
        }
        if (Direction == 1)
        {
            //0 is no idrection. 1 is up, 2 is right, 3 is left, 4 is down 
            switch (boards)
            {
                case 1:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 1;
                    MadeBoard = true;

                    break;
                case 2:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 2;
                    MadeBoard = true;

                    break;
                case 3:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 3;
                    MadeBoard = true;

                    break;
                default:
                    break;

            }
        }
        if (Direction == 2)
        {
            //0 is no idrection. 1 is up, 2 is right, 3 is left, 4 is down 
            switch (boards)
            {
                case 1:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 1;
                    MadeBoard = true;
                    break;
                case 2:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 2;
                    MadeBoard = true;

                    break;
                case 4:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 4;
                    MadeBoard = true;

                    break;
                default:
                    break;
            }
        }
        if (Direction == 3)
        {
            //0 is no idrection. 1 is up, 2 is right, 3 is left, 4 is down 
            switch (boards)
            {
                case 1:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 1;
                    MadeBoard = true;

                    break;
                case 3:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 3;
                    MadeBoard = true;

                    break;
                case 4:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 4;
                    MadeBoard = true;

                    break;
                default:
                    break;
            }
        }
        if (Direction == 4)
        {
            //0 is no idrection. 1 is up, 2 is right, 3 is left, 4 is down 
            switch (boards)
            {
                case 2:
                    Board = Instantiate(GameBoard, transform.position + new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 2;
                    MadeBoard = true;


                    break;
                case 3:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(BoxX / 1.5f + Random.Range(SmallDistance, MaxDistance), Random.Range(SmallDistanceX, SmallDistanceY), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 3;
                    MadeBoard = true;

                    break;
                case 4:
                    Board = Instantiate(GameBoard, transform.position - new Vector3(Random.Range(SmallDistanceX, SmallDistanceY), BoxY / 1.5f + Random.Range(SmallDistance, MaxDistance), 0), Quaternion.identity);
                    Board.GetComponent<BoardMaker>().Direction = 4;
                    MadeBoard = true;

                    break;
                default:
                    break;
            }
        }
        if (MadeBoard)
        {

            switch (boards)
            {
                case 1:
                    if (boardManager.BoardY[PosY + 1].BoardX[PosX] == 0)
                    {
                        boardManager.BoardY[PosY + 1].BoardX[PosX] = 1;
                        Board.GetComponent<BoardMaker>().BoardPosition = new Vector2(PosY + 1, PosX);
                        CreateBoard.totalBoards++;

                    }
                    else
                    {
                        Destroy(Board);
                    }
                    break;
                case 2:
                    if (boardManager.BoardY[PosY].BoardX[PosX + 1] == 0)
                    {
                        boardManager.BoardY[PosY].BoardX[PosX + 1] = 1;
                        Board.GetComponent<BoardMaker>().BoardPosition = new Vector2(PosY, PosX + 1);
                        CreateBoard.totalBoards++;

                    }
                    else
                    {
                        Destroy(Board);
                    }
                    break;
                case 3:
                    if (boardManager.BoardY[PosY].BoardX[PosX - 1] == 0)
                    {
                        boardManager.BoardY[PosY].BoardX[PosX - 1] = 1;
                        Board.GetComponent<BoardMaker>().BoardPosition = new Vector2(PosY, PosX - 1);
                        CreateBoard.totalBoards++;

                    }
                    else
                    {
                        Destroy(Board);
                    }
                    break;
                case 4:
                    if (boardManager.BoardY[PosY - 1].BoardX[PosX] == 0)
                    {
                        boardManager.BoardY[PosY - 1].BoardX[PosX] = 1;
                        Board.GetComponent<BoardMaker>().BoardPosition = new Vector2(PosY - 1, PosX);
                        CreateBoard.totalBoards++;

                    }
                    else
                    {
                        Destroy(Board);
                    }
                    break;
            }
        }

    }
}

*/


}







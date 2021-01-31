using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    public static int totalBoards;
    public static int MaxBoard;
    [System.Serializable]
    public class Board
    {
        public int[] BoardX;

    }
    public Transform PathHolder;
    public Board[] BoardY;

    public int Max;
    public int Min;
    public float ColorChange;
    public Material PathColor;

    public Text Title;
    public Text TextDescriptions;
    public Text PillarsLeft;

    public static int Activated;
    public static int ActivatedTotal;
    public static bool EnemyAlive;


    public int ySize, xSize;
    public float DistanceBetween;
    public float ChanceToSpawn;


    public float PathWayWidth;
    private float DisplayTimer;

    public GameObject PathWay;
    public GameObject ItemData;
    public GameObject Transistion;
    public GameObject[] Boards;
    public Vector3[] BoardsSpawnLocations;
    public Vector2 BoardsSpawnAddon;
    public float MultiplierMin = 1f, MultiplierMax = 1f;
    public bool[] SpawnUsed;
    GameObject[] AllPaths;

    public List<Transform> AllLocation = new List<Transform>();
    [HideInInspector]
    public static int BoardsMade;
    [HideInInspector]
    public static int value;
    private PlayerController PlayerControl;
    private Transform playerPos;
    private bool SpawnOnce;
    public List<Material> AllMaterials = new List<Material>();

    // Start is called before the first frame update
    void Awake()
    {
        MaxBoard = Max;
        Activated = 0;
        ActivatedTotal = 0;
        BoardsMade = 0;
        value = 0;
        SpawnOnce = false;

        if (GameObject.FindGameObjectWithTag("ItemData") == null)
        {
            Instantiate(ItemData, ItemData.transform.position, Quaternion.identity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        PillarsLeft = GameObject.FindGameObjectWithTag("PillarText").GetComponent<Text>();
        TextDescriptions = GameObject.FindGameObjectWithTag("DescriptionText").GetComponent<Text>();
        Title = GameObject.FindGameObjectWithTag("TitleText").GetComponent<Text>();

        PillarsLeft.text = "";
        Title.text = "";

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerControl = playerPos.GetComponent<PlayerController>();
        value = 0;

        for (int i = 0; i < BoardsSpawnLocations.Length; i++)
        {
            float RandChance = Random.Range(0f, 1f);
            if (RandChance >= ChanceToSpawn && BoardsMade >= Min)
            {
                print("skipped");
            }
            else {
                int RandomBoardLoc = Random.Range(0, BoardsSpawnLocations.Length);
                while (SpawnUsed[RandomBoardLoc])
                {
                    RandomBoardLoc = Random.Range(0, BoardsSpawnLocations.Length);
                }
                SpawnBoard(BoardsSpawnLocations[RandomBoardLoc]);
                SpawnUsed[RandomBoardLoc] = true;
            }
        }




        PathHolder.gameObject.SetActive(false);
        AllPaths = GameObject.FindGameObjectsWithTag("Bridge");
        for (int i = 0; i < AllPaths.Length; i++)
        {
            PathCollider LineR = AllPaths[i].GetComponent<PathCollider>();
            LineR.RemoveColor();
        }
        ActivatedTotal += value + 1;
    }

    void SpawnBoard(Vector3 Location)
    {
        GameObject Board = Instantiate(Boards[Random.Range(0, Boards.Length)], (Location * Random.Range(MultiplierMin, MultiplierMax)) + new Vector3(Random.Range(-BoardsSpawnAddon.x, BoardsSpawnAddon.x),Random.Range(-BoardsSpawnAddon.y, BoardsSpawnAddon.y),0), Quaternion.identity, transform);
        GameObject Pathway = Instantiate(PathWay, transform.position, Quaternion.identity, PathHolder);
        LineRenderer PathwayLR = Pathway.GetComponent<LineRenderer>();
        PathCollider PathwayCollider = Pathway.GetComponent<PathCollider>();
        PathwayLR.startWidth = PathWayWidth;
        PathwayLR.endWidth = PathWayWidth;
        PathwayLR.SetPosition(0, transform.position);
        PathwayLR.SetPosition(1, Board.transform.position);

        PathwayCollider.Value = value;
        PathwayCollider.Origin = transform.position;
        PathwayCollider.EndPoint = Board.transform.position;
        PlayerControl.OnPath.Add(false);
        value++;
        BoardsMade++;
    }


    // Update is called once per frame
    void Update()
    {

        if (Activated < ActivatedTotal)
        {
            PillarsLeft.text = Activated.ToString() + "/" + ActivatedTotal.ToString() + " memories have been activated";
        }
        else if (Vector2.Distance(playerPos.position, Vector3.zero) > 4.5f)
        {
            PillarsLeft.text = "all memories have been activated. Return to center";
        }
        else
        {
            if (SpawnOnce == false)
            {
                Instantiate(Transistion, Transistion.transform.position, Quaternion.identity);
                Invoke("LoadScene", 2f);
                SpawnOnce = true;

            }
    }

        GameObject[] AllEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] AllPaths = GameObject.FindGameObjectsWithTag("Bridge");
        if (AllEnemies.Length <= 0)
        {
            EnemyAlive = false;

        }
        if (AllEnemies.Length > 0)
        {
            EnemyAlive = true;

            for (int i = 0; i < AllPaths.Length; i++)
            {
                PathCollider LineR = AllPaths[i].GetComponent<PathCollider>();
                LineR.RemoveColor();
                LineR.PathToggle = false;
            }
        }
        else if (Activated > 0)
        {
            PathHolder.gameObject.SetActive(true);
            for (int i = 0; i < AllPaths.Length; i++)
            {
                PathCollider LineR = AllPaths[i].GetComponent<PathCollider>();
                LineR.ChangeColor(ColorChange);
                LineR.PathToggle = true;

            }
        }
        DisplayTimer -= Time.deltaTime;
        if(DisplayTimer < 0)
        {
            Title.color = Color.Lerp(Title.color, Color.clear, Time.deltaTime);
            TextDescriptions.color = Color.Lerp(TextDescriptions.color, Color.clear, Time.deltaTime);
        }
    }

    public void MessageOnScreen(string title, string Description, int value)
    {
        if(value == 11 && DisplayTimer > 0) 
        {
            return;
        }
        Title.text = title;
        TextDescriptions.text = Description;
        Title.color = Color.white;
        TextDescriptions.color = Color.white;
        DisplayTimer = 6f;
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}



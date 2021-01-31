using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnother : MonoBehaviour
{


    public int Max;
    public int Min;

    public float DistanceBetween;
    public float ChanceToSpawn;


    public float PathWayWidth;

    private Transform PathHolder;

    public GameObject PathWay;
    public GameObject[] Boards;
    public Vector3[] BoardsSpawnLocations;
    public Vector2 BoardsSpawnAddon;
    public float MultiplierMin = 1f, MultiplierMax = 1f;
    public bool[] SpawnUsed;

    public List<Transform> AllLocation = new List<Transform>();



    private int value;
    private PlayerController PlayerControl;
    private Transform playerPos;
    Vector3 SpawnPosition;

    private void Awake()
    {
        PathHolder = GameObject.FindGameObjectWithTag("PathHolder").transform;
    }

    // Start is called before the first frame update
    void Start()
    {

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerControl = playerPos.GetComponent<PlayerController>();
        value = 0;


        float RandChance = Random.Range(0f, 1f);
        if (RandChance >= ChanceToSpawn && CreateBoard.BoardsMade >= Min)
        {
            return;
        }
        else
        {
            int RandomBoardLoc = Random.Range(0, BoardsSpawnLocations.Length);
            while (SpawnUsed[RandomBoardLoc])
            {
                RandomBoardLoc = Random.Range(0, BoardsSpawnLocations.Length);
            }
            GetLocation();
            SpawnBoard();
            SpawnUsed[RandomBoardLoc] = true;
            CreateBoard.ActivatedTotal += value;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetLocation()
    {

        if (transform.position.y > 0)
        {
            if (transform.position.x < 0)
            {
                SpawnPosition = BoardsSpawnLocations[0];
            }
            else if (transform.position.x > 0)
            {
                SpawnPosition = BoardsSpawnLocations[1];

            }
        }
        else if (transform.position.y < 0)
        {
            if (transform.position.x < 0)
            {
                SpawnPosition = BoardsSpawnLocations[2];

            }
            else if (transform.position.x > 0)
            {
                SpawnPosition = BoardsSpawnLocations[3];

            }
        }
    }

    void SpawnBoard()
    {
        Vector3 SpawnLocation = transform.position + (SpawnPosition * Random.Range(MultiplierMin, MultiplierMax)) + new Vector3(Random.Range(-BoardsSpawnAddon.x, BoardsSpawnAddon.x), Random.Range(-BoardsSpawnAddon.y, BoardsSpawnAddon.y), 0);


        GameObject Board = Instantiate(Boards[Random.Range(0, Boards.Length)], SpawnLocation, Quaternion.identity, transform);
        GameObject Pathway = Instantiate(PathWay, Vector3.zero, Quaternion.identity);
        LineRenderer PathwayLR = Pathway.GetComponent<LineRenderer>();
        PathCollider PathwayCollider = Pathway.GetComponent<PathCollider>();
        PathwayLR.startWidth = PathWayWidth;
        PathwayLR.endWidth = PathWayWidth;
        PathwayLR.SetPosition(0, transform.position);
        PathwayLR.SetPosition(1, SpawnLocation);

        PathwayCollider.Value = CreateBoard.value;
        PathwayCollider.Origin = transform.localPosition;
        PathwayCollider.EndPoint = SpawnLocation;
        PlayerControl.OnPath.Add(false);
        CreateBoard.value++;
        CreateBoard.BoardsMade++;
        value++;
    }

}

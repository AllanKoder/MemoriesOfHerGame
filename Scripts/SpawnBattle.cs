using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpawnBattle : MonoBehaviour
{
    public float DistanceToActivate;
    [HideInInspector]
    public int CombatState = 0;
    public Color StartColor, ActivatedColor;
    public Text ActionText;

    private Transform Player;
    private LineRenderer lr;
    private float RandomWidth;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        RandomWidth = Random.Range(0.08f, 0.12f);
        lr.startWidth = 0;
        lr.endWidth = 0;
        CreateBoard.totalBoards++;


    }

    // Update is called once per frame
    void Update()
    {

        lr.SetPosition(0, transform.position + new Vector3(0, 1.55f, 0));
        lr.SetPosition(1, transform.position + new Vector3(0, 12f, 0f));
        if (Vector2.Distance(Player.position, transform.position) < DistanceToActivate && CombatState == 0)
        {
            if (!UsingController.ControllerConnected)
            {
                //Press "e" to activate Memory Pillar
                //Press "both M1 and M2" to activate Memory Pillar
                //Press "both M1 and M2" to activate Memory Pillar
                ActionText.text = "Press 'e' to activate Memory Pillar";
                if (Input.GetAxis("Action") == 1)
                {
                    CombatState = 1;
                    CreateBoard.Activated++;
                }
            }
        }
        else
        {
            ActionText.text = "";
        }
        if (!CreateBoard.EnemyAlive && CombatState == 2)
        {
            lr.startWidth = RandomWidth;
            lr.endWidth = RandomWidth;
        }
    }
}

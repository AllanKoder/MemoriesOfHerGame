using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeItem : MonoBehaviour
{
    [System.Serializable]
    public class Descriptions
    {
        public string[] moreDetails;

    }

    public Sprite[] ItemType;
    public Sprite[] LostMemory;
    public string[] Names;
    public string[] purpose;
    public Descriptions[] Story;

    public int Value = 1;
    public float Distance;
    public float ChanceOfItem;
    private SpriteRenderer Sr;
    private Transform PlayerPos;
    private PlayerController PlayerController;
    private CreateBoard DisplayScreen;
    private ItemData DataItem;

    public GameObject AudioPlay;
    // Start is called before the first frame update
    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
        DisplayScreen = FindObjectOfType<CreateBoard>();
        DataItem = FindObjectOfType<ItemData>();
        //1 - (1 / (0.15 * 4 + 1) =
        float RandItemChance = 1 - (1 / (0.15f * PlayerController.ChanceMultipler + 1));
        float RandChance = Random.Range(0f, 1f);


        if(RandItemChance > RandChance)
        {
            Value = Random.Range(0, ItemType.Length) + 1;
            Sr.sprite = ItemType[Value - 1];

        }
        else
        {
            Value = 11;
            Sr.sprite = LostMemory[Random.Range(0, LostMemory.Length)];
        }
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, PlayerPos.position) < Distance)
        {
            UpgradePlayer();
        }
    }

    void UpgradePlayer()
    {
        switch (Value)
        {
            case 1:
                PlayerController.HealthRegenRate += 0.25f;
                display();
                break;
            case 2:
                PlayerController.MovementSpeedMultipier += 0.25f;
                display();
                break;
            case 3:
                PlayerController.BallMultipler += 1f;
                display();
                break;
            case 4:
                PlayerController.AmmoMultipler += 0.15f;
                display();

                break;
            case 5:
                PlayerController.DamageToEnemy += 0.1f;
                display();

                break;
            case 6:
                PlayerController.ChanceMultipler += 1f;
                display();

                break;
            case 7:
                PlayerController.Music += 1;
                display();
                break;
            case 8:
                PlayerController.CoinMultipler += 0.2f;
                display();

                break;
            case 9:
                FindObjectOfType<PlayerController>().FullHeal();
                display();
                break;
            case 10:
                PlayerController.HealthMultipier += 0.10f;
                FindObjectOfType<PlayerController>().BreadHeal();

                display();
                break;
            case 11:
                display();
                break;
        }
        if (Value != 11)
        {
            Instantiate(AudioPlay, transform.position, transform.rotation);

        }
        Destroy(gameObject);
    }

    void display()
    {
        DisplayScreen.MessageOnScreen(Names[Value - 1], Story[Value - 1].moreDetails[Random.Range(0, Story[Value - 1].moreDetails.Length)] + "(" + purpose[Value - 1] + ")", Value);
        DataItem.GainedItem(Value - 1);
    }
}

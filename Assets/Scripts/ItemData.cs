using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemData : MonoBehaviour
{


    public Image[] ItemImage;
    public Text[] AmountText;
    public Sprite[] ItemIndex;
    public Sprite Nothing;

    public int[] Items;
    public int[] AmountOfItems;
    private bool Added;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }   
    
    void Start()
    {
        Added = false;
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] >= 0)
            {
                ItemImage[i].sprite = ItemIndex[Items[i]];
                AmountText[i].text = "x" + Items[Items[i]].ToString("0");
            }
        }

    }


    public void ClearData()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            AmountOfItems[i] = 0;
            Items[i] = -1;
            AmountText[i].text = "";
            ItemImage[i].sprite = Nothing;
        }
    }
    public void GainedItem(int Index)
    {
        Added = false;
        for (int i = 0; i < Items.Length; i++)
        {
            if(Index == Items[i])
            {
                Added = true;
                AmountOfItems[i] += 1;
                AmountText[i].text = "x" + AmountOfItems[i].ToString("0");
            }
            if (Items[i] == -1 && !Added && Index != 10)
            {
                Items[i] = Index;
                ItemImage[i].sprite = ItemIndex[Index];
                AmountOfItems[i] += 1;
                AmountText[i].text = "x" + AmountOfItems[i].ToString("0");
                Added = true;
            }

        }
    }
        // Update is called once per frame
        void Update()
    {

    }
}

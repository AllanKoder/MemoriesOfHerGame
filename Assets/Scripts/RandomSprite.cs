using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] Sprites;
    private SpriteRenderer sr;
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = Sprites[Random.Range(0, Sprites.Length)];
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.position.y - transform.position.y > -0.1f)
        {
            sr.sortingOrder = 4;
        }
        else
        {
            sr.sortingOrder = 2;
        }
    }
}

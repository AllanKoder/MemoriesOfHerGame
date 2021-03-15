using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public float MovementSpeed;

    private Rigidbody2D rb;

    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
        rb.position = Vector2.MoveTowards(transform.position, Player.position, MovementSpeed * Time.fixedDeltaTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float Speed = 400f;
    public float Damage = 20f;
    public float Inaccuracy = 1f;
    public GameObject DeathPart;
    private Rigidbody2D rb;
    private Transform Player;
    // Start is called before the first frame update
    void Start()
    {

        var mousePos = Input.mousePosition;
        mousePos.z = 6f;
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 diff = Player.position - transform.position + new Vector3(Random.Range(-Inaccuracy,Inaccuracy), Random.Range(-Inaccuracy,Inaccuracy),0);
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string NameTag = collision.gameObject.tag;
        if (NameTag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(Damage);
            Instantiate(DeathPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Pillar" || collision.gameObject.tag == "Wall")
        {
            Instantiate(DeathPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float Speed;
    public GameObject DeathPart;
    public GameObject AudioPlay;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {

        var mousePos = Input.mousePosition;
        mousePos.z = 6f;
        rb = GetComponent<Rigidbody2D>();
        Vector3 diff = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        Instantiate(AudioPlay, transform.position, transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.up * Speed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string NameTag = collision.gameObject.tag;
        if (collision.gameObject.tag == "Pillar" || collision.gameObject.tag == "Wall")
        {
            Instantiate(DeathPart, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

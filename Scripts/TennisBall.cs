using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    public float AttackSpeed;
    public GameObject BallDeath;
    private List<float> Distances = new List<float> {0};

    private Rigidbody2D rb;
    private Transform Enemy;
    private bool Target;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        GameObject[] AllEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < AllEnemies.Length; i++)
        {
            Distances.Add(Vector2.Distance(AllEnemies[i].transform.position, transform.position));
        }
        float MinDistance = Distances.Min();
        int index = Distances.IndexOf(MinDistance);
        if (AllEnemies.Length > 0)
        {
            Enemy = AllEnemies[index].transform;
            Target = true;
        }
        else
        {
            Destroy(gameObject, 2f);
            Target = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Target && Enemy != null)
        {
            rb.position = Vector2.MoveTowards(transform.position, Enemy.position, Time.fixedDeltaTime * AttackSpeed);
        }
    }
    private void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pillar" || collision.gameObject.tag == "Wall")
        {
            Instantiate(BallDeath, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

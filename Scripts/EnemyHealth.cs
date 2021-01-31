using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float Health = 25f;
    public float Damage = 10f;
    public float DamageToPlayer = 10f;
    public float HealthLerp = 1f;
    public float DamageIteration = 0.5f;

    private float Timer;
    public GameObject Particles;
    public GameObject Item;
    public GameObject AudioPlay;
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Health *= PlayerController.EnemyPower * 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        sr.color = Color.Lerp(sr.color, Color.white, HealthLerp * Time.deltaTime);
        if(Health <= 0)
        {
            Instantiate(Particles, transform.position, Quaternion.identity);
            Instantiate(Item, transform.position, Quaternion.identity);
            Instantiate(AudioPlay, transform.position, transform.rotation);
            Destroy(gameObject);

        }
        Timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBullet")
        {
            sr.color = Color.black;
            Health -= (Damage * PlayerController.DamageToEnemy) / PlayerController.EnemyPower;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "TennisBall")
        {
            sr.color = Color.black;
            Health -= (Damage * PlayerController.BallMultipler * PlayerController.DamageToEnemy * 5f) / PlayerController.EnemyPower;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MusicAttack")
        {
            if (Timer < 0)
            {
                sr.color = Color.black;
                Health -= (PlayerController.Music * PlayerController.DamageToEnemy * 5f )/ PlayerController.EnemyPower;
                Timer = DamageIteration;
            }
        }
    }
}

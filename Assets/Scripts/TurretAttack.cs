using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{

    public float ShootyAmount = 3f;
    public float BulletWait = 0.1f;
    public float ReloadTime = 3f;
    public bool WillLook;
    public Sprite[] TurretDirection;

    private float BulletTimer;
    private float ShootyTimer;
    private float Timer;
    private Transform Player;
    private SpriteRenderer sr;
    private bool ShootBack;

    public GameObject EnemyBullet;
    public GameObject Particles;
    public GameObject AudioPlay;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        ShootyTimer = 0;
        BulletTimer = ReloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (WillLook)
        {
            if (Player.position.x - transform.position.x < 0 && Mathf.Abs(Player.position.x - transform.position.x) > Mathf.Abs(Player.position.y - transform.position.y))
            {
                sr.sprite = TurretDirection[0];
                ShootBack = false;

            }
            else if (Player.position.x - transform.position.x > 0 && Mathf.Abs(Player.position.x - transform.position.x) > Mathf.Abs(Player.position.y - transform.position.y))
            {
                sr.sprite = TurretDirection[1];
                ShootBack = false;

            }
            else if (Player.position.y - transform.position.y > 0)
            {
                sr.sprite = TurretDirection[2];
                ShootBack = true;
            }
            else if (Player.position.y - transform.position.y < 0)
            {
                sr.sprite = TurretDirection[3];
                ShootBack = false;

            }
        }


        if (BulletTimer < 0)
        {
            ShootyTimer = ShootyAmount;
            BulletTimer = ReloadTime;
        }

        if (ShootyTimer > 0 && Timer < 0)
        {
            Shoot();
            Timer = 0 + BulletWait;
        }
        Timer -= Time.deltaTime;
        BulletTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        Instantiate(AudioPlay, transform.position, transform.rotation);
        ShootyTimer -= 1f;
        Instantiate(EnemyBullet, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
        if (!ShootBack)
        {
            Instantiate(Particles, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
        }
    }

    /*
     *         ShootyTimer -= ShootyAmount;
        BulletTimer = WaitShooty;
        Timer = ReloadTime;
    */
}

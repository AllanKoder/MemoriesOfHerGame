using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Health;
    [Space]
    public float MovementSpeed;
    public float HealthMax;
    public static float HealthMultipier = 1f;
    public static float MovementSpeedMultipier = 1f;
    public static float AmmoMultipler = 1f;
    public static float ChanceMultipler = 2f;
    public static float HealthRegenRate = 1f;
    public static float CoinMultipler = 1f;
    public static float DamageToEnemy = 1f;
    public static float EnemyPower = 0.8f;
    public static float BallMultipler = 0f;
    public static float Music = 0f;

    public GameObject MusicAttack;
    public GameObject TennisBall;
    public GameObject TearsParticle;
    public GameObject AudioPlay;

    public static bool PlayerFalling;

    [Space]
    public float lerpSpeed;
    public CameraScript cs;
    public bool OnPlatform;
    public List<bool> OnPath = new List<bool>();


    public float ShootyAmount;
    public float WaitShooty;
    public float bulletClampMax;
    public float ReloadTime;
    public float ReloadSpeed;
    public float InvincibilityTime;
    public float FallWaitTime;
    public float FallBackSpeed;

    public Slider ShootingBar;
    public Slider HealthBar;
    public Text HealthText;
    public Text StrengthText;
    public GameObject PlayerBullet;
    public GameObject TurnToBlack;
    public Transform BoardSpawnLocation;

    [Space]

    private float InputX, InputY;
    private Vector2 MovementDirection;
    private float Timer;
    private float BallTimer;
    private float FallTimer;
    private float BulletTimer;
    private float ShootyTimer;
    private float Invincibility;
    private int Platforms;
    private bool Teleported;
    private bool UsedFallTimer;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer Sr;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Sr = GetComponent<SpriteRenderer>();
        ShootingBar = GameObject.FindGameObjectWithTag("ReloadBar").GetComponent<Slider>();
        cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
        HealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        HealthText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<Text>();
        StrengthText = GameObject.FindGameObjectWithTag("EnemyStrength").GetComponent<Text>();

        ShootyTimer = bulletClampMax;
        Health = HealthMax * HealthMultipier;
        OnPlatform = true;
        MusicAttack.SetActive(false);
        EnemyPower += 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        //Sliders 
        ShootingBar.value = ShootyTimer / (bulletClampMax * AmmoMultipler);
        HealthBar.value = Health / (HealthMax * HealthMultipier);
        HealthText.text = Health.ToString("0") + "/" + (HealthMax * HealthMultipier).ToString("0");
        StrengthText.text = "Enemy Strength: " + EnemyPower.ToString("0.00");
        if (Health > 0)
        {
            Health += Time.deltaTime * HealthRegenRate * 1.25f;
        }
        EnemyPower += Time.deltaTime * 0.003f;

        Health = Mathf.Clamp(Health, 0, HealthMax * HealthMultipier);

        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");

        if (InputX > 0)
        {
            Sr.flipX = false;
        }
        if(InputX < 0)
        {
            Sr.flipX = true;
        }
        if(Invincibility > 0)
        {
            Sr.color = new Color(255, 255, 255, 0.5f);
        }
        else
        {
            Sr.color = Color.white;
        }

        //Movemment Animation 
        if (Vector2.Distance(new Vector2(InputX,InputY),Vector2.zero) > 0.1f)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }


        if(Music > 0)
        {
            MusicAttack.SetActive(true);
        }
        if (BallMultipler > 0)
        {
            if (BallTimer < 0)
            {
                BallTimer = 5f / (BallMultipler * 0.75f);
                Instantiate(TennisBall, transform.position, Quaternion.identity);
            }
        }

        MovementDirection = Vector2.ClampMagnitude(Vector2.Lerp(MovementDirection, new Vector2(InputX, InputY), lerpSpeed * Time.deltaTime),350f);
        if (FallTimer <= 0)
        {
            PlayerFalling = false;
            anim.SetBool("Falling", false);
            if (!UsingController.ControllerConnected)
            {
                if (Input.GetMouseButton(0) && BulletTimer < 0 && ShootyTimer > 0)
                {
                    Shoot();
                }
            }
        }
        ShootyTimer = Mathf.Clamp(ShootyTimer, 0, bulletClampMax * AmmoMultipler);
        BulletTimer -= Time.deltaTime * AmmoMultipler;
        Timer -= Time.deltaTime;
        BallTimer -= Time.deltaTime;
        Invincibility -= Time.deltaTime;
        if (Timer < 0)
        {
            ShootyTimer = Mathf.Lerp(ShootyTimer, bulletClampMax * AmmoMultipler, ReloadSpeed * Time.deltaTime);
            ShootyTimer += 0.01f * Time.deltaTime;
        }

        Platforms = 0;
        for (int i = 0; i < OnPath.Count; i++)
        {
            if (OnPath[i])
            {
                Platforms++;
            }
        }
        if (!OnPlatform && Platforms <= 0 && !UsedFallTimer)
        {
            UsedFallTimer = true;
            FallTimer = FallWaitTime;
            anim.SetTrigger("FallAnimate");
        }
        FallTimer -= Time.deltaTime;
        if(FallTimer > 0)
        {
            anim.SetBool("Falling", true);
            PlayerFalling = true;
        }
        if(Health <= 0)
        {
            Instantiate(TurnToBlack, TurnToBlack.transform.position, Quaternion.identity);
            Invoke("LoadEnd", 2f);
        }


    }


    void LoadEnd()
    {
        SceneManager.LoadScene("TryAgain");

    }
    private void FixedUpdate()
    {
        if (Health > 0)
        {
            if (FallTimer < 0 && UsedFallTimer)
            {
                transform.position = BoardSpawnLocation.position + new Vector3(0, -0.5f, 0);
                Health -= EnemyPower * 10f;
                UsedFallTimer = false;

            }
            if (FallTimer < 0)
            {
                if (CreateBoard.EnemyAlive)
                {
                    rb.velocity = MovementDirection * MovementSpeed * MovementSpeedMultipier * Time.fixedDeltaTime;
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, 6.5f);
                }
                else
                {
                    rb.velocity = MovementDirection * 220f * MovementSpeedMultipier * Time.fixedDeltaTime;
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, 6.5f);

                }
            }

            else
            {
                rb.velocity = Vector2.ClampMagnitude(Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * 5f), 360f);
                transform.Translate(0, 0, FallBackSpeed * Time.deltaTime);
            }
        }
    }


    public void FullHeal()
    {
        if (Health > 0)
        {
            Health = HealthMax * HealthMultipier;
        }
    }

    public void BreadHeal()
    {
        if (Health > 0)
        {
            Health += 10f;
        }
    }


    void Shoot()
    {
        ShootyTimer -= ShootyAmount;
        BulletTimer = WaitShooty;
        Timer = ReloadTime;
        Instantiate(PlayerBullet, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
        Instantiate(TearsParticle, transform.position + new Vector3(0, 0.14f, 0), Quaternion.identity);
    }


    public void TakeDamage(float Amount)
    {
        if (Invincibility < 0 && Amount > 0 && FallTimer <= 0)
        {
            if (cs != null)
            {
                StartCoroutine(cs.Shake(0.15f, 0.05f));
            }
            Invincibility = InvincibilityTime;
            Health -= Amount * EnemyPower * Mathf.Pow(CoinMultipler, -1f) / 1.25f;
            Instantiate(AudioPlay, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string NameTag = collision.gameObject.tag;
        if (NameTag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().DamageToPlayer);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        string NameTag = collision.gameObject.tag;
        if (NameTag == "Enemy")
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyHealth>().DamageToPlayer);
        }
    }
}

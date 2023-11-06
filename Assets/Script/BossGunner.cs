using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunner : MonoBehaviour
{
    private PlayerController playercontroller;
    private Rigidbody2D rb;

    public float patternRateMin = 0.5f;
    public float patternRateMax = 2f;
    private float patternRate;
    private float timeAfterPattern;
    private bool patternON = false;
    private bool bulletRainON = false;

    public GameObject bullet;
    public GameObject bullet_parry;
    public GameObject bulletGun;
    public GameObject bomb;
    public GameObject bombGun;
    public GameObject bulletRain;

    private Animator anim;

    public GameObject target;

    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        patternRate = Random.Range(patternRateMin, patternRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer();

        if (!patternON)
        {
            timeAfterPattern += Time.deltaTime;
        }

        if (timeAfterPattern >= patternRate)
        {
            timeAfterPattern = 0f;

            int random = Random.Range(0, 10);

            switch (random)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    Invoke("Pattern_Shooting", 0.5f);
                    break;
                case 4:
                case 5:
                    Invoke("Pattern_Shooting_Parry", 0.5f);
                    break;
                case 6:
                case 7:
                    Pattern_Bomb();
                    break;
                case 8:
                case 9:
                    Pattern_BulletRain();
                    break;
            }

            patternRate = Random.Range(patternRateMin, patternRateMax);
        }


        if (bulletRainON)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 40f, transform.position.z), 0.01f);
        }

        if (!bulletRainON)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -6.2f, transform.position.z), 0.01f);
        }

    }

    private void LookPlayer()
    {
        if (playercontroller.dirPos.x <= 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Pattern_Shooting()
    {
        patternON = true;

        BulletSpawn();
        Invoke("BulletSpawn", 0.2f);
        Invoke("BulletSpawn", 0.4f);


        Invoke("PatternStop", 0.6f);
    }

    private void BulletSpawn()
    {
        Instantiate(bullet, bulletGun.transform.position, bulletGun.transform.rotation);
        anim.SetBool("isShooting", true);
    }

    private void Pattern_Shooting_Parry()
    {
        patternON = true;

        ParryBulletSpawn();


        Invoke("PatternStop", 0.2f);
    }

    private void ParryBulletSpawn()
    {
        Instantiate(bullet_parry, bulletGun.transform.position, bulletGun.transform.rotation);
    }

    private void Pattern_Bomb()
    {
        patternON = true;

        BombSpawn();
        Invoke("BombSpawn", 0.2f);
        Invoke("BombSpawn", 0.4f);
        Invoke("BombSpawn", 0.6f);
        Invoke("BombSpawn", 0.8f);

        Invoke("PatternStop", 1f);
    }

    private void BombSpawn()
    {
        Instantiate(bomb, bombGun.transform.position, bombGun.transform.rotation);
    }

    private void Pattern_BulletRain()
    {
        patternON = true;

        bulletRainON = true;

        Invoke("BulletRainSpawn", 0.5f);
        Invoke("BulletRainSpawn", 1f);
        Invoke("BulletRainSpawn", 1.5f);
        Invoke("BulletRainSpawn", 2f);
        Invoke("BulletRainSpawn", 2.5f);

        Invoke("Move", 3f);

        Invoke("BulletRainStop", 4f);


        Invoke("PatternStop", 6f);
    }

    private void Move()
    {
        transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
    }

    private void BulletRainSpawn()
    {
        for (int i = 0; i < 10; i++)
        {
            int random = Random.Range(60, 120);

            Instantiate(bullet, bulletRain.transform.position, Quaternion.Euler(new Vector3(0, 0, -random)));
        }
    }

    private void BulletRainStop()
    {
        bulletRainON = false;
    }

    private void PatternStop()
    {
        patternON = false;
        anim.SetBool("isShooting", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGunner : MonoBehaviour
{
    private PlayerController playercontroller;

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
        anim = GetComponent<Animator>();

        patternRate = Random.Range(patternRateMin, patternRateMax);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.GameStart && GameObject.FindWithTag("boss").GetComponent<BossStatus>().bossHP > 0)
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
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -6.2f, transform.position.z), 0.02f);
            }
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

        anim.SetBool("isShooting", true);
        BulletSpawn();
        Invoke("BulletSpawn", 0.2f);
        Invoke("BulletSpawn", 0.4f);

        Invoke("PatternStop", 0.4f);
    }

    private void BulletSpawn()
    {
        Instantiate(bullet, bulletGun.transform.position, bulletGun.transform.rotation);
    }

    private void Pattern_Shooting_Parry()
    {
        patternON = true;

        float ran = Random.Range(1.2f, 2.5f);
        GameObject.Find("Boss_Gunner").transform.Find("ExclamationMark").gameObject.SetActive(true);

        Invoke("ExclamationMarkOff", 0.5f);

        Invoke("AimOn", 0.7f);

        Invoke("AimOff", ran - 0.3f);

        Invoke("ParryBulletSpawn", ran);

        Invoke("PatternStop", ran + 0.5f);
    }

    private void AimOn()
    {
        GameObject.FindWithTag("Player").transform.Find("Aim").gameObject.SetActive(true);
    }

    private void AimOff()
    {
        GameObject.FindWithTag("Player").transform.Find("Aim").gameObject.SetActive(false);
    }

    private void ExclamationMarkOff()
    {
        GameObject.Find("Boss_Gunner").transform.Find("ExclamationMark").gameObject.SetActive(false);
    }

    private void ParryBulletSpawn()
    {
        Instantiate(bullet_parry, bulletGun.transform.position, bulletGun.transform.rotation);
    }

    private void Pattern_Bomb()
    {
        patternON = true;

        anim.SetBool("isGrander", true);

        Invoke("BombSpawn", 0.4f);
        Invoke("BombSpawn", 0.5f);
        Invoke("BombSpawn", 0.6f);
        Invoke("BombSpawn", 0.7f);
        Invoke("BombSpawn", 0.8f);

        Invoke("PatternStop", 1f);
    }

    private void BombSpawn()
    {
        anim.SetBool("isGrander", false);
        Instantiate(bomb, bombGun.transform.position, bombGun.transform.rotation);
    }

    private void Pattern_BulletRain()
    {
        patternON = true;

        anim.SetBool("isJump", true);
        Invoke("BulletRainStart", 0.7f);

        Invoke("BulletRainSpawn", 1.5f);
        Invoke("BulletRainSpawn", 2f);
        Invoke("BulletRainSpawn", 2.5f);
        Invoke("BulletRainSpawn", 3f);
        Invoke("BulletRainSpawn", 3.5f);

        Invoke("Move", 3f);

        Invoke("BulletRainStop", 6f);


        Invoke("PatternStop", 9f);
    }

    private void BulletRainStart()
    {
        bulletRainON = true;
    }

    private void Move()
    {
        anim.SetBool("isJump", false);
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

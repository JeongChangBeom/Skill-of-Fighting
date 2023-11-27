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
    private bool snipingON = false;

    public GameObject bullet;
    public GameObject bullet_parry;
    public GameObject bulletGun;
    public GameObject bomb;
    public GameObject bombGun;
    public GameObject bulletRain;

    private Animator anim;

    public GameObject target;
    public GameObject skid;

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
                        StartCoroutine(Pattern_Shooting_Parry());
                        //StartCoroutine(Pattern_Shooting());
                        break;
                    case 4:
                    case 5:
                        StartCoroutine(Pattern_Shooting_Parry());
                        break;
                    case 6:
                    case 7:
                        StartCoroutine(Pattern_Shooting_Parry());
                        //StartCoroutine(Pattern_Bomb());
                        break;
                    case 8:
                    case 9:
                        StartCoroutine(Pattern_Shooting_Parry());
                        // StartCoroutine(Pattern_BulletRain());
                        break;
                }

                patternRate = Random.Range(patternRateMin, patternRateMax);
            }

            if (bulletRainON)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 40f, transform.position.z), 0.03f);
            }

            if (!bulletRainON)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, -6.05f, transform.position.z), 30 * Time.deltaTime);
            }

            if (snipingON)
            {
                if (transform.position.x > 0)
                {
                    if (playercontroller.dirPos.x < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(30f, transform.position.y, transform.position.z), 0.1f);
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(12f, transform.position.y, transform.position.z), 0.1f);
                    }
                }
                else
                {
                    if (playercontroller.dirPos.x < 0)
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(-12f, transform.position.y, transform.position.z), 0.1f);
                    }
                    else
                    {
                        transform.position = Vector3.Lerp(transform.position, new Vector3(-30f, transform.position.y, transform.position.z), 0.1f);
                    }
                }
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

    IEnumerator Pattern_Shooting()
    {
        patternON = true;
        anim.SetBool("isShooting", true);

        Instantiate(bullet, bulletGun.transform.position, bulletGun.transform.rotation);

        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, bulletGun.transform.position, bulletGun.transform.rotation);

        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, bulletGun.transform.position, bulletGun.transform.rotation);

        PatternStop();
    }


    IEnumerator Pattern_Shooting_Parry()
    {
        patternON = true;
        float ran = Random.Range(0.5f, 1.5f);
        GameObject.Find("Boss_Gunner").transform.Find("ExclamationMark").gameObject.SetActive(true);
        anim.SetBool("isSniping", true);

        yield return new WaitForSeconds(0.5f);
        GameObject.Find("Boss_Gunner").transform.Find("ExclamationMark").gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        GameObject.FindWithTag("Player").transform.Find("Aim").gameObject.SetActive(true);

        yield return new WaitForSeconds(ran);
        GameObject.FindWithTag("Player").transform.Find("Aim").gameObject.SetActive(false);

        yield return new WaitForSeconds(0.3f);
        skid.SetActive(true);
        skid.transform.position = transform.position + new Vector3(0, -3.5f, 0);
        skid.GetComponent<Animator>().SetBool("isSkid", true);
        Instantiate(bullet_parry, bulletGun.transform.position, bulletGun.transform.rotation);
        snipingON = true;

        yield return new WaitForSeconds(0.3f);
        anim.SetBool("isSniping", false);
        snipingON = false;

        yield return new WaitForSeconds(0.5f);
        skid.GetComponent<Animator>().SetBool("isSkid", false);
        skid.SetActive(false);
        StartCoroutine(Pattern_BulletRain());
    }


    IEnumerator Pattern_Bomb()
    {
        patternON = true;
        anim.SetBool("isGrander", true);

        yield return new WaitForSeconds(0.4f);
        anim.SetBool("isGrander", false);
        Instantiate(bomb, bombGun.transform.position, bombGun.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        Instantiate(bomb, bombGun.transform.position, bombGun.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        Instantiate(bomb, bombGun.transform.position, bombGun.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        Instantiate(bomb, bombGun.transform.position, bombGun.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        Instantiate(bomb, bombGun.transform.position, bombGun.transform.rotation);

        PatternStop();
    }

    IEnumerator Pattern_BulletRain()
    {
        patternON = true;
        anim.SetBool("isJump", true);

        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < 10; i++)
        {
            int random = Random.Range(60, 120);

            Instantiate(bullet, bulletRain.transform.position, Quaternion.Euler(new Vector3(0, 0, -random)));
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            int random = Random.Range(60, 120);

            Instantiate(bullet, bulletRain.transform.position, Quaternion.Euler(new Vector3(0, 0, -random)));
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            int random = Random.Range(60, 120);

            Instantiate(bullet, bulletRain.transform.position, Quaternion.Euler(new Vector3(0, 0, -random)));
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            int random = Random.Range(60, 120);

            Instantiate(bullet, bulletRain.transform.position, Quaternion.Euler(new Vector3(0, 0, -random)));
        }
        if (transform.position.x > 0)
        {
            transform.position = new Vector3(-21, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(21, transform.position.y, transform.position.z);
        }

        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 10; i++)
        {
            int random = Random.Range(60, 120);

            Instantiate(bullet, bulletRain.transform.position, Quaternion.Euler(new Vector3(0, 0, -random)));
        }

        yield return new WaitForSeconds(2.5f);
        bulletRainON = false;

        yield return new WaitForSeconds(3.0f);
        PatternStop();
    }
    private void PatternStop()
    {
        patternON = false;
        anim.SetBool("isShooting", false);
    }

    public void BulletRainOn()
    {
        bulletRainON = true;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "ground")
        {
            anim.SetBool("isJump", false);
        }
    }

}

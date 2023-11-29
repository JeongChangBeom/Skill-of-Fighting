using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHunter : MonoBehaviour
{
    private PlayerController playercontroller;

    public float patternRateMin = 0.5f;
    public float patternRateMax = 3f;
    private float patternRate;
    private float timeAfterPattern;
    private bool patternON = false;
    private bool backstepON = false;
    private bool moveON = false;

    public GameObject arrow;
    public GameObject arrow_parry;
    public GameObject smoke;

    public GameObject targetRight;
    public GameObject targetLeft;

    private Animator anim;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        patternRate = Random.Range(patternRateMin, patternRateMax);
    }

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
                        StartCoroutine(Pattern_Shooting());
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        StartCoroutine(Pattern_Shooting_Parry());
                        break;
                    case 8:
                    case 9:
                        StartCoroutine(Pattern_Move());
                        break;
                }

                patternRate = Random.Range(patternRateMin, patternRateMax);
            }
            else
            {
                anim.SetBool("isAttack", false);
            }

            if (backstepON)
            {
                if (transform.position.x >= 0)
                {
                    transform.position = Vector3.Lerp(transform.position, targetRight.transform.position, 0.02f);
                }
                else if (transform.position.x < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, targetLeft.transform.position, 0.02f);
                }
            }

            if (moveON)
            {
                if (transform.position.x >= 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(17, transform.position.y, transform.position.z), 0.01f);
                }
                else if (transform.position.x < 0)
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(-17, transform.position.y, transform.position.z), 0.01f);
                }
            }
        }
    }

    private void LookPlayer()
    {
        if (playercontroller.dirPos.x <= 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    IEnumerator Pattern_Shooting() {
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.5f);
        if (playercontroller.dirPos.x <= 0)
        {
            arrow.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            arrow.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

        }
        Instantiate(arrow, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
        SoundManager.instance.Arrow_Sound();
    }

    IEnumerator Pattern_Shooting_Parry()
    {
        SoundManager.instance.StageExclamationMarkOn_Sound();
        GameObject.Find("Boss_Hunter").transform.Find("ExclamationMark").gameObject.SetActive(true);

        patternON = true;

        if (playercontroller.dirPos.x <= 0)
        {
            arrow_parry.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            arrow_parry.transform.localScale = new Vector3(-2f, 2f, 2f);
        }

        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Boss_Hunter").transform.Find("ExclamationMark").gameObject.SetActive(false);

        yield return new WaitForSeconds(0.3f);
        Instantiate(arrow_parry, transform.position, transform.rotation);
        SoundManager.instance.ParryArrow_Sound();
        PatternStop();
    }

    public IEnumerator Pattern_Move()
    {
        patternON = true;
        backstepON = true;
        Instantiate(smoke, transform.position, transform.rotation);
        anim.SetBool("isBackstep", true);
        SoundManager.instance.Smoke_Sound();

        yield return new WaitForSeconds(0.5f);
        backstepON = false;
        anim.SetBool("isBackstep", false);
        if (transform.position.x >= 0)
        {
            transform.position = targetLeft.transform.position;
        }
        else
        {
            transform.position = targetRight.transform.position;
        }

        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isMove", true);
        moveON = true;

        yield return new WaitForSeconds(3.4f);
        anim.SetBool("isMove", false);
        moveON = false;

        yield return new WaitForSeconds(0.5f);
        PatternStop();


    }

    private void PatternStop()
    {
        patternON = false;
    }
}

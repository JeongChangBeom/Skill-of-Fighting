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
                case 4:
                case 5:
                    Invoke("Pattern_Shooting", 0.5f);
                    anim.SetBool("isAttack", true);
                    break;
                case 6:
                case 7:
                    Pattern_Shooting_Parry();
                    break;
                case 8:
                case 9:
                    Pattern_Move();
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

    private void Pattern_Shooting()
    {
        if (playercontroller.dirPos.x <= 0)
        {
            arrow.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            arrow.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

        }
        Instantiate(arrow, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
    }

    private void Pattern_Shooting_Parry()
    {
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
        Invoke("ShootingAnim", 0.5f);

        Invoke("ExclamationMarkOff", 0.7f);

        Invoke("ArrowSpawn", 1f);

        Invoke("PatternStop", 1f);
    }

    private void ShootingAnim()
    {
        anim.SetBool("isAttack", true);
    }

    private void ExclamationMarkOff()
    {
        GameObject.Find("Boss_Hunter").transform.Find("ExclamationMark").gameObject.SetActive(false);
    }
    private void ArrowSpawn()
    {
        Instantiate(arrow_parry, transform.position, transform.rotation);
    }

    public void Pattern_Move()
    {
        patternON = true;

        backstepON = true;

        Instantiate(smoke, transform.position, transform.rotation);

        anim.SetBool("isBackstep", true);
        Invoke("BackstepStop", 0.5f);

        Invoke("PositionChange", 0.5f);

        Invoke("Move", 0.6f);

        Invoke("MoveStop", 4f);

        Invoke("PatternStop", 4.5f);
    }

    private void PositionChange()
    {
        if (transform.position.x >= 0)
        {
            transform.position = targetLeft.transform.position;
        }
        else
        {
            transform.position = targetRight.transform.position;
        }
    }

    private void Move()
    {
        anim.SetBool("isMove", true);
        moveON = true;
    }
    private void MoveStop()
    {
        anim.SetBool("isMove", false);
        moveON = false;
    }

    private void BackstepStop()
    {
        backstepON = false;
        anim.SetBool("isBackstep", false);
    }

    private void PatternStop()
    {
        patternON = false;
    }
}

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

    public GameObject arrow;
    public GameObject arrow_parry;
    public GameObject smoke;

    private Animator anim;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();

        patternRate = Random.Range(patternRateMin, patternRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer();

        timeAfterPattern += Time.deltaTime;

        if (timeAfterPattern >= patternRate)
        {
            timeAfterPattern = 0f;

            int random = Random.Range(0, 10);

            switch(random){
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    Invoke("Pattern_Shooting",0.5f);
                    anim.SetBool("isAttack", true);
                    break;
                case 6:
                case 7:
                    Invoke("Pattern_Shooting_Parry", 0.5f);
                    anim.SetBool("isAttack", true);
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
        if(playercontroller.dirPos.x <= 0)
        {
            arrow.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if(playercontroller.dirPos.x > 0)
        {
            arrow.transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);

        }
        Instantiate(arrow, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
    }

    private void Pattern_Shooting_Parry()
    {
        if (playercontroller.dirPos.x <= 0)
        {
            arrow_parry.transform.localScale = new Vector3(2f, 2f, 2f);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            arrow_parry.transform.localScale = new Vector3(-2f, 2f, 2f);
        }
        Instantiate(arrow_parry, transform.position, transform.rotation);
    }

    public void Pattern_Move()
    {
        Instantiate(smoke, transform.position, transform.rotation);

        if (transform.position.x >= 0)
        {
            transform.position = new Vector3(-19f, -8.16f, 1);
        }
        else
        {
            transform.position = new Vector3(19f, -8.16f, 1);
        }
    }
}

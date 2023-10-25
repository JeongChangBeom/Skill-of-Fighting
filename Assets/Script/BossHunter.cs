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
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

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
                    Pattern_Shooting();
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
        Instantiate(arrow, transform.position, transform.rotation);
    }

    private void Pattern_Shooting_Parry()
    {
        Instantiate(arrow_parry, transform.position, transform.rotation);
    }

    public void Pattern_Move()
    {
        if(transform.position.x >= 0)
        {
            transform.position = new Vector3(-22.5f, -8.16f, 1);
        }
        else
        {
            transform.position = new Vector3(22.5f, -8.16f, 1);
        }
    }
}

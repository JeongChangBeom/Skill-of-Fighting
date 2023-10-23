using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHunter : MonoBehaviour
{
    private PlayerController playercontroller;

    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 3f;
    private float patternRate;
    private float timeAfterPattern;

    public GameObject arrow;
    public GameObject arrow_parry;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        patternRate = Random.Range(spawnRateMin, spawnRateMax);
    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer();

        timeAfterPattern += Time.deltaTime;

        if (timeAfterPattern >= patternRate)
        {
            timeAfterPattern = 0f;

            int random = Random.Range(0, 3);

            switch(random){
                case 0:
                    Pattern_Shooting();
                    break;
                case 1:
                    Pattern_Shooting_Parry();
                    break;
                case 2:
                    print(3);
                    break;
            }

            patternRate = Random.Range(spawnRateMin, spawnRateMax);
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

    private void Pattern_Move()
    {

    }
}

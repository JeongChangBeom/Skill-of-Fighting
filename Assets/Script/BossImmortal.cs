using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossImmortal : MonoBehaviour
{
    public float patternRateMin = 0.5f;
    public float patternRateMax = 1f;
    private float patternRate;
    private float timeAfterPattern;
    private bool patternON = false;

    private Animator Left_Arm_anim;
    private Animator Right_Arm_anim;

    public GameObject armLeft;
    public GameObject armRight;
    public GameObject smashEff;

    public GameObject missileready;
    public GameObject missile;
    public GameObject missile_tagert;

    private bool missileON = false;


    void Start()
    {
        Left_Arm_anim = GameObject.Find("Boss_Immortal").transform.Find("Immortal_Arm_Left").GetComponent<Animator>();
        Right_Arm_anim = GameObject.Find("Boss_Immortal").transform.Find("Immortal_Arm_Right").GetComponent<Animator>();

        patternRate = Random.Range(patternRateMin, patternRateMax);
    }

    // Update is called once per frame
    void Update()
    {
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
                    Pattern_RocketPunch_Left();
                    break;
                case 2:
                case 3:
                    Pattern_RocketPunch_Right();
                    break;
                case 4:
                case 5:
                    Pattern_Smash_Left();
                    break;
                case 6:
                case 7:
                    Pattern_Smash_Right();
                    break;
                case 8:
                case 9:
                    Pattern_Missile();
                    break;
            }

            patternRate = Random.Range(patternRateMin, patternRateMax);
        }

        if (missileON)
        {
            missileready.transform.position = Vector3.Lerp(missileready.transform.position, missile_tagert.transform.position, 0.005f);
        }
        else
        {
            missileready.transform.position = new Vector3(0, 0, 0);
        }
    }

    private void Pattern_RocketPunch_Left()
    {
        patternON = true;

        Left_Arm_anim.SetBool("isPunch", true);

        Invoke("LeftPunchSapwn", 0.5f);

        Invoke("PunchStop", 2.0f);

        Invoke("PatternStop", 3.0f);
    }

    private void Pattern_RocketPunch_Right()
    {
        patternON = true;

        Right_Arm_anim.SetBool("isPunch", true);

        Invoke("RightPunchSapwn", 0.5f);

        Invoke("PunchStop", 2.0f);

        Invoke("PatternStop", 3.0f);
    }

    private void LeftPunchSapwn()
    {
        Instantiate(armLeft, armLeft.transform.position, armLeft.transform.rotation);
    }

    private void RightPunchSapwn()
    {
        Instantiate(armRight, armRight.transform.position, armRight.transform.rotation);
    }

    private void PunchStop()
    {
        Left_Arm_anim.SetBool("isPunch", false);
        Right_Arm_anim.SetBool("isPunch", false);
    }

    private void Pattern_Smash_Left()
    {
        patternON = true;

        Left_Arm_anim.SetBool("isSmash", true);

        Invoke("LeftSmashEff", 0.5f);

        Invoke("SmashStop", 1.0f);

        Invoke("PatternStop", 2.0f);
    }

    private void Pattern_Smash_Right()
    {
        patternON = true;

        Right_Arm_anim.SetBool("isSmash", true);

        Invoke("RightSmashEff", 0.5f);

        Invoke("SmashStop", 1.0f);

        Invoke("PatternStop", 2.0f);
    }

    private void LeftSmashEff()
    {
        Instantiate(smashEff, new Vector3(-5.5f, -10f, 0f), smashEff.transform.rotation);
    }

    private void RightSmashEff()
    {
        Instantiate(smashEff, new Vector3(5.5f, -10f, 0f), smashEff.transform.rotation);
    }

    private void SmashStop()
    {
        Left_Arm_anim.SetBool("isSmash", false);
        Right_Arm_anim.SetBool("isSmash", false);
    }

    private void Pattern_Missile()
    {
        missileON = true;

        Invoke("MissileSpawn", 1f);

        Invoke("MissileStop", 1.5f);
    }

    private void MissileStop()
    {
        missileON = false;
    }

    private void MissileSpawn()
    {
        int random = Random.Range(-10, 11);

        Instantiate(missile, new Vector3(missile_tagert.transform.position.x + random, missile_tagert.transform.position.y, missile_tagert.transform.position.z), missile_tagert.transform.rotation);
    }

    private void PatternStop()
    {
        patternON = false;
    }
}

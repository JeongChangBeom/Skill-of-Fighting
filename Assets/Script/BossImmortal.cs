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

    private SpriteRenderer sp;
    public Sprite immortal;
    public Sprite immortal_razer;

    private Animator Left_Arm_anim;
    private Animator Right_Arm_anim;

    public GameObject razer;
    public GameObject RazerTarget;

    public GameObject armLeft;
    public GameObject armRight;
    public GameObject smashEff;

    public GameObject missileready;
    public GameObject missile;
    public GameObject missile_tagert;

    private bool missileON = false;
    private bool razerON = false;

    CameraController cameracontroller;
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        Left_Arm_anim = GameObject.Find("Boss_Immortal").transform.Find("Immortal_Arm_Left").GetComponent<Animator>();
        Right_Arm_anim = GameObject.Find("Boss_Immortal").transform.Find("Immortal_Arm_Right").GetComponent<Animator>();
        cameracontroller = GameObject.Find("Main Camera").GetComponent<CameraController>();

        patternRate = Random.Range(patternRateMin, patternRateMax);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager.instance.GameStart && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().mainHP > 0)
        {
            if (!patternON)
            {
                timeAfterPattern += Time.deltaTime;
            }

            if (timeAfterPattern >= patternRate)
            {
                timeAfterPattern = 0f;
                int random = Random.Range(0, 10);

                if (GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().leftarmHP > 0 && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().rightarmHP > 0)
                {
                    switch (random)
                    {
                        case 0:
                            StartCoroutine(Pattern_RocketPunch_Left());
                            break;
                        case 1:
                            StartCoroutine(Pattern_RocketPunch_Right());
                            break;
                        case 2:
                        case 3:
                            StartCoroutine(Pattern_Smash_Left());
                            break;
                        case 4:
                        case 5:
                            StartCoroutine(Pattern_Smash_Right());
                            break;
                        case 6:
                            if (!missileON)
                            {
                                StartCoroutine(Pattern_Missile());
                            }
                            break;
                        case 7:
                        case 8:
                        case 9:
                            if (!razerON)
                            {
                                StartCoroutine(Pattern_Razer_Parry());
                            }
                            break;

                    }
                }
                else if(GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().leftarmHP > 0 && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().rightarmHP <= 0)
                {
                    switch (random)
                    {
                        case 0:
                        case 1:
                            StartCoroutine(Pattern_RocketPunch_Left());
                            break;
                        case 2:
                        case 3:
                            StartCoroutine(Pattern_Smash_Left());
                            break;
                        case 4:
                        case 5:
                            if (!missileON)
                            {
                                StartCoroutine(Pattern_Missile());
                            }
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            if (!razerON)
                            {
                                StartCoroutine(Pattern_Razer_Parry());
                            }
                            break;
                    }
                }
                else if (GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().leftarmHP <= 0 && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().rightarmHP > 0)
                {
                    switch (random)
                    {
                        case 0:
                        case 1:
                            StartCoroutine(Pattern_RocketPunch_Right());
                            break;
                        case 2:
                        case 3:
                            StartCoroutine(Pattern_Smash_Right());
                            break;
                        case 4:
                        case 5:
                            if (!missileON)
                            {
                                StartCoroutine(Pattern_Missile());
                            }
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            if (!razerON)
                            {
                                StartCoroutine(Pattern_Razer_Parry());
                            }
                            break;
                    }
                }
                else
                {
                    switch (random)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            if (!missileON)
                            {
                                StartCoroutine(Pattern_Missile());
                            }
                            break;
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                            if (!razerON)
                            {
                                StartCoroutine(Pattern_Razer_Parry());
                            }
                            break;
                    }
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
    }

    IEnumerator Pattern_RocketPunch_Left()
    {
        patternON = true;
        Left_Arm_anim.SetBool("isPunch", true);
        SoundManager.instance.Punch01_Sound();

        yield return new WaitForSeconds(0.5f);
        Instantiate(armLeft, armLeft.transform.position, armLeft.transform.rotation);
        SoundManager.instance.Punch02_Sound();

        yield return new WaitForSeconds(1.5f);
        Left_Arm_anim.SetBool("isPunch", false);
        SoundManager.instance.Punch01_Sound();

        yield return new WaitForSeconds(1.0f);
        PatternStop();
    }

    IEnumerator Pattern_RocketPunch_Right()
    {
        patternON = true;
        Right_Arm_anim.SetBool("isPunch", true);
        SoundManager.instance.Punch01_Sound();

        yield return new WaitForSeconds(0.5f);
        Instantiate(armRight, armRight.transform.position, armRight.transform.rotation);
        SoundManager.instance.Punch02_Sound();

        yield return new WaitForSeconds(1.5f);
        Right_Arm_anim.SetBool("isPunch", false);
        SoundManager.instance.Punch01_Sound();

        yield return new WaitForSeconds(1.0f);
        PatternStop();
    }

    IEnumerator Pattern_Smash_Left()
    {
        patternON = true;
        Left_Arm_anim.SetBool("isSmash", true);

        yield return new WaitForSeconds(0.5f);
        Instantiate(smashEff, new Vector3(-5.5f, -10f, 0f), smashEff.transform.rotation);
        SoundManager.instance.Smash_Sound();
        cameracontroller.StartCoroutine(cameracontroller.Shake());

        yield return new WaitForSeconds(0.5f);
        Left_Arm_anim.SetBool("isSmash", false);

        yield return new WaitForSeconds(1.0f);
        PatternStop();
    }
    
    IEnumerator Pattern_Smash_Right()
    {
        patternON = true;
        Right_Arm_anim.SetBool("isSmash", true);

        yield return new WaitForSeconds(0.5f);
        Instantiate(smashEff, new Vector3(5.5f, -10f, 0f), smashEff.transform.rotation);
        SoundManager.instance.Smash_Sound();
        cameracontroller.StartCoroutine(cameracontroller.Shake());

        yield return new WaitForSeconds(0.5f);
        Right_Arm_anim.SetBool("isSmash", false);

        yield return new WaitForSeconds(1.0f);
        PatternStop();
    }

    IEnumerator Pattern_Missile()
    {
        missileON = true;
        SoundManager.instance.MissileBefore_Sound();

        if (missileON)
        {
            yield return new WaitForSeconds(1.0f);
            int random = Random.Range(-10, 11);
            Instantiate(missile, new Vector3(missile_tagert.transform.position.x + random, missile_tagert.transform.position.y, missile_tagert.transform.position.z), missile_tagert.transform.rotation);

            yield return new WaitForSeconds(0.5f);
        }
        missileON = false;
    }

    IEnumerator Pattern_Razer_Parry()
    {
        razerON = true;
        if (razerON)
        {
            sp.sprite = immortal_razer;
            SoundManager.instance.RaserBefore_Sound();

            yield return new WaitForSeconds(0.5f);
            Instantiate(razer, RazerTarget.transform.position, RazerTarget.transform.rotation);
            SoundManager.instance.Raser_Sound();

            yield return new WaitForSeconds(0.1f);
            sp.sprite = immortal;
        }
        razerON = false;
    }

    private void PatternStop()
    {
        patternON = false;
    }
}

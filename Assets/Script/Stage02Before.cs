using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage02Before : MonoBehaviour
{
    public int textCount = 0;
    public int maxCount = 4;

    public GameObject bullet_parry;
    public GameObject bulletGun;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public GameObject guardian;
    public GameObject gunner;

    public Text playerText;
    public Text bossText;

    string[] talk;

    private bool guardianMove = false;
    private bool gunnerMove = false;

    private bool player = false;
    private bool boss = false;

    private bool countOn = false;
    private bool typing = false;

    private bool isTypingRuning = false;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        talk = new string[] { "당신이 거너군요. 스승님을 왜 죽인 것이죠?",
                              "...",
                              "왜!" ,
                              "..." ,
                            };

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(ScenarioOn());
    }

    void Update()
    {
        if (textCount >= maxCount)
        {
            GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeOut", true);
            StartCoroutine(NextStage());
        }

        if (countOn)
        {
            backgroundPanel.SetActive(true);

            if (Input.anyKeyDown && !isTypingRuning)
            {
                textCount++;
                typing = false;
            }
        }
        else
        {
            backgroundPanel.SetActive(false);
        }

        if (!typing)
        {
            if ((textCount % 2 == 0 || textCount == 0) && textCount < maxCount)
            {
                player = true;
                boss = false;
            }
            else if ((textCount % 2 != 0 || textCount != 0) && textCount < maxCount)
            {
                player = false;
                boss = true;
            }
            else
            {
                player = false;
                boss = false;
            }

            if (textCount < maxCount)
            {
                StartCoroutine(TypingText(talk[textCount]));
            }
        }

        if (player)
        {
            playerPanel.SetActive(true);
            bossPanel.SetActive(false);
        }
        else if (boss)
        {
            bossPanel.SetActive(true);
            playerPanel.SetActive(false);
        }
        else
        {
            playerPanel.SetActive(false);
            bossPanel.SetActive(false);
        }

        if (guardianMove)
        {
            guardian.transform.position = Vector3.MoveTowards(guardian.transform.position, new Vector3(-9f, guardian.transform.position.y, guardian.transform.position.z), 30f * Time.deltaTime);
        }

        if (gunnerMove)
        {
            gunner.transform.position = Vector3.MoveTowards(gunner.transform.position, new Vector3(gunner.transform.position.x, -6.2f, guardian.transform.position.z), 40f * Time.deltaTime);
        }
    }

    IEnumerator TypingText(string s)
    {
        typing = true;
        isTypingRuning = true;
        playerText.text = "";
        bossText.text = "";

        foreach (char letter in s.ToCharArray())
        {
            if (player)
            {
                playerText.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
            else if (boss)
            {
                bossText.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
        }

        isTypingRuning = false;
    }
    IEnumerator ScenarioOn()
    {
        yield return new WaitForSeconds(3.0f);
        Instantiate(bullet_parry, bulletGun.transform.position, bulletGun.transform.rotation);

        yield return new WaitForSeconds(0.1f);
        guardianMove = true;
        guardian.GetComponent<Animator>().SetBool("isBackStep", true);

        yield return new WaitForSeconds(0.1f);
        guardianMove = false;
        guardian.GetComponent<Animator>().SetBool("isBackStep", false);

        yield return new WaitForSeconds(0.5f);
        gunnerMove = true;

        yield return new WaitForSeconds(3.0f);
        countOn = true;

    }
    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage02");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage01After : MonoBehaviour
{
    public int textCount = 0;
    public int maxCount = 10;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public GameObject guardian;

    public Text playerText;
    public Text bossText;

    string[] talk;

    private bool guardianMove = false;

    private bool player = false;
    private bool boss = false;

    private bool countOn = false;
    private bool typing = false;

    private bool isTypingRuning = false;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        talk = new string[] { "어쨰서 그런 짓을 하신것이죠?",
                              "나는....	   		전통무기이기 싫었다!",
                              "예?" ,
                              "현대 무기 측에서 나에게 얘기하더군 \n너희 전통무기의 수장의 이름과 위치를 알려준다면 \n나를 현대무기에 껴주겠다고" ,
                              "어째서 현대 무기가 되고 싶으셨던 거에요?" ,
                              "너무 구닥다리이지 않나 새시대를 살아가고 싶었을 뿐이지.",
                              "....",
                              "아무튼! 너의 스승을 죽인것은 거너라는 인물이다. \n그는 지금 황무지에 있다!",
                            };

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(PlayerIn());
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
            guardian.transform.position = Vector3.MoveTowards(guardian.transform.position, new Vector3(-5f, guardian.transform.position.y, guardian.transform.position.z), 10f * Time.deltaTime);
        }

        if (guardian.transform.position.x >= -5f)
        {
            guardian.GetComponent<Animator>().SetBool("isMove", false);
            guardianMove = false;
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
    IEnumerator PlayerIn()
    {
        yield return new WaitForSeconds(2.0f);
        countOn = true;
    }
    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage02_Before");
    }
}
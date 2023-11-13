using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage01Before : MonoBehaviour
{
    public int textCount = 10;

    public GameObject hunter;
    public GameObject smoke;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public Text playerText;
    public Text bossText;

    string str;
    string[] talk;

    private bool player = false;
    private bool boss = false;

    private bool countOn = false;
    private bool typing = false;
    void Start()
    {
        playerText.text = "";
        bossText.text = "";
        str = "";

        talk = new string[] { "첫번째 스크립트", "두번째 스크립트", "세번째 스크립트" , "네번째 스크립트" , "다섯번째 스크립트" ,
        "여섯번째 스크립트","일곱번째 스크립트","여덟번째 스크립트","아홉번째 스크립트","열번째 스크립트"};

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(HunterIn());
    }

    void Update()
    {
        if (textCount <= 0)
        {
            GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeOut", true);
            StartCoroutine(NextStage());
        }

        if (countOn)
        {
            backgroundPanel.SetActive(true);

            if (Input.anyKeyDown)
            {
                textCount--;
                typing = false;
            }
        }
        else
        {
            backgroundPanel.SetActive(false);
        }

        if (!typing)
        {
            switch (textCount)
            {
                case 10:
                    player = true;
                    boss = false;
                    str = talk[0];
                    StartCoroutine(TypingText(str));
                    break;
                case 9:
                    player = false;
                    boss = true;
                    str = talk[1];
                    StartCoroutine(TypingText(str));
                    break;
                case 8:
                    player = true;
                    boss = false;
                    str = talk[2];
                    StartCoroutine(TypingText(str));
                    break;
                case 7:
                    player = false;
                    boss = true;
                    str = talk[3];
                    StartCoroutine(TypingText(str));
                    break;
                case 6:
                    player = true;
                    boss = false;
                    str = talk[4];
                    StartCoroutine(TypingText(str));
                    break;
                case 5:
                    player = false;
                    boss = true;
                    str = talk[5];
                    StartCoroutine(TypingText(str));
                    break;
                case 4:
                    player = true;
                    boss = false;
                    str = talk[6];
                    StartCoroutine(TypingText(str));
                    break;
                case 3:
                    player = false;
                    boss = true;
                    str = talk[7];
                    StartCoroutine(TypingText(str));
                    break;
                case 2:
                    player = true;
                    boss = false;
                    str = talk[8];
                    StartCoroutine(TypingText(str));
                    break;
                case 1:
                    player = false;
                    boss = true;
                    str = talk[9];
                    StartCoroutine(TypingText(str));
                    break;
                default:
                    player = false;
                    boss = false;
                    str = "";
                    break;
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
    }

    IEnumerator TypingText(string s)
    {
        typing = true;
        playerText.text = "";
        bossText.text = "";

        foreach (char letter in s.ToCharArray())
        {
            if (player)
            {
                playerText.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
            else if (boss)
            {
                bossText.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }


    IEnumerator HunterIn()
    {
        yield return new WaitForSeconds(2.0f);
        smoke.SetActive(true);

        yield return new WaitForSeconds(0.4f);
        hunter.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        countOn = true;
    }

    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage01");
    }
}

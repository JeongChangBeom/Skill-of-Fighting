using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage01Before : MonoBehaviour
{
    public int textCount = 0;
    public int maxCount = 10;

    public GameObject hunter;
    public GameObject smoke;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public Text playerText;
    public Text bossText;

    public string[] talk = {
        "첫번째 스크립트", "두번째 스크립트", "세번째 스크립트" , "네번째 스크립트" , "다섯번째 스크립트" ,
        "여섯번째 스크립트","일곱번째 스크립트","여덟번째 스크립트","아홉번째 스크립트","열번째 스크립트"
    };

    private bool player = false;
    private bool boss = false;
    private bool countOn = false;
    private bool typing = false;

    private Coroutine typingCoroutine;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(HunterIn());
    }

    void Update()
    {
        if (textCount >= maxCount)
        {
            GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeOut", true);
            StartCoroutine(NextStage());
            return;
        }

        backgroundPanel.SetActive(countOn);

        if (countOn && Input.anyKeyDown && !typing)
        {
            textCount++;
            typingCoroutine = StartCoroutine(TypingText(talk[textCount]));
        }

        if (textCount % 2 == 0)
        {
            player = true;
            boss = false;
        }
        else
        {
            player = false;
            boss = true;
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

        foreach (char letter in s)
        {
            if (player)
            {
                playerText.text += letter;
            }
            else if (boss)
            {
                bossText.text += letter;
            }
            yield return new WaitForSeconds(0.1f);
        }
        typing = false;
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

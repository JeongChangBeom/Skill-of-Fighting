using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage03Before : MonoBehaviour
{
    public int textCount = 0;
    public int maxCount = 9;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public GameObject guardian;
    public GameObject ground1;
    public GameObject ground2;

    public Sprite Immortal_redeyes;

    public Text playerText;
    public Text bossText;

    string[] talk;

    private bool player = false;
    private bool boss = false;

    private bool countOn = false;
    private bool typing = false;

    private bool isTypingRuning = false;
    private bool redeyes = false;

    private int textAmount = 0;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        talk = new string[] { "흠 누구인가?",
                              "당신이 이모탈인가요?",
                              "그래 내가 이모탈이다." ,
                              "왜 저의 스승을 죽이라는 명령을 내렸습니까?" ,
                              "하하하\n나 이모탈은 너희 같이 이제는 쓸모없는 무기들을 이 세상에서 배제하려한다.\n그 첫번째 수순이 그였을 뿐이야.",
                              "대체 왜...",
                              "그야 너무 이상하지 않는가?\n사실상 너희 전통무기들은 이젠 필요없는 존재들 아닌가?\n전통성이라는 이유 하나만으로 너무 특별한 대접을 받고 있다 생각하지 않는가?",
                              "그게 무슨...",
                              "나는 그런 너희들이 싫었다.\n증오한다.\n섬멸한다.",
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
                player = false;
                boss = true;
            }
            else if ((textCount % 2 != 0 || textCount != 0) && textCount < maxCount)
            {
                player = true;
                boss = false;
            }
            else
            {
                player = false;
                boss = false;
            }

            if (textCount < maxCount && countOn)
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

        if(textCount == 8)
        {
            GameObject.Find("Canvas").transform.Find("BackGroundPanel/BossText/CharacterImage").GetComponent<Image>().sprite = Immortal_redeyes;
            if (!redeyes)
            {
                redeyes = true;
                SoundManager.instance.RaserBefore_Sound();
            }
        }
    }

    IEnumerator TypingText(string s)
    {
        typing = true;
        isTypingRuning = true;
        playerText.text = "";
        bossText.text = "";
        textAmount = 0;

        foreach (char letter in s.ToCharArray())
        {
            if(textAmount == 0 || textAmount % 3 == 0)
            {
                SoundManager.instance.Key_Sound();
            }
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
            textAmount++;
        }

        isTypingRuning = false;
    }
    IEnumerator ScenarioOn()
    {
        yield return new WaitForSeconds(2.0f);
        ground1.GetComponent<BoxCollider2D>().enabled = false;
        ground2.GetComponent<BoxCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        guardian.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 32f, ForceMode2D.Impulse);
        guardian.GetComponent<Animator>().SetBool("isJump", true);

        yield return new WaitForSeconds(0.5f);
        ground1.GetComponent<BoxCollider2D>().enabled = true;
        ground2.GetComponent<BoxCollider2D>().enabled = true;
        guardian.GetComponent<Animator>().SetBool("isJump", false);

        yield return new WaitForSeconds(1.5f);
        guardian.transform.Find("ExclamationMark").gameObject.SetActive(true);
        SoundManager.instance.ExclamationMarkOn_Sound();

        yield return new WaitForSeconds(1.0f);
        guardian.transform.Find("ExclamationMark").gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);
        countOn = true;

    }
    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage03");
    }
}
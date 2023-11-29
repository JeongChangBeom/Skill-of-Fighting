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

    private int textAmount = 0;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        talk = new string[] { "헌터씨 여기서 뭐하시나요?",
                              "! 뭐.... 	뭐야 너",
                              "??" ,
                              "너 다 알고 왔지?" ,
                              "혹시?" ,
                              "맞다! 내가 너의 스승의 위치를 현대무기들한테 알려줬지!",
                              "예?",
                              "??? 알고 온거 아니었나?",
                              "아니요?",
                              "...  전투다!"};

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
            if((textCount % 2 == 0 || textCount == 0) && textCount < maxCount)
            {
                player = true;
                boss = false;
            }
            else if((textCount % 2 != 0 || textCount != 0) && textCount < maxCount)
            {
                player = false;
                boss = true;
            }
            else
            {
                player = false;
                boss = false;
            }

            if(textCount < maxCount && countOn)
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

        if (guardian.transform.position.x >= -5f && guardianMove)
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
        textAmount = 0;

        foreach (char letter in s.ToCharArray())
        {
            if (textAmount == 0 || textAmount % 3 == 0)
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
        guardianMove = true;
        guardian.GetComponent<Animator>().SetBool("isMove",true);

        yield return new WaitForSeconds(2.0f);
        GameObject.Find("Boss_Hunter").transform.Find("ExclamationMark").gameObject.SetActive(true);
        SoundManager.instance.ExclamationMarkOn_Sound();

        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Boss_Hunter").transform.Find("ExclamationMark").gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        countOn = true;
    }
    IEnumerator NextStage()
    { 
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage01");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage02Before : MonoBehaviour
{
    public int textCount = 0;
    public int maxCount = 10;

    public GameObject gunner;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public Text playerText;
    public Text bossText;

    string[] talk;

    private bool player = false;
    private bool boss = false;

    private bool countOn = false;
    private bool typing = false;

    private bool isTypingRuning = false;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        talk = new string[] { "ù��° ��ũ��Ʈ", "�ι�° ��ũ��Ʈ", "����° ��ũ��Ʈ" , "�׹�° ��ũ��Ʈ" , "�ټ���° ��ũ��Ʈ" ,
        "������° ��ũ��Ʈ","�ϰ���° ��ũ��Ʈ","������° ��ũ��Ʈ","��ȩ��° ��ũ��Ʈ","����° ��ũ��Ʈ"};

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(GunnerIn());
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
                yield return new WaitForSeconds(0.1f);
            }
            else if (boss)
            {
                bossText.text += letter;
                yield return new WaitForSeconds(0.1f);
            }
        }

        isTypingRuning = false;
    }
    IEnumerator GunnerIn()
    {
        yield return new WaitForSeconds(3.4f);
        countOn = true;
    }
    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage02");
    }
}
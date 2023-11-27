using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage02After : MonoBehaviour
{
    public int textCount = 0;
    public int maxCount = 11;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public GameObject guardian;
    public GameObject gunner;
    public GameObject boom;
    public GameObject eff1;
    public GameObject eff2;

    public Text playerText;
    public Text bossText;

    string[] talk;

    private bool guardianMove = false;

    private bool player = false;
    private bool boss = false;

    private bool countOn = false;
    private bool typing = false;

    private bool isTypingRuning = false;
    private bool isCoroutine = false;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        talk = new string[] { "������. �����....",
                              "...?",
                              "�� ���� ���������̴�. \n�ʿ��� ���� ���� ��� ������ �ǵ��� �Դ�." ,
                              "???" ,
                              "�ǹ��̰����� �ϴ� ���� ����\n���� ������ ���� ������ ���� �´�.\n������ ���� �����ؼ� �׸� ���̰� ���ڴ� ���� �ִ�.",
                              "�װ� ��������?",
                              "�̸�Ż...\n���빫�� ���忡�� ���̶� �Ҹ��� ���̴�.\n�װ� ���� ������Ű�� ���� ������ ���̶� ���Ͽ���.",
                              "!!",
                              "�̸�Ż�� �ִ� ��ġ�� ��� ��θ� �˷��ְڴ�.\nũ��...\n�ð��� ����. ���� �� �ٽ� �������·� ���� ���̴�.\n�̸�Ż ���ڰ� �ִ� ���� ���� �� ���� ���� �ο��� �����Ŷ�.",
                              "�׷� ����� ����...",
                              "�׷� �ð� ���� � ���Ŷ�"
                            };

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(ScenarioOn());
    }

    void Update()
    {
        if (textCount >= maxCount && !isCoroutine)
        {
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
            guardian.transform.position = Vector3.MoveTowards(guardian.transform.position, new Vector3(40f, guardian.transform.position.y, guardian.transform.position.z), 10f * Time.deltaTime);
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
        yield return new WaitForSeconds(2.0f);
        countOn = true;

    }
    IEnumerator NextStage()
    {
        isCoroutine = true;
        countOn = false;
        guardianMove = true;
        guardian.GetComponent<Animator>().SetBool("isMove", true);

        yield return new WaitForSeconds(4.0f);
        boom.SetActive(true);

        yield return new WaitForSeconds(2.5f);
        boom.SetActive(false);
        gunner.SetActive(false);
        eff1.SetActive(true);
        eff2.SetActive(true);

        yield return new WaitForSeconds(3.0f);
        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeOut", true);

        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Stage03_Before");
    }
}
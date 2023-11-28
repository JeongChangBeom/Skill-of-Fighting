using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage01After : MonoBehaviour
{
    public int textCount = 0;
    public int maxCount = 8;

    public GameObject backgroundPanel;
    public GameObject playerPanel;
    public GameObject bossPanel;

    public GameObject guardian;

    public Text playerText;
    public Text bossText;

    string[] talk;

    private bool guardianExit = false;

    private bool player = false;
    private bool boss = false;

    private bool countOn = false;
    private bool typing = false;
    private bool shakeOn = false;

    private bool isTypingRuning = false;

    void Start()
    {
        playerText.text = "";
        bossText.text = "";

        talk = new string[] { "��� �׷� ���� �ϽŰ�����?",
                              "����.... ���빫���̱� �Ⱦ���!",
                              "��?" ,
                              "���� ���� ������ ����ϴ��� \n���� ���빫���� ������ �̸��� ��ġ�� �˷��شٸ� \n���� ���빫�⿡ ���ְڴٰ�" ,
                              "��°�� ���� ���Ⱑ �ǰ� �����̴� �ſ���?" ,
                              "�ʹ� ���ڴٸ����� �ʳ� ���ô븦 ��ư��� �;��� ������.",
                              "....",
                              "�ƹ�ư! ���� ������ ���ΰ��� �ųʶ�� �ι��̴�. \n�״� ���� Ȳ������ �ִ�!",
                            };

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(ScenarioOn());
    }

    void Update()
    {
        if (textCount >= maxCount)
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

        if (guardianExit)
        {
            guardian.transform.position = Vector3.MoveTowards(guardian.transform.position, new Vector3(40f, guardian.transform.position.y, guardian.transform.position.z), 10f * Time.deltaTime);
        }

        if(textCount == 1 && !shakeOn)
        {
            StartCoroutine(Shake());
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
        guardianExit = true;
        countOn = false;
        guardian.GetComponent<Animator>().SetBool("isMove", true);

        yield return new WaitForSeconds(3.0f);

        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeOut", true);

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage02_Before");
    }

    public IEnumerator Shake()
    {
        shakeOn = true;
        Transform cam = GameObject.FindWithTag("Player").transform.Find("Main Camera").gameObject.transform;

        Vector3 originPosition = cam.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < 0.5)
        {
            Vector3 randomPoint = originPosition + Random.insideUnitSphere * 20.0f;
            cam.localPosition = Vector3.Lerp(cam.localPosition, randomPoint, Time.deltaTime * 8.0f);

            yield return null;

            elapsedTime += Time.deltaTime;
        }
        cam.localPosition = originPosition;
    }
}
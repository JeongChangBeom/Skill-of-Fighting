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

        talk = new string[] { "으으윽. 가디언....",
                              "...?",
                              "난 지금 세뇌상태이다. \n너와의 전투 덕에 잠시 정신이 되돌아 왔다." ,
                              "???" ,
                              "의문이겠지만 일단 말을 들어라\n너의 스승을 죽인 범인은 내가 맞다.\n하지만 나를 세뇌해서 그를 죽이게 한자는 따로 있다.",
                              "그게 누군가요?",
                              "이모탈...\n현대무기 입장에서 신이라 불리는 자이다.\n그가 나를 세뇌시키고 너의 스승을 죽이라 명하였다.",
                              "!!",
                              "이모탈이 있는 위치와 비밀 통로를 알려주겠다.\n크흑...\n시간이 없다. 나는 곧 다시 세뇌상태로 변할 것이다.\n이모탈 그자가 있는 곳에 가서 이 쓸모 없는 싸움을 끝내거라.",
                              "그럼 당신은 어찌...",
                              "그럴 시간 없다 어서 가거라"
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
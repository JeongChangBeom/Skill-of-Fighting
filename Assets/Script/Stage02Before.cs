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
    private bool shakeOn = false;

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

        talk = new string[] { "����� �ųʱ���. ���´��� �� ���� ������?",
                              "...",
                              "��!" ,
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

        if (guardianMove)
        {
            guardian.transform.position = Vector3.MoveTowards(guardian.transform.position, new Vector3(-9f, guardian.transform.position.y, guardian.transform.position.z), 30f * Time.deltaTime);
        }

        if (gunnerMove)
        {
            gunner.transform.position = Vector3.MoveTowards(gunner.transform.position, new Vector3(gunner.transform.position.x, -6.2f, guardian.transform.position.z), 40f * Time.deltaTime);
        }

        if (textCount == 2 && !shakeOn)
        {
            StartCoroutine(Shake());
            SoundManager.instance.Shake_Sound();
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
        yield return new WaitForSeconds(3.0f);
        Instantiate(bullet_parry, bulletGun.transform.position, bulletGun.transform.rotation);
        SoundManager.instance.SnipeShoot_Sound();

        yield return new WaitForSeconds(0.1f);
        guardianMove = true;
        guardian.GetComponent<Animator>().SetBool("isBackStep", true);
        SoundManager.instance.GuardianBackStep_Sound();

        yield return new WaitForSeconds(0.1f);
        guardianMove = false;
        guardian.GetComponent<Animator>().SetBool("isBackStep", false);

        yield return new WaitForSeconds(0.5f);
        gunnerMove = true;

        yield return new WaitForSeconds(2.0f);
        countOn = true;

    }
    public IEnumerator Shake()
    {
        shakeOn = true;
        Transform cam = GameObject.Find("Main Camera").gameObject.transform;

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

    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage02");
    }
}
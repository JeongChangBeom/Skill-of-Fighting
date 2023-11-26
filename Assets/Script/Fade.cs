using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private Animator anim;
    private BossStatus bossStatus;

    void Start()
    {
        anim = GetComponent<Animator>();
        bossStatus = GameObject.FindWithTag("boss").GetComponent<BossStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.GameStart)
        {
            anim.SetBool("FadeIn", true);
        }

        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            if (GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().mainHP <= 0)
            {
                Invoke("FadeOut", 2.0f);

                StartCoroutine(NextStage());
            }
        }
        else
        {
            if (bossStatus.bossHP <= 0)
            {
                Invoke("FadeOut", 2.0f);

                StartCoroutine(NextStage());
            }
        }
    }

    private void FadeOut()
    {
        anim.SetBool("FadeOut", true);
    }

    IEnumerator NextStage()
    {
        yield return new WaitForSecondsRealtime(4f);

        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "Stage01")
        {
            SceneManager.LoadScene("Stage01_After");
        }

        if (SceneManager.GetActiveScene().name == "Stage02")
        {
            SceneManager.LoadScene("Stage02_After");
        }

        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            SceneManager.LoadScene("Stage03_After");
        }
    }
}

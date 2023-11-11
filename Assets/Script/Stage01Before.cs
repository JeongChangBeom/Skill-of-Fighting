using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage01Before : MonoBehaviour
{
    int textCount = 10;
    void Start()
    {
        GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeIn", true);

        StartCoroutine(HunterIn());
    }

    void Update()
    {
        print(textCount);

        if(textCount <= 0)
        {
            GameObject.Find("Canvas").GetComponent<Animator>().SetBool("FadeOut", true);
            StartCoroutine(NextStage());
        }

        if (Input.anyKeyDown)
        {
            textCount--;
        }
    }

    IEnumerator HunterIn()
    {
        yield return new WaitForSeconds(1f);
        GameObject.Find("Hunter_smoke").gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        GameObject.FindWithTag("boss").gameObject.SetActive(true);
    }

   IEnumerator NextStage()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Stage01");
    }
}

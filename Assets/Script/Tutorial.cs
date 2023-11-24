using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    void Start()
    {
        if (!GameManager.Dotutorial)
        {
            StartCoroutine(TutorialOn());
        }
    }

    IEnumerator TutorialOn()
    {
        yield return new WaitForSeconds(2.001f);
        GameManager.instance.GameStart = false;
        Time.timeScale = 0;
        GameObject.Find("Canvas").transform.Find("Tutorial").gameObject.SetActive(true);
    }
}

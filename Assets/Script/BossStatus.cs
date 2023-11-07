using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStatus : MonoBehaviour
{
    public Vector3 bossPosition;

    public float bossHP = 3;

    private GameObject clearText;


    private void Start()
    {
        clearText = GameObject.Find("Canvas").transform.Find("ClearText").gameObject;
    }
    private void Update()
    {
        if (bossHP <= 0)
        {
            BossDie();
        }

        BossPosition();
    }

    public void BossDie()
    {
        clearText.SetActive(true);

        Time.timeScale = 0;

        StartCoroutine(NextStage());
    }
    
    public void BossPosition()
    {
        bossPosition = transform.position;
    }


    IEnumerator NextStage()
    {
        yield return new WaitForSecondsRealtime(2f);

        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().name == "Stage01")
        {
            SceneManager.LoadScene("Stage02");
        }

        if (SceneManager.GetActiveScene().name == "Stage02")
        {
            SceneManager.LoadScene("Stage03");
        }

        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            SceneManager.LoadScene("Ending");
        }
    }
}

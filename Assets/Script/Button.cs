using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void MainMenu_Start()
    {
        //SceneManager.LoadScene("Scenario");
        SceneManager.LoadScene("Stage01_Before");
    }

    public void MainMenu_Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }

    public void MainMenu_OptionON()
    {
        GameObject.Find("Canvas").transform.Find("Option").gameObject.SetActive(true);
    }

    public void MainMenu_OptionOFF()
    {
        GameObject.Find("Canvas").transform.Find("Option").gameObject.SetActive(false);
    }

    public void Stage01_TutorialOff()
    {
        GameObject.Find("Canvas").transform.Find("Tutorial").gameObject.SetActive(false);
        GameManager.Dotutorial = true;
        GameManager.instance.GameStart = true;
        Time.timeScale = 1;
    }

    public void OnMouseEnter()
    {
        SoundManager.instance.ButtonMouseOn_Sound();
        if (gameObject.name == "OptionButton")
        {
            transform.localScale = transform.localScale + new Vector3(0.1f, 0.5f, 0.5f);
        }
        else
        {
            transform.localScale = transform.localScale + new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void OnMouseExit()
    {
        if(gameObject.name == "OptionButton")
        {
            transform.localScale = new Vector3(0.9f, 4.0f, 3.0f);
        }
        else
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }
    }
}

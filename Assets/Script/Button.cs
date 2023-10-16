using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void MainMenu_Start()
    {
        //SceneManager.LoadScene("Scenario");
        SceneManager.LoadScene("Test");
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
}

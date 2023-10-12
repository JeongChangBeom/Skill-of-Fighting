using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void MainMenu_Start()
    {
        SceneManager.LoadScene("Scenario");
    }
    
    public void MainMenu_Exit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
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

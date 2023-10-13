using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    private GameObject gameoverText;

    public bool isDead = false;
    void Start()
    {
        gameoverText = GameObject.Find("Canvas").transform.Find("GameOverText").gameObject;
    }
    
    public void Die()
    {
        isDead = true;

        gameoverText.SetActive(true);

        Time.timeScale = 0;
    }
}

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
        if (Input.GetKeyDown(KeyCode.P))
        {
            bossHP--;
        }

        if (bossHP <= 0)
        {
            BossDie();
        }

        BossPosition();
    }

    public void BossDie()
    {
        clearText.SetActive(true);

    }
    
    public void BossPosition()
    {
        bossPosition = transform.position;
    }
}

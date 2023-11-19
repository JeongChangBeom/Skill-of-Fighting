using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImmortalStatus : MonoBehaviour
{
    public Vector3 bossPosition;

    public float mainHP = 3;
    public float leftarmHP = 2;
    public float rightarmHP = 2;

    private GameObject clearText;


    private void Start()
    {
        clearText = GameObject.Find("Canvas").transform.Find("ClearText").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            mainHP--;
        }

        if (mainHP <= 0)
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

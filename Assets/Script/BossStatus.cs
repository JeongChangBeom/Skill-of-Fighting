using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ParryAttack"))
        {
            bossHP--;
        }
    }

    public void BossDie()
    {
        clearText.SetActive(true);

        Time.timeScale = 0;
    }
    
    public void BossPosition()
    {
        bossPosition = transform.position;
    }
}

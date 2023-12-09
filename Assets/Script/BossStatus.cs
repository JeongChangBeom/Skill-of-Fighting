using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossStatus : MonoBehaviour
{
    public Vector3 bossPosition;

    public float bossHP = 3;

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
        GameObject.FindWithTag("boss").GetComponent<Animator>().SetBool("isDie", true);
        GameObject.Find("Player").gameObject.layer = 8;
    }
    
    public void BossPosition()
    {
        bossPosition = transform.position;
    }
}

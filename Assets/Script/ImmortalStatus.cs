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

    private GameObject leftArm;
    private GameObject rightArm;

    private GameObject clearText;

    public bool immortalDestroy = false;

    private void Start()
    {
        clearText = GameObject.Find("Canvas").transform.Find("ClearText").gameObject;
        leftArm = GameObject.FindWithTag("boss").transform.Find("Immortal_Arm_Left").gameObject;
        rightArm = GameObject.FindWithTag("boss").transform.Find("Immortal_Arm_Right").gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (leftarmHP > 0 && rightarmHP > 0)
            {
                leftarmHP--;
            }
            else if(leftarmHP <= 0 && rightarmHP > 0)
            {
                rightarmHP--;
            }
            else if(leftarmHP > 0 && rightarmHP <= 0)
            {
                leftarmHP--;
            }
            else
            {
                mainHP--;
            }
        }

        if(leftarmHP <= 0)
        {
            leftArm.SetActive(false);
        }
        else
        {
            leftArm.SetActive(true);
        }

        if (rightarmHP <= 0)
        {
            rightArm.SetActive(false);
        }
        else
        {
            rightArm.SetActive(true);
        }

        if(leftarmHP <= 0 && rightarmHP <= 0)
        {
            GameObject.FindWithTag("boss").GetComponent<PolygonCollider2D>().enabled = true;
        }
        else
        {
            GameObject.FindWithTag("boss").GetComponent<PolygonCollider2D>().enabled = false;
        }

        if (mainHP <= 0)
        {
            StartCoroutine(BossDie());
        }

        BossPosition();
    }

    IEnumerator BossDie()
    {
        GameObject.Find("Player").gameObject.layer = 8;

        yield return new WaitForSeconds(2.0f);
        GameObject.Find("Main Camera").transform.Find("Immortal_Destroy01").GetComponent<Animator>().SetBool("isDie", true);

        yield return new WaitForSeconds(0.5f);
        GameObject.FindWithTag("boss").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.FindWithTag("boss").transform.Find("Missile_Ready").GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1.0f);
        immortalDestroy = true;
    }

    public void BossPosition()
    {
        bossPosition = transform.position;
    }
}

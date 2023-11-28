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

    public bool immortalDestroy = false;

    private bool immortaldie = false;

    CameraController cameracontroller;

    private void Start()
    {
        leftArm = GameObject.FindWithTag("boss").transform.Find("Immortal_Arm_Left").gameObject;
        rightArm = GameObject.FindWithTag("boss").transform.Find("Immortal_Arm_Right").gameObject;
        cameracontroller = GameObject.Find("Main Camera").GetComponent<CameraController>();
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
            leftArm.GetComponent<Animator>().SetBool("isDestroy", true);
            Invoke("LeftArmDestroy", 2.0f);
        }
        else
        {
            leftArm.SetActive(true);
        }

        if (rightarmHP <= 0)
        {
            rightArm.GetComponent<Animator>().SetBool("isDestroy", true);
            Invoke("RightArmDestroy", 2.0f);
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

        if (mainHP <= 0 && !immortaldie)
        {
            StartCoroutine(BossDie());
        }

        BossPosition();
    }

    private void LeftArmDestroy()
    {
        leftArm.SetActive(true);
    }

    private void RightArmDestroy()
    {
        rightArm.SetActive(false);
    }

    IEnumerator BossDie()
    {
        immortaldie = true;

        GameObject.Find("Player").gameObject.layer = 8;

        cameracontroller.StartCoroutine(cameracontroller.Shake());

        yield return new WaitForSeconds(0.5f);
        cameracontroller.StartCoroutine(cameracontroller.Shake());

        yield return new WaitForSeconds(0.5f);
        cameracontroller.StartCoroutine(cameracontroller.Shake());

        yield return new WaitForSeconds(0.5f);
        cameracontroller.StartCoroutine(cameracontroller.Shake());
        GameObject.Find("Main Camera").transform.Find("Immortal_Destroy01").GetComponent<Animator>().SetBool("isDie", true);

        yield return new WaitForSeconds(0.5f);
        cameracontroller.StartCoroutine(cameracontroller.Shake());

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

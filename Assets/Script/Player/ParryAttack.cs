using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryAttack : MonoBehaviour
{
    public float rotateSpeed = 200f;
    public bool reverse = false;

    public bool parryattackOn = false;

    public bool parryattackStart = false;

    private PlayerController playercontroller;

    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (parryattackOn && playercontroller.dirPos.x > 0)
        {
            playercontroller.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            ParryAttackLeft();
        }
        else if(parryattackOn && playercontroller.dirPos.x <= 0)
        {
            playercontroller.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            ParryAttackRight();
        }
    }
    private void ParryAttackLeft()
    {
        StartCoroutine(ParryAttackOff());

        if (!parryattackStart)
        {
            transform.localEulerAngles = new Vector3(0, 0, -45);
            parryattackStart = true;
        }

        if (transform.rotation.eulerAngles.z < 316 && transform.rotation.eulerAngles.z > 314)
        {
            reverse = false;
        }
        else if (transform.rotation.eulerAngles.z < 46 && transform.rotation.eulerAngles.z > 44)
        {
            reverse = true;
        }

        if (!reverse)
        {
            transform.Rotate(0, 0, -Time.unscaledDeltaTime * rotateSpeed, Space.Self);
        }
        else
        {
            transform.Rotate(0, 0, Time.unscaledDeltaTime * rotateSpeed, Space.Self);
        }
    }


    private void ParryAttackRight()
    {
        StartCoroutine(ParryAttackOff());

        if (!parryattackStart)
        {
            transform.localEulerAngles = new Vector3(0, 0, -45);
            parryattackStart = true;
        }

        if (transform.rotation.eulerAngles.z < 316 && transform.rotation.eulerAngles.z > 314)
        {
            reverse = false;
        }
        else if (transform.rotation.eulerAngles.z < 46 && transform.rotation.eulerAngles.z > 44)
        {
            reverse = true;
        }

        if (!reverse)
        {
            transform.Rotate(0, 0, Time.unscaledDeltaTime * rotateSpeed, Space.Self);
        }
        else
        {
            transform.Rotate(0, 0, -Time.unscaledDeltaTime * rotateSpeed, Space.Self);
        }
    }


    IEnumerator ParryAttackOff()
    {
        yield return new WaitForSecondsRealtime(0.8f);

        Time.timeScale = 1;
        GameObject.Find("Player").transform.Find("Parry_Attack").gameObject.SetActive(false);
        parryattackOn = false;
        parryattackStart = false;
    }
}
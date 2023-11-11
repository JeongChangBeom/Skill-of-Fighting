using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    private ParryAttack parryAttack;
    private GameObject player;
    private void Start()
    {
        parryAttack = GameObject.Find("Player").transform.Find("Parry_Attack").GetComponent<ParryAttack>();
        player = GameObject.Find("Player").gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("bossAttack"))
        {
            print("Guard");

            player.gameObject.layer = 8;
            Invoke("AvoidOff", 0.5f);
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("bossAttack_parry"))
        {
            print("Parry");

            GameObject.Find("Player").GetComponent<Animator>().SetBool("isParry", true);
            GameObject.Find("Player").transform.Find("Parry_Attack").gameObject.SetActive(true);
            parryAttack.parryattackOn = true;
            Destroy(collider.gameObject);
            GameObject.Find("Player").transform.Find("Parry_Range").gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    private void AvoidOff()
    {
        player.gameObject.layer = 7;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_test : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 myPos;
    Vector3 newPos;

    public float speed = 0.003f;

    private ParryAttack parryAttack;

    private PlayerHp playerHp;

    void Start()
    {
        parryAttack = GameObject.Find("Player").transform.Find("Parry_Attack").GetComponent<ParryAttack>();

        playerHp = GameObject.Find("Player").GetComponent<PlayerHp>();

        targetPos = GameObject.Find("Player").transform.position;
        myPos = transform.position;

        newPos = (targetPos - myPos) * speed;

        Destroy(gameObject, 5f);
    }
    void FixedUpdate()
    {
        transform.position = transform.position + newPos;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("parry"))
        {
            print("Parry");
            GameObject.Find("Player").transform.Find("Parry_Attack").gameObject.SetActive(true);
            parryAttack.parryattackOn = true;
            Destroy(this.gameObject);
            GameObject.Find("Player").transform.Find("Parry_Range").gameObject.SetActive(false);
            Time.timeScale = 0;
        }

        if (collider.gameObject.CompareTag("Player"))
        {
            playerHp.Die();
            print("Damage");
            Destroy(this.gameObject);
        }
    }
}

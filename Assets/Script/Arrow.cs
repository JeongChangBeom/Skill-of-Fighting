using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float attackSpeed;
    private PlayerController playercontroller;

    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (playercontroller.dirPos.x <= 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * -attackSpeed, ForceMode2D.Impulse);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);
        }

        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerHp>().StartCoroutine(collider.gameObject.GetComponent<PlayerHp>().Die());
            Destroy(this.gameObject);
        }
    }
}

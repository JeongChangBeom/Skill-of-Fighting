using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPunch : MonoBehaviour
{
    public float attackSpeed;
    void Start()
    {
        if(this.gameObject.name == "Immortal_Arm_Side_Left(Clone)")
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);
        }
        else if (this.gameObject.name == "Immortal_Arm_Side_Right(Clone)")
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * -attackSpeed, ForceMode2D.Impulse);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackSpeed = 1000;
    public PlayerController playercontroller;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
        if(playercontroller.dirPos.x <= 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);
        }
        else if(playercontroller.dirPos.x > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * -attackSpeed, ForceMode2D.Impulse);
        }

        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("boss"))
        {
            collision.gameObject.GetComponent<BossStatus>().bossHP--;
            Destroy(this.gameObject);
        }
    }
}

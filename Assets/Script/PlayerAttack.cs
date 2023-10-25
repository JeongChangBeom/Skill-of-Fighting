using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackSpeed = 100;
    private PlayerController playercontroller;
    private BossHunter bossHunter;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        bossHunter = GameObject.FindWithTag("boss").GetComponent<BossHunter>();
        
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
            if(collision.gameObject.name == "Boss_Hunter")
            {
                if(collision.gameObject.GetComponent<BossStatus>().bossHP > 1)
                {
                    bossHunter.Pattern_Move();
                }
            }

            collision.gameObject.GetComponent<BossStatus>().bossHP--;
            Destroy(this.gameObject);
        }
    }
}

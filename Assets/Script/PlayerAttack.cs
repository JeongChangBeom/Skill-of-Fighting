using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public float attackSpeed = 100;
    private PlayerController playercontroller;
    private BossHunter bossHunter;

    private SpriteRenderer attackImg;
    public Sprite arrow;
    public Sprite bullet;

    void Start()
    {
        attackImg = GetComponent<SpriteRenderer>();

        if(SceneManager.GetActiveScene().name == "Stage01")
        {
            attackImg.sprite = arrow;
        }
        if (SceneManager.GetActiveScene().name == "Stage02")
        {
            attackImg.sprite = bullet;
        }

        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        bossHunter = GameObject.FindWithTag("boss").GetComponent<BossHunter>();


        if (SceneManager.GetActiveScene().name == "Stage01")
        {
            if (playercontroller.dirPos.x <= 0)
            {
                transform.localScale = new Vector3(-3, 1, 0);
                GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);
            }
            else if (playercontroller.dirPos.x > 0)
            {
                transform.localScale = new Vector3(3, 1, 0);
                GetComponent<Rigidbody2D>().AddForce(transform.right * -attackSpeed, ForceMode2D.Impulse);
            }
        }

        if (SceneManager.GetActiveScene().name == "Stage02")
        {
            if (playercontroller.dirPos.x <= 0)
            {
                transform.localScale = new Vector3(2, 1, 1);
                GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);
            }
            else if (playercontroller.dirPos.x > 0)
            {
                transform.localScale = new Vector3(-2, 1, 1);
                GetComponent<Rigidbody2D>().AddForce(transform.right * -attackSpeed, ForceMode2D.Impulse);
            }
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

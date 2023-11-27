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
    public Sprite razer;

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
        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            attackImg.sprite = razer;
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

        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            if (playercontroller.dir == 1)
            {
                transform.localScale = new Vector3(4, 1, 1);
                GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);
            }
            else if (playercontroller.dir == -1)
            {
                transform.localScale = new Vector3(-4, 1, 1);
                GetComponent<Rigidbody2D>().AddForce(transform.right * -attackSpeed, ForceMode2D.Impulse);
            }
        }


        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("boss") || collider.gameObject.CompareTag("ImmortalArm"))
        {
            if(collider.gameObject.name == "Boss_Hunter")
            {
                if(collider.gameObject.GetComponent<BossStatus>().bossHP > 1)
                {
                    bossHunter.StartCoroutine(bossHunter.Pattern_Move());
                }
            }
            if(SceneManager.GetActiveScene().name == "Stage03")
            {
                if (collider.gameObject.name == "Immortal_Arm_Left")
                {
                    if (GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().leftarmHP > 0)
                    {
                        GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().leftarmHP--;
                    }
                }

                if (collider.gameObject.name == "Immortal_Arm_Right")
                {
                    if (GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().rightarmHP > 0)
                    {
                        GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().rightarmHP--;
                    }
                }

                if(collider.gameObject.name == "Boss_Immortal")
                {
                    if(GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().leftarmHP <= 0 && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().rightarmHP <= 0)
                    {
                        GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().mainHP--;
                    }
                }
            }
            else
            {
                collider.gameObject.GetComponent<BossStatus>().bossHP--;
            }

            Destroy(this.gameObject);
        }
    }
}

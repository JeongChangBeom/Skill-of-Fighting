using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public int attackSpeed;
    private PlayerController playercontroller;
    public GameObject explosion;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        attackSpeed = Random.Range(5, 18);

        if (playercontroller.dirPos.x <= 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * -attackSpeed, ForceMode2D.Impulse);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Invoke("ExplosionBomb", 1.0f);
            Invoke("DestroyBomb", 2.0f);
        }
    }

    private void ExplosionBomb()
    {
        this.gameObject.SetActive(false);
        Instantiate(explosion, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
    }

    private void DestroyBomb()
    {
        Destroy(this.gameObject);
    }
}

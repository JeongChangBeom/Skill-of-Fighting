using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int attackSpeed = 30;
    public GameObject explosion;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * -attackSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            ExplosionMissile();
            Invoke("DestroyMissile", 0.1f);
        }
    }

    private void ExplosionMissile()
    {
        this.gameObject.SetActive(false);
        Instantiate(explosion, transform.position - new Vector3(0, 5, 0), Quaternion.Euler(new Vector3(0, 0, 0)));
        SoundManager.instance.Missile_Sound();
    }

    private void DestroyMissile()
    {
        Destroy(this.gameObject);
    }
}

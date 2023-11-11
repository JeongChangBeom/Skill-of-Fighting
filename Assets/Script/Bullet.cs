using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float attackSpeed;
    private ParticleSystem ps;
    private PlayerController playercontroller;
    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();

        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        GetComponent<Rigidbody2D>().AddForce(transform.right * attackSpeed, ForceMode2D.Impulse);

        Destroy(gameObject, 5);
    }

    void Update()
    {
        if (ps != null)
        {
            ParticleSystem.MainModule main = ps.main;
            if (main.startRotation.mode == ParticleSystemCurveMode.Constant)
            {
                main.startRotation = -transform.eulerAngles.z * Mathf.Deg2Rad;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerHp>().Die();
            Destroy(this.gameObject);
        }
    }
}

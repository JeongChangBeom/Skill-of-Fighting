using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashShockWave : MonoBehaviour
{
    private void Start()
    {
        Invoke("CollisionON", 0.2f);

        Invoke("CollisionOff", 0.4f);
    }

    private void CollisionON()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void CollisionOff()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerHp>().Die();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    private void Start()
    {
        Invoke("CollisionOff", 0.2f);
    }
    
    private void CollisionOff()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.GetComponent<PlayerHp>().Die();
        }
    }
}

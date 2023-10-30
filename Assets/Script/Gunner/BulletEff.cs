using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEff : MonoBehaviour
{
    private PlayerController playercontroller;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (playercontroller.dirPos.x > 0)
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
        }
    }
}

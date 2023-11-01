using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowEff : MonoBehaviour
{
    private PlayerController playercontroller;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (playercontroller.dirPos.x > 0)
        {
            transform.localScale = new Vector3(-5f, 5f, 5f);
        }
    }
}

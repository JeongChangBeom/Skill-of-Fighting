using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHunter : MonoBehaviour
{
    private PlayerController playercontroller;
    void Start()
    {
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playercontroller.dirPos.x <= 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (playercontroller.dirPos.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}

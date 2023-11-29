using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioGunner : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isJump", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            SoundManager.instance.Land_Sound();
            anim.SetBool("isJump", false);
        }
    }
}

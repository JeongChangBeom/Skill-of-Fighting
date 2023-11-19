using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletGun : MonoBehaviour
{
    private Transform target;
    private PlayerController playercontroller;

    private void Start()
    {
        target = GameObject.FindWithTag("Player").gameObject.transform;
        playercontroller = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }
    void Update()
    {

        if(playercontroller.dirPos.x <= 0 && SceneManager.GetActiveScene().name != "Stage03")
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(playercontroller.dirPos.x > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        Vector3 dir = target.transform.position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}

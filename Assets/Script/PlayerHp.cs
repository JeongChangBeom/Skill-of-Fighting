using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHp : MonoBehaviour
{
    private Animator anim;

    public bool isDead = false;
    void Start()
    {
        anim = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }
    
    public IEnumerator Die()
    {
        GameObject.FindWithTag("Player").transform.Find("Parry_Range").gameObject.SetActive(false);

        GameObject.Find("Canvas").transform.Find("BackStep_Image").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("Parry_Image").gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Stage03")
        {
            GameObject.Find("Canvas").transform.Find("BossHp").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("LeftArmHp").gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.Find("RightArmHp").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("BossHp").gameObject.SetActive(false);
        }
        GameObject.Find("Canvas").transform.Find("PauseText").gameObject.SetActive(false);
        GameObject.Find("Canvas").transform.Find("BossName").gameObject.SetActive(false);

        GameObject.Find("Map").transform.Find("Die").gameObject.SetActive(true);

        isDead = true;

        Time.timeScale = 0;

        GameObject.Find("Player").transform.Find("Spotlight").gameObject.SetActive(true);
        anim.SetBool("isDie", true);

        yield return new WaitForSecondsRealtime(2.0f);
        GameObject.Find("Canvas").transform.Find("GameOverText").gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Vector3 playerPosition;

    public Vector3 dirPos;

    private bool moveOn = true;
    private bool backstepOn = true;
    private bool parryOn = true;
    public bool isJump = true;

    Rigidbody2D rb;

    public float moveSpeed = 5.0f;
    public float MaxmoveSpeed = 10.0f;

    public float jumpPower = 20.0f;
    public float backstepPower = 100.0f;

    public float parryCooldown = 3.0f;
    public float backstepCooldown = 3.0f;

    public int dir = 1;

    private ParryAttack parryattack;

    public GameObject playerAttack_Prefab;

    private PlayerHp playerHp;
    private BossStatus bossStatus;

    private Animator anim;

    public GameObject parryEff;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parryattack = GameObject.FindWithTag("Player").transform.Find("Parry_Attack").GetComponent<ParryAttack>();
        anim = GetComponent<Animator>();
        playerHp = GetComponent<PlayerHp>();
        bossStatus = GameObject.FindWithTag("boss").GetComponent<BossStatus>();
    }

    void Update()
    {
        if (GameManager.instance.GameStart)
        {
            if (SceneManager.GetActiveScene().name == "Stage03")
            {
                playerPosition = transform.position;
                dirPos = playerPosition - GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().bossPosition;
            }
            else
            {
                playerPosition = transform.position;
                dirPos = playerPosition - bossStatus.bossPosition;
            }

            if (SceneManager.GetActiveScene().name == "Stage03")
            {
                if (!playerHp.isDead && GameObject.FindWithTag("boss").GetComponent<ImmortalStatus>().mainHP > 0 && !GameManager.instance.isPause)
                {
                    Move();
                    Jump();
                    BackStep();
                    Parry();
                }
            }
            else
            {
                if (!playerHp.isDead && bossStatus.bossHP > 0 && !GameManager.instance.isPause)
                {
                    Move();
                    Jump();
                    BackStep();
                    Parry();
                }
            }
        }
    }

    //좌우이동
    private void Move()
    {
        int key = 0;

        if (Input.GetButtonUp("Horizontal") && moveOn && !parryattack.parryattackOn)
        {
            rb.velocity = new Vector2(rb.velocity.normalized.x * 0.1f, rb.velocity.y);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && moveOn && !parryattack.parryattackOn)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            key = -1;
            dir = -1;
            anim.SetBool("isMove", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow) && moveOn && !parryattack.parryattackOn)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            key = 1;
            dir = 1;
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        //현재 플레이어 속도
        float playerSpeed = Mathf.Abs(rb.velocity.x);

        //속도 제한
        if (playerSpeed < MaxmoveSpeed)
        {
            rb.AddForce(transform.right * key * moveSpeed);
        }
    }

    //점프
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !parryattack.parryattackOn)
        {
            if (!isJump)
            {
                SoundManager.instance.GuardianJump_Sound();
                anim.SetBool("isJump", true);
                isJump = true;
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
        else
        {
            anim.SetBool("isJump", false);
        }
    }

    private void BackStep()
    {
        if (!backstepOn)
        {
            backstepCooldown -= Time.deltaTime;
            if (backstepCooldown <= 0.0f)
            {
                backstepOn = true;
            }
        }
        else
        {
            backstepCooldown = 3.0f;
        }

        if (Input.GetKeyDown(KeyCode.S) && backstepOn && !parryattack.parryattackOn)
        {
            SoundManager.instance.GuardianBackStep_Sound();
            anim.SetBool("isBackStep", true);
            this.gameObject.layer = 8;
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            moveOn = false;
            backstepOn = false;
            Invoke("BackStepCoolDown", 3f);

            if (dir == -1)
            {
                rb.AddForce(Vector2.right * backstepPower, ForceMode2D.Impulse);
            }
            else if (dir == 1)
            {
                rb.AddForce(Vector2.left * backstepPower, ForceMode2D.Impulse);
            }
            Invoke("BackStepStop", 0.1f);

        }
    }

    private void Parry()
    {
        if (!parryOn)
        {
            parryCooldown -= Time.deltaTime;
            if (parryCooldown <= 0.0f)
            {
                parryOn = true;
            }
        }
        else
        {
            parryCooldown = 3.0f;
        }

        if (Input.GetKeyDown(KeyCode.A) && parryOn && !parryattack.parryattackOn)
        {
            anim.SetBool("isCounter", true);
            parryOn = false;
            parryEff.SetActive(true);
            this.gameObject.layer = 8;
            GameObject.Find("Player").transform.Find("Parry_Range").gameObject.SetActive(true);
            Invoke("ParryOff", 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.A) && parryattack.parryattackOn)
        {
            Time.timeScale = 1;

            GameObject.Find("Player").transform.Find("Parry_Attack").gameObject.SetActive(false);
            parryattack.parryattackOn = false;
            parryattack.parryattackStart = false;

            anim.SetBool("isAttack", true);
            Invoke("AttackOff", 0.2f);

            if (dirPos.x <= 0)
            {
                Instantiate(playerAttack_Prefab, transform.position, parryattack.transform.rotation);
            }
            else if (dirPos.x > 0)
            {
                Instantiate(playerAttack_Prefab, transform.position, parryattack.transform.rotation);
            }
        }
    }

    private void AttackOff()
    {
        anim.SetBool("isAttack", false);
        anim.SetBool("isParry", false);
        anim.SetBool("isCounter", false);
    }


    private void ParryOff()
    {
        this.gameObject.layer = 7;
        GameObject.Find("Player").transform.Find("Parry_Range").gameObject.SetActive(false);
        anim.SetBool("isCounter", false);
        parryEff.SetActive(false);
    }

    private void BackStepCoolDown()
    {
        backstepOn = true;
    }

    private void BackStepStop()
    {
        anim.SetBool("isBackStep", false);
        Invoke("AvoidStop", 0.5f);
        rb.velocity = Vector3.zero;
        rb.gravityScale = 4;
        moveOn = true;
    }

    private void AvoidStop()
    {
        this.gameObject.layer = 7;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isJump = false;
        }
    }

    public void GuardianMoveSoundEvent()
    {
        SoundManager.instance.GuardianMove_Sound();
    }

    public void GuardianParrySoundEvent()
    {
        SoundManager.instance.GuardianParry_Sound();
    }
}

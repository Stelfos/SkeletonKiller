using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : PlayerController
{
    public GameObject characterScreen;

    public PlayerStats playerStats;

    private void Start()
    {
        m_CapsulleCollider  = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();
        jumpForce = playerStats.agility + 5;
        MoveSpeed = playerStats.agility * 0.3f + 6;
    }



    private void Update()
    {
        if (!isDead)
            CheckInput();

        if (m_rigidbody.velocity.magnitude > 30)
        {
            m_rigidbody.velocity = new Vector2(m_rigidbody.velocity.x - 0.1f, m_rigidbody.velocity.y - 0.1f);

        }
    }

    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isInMenu)
        {
            isInMenu = true;
            characterScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.P) && isInMenu)
        {
            isInMenu = false;
            characterScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            m_MoveX = Input.GetAxis("Horizontal");

            GroundCheckUpdate();

            if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    m_Anim.Play("Attack");
                }
                else
                {
                    if (m_MoveX == 0)
                    {
                        if (!OnceJumpRayCheck)
                            m_Anim.Play("Idle");
                    }
                    else
                    {
                        m_Anim.Play("Run");
                    }
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                if (isGrounded)
                {
                    if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                        return;
                    transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
                }

                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    return;

                if (!Input.GetKey(KeyCode.A))
                    Filp(false);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (isGrounded)
                {

                    if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                        return;

                    transform.transform.Translate(Vector2.right * m_MoveX * MoveSpeed * Time.deltaTime);
                }
                else
                {
                    transform.transform.Translate(new Vector3(m_MoveX * MoveSpeed * Time.deltaTime, 0, 0));
                }

                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    return;

                if (!Input.GetKey(KeyCode.D))
                    Filp(true);
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                    return;

                if (currentJumpCount < JumpCount)  // 0 , 1
                {
                    PrefromJump();
                }
            }
        }
    }
    protected override void LandingEvent()
    {
        if (!m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Run") && !m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            m_Anim.Play("Idle");
    }

    public void Die()
    {
        m_Anim.Play("Die");
        GameOver();
    }
    public void TakeDMG(float dmg)
    {
        playerStats.currentHealth -= dmg;
        if (playerStats.currentHealth <= 0)
        {
            playerStats.currentHealth = 0;
            Die();
        }
    }

    public void GrantExp(float exp)
    {
        playerStats.exp += exp;
        if (playerStats.exp >= playerStats.maxExp)
        {
            playerStats.exp -= playerStats.maxExp;
            playerStats.LevelUp();
        }
    }
}

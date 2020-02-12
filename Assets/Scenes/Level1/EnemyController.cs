using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    public Animator animator;
    public BoxCollider2D groundDetect;
    public BoxCollider2D wallDetect;

    public bool walkinRight = true;
    private bool seesPlayer = false;

    public float idleTimer = 0;
    public float maxIdleTime = 2;

    public float atackCooldown = 0;
    private void Update()
    {
        if (!animator.GetBool("isDead"))
        {
            //Enemy is walking
            if (animator.GetBool("isWalking"))
            {
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1attack")
                    && !animator.GetCurrentAnimatorStateInfo(0).IsName("IdleEnemy2")
                    && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Die"))
                    animator.Play("Enemy1Walk");
                if (Time.timeScale != 0)
                    transform.Translate(walkSpeed, 0, 0);
            }
            //Enemy is idle
            if (!animator.GetBool("isWalking")
                && !animator.GetBool("isAtacking")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Die"))
            {
                animator.Play("IdleEnemy2");
                idleTimer += Time.deltaTime;
                if (idleTimer >= maxIdleTime)
                {
                    animator.SetBool("isWalking", true);
                    idleTimer = 0;
                }
            }
            //Enemy is atacking
            if (animator.GetBool("isAtacking"))
            {
                atackCooldown += Time.deltaTime;
                if (atackCooldown >= attackSpeed)
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Walk")
                        && !animator.GetCurrentAnimatorStateInfo(0).IsName("IdleEnemy2")
                        && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Die"))
                    {
                        animator.Play("Enemy1attack");
                    }
                }
                else
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1attack")
                        && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1Die"))
                        animator.Play("IdleEnemy2");
                }

            }
        }
        
    }

    public void EnemySpotted()
    {
        seesPlayer = true;
        animator.SetBool("isWalking", false);
        animator.SetBool("isAtacking", true);
    }

    public void EnemyMissing()
    {
        seesPlayer = false;
    }

    public void AttackAnimEnded()
    {
        atackCooldown = 0;
        animator.Play("IdleEnemy2");
        animator.SetBool("isAtacking", false);
    }

    public void Rotate()
    {
        transform.Rotate(0, 180, 0);
        transform.Translate(2.72f, 0, 0);

        walkinRight = !walkinRight;

        animator.SetBool("isWalking", false);
    }

    public override void Die()
    {
        GameObject.Find("Player").GetComponent<Swordman>().grantExp(exp);
        Destroy(gameObject, 0.7f);
        animator.SetBool("isDead", true);
        animator.Play("Enemy1Die");
        if (name == "Boss")
        {
            GameObject.Find("Player").GetComponent<Swordman>().GameOver();
        }  
    }

    public override void TakeDMG(float dmg)
    {
        if (!seesPlayer)
            Rotate();
        if (hp >= 0)
        {
            hp -= dmg;
            if (hp <= 0)
            {
                hp = 0;
                Debug.Log("Can you die?");
                Die();
            }
            healthBar.localScale = new Vector3(hp / maxHp, 1, 1);
        }

    }
}

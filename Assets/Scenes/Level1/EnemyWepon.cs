using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWepon : MonoBehaviour
{
    public BoxCollider2D wepon;
    public Enemy enemyStats;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Swordman>().TakeDMG(enemyStats.attack);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour
{
    public GameObject enemy;
    public bool continuous;

    //Wall detection
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Ground") && !continuous)
        {
            enemy.GetComponent<EnemyController>().Rotate();
        }
    }

    //Ground detection
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Ground") && continuous)
        {
            enemy.GetComponent<EnemyController>().Rotate();
        }
    }
}

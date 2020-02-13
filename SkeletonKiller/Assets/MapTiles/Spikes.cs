using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<Swordman>().Die();
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponentInChildren<Enemy>().Die();
        }
    }
}

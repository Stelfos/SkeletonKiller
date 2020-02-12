using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWepon : MonoBehaviour
{
    public BoxCollider2D weapon;
    public PlayerStats player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponentInChildren<Enemy>().TakeDMG(player.streanth*10);
        }
    }
}

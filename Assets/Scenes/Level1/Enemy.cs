using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHp = 100;
    public float hp = 100;
    public float attack = 30;
    public float walkSpeed = 10;
    public float attackSpeed = 3;
    public float exp = 30;

    public Transform healthBar;

    public virtual void TakeDMG(float dmg) { }

    public virtual void Die() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    public PlayerStats player;
    public Text healthTxt;
    public GameObject healthBar;

    public GameObject expBar;

    private RectTransform hprt;
    private RectTransform exprt;

    private void Start()
    {
        hprt = healthBar.GetComponent<RectTransform>();
        exprt = expBar.GetComponent<RectTransform>();
    }
    private void Update()
    {
        healthTxt.text = player.currentHealth + " / " + player.vitality;
        hprt.localScale = new Vector3 (player.currentHealth / player.vitality, hprt.localScale.y, hprt.localScale.z);
        exprt.localScale = new Vector3(player.exp / player.maxExp, exprt.localScale.y, exprt.localScale.z);
    }
}

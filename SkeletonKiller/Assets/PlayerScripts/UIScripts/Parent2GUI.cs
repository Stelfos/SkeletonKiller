using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent2GUI : AttributesGUI
{
    public PlayerStats parent2stats;
    private void OnEnable()
    {      
        playerStats = GameObject.Find("ParentStats2").GetComponent<PlayerStats>();
        parent2stats = playerStats;
    }

    public void SwapParent()
    {
        playerStats = GameObject.Find("Player").GetComponentInChildren<PlayerStats>();
        parent2stats.vitality = playerStats.vitality;
        parent2stats.vitalityDNA = playerStats.vitalityDNA;
        parent2stats.streanth = playerStats.streanth;
        parent2stats.streanthDNA = playerStats.streanthDNA;
        parent2stats.agility = playerStats.agility;
        parent2stats.agilityDNA = playerStats.agilityDNA;
        parent2stats.size = playerStats.size;
        parent2stats.sizeDNA = playerStats.sizeDNA;
        parent2stats.currentHealth = playerStats.vitality;
        GameObject.Find("GameOver").GetComponent<GameOver>().Play();
    }
}


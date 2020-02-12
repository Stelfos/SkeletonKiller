using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentGUI : AttributesGUI
{
    public PlayerStats parent1stats;
    private void OnEnable()
    {
        playerStats = GameObject.Find("ParentStats1").GetComponent<PlayerStats>();
        parent1stats = playerStats;
    }

    public void SwapParent()
    {
        playerStats = GameObject.Find("Player").GetComponentInChildren<PlayerStats>();
        parent1stats.vitality = playerStats.vitality;
        parent1stats.vitalityDNA = playerStats.vitalityDNA;
        parent1stats.streanth = playerStats.streanth;
        parent1stats.streanthDNA = playerStats.streanthDNA;
        parent1stats.agility = playerStats.agility;
        parent1stats.agilityDNA = playerStats.agilityDNA;
        parent1stats.size = playerStats.size;
        parent1stats.sizeDNA = playerStats.sizeDNA;
        parent1stats.currentHealth = playerStats.vitality;
        GameObject.Find("GameOver").GetComponent<GameOver>().Play();
    }
}

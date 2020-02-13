using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class AttributesGUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI streanthText;
    public TextMeshProUGUI vitalityText;
    public TextMeshProUGUI height;
    public TextMeshProUGUI width;

    public Transform agility;
    public Transform streanth;
    public Transform vitality;

    private void Update()
    {
        agilityText.text = playerStats.agility.ToString();
        streanthText.text = playerStats.streanth.ToString();
        vitalityText.text = playerStats.vitality.ToString();
        height.text = "Height " + Math.Round(playerStats.size.x, 1).ToString();
        width.text = "Width " + Math.Round(playerStats.size.y, 1).ToString();

        agility.localScale = new Vector3(playerStats.agility / 10f, 1, 1);
        streanth.localScale = new Vector3(playerStats.streanth / 10f, 1, 1);
        vitality.localScale = new Vector3(playerStats.currentHealth / playerStats.vitality, 1, 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

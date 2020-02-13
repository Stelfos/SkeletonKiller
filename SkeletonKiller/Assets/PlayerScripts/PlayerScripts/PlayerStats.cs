using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class PlayerStats : MonoBehaviour
{
    [Header("[Player Statistics]")]
    public float currentHealth;
    public float vitality = 100;
    public char[] vitalityDNA = new char[2]{ 'A', 'a' };
    

    public float streanth = 5;
    public char[] streanthDNA = new char[2]{ 'A', 'a' };

    public float agility = 5;
    public char[] agilityDNA = new char[2]{ 'A', 'a' };

    public Vector3 size = new Vector3(1,1,1);
    public char[] sizeDNA = new char[4]{ 'A', 'a', 'A', 'a' };

    public float exp = 0;
    public float maxExp = 100;

    //Generates new parent when starting fresh game
    private void Start()
    {
        GenerateParentStats();
    }

    public void GenerateParentStats()
    {
        vitality = UnityEngine.Random.Range(70, 100);
        vitalityDNA = RandomDNA();
        streanth = UnityEngine.Random.Range(3, 7);
        streanthDNA = RandomDNA();
        agility = UnityEngine.Random.Range(3, 7);
        agilityDNA = RandomDNA();
        size.x = UnityEngine.Random.Range(0.9f, 1.1f);
        size.y = UnityEngine.Random.Range(0.9f, 1.1f);
        sizeDNA = RandomSizeDNA();
        currentHealth = vitality;
        exp = 0;
        maxExp = 100;
    }

    private char[] RandomDNA()
    {
        char[] DNA;
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                DNA = new char[2] { 'a', 'a'};
                break;
            case 1:
                DNA = new char[2] { 'A', 'a'};
                break;
            case 2:
                DNA = new char[2] { 'A', 'A'};
                break;
            default:
                DNA = new char[2] { 'A', 'a'};
                break;

        }
        return DNA;
    }
    private char[] RandomSizeDNA()
    {
        char[] DNA;
        switch (UnityEngine.Random.Range(0, 8))
        {
            case 0:
                DNA = new char[4] { 'a', 'a', 'a', 'a' };
                break;
            case 1:
                DNA = new char[4] { 'a', 'a', 'A', 'a' };
                break;

            case 2:
                DNA = new char[4] { 'a', 'a', 'A', 'A' };
                break;
            case 3:
                DNA = new char[4] { 'A', 'a', 'a', 'a' };
                break;
            case 4:
                DNA = new char[4] { 'A', 'a', 'A', 'a' };
                break;
            case 5:
                DNA = new char[4] { 'A', 'a', 'A', 'A' };
                break;
            case 6:
                DNA = new char[4] { 'A', 'A', 'a', 'a' };
                break;

            case 7:
                DNA = new char[4] { 'A', 'A', 'A', 'a' };
                break;
            case 8:
                DNA = new char[4] { 'A', 'A', 'A', 'A' };
                break;
            default:
                DNA = new char[4] { 'A', 'a', 'A', 'a' };
                break;

        }
        return DNA;
    }

    //Generates new chlid when parents are given
    public void GenerateChild(params PlayerStats[] parents)
    {

        vitalityDNA = GenerateDNA(GenerateDNAList("vitality", parents));
        Array.Sort(vitalityDNA);
        vitality = SetPlayerAttribute(vitalityDNA, GenrateAttibuteList("vitality", parents), 200, 70);

        streanthDNA = GenerateDNA(GenerateDNAList("streanth", parents));
        Array.Sort(streanthDNA);
        streanth = SetPlayerAttribute(streanthDNA, GenrateAttibuteList("streanth", parents), 10, 1);

        agilityDNA = GenerateDNA(GenerateDNAList("agility", parents));
        Array.Sort(agilityDNA);
        agility = SetPlayerAttribute(agilityDNA, GenrateAttibuteList("agility", parents), 10, 1);

        sizeDNA = GenerateDNA(GenerateDNAList("size", parents));
        Array.Sort(sizeDNA);
        size = SetPlayerSize(sizeDNA, GenrateSizeList(parents));


        currentHealth = vitality;
        this.GetComponentInParent<Transform>().transform.localScale = size;
    }

    private List<Vector2> GenrateSizeList(params PlayerStats[] parents)
    {
        List<Vector2> sizeList = new List<Vector2>();
        foreach (PlayerStats parent in parents)
        {
            sizeList.Add(new Vector2(parent.size.x , parent.size.y));
        }
        return sizeList;
    }

    private Vector3 SetPlayerSize(char[] sizeDNA, List<Vector2> list)
    {
        Vector3 size;
        string sDNA = new string(sizeDNA, 0, 2);
        switch (sDNA)
        {
            case "aa":
                size.x = Math.Max(list.Min(v => v.x) + UnityEngine.Random.Range(-0.05f, 0.05f), 0.5f);
                break;

            case "Aa":
                size.x = Math.Min(list.Max(v => v.x) + UnityEngine.Random.Range(-0.1f, 0.05f), 2);
                break;

            case "AA":
                size.x = Math.Min(list.Max(v => v.x) + UnityEngine.Random.Range(-0.05f, 0.1f), 2);
                break;

            default:
                size.x = UnityEngine.Random.Range(0.5f, 2);
                Debug.Log("MUTATION (DNA: " + sDNA + ")");
                break;
        }

        sDNA = new string(sizeDNA, 2, 2);
        switch (sDNA)
        {
            case "aa":
                size.y = Math.Max(list.Min(v => v.y) + UnityEngine.Random.Range(-0.05f, 0.05f), 0.5f);
                break;

            case "Aa":
                size.y = Math.Max(list.Min(v => v.y)+ UnityEngine.Random.Range(-0.1f, 0.05f), 2);
                break;

            case "AA":
                size.y = Math.Min(list.Max(v => v.y) + UnityEngine.Random.Range(-0.05f, 0.1f), 2);
                break;

            default:
                size.y = UnityEngine.Random.Range(0.5f, 2);
                Debug.Log("MUTATION (DNA: " + sDNA + ")");
                break;
        }
        size.z = 1;
        return size;
    }

    //Generates List of floats including every parent for chosen attribute 
    private List<float> GenrateAttibuteList(string attribute, params PlayerStats[] parents)
    {
        List<float> attributeList = new List<float>();
        switch (attribute)
        {
            case "vitality":
                foreach (PlayerStats parent in parents)
                {
                    attributeList.Add(parent.vitality);
                }
                break;
            case "streanth":
                foreach (PlayerStats parent in parents)
                {
                    attributeList.Add(parent.streanth);
                }
                break;

            case "agility":
                foreach (PlayerStats parent in parents)
                {
                    attributeList.Add(parent.agility);
                }
                break;
            default:
                Debug.Log("Can't recognize attribute: " + attribute);
                break;
        }
        return attributeList;
    }

    private int SetPlayerAttribute(char[] DNA, List<float> parentsAttribute, float max, float min)
    {
        float deviation = max * 0.1f;
        float attributeValue;
        string sDNA = String.Join("",DNA);
        switch (sDNA)
        {
            case "aa":
                attributeValue = Math.Min(Math.Max(parentsAttribute.Min() + UnityEngine.Random.Range(deviation * -1, deviation) , min), max);
                break;

            case "Aa":
                attributeValue = Math.Max(Math.Min(parentsAttribute.Max() + UnityEngine.Random.Range(deviation * -2, deviation * 1), max), min);
                break;

            case "AA":
                attributeValue = Math.Max(Math.Min(parentsAttribute.Max() + UnityEngine.Random.Range(deviation * -1, deviation * 2), max), min);
                break;
            default:
                attributeValue = UnityEngine.Random.Range(min, max);
                Debug.Log("MUTATION (DNA: " + sDNA + ")");
                break;
        }

        return (int)attributeValue;
    }

    //Generates genotyp for a child of any given number of parents and any given number of allels
    char[] GenerateDNA(List<char[]> DNA)
    {
        int numberOfParents = DNA.Count;
        int numberOfGens = DNA[0].Length;
        char[] newDNA = new char[numberOfGens];
        int randomGen;
        for(int i = 0; i < numberOfGens; i++)
        {
            randomGen = UnityEngine.Random.Range(0, numberOfParents);
            newDNA[i] = DNA[randomGen][i];
        }
        return newDNA;
    }

    //Generates List of char array of DNA agentypes of every parent
    List<char[]> GenerateDNAList(string attribute, params PlayerStats[] parents)
    {
        List<char[]> DNAList = new List<char[]>();
        switch (attribute)
        {
            case "vitality":
                foreach (PlayerStats parent in parents)
                {
                    DNAList.Add(parent.vitalityDNA);
                }
                break;
            case "streanth":
                foreach (PlayerStats parent in parents)
                {
                    DNAList.Add(parent.streanthDNA);
                }
                break;

            case "agility":
                foreach (PlayerStats parent in parents)
                {
                    DNAList.Add(parent.agilityDNA);
                }
                break;

            case "size":
                foreach (PlayerStats parent in parents)
                {
                    DNAList.Add(parent.sizeDNA);
                }
                break;
        }
        return DNAList;
    }

    public void LevelUp()
    {
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                if (streanth < 10)
                {
                    streanth++;
                    break;
                }
                else if (agility < 10)
                {
                    agility++;
                    break;
                }
                else goto case default;
            case 1:
                if (agility < 10)
                {
                    agility++;
                    break;
                }
                else if (streanth < 10)
                {
                    streanth++;
                    break;
                }
                else goto case default;
            case 2:
            default:
                if (vitality < 200)
                {
                    vitality += 10;
                    currentHealth += 10;
                }               
                break;
        }
    }

}

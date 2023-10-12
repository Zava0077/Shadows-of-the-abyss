using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legendary : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCrit = (float)Random.Range(0, 100);
        attackDamage = (float)Random.Range(0, 100) / 2;
        attackSpeed = (float)Random.Range(0, 100) / 25;
        castCrit = (float)Random.Range(0, 100);
        castDamage = (float)Random.Range(0, 100) / 8;
        castSpeed = (float)Random.Range(0, 100) / 50;
        defence = (float)Random.Range(0, 100) / 2;
        evasionChance = (float)Random.Range(0, 100) / 2;
        globalCrit = (float)Random.Range(0, 100);
        globalCritMulti = (float)Random.Range(0, 100) / 35;
        globalDamage = (float)Random.Range(0, 100);
        HPRegen = (float)Random.Range(0, 100) / 2;
        iceDamage = (float)Random.Range(0, 100) / 2;
        iceResist = (float)Random.Range(0, 100) / 2;
        igniteDamage = (float)Random.Range(0, 100) / 2;
        igniteResist = (float)Random.Range(0, 100) / 2;
        inscriptionAdded = (float)Random.Range(4, 2);
        lightningDamage = (float)Random.Range(0, 100) / 2;
        lightningResist = (float)Random.Range(0, 100) / 2;
        luck = (float)Random.Range(0, 100) / 50;
        maxHP = (float)Random.Range(0, 100) / 2;
        maxHPPercent = (float)Random.Range(0, 100) / 2;
        maxMP = (float)Random.Range(0, 100) / 10;
        maxMPPercent = (float)Random.Range(0, 100) / 2;
        moveSpeed = (float)Random.Range(0, 100) / 2;
        MPRegen = (float)Random.Range(0, 100) / 2;
        poisonDamage = (float)Random.Range(0, 100) / 2;
        poisonResist = (float)Random.Range(0, 100) / 2;
        projSpeed = (float)Random.Range(0, 100) / 2;
        propertiesNum = 7;
        pureDamage = (float)Random.Range(0, 100) / 2;
        voidDamage = (float)Random.Range(0, 100) / 2;
        voidResist = (float)Random.Range(0, 100) / 2;
        rarityName = "Legendary";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

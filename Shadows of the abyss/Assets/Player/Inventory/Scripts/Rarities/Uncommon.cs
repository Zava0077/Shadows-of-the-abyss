using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncommon : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCrit = (float)Random.Range(0, 100) / 10;
        attackDamage = (float)Random.Range(0, 100) / 10;
        attackSpeed = (float)Random.Range(0, 100) / 50;
        castCrit = (float)Random.Range(0, 100) / 10;
        castDamage = (float)Random.Range(0, 100) / 10;
        castSpeed = (float)Random.Range(0, 100) / 75;
        defence = (float)Random.Range(0, 100) / 25;
        evasionChance = (float)Random.Range(0, 100) / 25;
        globalCrit = (float)Random.Range(0, 100) / 75;
        globalCritMulti = (float)Random.Range(0, 100) / 75;
        globalDamage = (float)Random.Range(0, 100) / 25;
        HPRegen = (float)Random.Range(0, 100) / 25;
        iceDamage = (float)Random.Range(0, 100) / 25;
        iceResist = (float)Random.Range(0, 100) / 25;
        igniteDamage = (float)Random.Range(0, 100) / 25;
        igniteResist = (float)Random.Range(0, 100) / 25;
        inscriptionAdded = 1;
        lightningDamage = (float)Random.Range(0, 100) / 25;
        lightningResist = (float)Random.Range(0, 100) / 25;
        luck = (float)Random.Range(0, 100) / 100;
        maxHP = (float)Random.Range(0, 100) / 25;
        maxHPPercent = (float)Random.Range(0, 100) / 25;
        maxMP = (float)Random.Range(0, 100) / 25;
        maxMPPercent = (float)Random.Range(0, 100) / 25;
        moveSpeed = (float)Random.Range(0, 100) / 25;
        MPRegen = (float)Random.Range(0, 100) / 25;
        poisonDamage = (float)Random.Range(0, 100) / 25;
        poisonResist = (float)Random.Range(0, 100) / 25;
        projSpeed = (float)Random.Range(0, 100) / 25;
        propertiesNum = 2;
        pureDamage = (float)Random.Range(0, 100) / 25;
        voidDamage = (float)Random.Range(0, 100) / 25;
        voidResist = (float)Random.Range(0, 100) / 25;
        rarityName = "Uncommon";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

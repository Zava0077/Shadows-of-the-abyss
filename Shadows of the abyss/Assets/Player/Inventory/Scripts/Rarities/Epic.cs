using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epic : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCrit = (float)Random.Range(0, 100) / 6;
        attackDamage = (float)Random.Range(0, 100) / 6;
        attackSpeed = (float)Random.Range(0, 100) / 55;
        castCrit = (float)Random.Range(0, 100) / 8;
        castDamage = (float)Random.Range(0, 100) / 8;
        castSpeed = (float)Random.Range(0, 100) / 55;
        defence = (float)Random.Range(0, 100) / 6;
        evasionChance = (float)Random.Range(0, 100) / 6;
        globalCrit = (float)Random.Range(0, 100) / 6;
        globalCritMulti = (float)Random.Range(0, 100) / 65;
        globalDamage = (float)Random.Range(0, 100) / 6;
        HPRegen = (float)Random.Range(0, 100) / 8;
        iceDamage = (float)Random.Range(0, 100) / 6;
        iceResist = (float)Random.Range(0, 100) / 6;
        igniteDamage = (float)Random.Range(0, 100) / 6;
        igniteResist = (float)Random.Range(0, 100) / 6;
        inscriptionAdded = (float)Random.Range(3,4);
        lightningDamage = (float)Random.Range(0, 100) / 6;
        lightningResist = (float)Random.Range(0, 100) / 6;
        luck = (float)Random.Range(0, 100) / 50;
        maxHP = (float)Random.Range(0, 100) / 6;
        maxHPPercent = (float)Random.Range(0, 100) / 6;
        maxMP = (float)Random.Range(0, 100) / 10;
        maxMPPercent = (float)Random.Range(0, 100) / 6;
        moveSpeed = (float)Random.Range(0, 100) / 6;
        MPRegen = (float)Random.Range(0, 100) / 8;
        poisonDamage = (float)Random.Range(0, 100) / 6;
        poisonResist = (float)Random.Range(0, 100) / 6;
        projSpeed = (float)Random.Range(0, 100) / 6;
        propertiesNum = 4;
        pureDamage = (float)Random.Range(0, 100) / 6;
        voidDamage = (float)Random.Range(0, 100) / 6;
        voidResist = (float)Random.Range(0, 100) / 6;
        rarityName = "Epic";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

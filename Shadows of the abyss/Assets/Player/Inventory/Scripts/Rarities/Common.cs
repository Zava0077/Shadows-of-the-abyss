using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        attackCrit = (float)Random.Range(0,100)/25;
        attackDamage = (float)Random.Range(0,100)/25;
        attackSpeed = (float)Random.Range(0,100)/100;
        castCrit = (float)Random.Range(0,100)/25;
        castDamage = (float)Random.Range(0,100)/25;
        castSpeed = (float)Random.Range(0,100)/25;
        defence = (float)Random.Range(0,100)/50;
        evasionChance = (float)Random.Range(0,100)/50;
        globalCrit = (float)Random.Range(0,100)/ 50;
        globalCritMulti = (float)Random.Range(0,100)/ 75;
        globalDamage = (float)Random.Range(0,100)/25;
        HPRegen = (float)Random.Range(0,100)/50;
        iceDamage = (float)Random.Range(0,100)/ 50;
        iceResist = (float)Random.Range(0,100)/ 50;
        igniteDamage = (float)Random.Range(0,100)/ 50;
        igniteResist = (float)Random.Range(0,100)/ 50;
        inscriptionAdded = 1;
        lightningDamage = (float)Random.Range(0,100)/ 50;
        lightningResist = (float)Random.Range(0,100)/ 50;
        luck = (float)Random.Range(0,100)/100;
        maxHP = (float)Random.Range(0,100)/ 25;
        maxHPPercent = (float)Random.Range(0,100)/ 25;
        maxMP = (float)Random.Range(0,100)/25;
        maxMPPercent = (float)Random.Range(0,100)/25;
        moveSpeed = (float)Random.Range(0,100)/25;
        MPRegen = (float)Random.Range(0,100)/100;
        poisonDamage = (float)Random.Range(0,100)/ 50;
        poisonResist = (float)Random.Range(0,100)/ 50;
        projSpeed = (float)Random.Range(0,100)/ 25;
        propertiesNum = 1;
        pureDamage = (float)Random.Range(0,100)/ 50;
        voidDamage = (float)Random.Range(0,100)/ 50;
        voidResist = (float)Random.Range(0,100)/ 50;
        rarityName = "Common";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

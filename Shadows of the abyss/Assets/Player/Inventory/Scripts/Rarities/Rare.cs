using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rare : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCrit = (float)Random.Range(0, 100) / 10;
        attackDamage = (float)Random.Range(0, 100) / 10;
        attackSpeed = (float)Random.Range(0, 100) / 25;
        castCrit = (float)Random.Range(0, 100) / 8;
        castDamage = (float)Random.Range(0, 100) / 8;
        castSpeed = (float)Random.Range(0, 100) / 10;
        defence = (float)Random.Range(0, 100) / 10;
        evasionChance = (float)Random.Range(0, 100) / 10;
        globalCrit = (float)Random.Range(0, 100) / 10;
        globalCritMulti = (float)Random.Range(0, 100) / 75;
        globalDamage = (float)Random.Range(0, 100) / 10;
        HPRegen = (float)Random.Range(0, 100) / 22;
        iceDamage = (float)Random.Range(0, 100) / 10;
        iceResist = (float)Random.Range(0, 100) / 10;
        igniteDamage = (float)Random.Range(0, 100) / 10;
        igniteResist = (float)Random.Range(0, 100) / 10;
        inscriptionAdded = 2;
        lightningDamage = (float)Random.Range(0, 100) / 10;
        lightningResist = (float)Random.Range(0, 100) / 10;
        luck = (float)Random.Range(0, 100) / 100;
        maxHP = (float)Random.Range(0, 100) / 10;
        maxHPPercent = (float)Random.Range(0, 100) / 10;
        maxMP = (float)Random.Range(0, 100) / 10;
        maxMPPercent = (float)Random.Range(0, 100) / 10;
        moveSpeed = (float)Random.Range(0, 100) / 10;
        MPRegen = (float)Random.Range(0, 100) / 22;
        poisonDamage = (float)Random.Range(0, 100) / 10;
        poisonResist = (float)Random.Range(0, 100) / 10;
        projSpeed = (float)Random.Range(0, 100) / 10;
        propertiesNum = 2;
        pureDamage = (float)Random.Range(0, 100) / 10;
        voidDamage = (float)Random.Range(0, 100) / 10;
        voidResist = (float)Random.Range(0, 100) / 10;
        rarityName = "Rare";
    }
}

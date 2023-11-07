using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rare : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCritRare = (float)Random.Range(0, 100) / 10;
        attackDamageRare = (float)Random.Range(0, 100) / 10;
        attackSpeedRare = (float)Random.Range(0, 100) / 25;
        castCritRare = (float)Random.Range(0, 100) / 8;
        castDamageRare = (float)Random.Range(0, 100) / 8;
        castSpeedRare = (float)Random.Range(0, 100) / 10;
        defenceRare = (float)Random.Range(0, 100) / 10;
        evasionChanceRare = (float)Random.Range(0, 100) / 10;
        globalCritRare = (float)Random.Range(0, 100) / 10;
        globalCritMultiRare = (float)Random.Range(0, 100) / 75;
        globalDamageRare = (float)Random.Range(0, 100) / 10;
        HPRegenRare = (float)Random.Range(0, 100) / 22;
        iceDamageRare = (float)Random.Range(0, 100) / 10;
        iceResistRare = (float)Random.Range(0, 100) / 10;
        igniteDamageRare = (float)Random.Range(0, 100) / 10;
        igniteResistRare = (float)Random.Range(0, 100) / 10;
        inscSlotsRare = 2;
        lightningDamageRare = (float)Random.Range(0, 100) / 10;
        lightningResistRare = (float)Random.Range(0, 100) / 10;
        luckRare = (float)Random.Range(0, 100) / 100;
        maxHPRare = (float)Random.Range(0, 100) / 10;
        maxHPPercentRare = (float)Random.Range(0, 100) / 10;
        maxMPRare = (float)Random.Range(0, 100) / 10;
        maxMPPercentRare = (float)Random.Range(0, 100) / 10;
        moveSpeedRare = (float)Random.Range(0, 100) / 10;
        MPRegenRare = (float)Random.Range(0, 100) / 22;
        poisonDamageRare = (float)Random.Range(0, 100) / 10;
        poisonResistRare = (float)Random.Range(0, 100) / 10;
        projSpeedRare = (float)Random.Range(0, 100) / 10;
        propertiesNum = 2;
        pureDamageRare = (float)Random.Range(0, 100) / 10;
        voidDamageRare = (float)Random.Range(0, 100) / 10;
        voidResistRare = (float)Random.Range(0, 100) / 10;
        propertiesTier = 3;
        rarityTier = 2;
        rarityName = "Rare";
    }
}

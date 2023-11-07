using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Legendary : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCritRare = (float)Random.Range(0, 100);
        attackDamageRare = (float)Random.Range(0, 100) / 2;
        attackSpeedRare = (float)Random.Range(0, 100) / 25;
        castCritRare = (float)Random.Range(0, 100);
        castDamageRare = (float)Random.Range(0, 100) / 8;
        castSpeedRare = (float)Random.Range(0, 100) / 50;
        defenceRare = (float)Random.Range(0, 100) / 2;
        evasionChanceRare = (float)Random.Range(0, 100) / 2;
        globalCritRare = (float)Random.Range(0, 100);
        globalCritMultiRare = (float)Random.Range(0, 100) / 35;
        globalDamageRare = (float)Random.Range(0, 100);
        HPRegenRare = (float)Random.Range(0, 100) / 2;
        iceDamageRare = (float)Random.Range(0, 100) / 2;
        iceResistRare = (float)Random.Range(0, 100) / 2;
        igniteDamageRare = (float)Random.Range(0, 100) / 2;
        igniteResistRare = (float)Random.Range(0, 100) / 2;
        inscSlotsRare = (float)Random.Range(4, 2);
        lightningDamageRare = (float)Random.Range(0, 100) / 2;
        lightningResistRare = (float)Random.Range(0, 100) / 2;
        luckRare = (float)Random.Range(0, 100) / 50;
        maxHPRare = (float)Random.Range(0, 100) / 2;
        maxHPPercentRare = (float)Random.Range(0, 100) / 2;
        maxMPRare = (float)Random.Range(0, 100) / 10;
        maxMPPercentRare = (float)Random.Range(0, 100) / 2;
        moveSpeedRare = (float)Random.Range(0, 100) / 2;
        MPRegenRare = (float)Random.Range(0, 100) / 2;
        poisonDamageRare = (float)Random.Range(0, 100) / 2;
        poisonResistRare = (float)Random.Range(0, 100) / 2;
        projSpeedRare = (float)Random.Range(0, 100) / 2;
        propertiesNum = 7;
        pureDamageRare = (float)Random.Range(0, 100) / 2;
        voidDamageRare = (float)Random.Range(0, 100) / 2;
        voidResistRare = (float)Random.Range(0, 100) / 2;
        propertiesTier = 5;
        rarityTier = 4;
        rarityName = "Legendary";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

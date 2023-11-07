using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncommon : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCritRare = (float)Random.Range(0, 100) / 10;
        attackDamageRare = (float)Random.Range(0, 100) / 10;
        attackSpeedRare = (float)Random.Range(0, 100) / 50;
        castCritRare = (float)Random.Range(0, 100) / 10;
        castDamageRare = (float)Random.Range(0, 100) / 10;
        castSpeedRare = (float)Random.Range(0, 100) / 75;
        defenceRare = (float)Random.Range(0, 100) / 25;
        evasionChanceRare = (float)Random.Range(0, 100) / 25;
        globalCritRare = (float)Random.Range(0, 100) / 75;
        globalCritMultiRare = (float)Random.Range(0, 100) / 75;
        globalDamageRare = (float)Random.Range(0, 100) / 25;
        HPRegenRare = (float)Random.Range(0, 100) / 25;
        iceDamageRare = (float)Random.Range(0, 100) / 25;
        iceResistRare = (float)Random.Range(0, 100) / 25;
        igniteDamageRare = (float)Random.Range(0, 100) / 25;
        igniteResistRare = (float)Random.Range(0, 100) / 25;
        inscSlotsRare = 1;
        lightningDamageRare = (float)Random.Range(0, 100) / 25;
        lightningResistRare = (float)Random.Range(0, 100) / 25;
        luckRare = (float)Random.Range(0, 100) / 100;
        maxHPRare = (float)Random.Range(0, 100) / 25;
        maxHPPercentRare = (float)Random.Range(0, 100) / 25;
        maxMPRare = (float)Random.Range(0, 100) / 25;
        maxMPPercentRare = (float)Random.Range(0, 100) / 25;
        moveSpeedRare = (float)Random.Range(0, 100) / 25;
        MPRegenRare = (float)Random.Range(0, 100) / 25;
        poisonDamageRare = (float)Random.Range(0, 100) / 25;
        poisonResistRare = (float)Random.Range(0, 100) / 25;
        projSpeedRare = (float)Random.Range(0, 100) / 25;
        propertiesNum = 2;
        pureDamageRare = (float)Random.Range(0, 100) / 25;
        voidDamageRare = (float)Random.Range(0, 100) / 25;
        voidResistRare = (float)Random.Range(0, 100) / 25;
        propertiesTier = 2;
        rarityTier = 1;
        rarityName = "Uncommon";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

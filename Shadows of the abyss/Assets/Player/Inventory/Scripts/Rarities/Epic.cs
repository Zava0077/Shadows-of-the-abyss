using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epic : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        //Changes state of nums on item's insantination
        attackCritRare = (float)Random.Range(0, 100) / 6;
        attackDamageRare = (float)Random.Range(0, 100) / 6;
        attackSpeedRare = (float)Random.Range(0, 100) / 55;
        castCritRare = (float)Random.Range(0, 100) / 8;
        castDamageRare = (float)Random.Range(0, 100) / 8;
        castSpeedRare = (float)Random.Range(0, 100) / 55;
        defenceRare = (float)Random.Range(0, 100) / 6;
        evasionChanceRare = (float)Random.Range(0, 100) / 6;
        globalCritRare = (float)Random.Range(0, 100) / 6;
        globalCritMultiRare = (float)Random.Range(0, 100) / 65;
        globalDamageRare = (float)Random.Range(0, 100) / 6;
        HPRegenRare = (float)Random.Range(0, 100) / 8;
        iceDamageRare = (float)Random.Range(0, 100) / 6;
        iceResistRare = (float)Random.Range(0, 100) / 6;
        igniteDamageRare = (float)Random.Range(0, 100) / 6;
        igniteResistRare = (float)Random.Range(0, 100) / 6;
        inscSlotsRare = (float)Random.Range(3,4);
        lightningDamageRare = (float)Random.Range(0, 100) / 6;
        lightningResistRare = (float)Random.Range(0, 100) / 6;
        luckRare = (float)Random.Range(0, 100) / 50;
        maxHPRare = (float)Random.Range(0, 100) / 6;
        maxHPPercentRare = (float)Random.Range(0, 100) / 6;
        maxMPRare = (float)Random.Range(0, 100) / 10;
        maxMPPercentRare = (float)Random.Range(0, 100) / 6;
        moveSpeedRare = (float)Random.Range(0, 100) / 6;
        MPRegenRare = (float)Random.Range(0, 100) / 8;
        poisonDamageRare = (float)Random.Range(0, 100) / 6;
        poisonResistRare = (float)Random.Range(0, 100) / 6;
        projSpeedRare = (float)Random.Range(0, 100) / 6;
        propertiesNum = 4;
        pureDamageRare = (float)Random.Range(0, 100) / 6;
        voidDamageRare = (float)Random.Range(0, 100) / 6;
        voidResistRare = (float)Random.Range(0, 100) / 6;
        propertiesTier = 4;
        rarityTier = 3;
        rarityName = "Epic";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

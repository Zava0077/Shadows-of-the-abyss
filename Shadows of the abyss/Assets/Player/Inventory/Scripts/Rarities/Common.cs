using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common : Rarity
{
    // Start is called before the first frame update
    public void RarityStats()
    {
        attackCritRare = (float)Random.Range(0,100)/25;
        attackDamageRare = (float)Random.Range(0,100)/25;
        attackSpeedRare = (float)Random.Range(0,100)/100;
        castCritRare = (float)Random.Range(0,100)/25;
        castDamageRare = (float)Random.Range(0,100)/25;
        castSpeedRare = (float)Random.Range(0,100)/25;
        defenceRare = (float)Random.Range(0,100)/50;
        evasionChanceRare = (float)Random.Range(0,100)/50;
        globalCritRare = (float)Random.Range(0,100)/ 50;
        globalCritMultiRare = (float)Random.Range(0,100)/ 75;
        globalDamageRare = (float)Random.Range(0,100)/25;
        HPRegenRare = (float)Random.Range(0,100)/50;
        iceDamageRare = (float)Random.Range(0,100)/ 50;
        iceResistRare = (float)Random.Range(0,100)/ 50;
        igniteDamageRare = (float)Random.Range(0,100)/ 50;
        igniteResistRare = (float)Random.Range(0,100)/ 50;
        inscriptionAdded = 1;
        lightningDamageRare = (float)Random.Range(0,100)/ 50;
        lightningResistRare = (float)Random.Range(0,100)/ 50;
        luckRare = (float)Random.Range(0,100)/100;
        maxHPRare = (float)Random.Range(0,100)/ 25;
        maxHPPercentRare = (float)Random.Range(0,100)/ 25;
        maxMPRare = (float)Random.Range(0,100)/25;
        maxMPPercentRare = (float)Random.Range(0,100)/25;
        moveSpeedRare = (float)Random.Range(0,100)/25;
        MPRegenRare = (float)Random.Range(0,100)/100;
        poisonDamageRare = (float)Random.Range(0,100)/ 50;
        poisonResistRare = (float)Random.Range(0,100)/ 50;
        projSpeedRare = (float)Random.Range(0,100)/ 25;
        propertiesNum = 1;
        pureDamageRare = (float)Random.Range(0,100)/ 50;
        voidDamageRare = (float)Random.Range(0,100)/ 50;
        voidResistRare = (float)Random.Range(0,100)/ 50;
        propertiesTier = 1;
        rarityTier = 0;
        rarityName = "Common";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

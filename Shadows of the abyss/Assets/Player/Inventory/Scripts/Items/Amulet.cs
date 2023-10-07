using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Amulet : Jewelery
{
    public float critAdd;
    public float manaPercent;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        string description = "";
        int rareChance = rnd.Next(0, 100);
        string rareName = "";
        string[] rareList = gameObject.GetComponent<Slot>().rareList;
        int[] rareChances = gameObject.GetComponent<Slot>().rareChances;
        int ifChance = 0;
        extraDescription = "";
        if (gameObject.GetComponent<Slot>().type != "Usable" && gameObject.GetComponent<Slot>().type != "Empty" && gameObject.GetComponent<Slot>().type != "Scroll")
            for (int k = 0; k < rareList.Length; k++)
            {
                ifChance += rareChances[k];
                if (rareChance < ifChance)
                {
                    rareName = rareList[k];
                    ifChance = 0;
                    break;
                }
                else continue;
            }
        gameObject.GetComponent<Slot>().rareName = rareName;
        PrefixChooser(rareName, gameObject.GetComponent<Slot>().values[2], gameObject);
        description += "<color=" + qualityColor + ">" + gameObject.GetComponent<Slot>().rareName + "</color>" + " " + gameObject.GetComponent<Slot>().itemDescription + "\r\n";
        gameObject.GetComponent<Slot>().values[2] += damageSummand;
        gameObject.GetComponent<Slot>().values[3] += iceDamageSummand;
        gameObject.GetComponent<Slot>().values[4] += igniteDamageSummand;
        gameObject.GetComponent<Slot>().values[5] += lightningDamageSummand;
        gameObject.GetComponent<Slot>().values[6] += poisonDamageSummand;
        gameObject.GetComponent<Slot>().values[7] += voidDamageSummand;
        gameObject.GetComponent<Slot>().values[8] += pureDamageSummand;
        gameObject.GetComponent<Slot>().values[1] += defenceSummand;
        gameObject.GetComponent<Slot>().values[9] += iceResistSummand;
        gameObject.GetComponent<Slot>().values[10] += igniteResistSummand;
        gameObject.GetComponent<Slot>().values[11] += lightningResistSummand;
        gameObject.GetComponent<Slot>().values[12] += poisonResistSummand;
        gameObject.GetComponent<Slot>().values[13] += voidResistSummand;
        gameObject.GetComponent<Slot>().values[0] += maxHpSummand;
        gameObject.GetComponent<Slot>().values[14] += evasionChanceSummand;
        gameObject.GetComponent<Slot>().values[15] += criticalChanceSummand;
        //
        gameObject.GetComponent<Slot>().values[16] += manaCostSummand;
        gameObject.GetComponent<Slot>().values[17] += weaponSizeSummand;
        gameObject.GetComponent<Slot>().values[18] += attackSpeedSummand;
        gameObject.GetComponent<Slot>().values[19] += secondUsageChanceSummand;
        gameObject.GetComponent<Slot>().values[20] += tripleAttackChanceSummand;
        gameObject.GetComponent<Slot>().values[21] += explChanceSummand;
        gameObject.GetComponent<Slot>().values[22] = explTypeEqualer;
        gameObject.GetComponent<Slot>().values[23] += weaponCooldownSummand;
        gameObject.GetComponent<Slot>().values[24] += createProjectileChanceSummand;
        gameObject.GetComponent<Slot>().values[25] += spikes;
        gameObject.GetComponent<Slot>().values[29] += inscSummand;
        for (int k = 0; k < gameObject.GetComponent<Slot>().values.Length; k++)
            if (gameObject.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30 && k != 31 && k != 32)
                description += gameObject.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + gameObject.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

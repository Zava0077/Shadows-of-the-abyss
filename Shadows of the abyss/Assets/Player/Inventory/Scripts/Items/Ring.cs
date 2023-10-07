using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : Jewelery
{
    public float attackSpeed;
    public float castSpeed;
    public float regenHP;
    public float regenMP;
    // Start is called before the first frame update
    void Awake()
    {
        //мана, хп, аттакаспид, кастспид, реген’п, мана–еген
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
        gameObject.GetComponent<Slot>().values[0] += hpAdd + maxHpSummand;
        gameObject.GetComponent<Slot>().values[39] += manaAdd+ maxManaSummand;//+ criticalChanceSummand;
        gameObject.GetComponent<Slot>().values[18] += attackSpeed + attackSpeedSummand;
        gameObject.GetComponent<Slot>().values[37] += castSpeed + baseCastSpeedSummand;
        gameObject.GetComponent<Slot>().values[40] += regenHP + hpRegenSummand;
        gameObject.GetComponent<Slot>().values[9] += baseIceResistSummand + iceResistSummand;
        gameObject.GetComponent<Slot>().values[10] += baseIgniteResistSummand + igniteResistSummand;
        gameObject.GetComponent<Slot>().values[11] += baseLightningResistSummand + lightningResistSummand;
        gameObject.GetComponent<Slot>().values[12] += basePoisonResistSummand + poisonResistSummand;
        gameObject.GetComponent<Slot>().values[13] += baseVoidResistSummand + voidResistSummand;

        //
        for (int k = 0; k < gameObject.GetComponent<Slot>().values.Length; k++)
            if (gameObject.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30 && k != 31 && k != 32)
                description += gameObject.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + gameObject.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
        gameObject.GetComponent<Slot>().itemDescription += description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

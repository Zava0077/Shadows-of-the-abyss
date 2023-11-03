using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Breastplate : Armour
{
    
    void Awake()
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
        {
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
            switch (rnd.Next(0, 4))
            {
                case 0:
                    Common common = gameObject.AddComponent<Common>();
                    common.RarityStats();
                    break;
                case 1:
                    Uncommon uncommon = gameObject.AddComponent<Uncommon>();
                    uncommon.RarityStats();
                    break;
                case 2:
                    Rare rare = gameObject.AddComponent<Rare>();
                    rare.RarityStats();
                    break;
                case 3:
                    Epic epic = gameObject.AddComponent<Epic>();
                    epic.RarityStats();
                    break;
                case 4:
                    Legendary legendary = gameObject.AddComponent<Legendary>();
                    legendary.RarityStats();
                    break;
            }
        }
        gameObject.GetComponent<Slot>().rareName = rareName;
        PrefixChooser(rareName, gameObject.GetComponent<Slot>().values[2], gameObject);
        description += "<color=" + qualityColor + ">" + gameObject.GetComponent<Slot>().rareName + "</color>" + " " + gameObject.GetComponent<Slot>().itemDescription + "\r\n";
        var rarity = new Rarity();

        for (int i = 0; i < 6; i++)
        {
            if (rarity == null)
                switch (i)
                {
                    case 0:
                        rarity = GetComponent<Common>();
                        break;
                    case 1:
                        rarity = GetComponent<Uncommon>();
                        break;
                    case 2:
                        rarity = GetComponent<Rare>();
                        break;
                    case 3:
                        rarity = GetComponent<Epic>();
                        break;
                    case 4:
                        rarity = GetComponent<Legendary>();
                        break;
                    case 5:
                        rarity = GetComponent<Unique>();
                        break;
                }
        }
        float[] _properties = new float[rarity.propertiesNum];

        for (int i = 0; i < _properties.Length; i++)
        {
            int num = rnd.Next(1, 7); //7 is number of parameters down bellow;
            _properties[i] = !_properties.Contains(num) ? num : rnd.Next(1, 100) < 50 ? rnd.Next(num, 7) : rnd.Next(1, num);
        }
        gameObject.GetComponent<Slot>().values[1] += defence + defenceSummand + rarity.defenceRare;
        gameObject.GetComponent<Slot>().values[0] += maxHP != 0 || _properties.Contains(1) ? maxHPSummand + rarity.maxHPRare : 0;
        gameObject.GetComponent<Slot>().values[29] += incripSlots + inscSummand + rarity.inscriptionAdded;
        gameObject.GetComponent<Slot>().values[10] += igniteResist != 0 || _properties.Contains(2) ? igniteResistSummand + rarity.igniteResistRare : 0;
        gameObject.GetComponent<Slot>().values[9] += iceResist != 0 || _properties.Contains(3) ? iceResistSummand + rarity.iceResistRare : 0;
        gameObject.GetComponent<Slot>().values[11] += lightningResist != 0 || _properties.Contains(4) ? lightningResistSummand + rarity.lightningResistRare : 0;
        gameObject.GetComponent<Slot>().values[12] += poisonResist != 0 || _properties.Contains(5) ? poisonResistSummand + rarity.poisonResistRare : 0;
        gameObject.GetComponent<Slot>().values[14] += evasionChance != 0 || _properties.Contains(6) ? evasionChanceSummand + rarity.evasionChanceRare : 0;
        gameObject.GetComponent<Slot>().values[13] += voidResist != 0 || _properties.Contains(7) ? voidResistSummand + rarity.voidResistRare : 0;
        //
        for (int k = 0; k < gameObject.GetComponent<Slot>().values.Length; k++)
            if (gameObject.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30 && k != 31 && k != 32)
                description += gameObject.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + gameObject.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
        gameObject.GetComponent<Slot>().itemDescription = rarity.rarityName + " " + description;
    }
}

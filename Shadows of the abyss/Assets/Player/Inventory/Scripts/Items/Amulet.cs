using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Amulet : Jewelery
{
    public float globalCrit;
    public float maxMPPercent;
    // Start is called before the first frame update
    void Awake()
    {
        System.Random rnd = new System.Random();
        string[] floats = new string[] {"maxHP","maxHPPercent","globalCrit","luck", "igniteResist","iceResist","lightningResist","poisonResist","evasionResist","voidResist"};
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
            int num = rnd.Next(1, floats.Length); //8 is number of parameters down bellow;
            _properties[i] = !_properties.Contains(num) ? num : rnd.Next(1, 100) < 50 ? rnd.Next(num, 10) : rnd.Next(1, num);
        }
        int[] offset = new int[floats.Length];
        //
        FieldInfo[] rarityFields = typeof(Rarity).GetFields();
        FieldInfo[] itemFields = typeof(Amulet).GetFields();
        FieldInfo[] prefixFields = typeof(Prefixes).GetFields();
        string[] itemFieldNames = new string[itemFields.Length];
        string[] prefixFieldNames = new string[prefixFields.Length];
        for (int i = 0; i < prefixFields.Length; i++)
            prefixFieldNames[i] = prefixFields[i].Name;
        for (int i = 0; i < itemFields.Length; i++)
            itemFieldNames[i] = itemFields[i].Name;

        for (int i = 0; i < floats.Length; i++)
        {
            if (_properties.Contains(i) && itemFieldNames.Contains(floats[i]))
            {
                offset[i] = 1;
                typeof(Amulet).GetField(floats[i]).SetValue(this, Convert.ToInt32(typeof(Amulet).GetField(floats[i]).GetValue(this)) + offset[i]);
            }
        }
        //мана, хп, крит%, мана%.
        gameObject.GetComponent<Slot>().values[0] += maxHP != 0 ? maxHPSummand + (offset[0] != 0 ? rarity.maxHPRare : 0) - offset[0] : 0;
        gameObject.GetComponent<Slot>().values[39] += maxMP != 0 ? maxMPSummand + (offset[1] != 0 ? rarity.maxMPRare : 0) - offset[1] : 0;
        gameObject.GetComponent<Slot>().values[43] += maxMPPercent != 0 ? maxMPPercentSummand + (offset[2] != 0 ? rarity.maxMPPercentRare : 0) - offset[2] : 0;
        gameObject.GetComponent<Slot>().values[15] += globalCrit != 0 ? globalCritSummand + (offset[3] != 0 ? rarity.globalCritRare : 0) - offset[3] : 0;
        gameObject.GetComponent<Slot>().values[46] += luck != 0 ? luckSummand - greed + (offset[4] != 0 ? rarity.luckRare : 0) - offset[4] : 0;//сделать профеку на свойство Greedy
        gameObject.GetComponent<Slot>().values[10] += igniteResist != 0 ? igniteResistSummand + (offset[5] != 0 ? rarity.igniteResistRare : 0) - offset[5] : 0;
        gameObject.GetComponent<Slot>().values[9] += iceResist != 0 ? iceResistSummand + (offset[6] != 0 ? rarity.iceResistRare : 0) - offset[6] : 0;
        gameObject.GetComponent<Slot>().values[11] += lightningResist != 0 ? lightningResistSummand + (offset[7] != 0 ? rarity.lightningResistRare : 0) - offset[7] : 0;
        gameObject.GetComponent<Slot>().values[12] += poisonResist != 0 ? poisonResistSummand + (offset[8] != 0 ? rarity.poisonResistRare : 0) - offset[8] : 0;
        gameObject.GetComponent<Slot>().values[14] += evasionChance != 0 ? evasionChanceSummand + (offset[9] != 0 ? rarity.evasionChanceRare : 0) - offset[9] : 0;
        gameObject.GetComponent<Slot>().values[13] += voidResist != 0 ? voidResistSummand + (offset[0] != 0 ? rarity.voidResistRare : 0) - offset[10] : 0;
        //
       
        //for (int k = 0; k < gameObject.GetComponent<Slot>().values.Length; k++)
        //    if (gameObject.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30 && k != 31 && k != 32)
        //        description += gameObject.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + gameObject.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
      
        
        foreach (FieldInfo field1 in itemFields)
            if (field1.ToString().StartsWith("System.Single") && !prefixFieldNames.Contains(field1.Name) && (float)field1.GetValue(gameObject.GetComponent<Amulet>()) != 0)
                description += field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Amulet>())) + "</color>" + "</b>" + "\r\n";
        description += "-----Gained by Rarity-----\r\n";
        foreach (FieldInfo field1 in rarityFields)
            if (field1.ToString().StartsWith("System.Single") && itemFieldNames.Contains(field1.Name[..^4]) && (float)((typeof(Amulet).GetField(field1.Name[..^4])).GetValue(gameObject.GetComponent<Amulet>())) != 0)
                description += (float)field1.GetValue(gameObject.GetComponent<Rarity>()) != 0 ? field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Rarity>())) + "</color>" + "</b>" + "\r\n" : "";
            else Debug.Log("Не вышло добавить свойство равное нулю.");
        description += "-----Gained by Prefix-----\r\n";
        foreach (FieldInfo field1 in prefixFields)
            if (field1.ToString().StartsWith("System.Single") && itemFieldNames.Contains(field1.Name[..^7]) && (float)((typeof(Amulet).GetField(field1.Name[..^7])).GetValue(gameObject.GetComponent<Amulet>())) != 0)
                description += (float)field1.GetValue(gameObject.GetComponent<Prefixes>()) != 0 ? field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Prefixes>())) + "</color>" + "</b>" + "\r\n" : "";
            else Debug.Log("Не вышло добавить свойство равное нулю.");
        gameObject.GetComponent<Slot>().itemDescription = rarity.rarityName + " " + description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Equipment : Prefixes
{
    public FieldInfo[] rarityFields = typeof(Rarity).GetFields();
    public FieldInfo[] prefixFields = typeof(Prefixes).GetFields();
    public FieldInfo[] itemFields;
    Equipment _item;
    string[] floats; 
    public float icreaseAllDamage;
    public float inscSlots;
    public void EquipmentConstructor(string[] floats)
    {
        this.floats = floats;
        EquipmentAwake();
    }
    public Dictionary<string, int> links = new Dictionary<string, int>//Перенести словарь в класс Equipment со всеми ссылками
        {
            {"maxHP",0},
            {"defence",1},
            {"attackDamage",2},
            {"iceDamage",3},
            {"igniteDamage",4},
            {"lightningDamage",5},
            {"poisonDamage",6},
            {"voidDamage",7},
            {"pureDamage",8},
            {"iceResist",9},
            {"igniteResist",10},
            {"lightningResist",11},
            {"poisonResist",12},
            {"voidResist",13},
            {"evasionChance",14},
            {"globalCrit",15},
            {"attackSpeed",18},
            {"inscSlots",29},
            {"globalCritMulti",35},
            {"castDamage",36},
            {"castSpeed",37},
            {"castCrit",38},
            {"maxMP",39},
            {"maxHPPercent",42},
            {"maxMPPercent",43},
            {"projSpeed",44},
            {"moveSpeed",45},
            {"luck",46},
        };
    public Dictionary<string, string> descriptionLayersExamples = new Dictionary<string, string>
        {
            {"maxHP","Increases max HP "},
            {"defence","Increases defence "},
            {"attackDamage","Increases damage from attacks by "},
            {"iceDamage","Increases damage from ice by "},
            {"igniteDamage","Increases damage from ignite by "},
            {"lightningDamage","Increases damage from lightning by "},
            {"poisonDamage","Increases poisoning damage by "},
            {"voidDamage","Increases void damage by "},
            {"pureDamage","Increases amount of pure damage by "},
            {"iceResist","Increases resistance to ice by "},
            {"igniteResist","Increases resistance to ignite by "},
            {"lightningResist","Increases resistance to lightning by "},
            {"poisonResist","Increases resistance to poisoning by "},
            {"voidResist","Increases resistance to all voids by "},
            {"evasionChance","Increases chance to dodge attacks "},
            {"globalCrit","Increases global chance of critical attack by "},
            {"attackSpeed","Increases attack speed by "},
            {"inscSlots","Gives additional slots for inscriptions: "},
            {"globalCritMulti","Increases global crit damage "},
            {"castDamage","Increases damage from cast by "},
            {"castSpeed","Increases cast speed by "},
            {"castCrit","Increases chance to deal more damage from cast by "},
            {"maxMP","Increases max MP "},
            {"maxHPPercent","Multiplies max HP by "},
            {"maxMPPercent","Multiplies max MP by "},
            {"projSpeed","Increases projectile speed by "},
            {"moveSpeed","Increases your speed by "},
            {"luck","Increases your luckiness by "},
        };
    public void EquipmentAwake()
    {
        System.Random rnd = new System.Random();
        
        string[] prefixFieldNames = new string[prefixFields.Length];
        string[] itemFieldNames = new string[floats.Length];
        string[] rareList = gameObject.GetComponent<Slot>().rareList;
        string rareName = "";
        string description = "";
        int[] offset = new int[floats.Length];
        int[] rareChances = gameObject.GetComponent<Slot>().rareChances;
        int rareChance = rnd.Next(0, 100);
        int ifChance = 0;
        

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
        }
        extraDescription = "";
        gameObject.GetComponent<Slot>().rareName = rareName;
        PrefixChooser(rareName, gameObject.GetComponent<Slot>().values[2], gameObject);
        description += "<color=" + qualityColor + ">" + gameObject.GetComponent<Slot>().rareName + "</color>" + " " + gameObject.GetComponent<Slot>().itemDescription + "\r\n";
        float[] _properties = new float[RarityClass().propertiesNum];
        for (int i = 0; i < _properties.Length; i++)
        {
            int num = rnd.Next(1, floats.Length);
            _properties[i] = !_properties.Contains(num) ? num : rnd.Next(1, 100) < 50 ? rnd.Next(num, 10) : rnd.Next(1, num);
        }
        for (int i = 0; i < prefixFields.Length; i++)
            prefixFieldNames[i] = prefixFields[i].Name;
        for (int i = 0, k = 0; i < itemFieldNames.Length; i++, k++)
            if (itemFields[k].Name != "greed")
                itemFieldNames[i] = itemFields[k].Name;
            else i--;
        foreach (FieldInfo field1 in itemFields)
            if (field1.ToString().StartsWith("System.Single") && floats.Contains(field1.Name) && (float)field1.GetValue(_item) != 0)
                description += field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(_item)) + "</color>" + "</b>" + "\r\n";
        for (int i = 0; i < floats.Length; i++)
        {
            if ((_properties.Contains(i) && itemFieldNames.Contains(floats[i])) || prefixedStats.ContainsKey(floats[i] + "Prefixed"))
            {
                offset[i] = 1;
                _item.GetType().GetField(floats[i]).SetValue(this, Convert.ToInt32(_item.GetType().GetField(floats[i]).GetValue(_item)) + offset[i]);//добавить в словарь для дальнейшего вывода в описание
            }
        }
        for (int i = 0; i < floats.Length; i++)//если строка не гарантирована, но дарована префиксом, даётся в полной мере, если гарантирована, то не суммируется с полным баффом от префикса.
            gameObject.GetComponent<Slot>().values[links[floats[i]]] += (float)_item.GetType().GetField(floats[i]).GetValue(_item) != 0 ? (float)_item.GetType().GetField(floats[i]).GetValue(_item) + (float)_item.GetType().GetField(floats[i] + "Summand").GetValue(_item) + (offset[i] != 0 && !prefixedStats.ContainsKey(floats[i] + "Prefixed") ? (float)typeof(Rarity).GetField(floats[i] + "Rare").GetValue(RarityClass()) : 0) - offset[i] : 0;
        description += "-----Gained by Prefix-----\r\n";
        foreach (FieldInfo field1 in prefixFields)
            if (field1.ToString().StartsWith("System.Single") && itemFieldNames.Contains(field1.Name[..^7]) && (float)((_item.GetType().GetField(field1.Name[..^7])).GetValue(_item)) != 0)
                description += (float)field1.GetValue(gameObject.GetComponent<Prefixes>()) != 0 ? field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Prefixes>())) + "</color>" + "</b>" + "\r\n" : "";
        description += "-----Gained by Rarity-----\r\n";
        foreach (FieldInfo field1 in rarityFields)
            if (field1.ToString().StartsWith("System.Single") && itemFieldNames.Contains(field1.Name[..^4]) && (float)((_item.GetType().GetField(field1.Name[..^4])).GetValue(_item)) != 0)
                description += (float)field1.GetValue(gameObject.GetComponent<Rarity>()) != 0 ? field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Rarity>())) + "</color>" + "</b>" + "\r\n" : "";
        description += "-----STATS-----\r\n";
        for (int i = 0; i < floats.Length; i++)
            if (gameObject.GetComponent<Slot>().values[links[floats[i]]] != 0)
                description += descriptionLayersExamples[floats[i]] + gameObject.GetComponent<Slot>().values[links[floats[i]]] + "\r\n";
        gameObject.GetComponent<Slot>().itemDescription = RarityClass().rarityName + " " + description;
    }
    public void CurrentItem(Equipment item)
    {
        _item = item;
        itemFields = _item.GetType().GetFields();
    }
    public Rarity RarityClass()
    {
        System.Random rnd = new System.Random();
        if (GetComponent<Unique>() == null)
        {
            switch (rnd.Next(0, 4))
            {
                case 0:
                    Common common = gameObject.AddComponent<Common>();
                    common.RarityStats();
                    return GetComponent<Common>();
                case 1:
                    Uncommon uncommon = gameObject.AddComponent<Uncommon>();
                    uncommon.RarityStats();
                    return GetComponent<Uncommon>();
                case 2:
                    Rare rare = gameObject.AddComponent<Rare>();
                    rare.RarityStats();
                    return GetComponent<Rare>();
                case 3:
                    Epic epic = gameObject.AddComponent<Epic>();
                    epic.RarityStats();
                    return GetComponent<Epic>();
                case 4:
                    Legendary legendary = gameObject.AddComponent<Legendary>();
                    legendary.RarityStats();
                    return GetComponent<Legendary>();
            }
        }
        return GetComponent<Unique>();
    }
}

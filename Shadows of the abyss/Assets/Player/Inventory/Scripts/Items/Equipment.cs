using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Transactions;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

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
    public Dictionary<string, int> links = new Dictionary<string, int>//��������� ������� � ����� Equipment �� ����� ��������
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

            {"maxHPPrefixed" ,"Grants max HP "},
            {"defencePrefixed","Grants defence "},
            {"attackDamagePrefixed","Grants damage from attacks by "},
            {"iceDamagePrefixed","Grants damage from ice by "},
            {"igniteDamagePrefixed","Grants damage from ignite by "},
            {"lightningDamagePrefixed","Grants damage from lightning by "},
            {"poisonDamagePrefixed","Grants poisoning damage by "},
            {"voidDamagePrefixed","Grants void damage by "},
            {"pureDamagePrefixed","Grants amount of pure damage by "},
            {"iceResistPrefixed","Grants resistance to ice by "},
            {"igniteResistPrefixed","Grants resistance to ignite by "},
            {"lightningResistPrefixed","Grants resistance to lightning by "},
            {"poisonResistPrefixed","Grants resistance to poisoning by "},
            {"voidResistPrefixed","Grants resistance to all voids by "},
            {"evasionChancePrefixed","Grants chance to dodge attacks "},
            {"globalCritPrefixed","Grants global chance of critical attack by "},
            {"attackSpeedPrefixed","Grants attack speed by "},
            {"inscSlotsPrefixed","Grants additional slots for inscriptions: "},
            {"globalCritMultiPrefixed","Grants global crit damage "},
            {"castDamagePrefixed","Grants damage from cast by "},
            {"castSpeedPrefixed","Grants cast speed by "},
            {"castCritPrefixed","Grants chance to deal more damage from cast by "},
            {"maxMPPrefixed","Grants max MP "},
            {"maxHPPercentPrefixed","Grants HP above yours by "},
            {"maxMPPercentPrefixed","Grants MP above yours by "},
            {"projSpeedPrefixed","Grants projectile speed by "},
            {"moveSpeedPrefixed","Grants your speed by "},
            {"luckPrefixed","Grants your luckiness by "},
        };
    public void EquipmentAwake()
    {
        System.Random rnd = new System.Random();

        string[] prefixFieldNames = new string[prefixFields.Length];
        string[] rarityFieldNames = new string[rarityFields.Length];
        string[] itemFieldNames = new string[floats.Length];
        string[] rareList = gameObject.GetComponent<Slot>().rareList;
        string rareName = "";
        string description = "";
        var rarity = RarityClass();
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
        
        for (int i = 0; i < prefixFields.Length; i++)
            prefixFieldNames[i] = prefixFields[i].Name;
        for (int i = 0; i < rarityFields.Length; i++)
            rarityFieldNames[i] = rarityFields[i].Name;
        for (int i = 0, k = 0; i < itemFieldNames.Length; i++, k++)
            if (itemFields[k].Name != "greed")
                itemFieldNames[i] = itemFields[k].Name;
            else i--;
        foreach (FieldInfo field1 in itemFields)
            if (field1.ToString().StartsWith("System.Single") && floats.Contains(field1.Name) && (float)field1.GetValue(_item) != 0)
                description += field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(_item)) + "</color>" + "</b>" + "\r\n";
        List<string> damages = new List<string>();
        List<string> resists = new List<string>();
        List<string> newFloats = new List<string>();       
        int count = 0;
        int phase = 0;
        int _num = RarityClass().propertiesTier > 2 ? 2 : 1;
        int id = 0;
        int _j = 0;
        while (true)
        {
            if (phase == 0)
            {
                if (id >= prefixedStats.Count)
                    phase++;
                    for(; id < prefixedStats.Count; id++, _j++)
                        for (int i = 0; i < floats.Length; i++)
                            if (floats[i] == prefixedStats.ElementAt(_j).Key[..^8])
                            {
                                id = i;
                                break;
                            }
                id--;
                if (_j > prefixedStats.Count)
                    phase++;
            }
            if (phase == 1) id = rnd.Next(0, floats.Length - 1);
            if (count == _num) break;
            if (!damages.Contains(floats[id]) && floats[id].Contains("Damage")) damages.Add(floats[id]);
            else if (!resists.Contains(floats[id]) && (floats[id].Contains("Resist") || floats[id] == "evasionChance")) resists.Add(floats[id]);
            else continue;

            if (!prefixedStats.ContainsKey(floats[id] + "Prefixed"))
                count++;
        }
        foreach (string _float in floats)
        {
            if ((_float.Contains("Damage") && !damages.Contains(_float)) || (_float.Contains("Resist") || _float == "evasionChance") && !resists.Contains(_float)) continue;
            newFloats.Add(_float);
        }
        float[] _properties = new float[rarity.propertiesNum];
        List<int> ints = new List<int>();
        for (int i = 0; i < newFloats.Count; i++)
            ints.Add(i);
        for (int i = 0; i < _properties.Length; i++) //��� �� �����
        {
            int num = rnd.Next(1, ints.Count - 1);
            _properties[i] = ints[num];
            ints.Remove(ints[num]);
        }
        int[] offset = new int[newFloats.Count];
        int[] rareOffset = new int[newFloats.Count];
        int[] prefixOffset = new int[newFloats.Count];
        for (int i = 0; i < newFloats.Count; i++)
        {
            if (_properties.Contains(i) && itemFieldNames.Contains(newFloats[i]))
                rareOffset[i] = 1; //�������� ��������� ������ ��� �������� � ������ || ���: ������� ������ � �������������� � �� ���� �� �� ����������
            if (prefixedStats.ContainsKey(newFloats[i] + "Prefixed"))
                prefixOffset[i] = 1;
        }
        List<string> rarityDesc = new List<string>();
        List<string> prefixDesc = new List<string>();
        for (int i = 0; i < newFloats.Count; i++)//���� ������ �� �������������, �� �������� ���������, ����� � ������ ����, ���� �������������, �� �� ����������� � ������ ������ �� ��������.
        {
            gameObject.GetComponent<Slot>().values[links[newFloats[i]]] += (float)_item.GetType().GetField(newFloats[i]).GetValue(_item) != 0 || offset[i] != 0 || rareOffset[i] != 0 || prefixOffset[i] != 0 ? (float)_item.GetType().GetField(newFloats[i]).GetValue(_item) + (prefixOffset[i] != 0 || (float)_item.GetType().GetField(newFloats[i] + "Summand").GetValue(_item) != 0 ? (float)_item.GetType().GetField(newFloats[i] + "Summand").GetValue(_item) : 0) + (rareOffset[i] != 0 ? (float)typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity) : 0) : 0;
            if (gameObject.GetComponent<Slot>().values[links[newFloats[i]]] != 0)
            {
                if (/*rarityFieldNames.Contains(newFloats[i] + "Rare")  && */rareOffset[i] != 0 && (float)typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity) != 0)
                    rarityDesc.Add(descriptionLayersExamples[newFloats[i]] + Convert.ToString(typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity)) + "\r\n");       
                if (/*prefixFieldNames.Contains(newFloats[i] + "Summand") &&*/ (float)typeof(Prefixes).GetField(newFloats[i] + "Summand").GetValue(gameObject.GetComponent<Prefixes>()) != 0)
                    prefixDesc.Add(descriptionLayersExamples[newFloats[i]] + Convert.ToString(typeof(Prefixes).GetField(newFloats[i] + "Summand").GetValue(gameObject.GetComponent<Prefixes>())) + "\r\n");
            }  
        }
        if(prefixDesc.Count != 0)
        {
            description += $"<color={qualityColor}>------------------{rareName}-----------------</color>\r\n";
                 foreach (string prefix in prefixDesc)
                       description += prefix;
        }
        if (rarityDesc.Count != 0)
        {
            description += $"-------------{rarity.ToString().Split(" ")[1].Replace("(","").Replace(")", "")}-------------\r\n";
            foreach (string _rarity in rarityDesc)
                description += _rarity;
        }
        description += "-----STATS-----\r\n";
        for (int i = 0; i < floats.Length; i++)
            if (gameObject.GetComponent<Slot>().values[links[floats[i]]] != 0)
                description += descriptionLayersExamples[floats[i]] + gameObject.GetComponent<Slot>().values[links[floats[i]]] + "\r\n";
        gameObject.GetComponent<Slot>().itemDescription = rarity.rarityName + " " + description;
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

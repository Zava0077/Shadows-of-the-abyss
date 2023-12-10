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
            {"regenHP",40},
            {"regenMP",41},
        };
    public Dictionary<string, string> descriptionLayersExamples = new Dictionary<string, string> 
        {
            {"maxHP","Увеличивает макс хп на "},
            {"defence","Увеличивает защиту на "},
            {"attackDamage","Увеличивает урон от атак на "},
            {"iceDamage","Увеличивает урон льдом на "},
            {"igniteDamage","Увеличивает урон огнём на "},
            {"lightningDamage","Увеличивает урон молнией на "},
            {"poisonDamage","Увеличивает урон ядом на "},
            {"voidDamage","Увеличивает пустотный урон на "},
            {"pureDamage","Увеличивает чистый урон на "},
            {"iceResist","Увеличивает сопротивление ко льду на "},
            {"igniteResist","Увеличивает сопротивление к огню на "},
            {"lightningResist","Увеличивает сопротивление к току на "},
            {"poisonResist","Увеличивает сопротивление к яду на "},
            {"voidResist","Увеличивает сопротивление ко всем пустотам на "},
            {"evasionChance","Увеличивает шанс уклонится на "},
            {"globalCrit","Увеличивает глобальный шанс крита "},
            {"attackSpeed","Увеличивает скорость атаки "},
            {"inscSlots","Дополнительные слоты надписей: "},
            {"globalCritMulti","Увеличивает глобальный урон крита "},
            {"castDamage","Увеличивает урон чар на "},
            {"castSpeed","Увеличивает скорость сотворения чар на "},
            {"castCrit","Увеличивает шанс сотворить чудо на "},
            {"maxMP","Увеличивает макс ману на "},
            {"maxHPPercent","Множитель макс хп "},
            {"maxMPPercent","Множитель макс мп "},
            {"projSpeed","Увеличивает скорость снарядов на "},
            {"moveSpeed","Увеличивает скорость передвижения на "},
            {"luck","Увеличивает вашу удачу на "},
            {"regenHP","Увеличивает вашу регенерацию ХП на "},
            {"regenMP","Увеличивает вашу регенерацию МП на "},

            {"maxHPPrefixed" ,"Гарантирует максимального хп "},
            {"defencePrefixed","Гарантирует защиты "},
            {"attackDamagePrefixed","Гарантирует урона атаками "},
            {"iceDamagePrefixed","Гарантирует урона льдом "},
            {"igniteDamagePrefixed","Гарантирует урона огнём "},
            {"lightningDamagePrefixed","Гарантирует урона током "},
            {"poisonDamagePrefixed","Гарантирует урона ядом "},
            {"voidDamagePrefixed","Гарантирует урона пустотой "},
            {"pureDamagePrefixed","Гарантирует чистого урон "},
            {"iceResistPrefixed","Гарантирует сопротивления к льду "},
            {"igniteResistPrefixed","Гарантирует сопротивления к огню "},
            {"lightningResistPrefixed","Гарантирует сопротивления к току "},
            {"poisonResistPrefixed","Гарантирует сопротивления к ядам "},
            {"voidResistPrefixed","Гарантирует сопротивления к всем пустотам "},
            {"evasionChancePrefixed","Гарантирует шанса уворота "},
            {"globalCritPrefixed","Гарантирует глобального шанс крита "},
            {"attackSpeedPrefixed","Гарантирует скорости атаки "},
            {"inscSlotsPrefixed","Гарантированных слотов зачарований: "},
            {"globalCritMultiPrefixed","Гарантирует дополнительного урона критом "},
            {"castDamagePrefixed","Гарантирует урона чарами "},
            {"castSpeedPrefixed","Гарантирует скорости сотворения чар "},
            {"castCritPrefixed","Гарантирует шанса сотворить чудо "},
            {"maxMPPrefixed","Гарантирует сверх маны "},
            {"maxHPPercentPrefixed","Гарантирует множителя хп "},
            {"maxMPPercentPrefixed","Гарантирует множителя мп "},
            {"projSpeedPrefixed","Гарантирует скорости снарядов "},
            {"moveSpeedPrefixed","Гарантирует скорости перемещения "},
            {"luckPrefixed","Гарантирует удачи "},
            {"regenHPPrefixed","Гарантирует регенерации ХП на "},
            {"regenMPPrefixed","Гарантирует регенерации МП на "},
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
                description += descriptionLayersExamples[field1.ToString().TrimStart("System.Single ")] + field1.GetValue(_item) + "\r\n"; 
        List<string> damages = new List<string>();
        List<string> resists = new List<string>();
        List<string> newFloats = new List<string>();       
        int count = 0;
        int phase = 0;
        int _num = rarity.propertiesTier > 2 ? 2 : 1;
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
        float[] _properties = new float[rarity.propertiesNum > newFloats.Count ? newFloats.Count : rarity.propertiesNum];
        List<int> ints = new List<int>();
        for (int i = 0; i < newFloats.Count; i++) 
            ints.Add(i);
        for (int i = 0; i < _properties.Length; i++) //was properties;
        {
            int num = rnd.Next(0, ints.Count - 1);
            _properties[i] = ints[num];
            ints.Remove(ints[num]);
        }
        bool[] offset = new bool[newFloats.Count];
        bool[] rareOffset = new bool[newFloats.Count];
        bool[] prefixOffset = new bool[newFloats.Count];
        for (int i = 0; i < newFloats.Count; i++)
        {
            if (_properties.Contains(i) && itemFieldNames.Contains(newFloats[i]))
                rareOffset[i] = true; //добавить отдельный оффсет для префикса и рарити || БАГ: ПРЕФИКС УХОДИТ В НЕСУЩЕСТВУЮЩИЕ И НЕ ДАЁТ ИХ НА РЕЗУЛЬТАТЕ
            if (prefixedStats.ContainsKey(newFloats[i] + "Prefixed"))
                prefixOffset[i] = true;
        }
        List<string> rarityDesc = new List<string>();
        List<string> prefixDesc = new List<string>();
        for (int i = 0; i < newFloats.Count; i++)//если строка не гарантирована, но дарована префиксом, даётся в полной мере, если гарантирована, то не суммируется с полным баффом от префикса.
        {
            gameObject.GetComponent<Slot>().values[links[newFloats[i]]] += (float)_item.GetType().GetField(newFloats[i]).GetValue(_item) + 
                (rareOffset[i] && rarityFieldNames.Contains(newFloats[i] + "Rare") ? (float)typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity) : 0)
                + (prefixOffset[i] || (float)_item.GetType().GetField(newFloats[i]).GetValue(_item) != 0 ? prefixedStats.ContainsKey(newFloats[i]+"Prefixed") ? prefixedStats[newFloats[i]+"Prefixed"] : (float)_item.GetType().GetField(newFloats[i] + "Summand").GetValue(_item) : 0);
            if (gameObject.GetComponent<Slot>().values[links[newFloats[i]]] != 0)
            {
                if (rareOffset[i] && (float)typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity) != 0)
                    rarityDesc.Add(descriptionLayersExamples[newFloats[i]] + Convert.ToString(typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity)) + "\r\n");
                if (prefixedStats.ContainsKey(newFloats[i]+"Prefixed"))
                {
                    prefixDesc.Add(descriptionLayersExamples[newFloats[i] + "Prefixed"] + prefixedStats[newFloats[i] + "Prefixed"] + "\r\n");
                    continue;
                }
                if ((float)typeof(Prefixes).GetField(newFloats[i] + "Summand").GetValue(gameObject.GetComponent<Prefixes>()) != 0)
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
        //description += "-----СТАТЫ-----\r\n";
        //for (int i = 0; i < floats.Length; i++)
        //    if (gameObject.GetComponent<Slot>().values[links[floats[i]]] != 0)
        //        description += descriptionLayersExamples[floats[i]] + gameObject.GetComponent<Slot>().values[links[floats[i]]] + "\r\n";
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

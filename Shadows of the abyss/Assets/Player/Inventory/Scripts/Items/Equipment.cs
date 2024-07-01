using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
    public Rarity rarityMemory;
    public string prefixMemory;
    public string descriptionMemory = "";
    public bool isTrinket;
    Equipment _item;
    string[] floats;
    public float icreaseAllDamage;
    public float inscSlots;
    List<string> damages = new List<string>(); //память о дамагах
    List<string> resists = new List<string>(); //память о резистах
    List<string> newFloats = new List<string>(); //память о наименований свойств
    List<string> rarityDesc = new List<string>();
    List<string> prefixDesc = new List<string>();
    List<int> ints = new List<int>(); //память о ID свойств
    float[] _properties = new float[0]; //память о распределении ид свойствам
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
            {"bloodyCoin",47}
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

            {"bloodyCoin","Вы наносите дополнительный урон за ваши монеты.\r\nВесь наносимый урон снижен на 50%"}
        };
    public void EquipmentAwake([Optional] Rarity _new, [Optional] GameObject def, string prefName = null) //продолжить здесь
    {
        System.Random rnd = new System.Random();
        string[] prefixFieldNames = new string[prefixFields.Length];
        string[] rarityFieldNames = new string[rarityFields.Length];
        string[] itemFieldNames = new string[floats.Length];
        string[] rareList = gameObject.GetComponent<Slot>().rareList;
        string rareName = "";
        string description = "";
        var rarity = _new ? _new : RarityClass();//если рарити был задан заранее, принимаем его
        rarityMemory = rarity;
        descriptionMemory = gameObject.GetComponent<Slot>().itemDescription.Split(' ').Length > 1 ? descriptionMemory : gameObject.GetComponent<Slot>().itemDescription; //если описание уже заполнено - не меняем, иначе - меняем.
        rarityDesc.Clear();
        prefixDesc.Clear();
        int rareChance = rnd.Next(0, 100);
        GetComponent<Slot>().defaultSlot = gameObject; //запоминаем заводские значения предмета НЕ ТО ЧЕ ЗА ХУЙНЯ 
        //префикс
        int[] rareChances = gameObject.GetComponent<Slot>().rareChances;
        int ifChance = 0;
        if (prefName != null)
            rareName = prefName;
        else
        if (gameObject.GetComponent<Slot>().type != "Usable" && gameObject.GetComponent<Slot>().type != "Empty" && gameObject.GetComponent<Slot>().type != "Scroll" && !isTrinket)
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
        prefixMemory = rareName;
        extraDescription = "";
        gameObject.GetComponent<Slot>().rareName = rareName;
        PrefixChooser(rareName, gameObject.GetComponent<Slot>().values[2], gameObject);
        description += (rareName != "" ? "<color=" + qualityColor + ">" + rareName + "</color> " : "") + descriptionMemory + "\r\n";
        //
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
                description += descriptionLayersExamples[field1.ToString().TrimStart("System.Single ")] + (!isTrinket ? field1.GetValue(_item) : "") + "\r\n";
        //рарити
        if(!_new)
        {
            //хз потом
        }
       
        int count = 0;
        int phase = 0;
        int _num = 0;
        if (!isTrinket)
            _num = rarity.propertiesTier > 2 ? 2 : 1;
        int id = 0;
        int _j = 0;
        while (true && !_new)
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
            if (!damages.Contains(floats[id]) && floats[id].Contains("Damage")) damages.Add(floats[id]);
            else if (!resists.Contains(floats[id]) && (floats[id].Contains("Resist") || floats[id] == "evasionChance")) resists.Add(floats[id]);
            else continue;

            if (!prefixedStats.ContainsKey(floats[id] + "Prefixed"))
                count++;
            if (count == _num || (damages.Count == 0 && resists.Count == 0)) break;

        }
        if (!_new)
            foreach (string _float in floats)
            {
                if ((_float.Contains("Damage") && !damages.Contains(_float)) || (_float.Contains("Resist") || _float == "evasionChance") && !resists.Contains(_float)) continue;
                newFloats.Add(_float);
            }
       
        if(!isTrinket && !_new)
            _properties = new float[rarity.propertiesNum > newFloats.Count ? newFloats.Count : rarity.propertiesNum];
        
        for (int i = 0; i < newFloats.Count && !_new; i++) 
            ints.Add(i);
        for (int i = 0; i < _properties.Length && !_new; i++) //was properties;
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
            if(!isTrinket)
                if (_properties.Contains(i) && itemFieldNames.Contains(newFloats[i]))
                    rareOffset[i] = true; //добавить отдельный оффсет для префикса и рарити || БАГ: ПРЕФИКС УХОДИТ В НЕСУЩЕСТВУЮЩИЕ И НЕ ДАЁТ ИХ НА РЕЗУЛЬТАТЕ
            if (prefixedStats.ContainsKey(newFloats[i] + "Prefixed"))
                prefixOffset[i] = true;
        }
        //описание
        if(def)//при наличии надобности сбрасывать предмет до нуля, он сбросится
            Defaulter(newFloats, def);
        for (int i = 0; i < newFloats.Count; i++)//если строка не гарантирована, но дарована префиксом, даётся в полной мере, если гарантирована, то не суммируется с полным баффом от префикса.
        {
            gameObject.GetComponent<Slot>().values[links[newFloats[i]]] += (float)_item.GetType().GetField(newFloats[i]).GetValue(_item);
            if(!isTrinket)//добавление численных значений в предмет
            {
                gameObject.GetComponent<Slot>().values[links[newFloats[i]]] +=
                (rareOffset[i] && rarityFieldNames.Contains(newFloats[i] + "Rare") ? (float)typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity) : 0)
                + (prefixOffset[i] || (float)_item.GetType().GetField(newFloats[i]).GetValue(_item) != 0 ? prefixedStats.ContainsKey(newFloats[i] + "Prefixed") ? prefixedStats[newFloats[i] + "Prefixed"] : (float)_item.GetType().GetField(newFloats[i] + "Summand").GetValue(_item) : 0);
                if ((float)typeof(Prefixes).GetField(newFloats[i] + "Summand").GetValue(gameObject.GetComponent<Prefixes>()) != 0 && !prefixedStats.ContainsKey(newFloats[i]+"Prefixed"))
                    prefixDesc.Add(descriptionLayersExamples[newFloats[i]] + Convert.ToString(typeof(Prefixes).GetField(newFloats[i] + "Summand").GetValue(gameObject.GetComponent<Prefixes>())) + "\r\n");
                else
                {
                    ;
                }
            }
            if (gameObject.GetComponent<Slot>().values[links[newFloats[i]]] != 0)//добавление этих значений в виде описания || БАГ : неверное условие на выписывание стата при уже существующем гарантированном стате. (выдаются статы правильно, а выписываются - нет)
            {
                if (rareOffset[i] && (float)typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity) != 0)
                    rarityDesc.Add(descriptionLayersExamples[newFloats[i]] + Convert.ToString(typeof(Rarity).GetField(newFloats[i] + "Rare").GetValue(rarity)) + "\r\n");
                if (prefixedStats.ContainsKey(newFloats[i]+"Prefixed"))
                {
                    prefixDesc.Add(descriptionLayersExamples[newFloats[i] + "Prefixed"] + prefixedStats[newFloats[i] + "Prefixed"] + "\r\n");
                    continue;
                }
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
    public void RarityChanger(GameObject def = null)
    {
        EquipmentAwake(RarityClass(), def, prefixMemory); //новый рарити, сброс, лочит префикс
    }
    public void PrefixChanger(GameObject def = null)
    {
        foreach(string prefStat in prefixedStats.Keys)
            gameObject.GetComponent<Slot>().values[links[prefStat[..^8]]] = 0;
        EquipmentAwake(rarityMemory, def);//лочит рарити и сбрасывает
    }
    void Defaulter(List<string> newFloats, GameObject def)
    {
        for (int i = 0; i < newFloats.Count; i++)//сброс всех данных до предмета с завода
            gameObject.GetComponent<Slot>().values[links[newFloats[i]]] = def.GetComponent<Slot>().values[links[newFloats[i]]];
    }

    public void CurrentItem(Equipment item)
    {
        _item = item;
        itemFields = _item.GetType().GetFields();
        foreach (FieldInfo field in itemFields)
            if (field.ToString().Contains("isTrinket"))
            {
                isTrinket = Convert.ToBoolean(field.GetValue(_item));
                break;
            }
    }
    public Rarity RarityClass()
    {
        System.Random rnd = new System.Random();
        if (isTrinket)
            return null;
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

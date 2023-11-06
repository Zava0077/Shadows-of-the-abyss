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
        FieldInfo[] rarityFields = typeof(Rarity).GetFields();
        FieldInfo[] itemFields = typeof(Amulet).GetFields();
        FieldInfo[] prefixFields = typeof(Prefixes).GetFields();
        var rarity = new Rarity();
        string[] floats = new string[] {"maxHP","maxMP","maxMPPercent","globalCrit", "luck", "igniteResist","iceResist","lightningResist","poisonResist","evasionChance","voidResist"};
        string[] itemFieldNames = new string[floats.Length];
        string[] prefixFieldNames = new string[prefixFields.Length];
        string[] rareList = gameObject.GetComponent<Slot>().rareList;
        string rareName = "";
        string description = "";
        int[] offset = new int[floats.Length];
        int[] rareChances = gameObject.GetComponent<Slot>().rareChances;
        int ifChance = 0;
        int rareChance = rnd.Next(0, 100);

        Dictionary<string, int> links = new Dictionary<string, int>//ѕеренести словарь в класс Equipment со всеми ссылками
        {
            {floats[0],0},
            {floats[1],39},
            {floats[2],43},
            {floats[3],15},
            {floats[4],46},
            {floats[5],10},
            {floats[6],9},
            {floats[7],11},
            {floats[8],12},
            {floats[9],14},
            {floats[10],13},
        };

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
            if (field1.ToString().StartsWith("System.Single") && floats.Contains(field1.Name) && (float)field1.GetValue(gameObject.GetComponent<Amulet>()) != 0)
                description += field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Amulet>())) + "</color>" + "</b>" + "\r\n";
        for (int i = 0; i < floats.Length; i++)
        {
            if ((_properties.Contains(i) && itemFieldNames.Contains(floats[i])) || prefixedStats.ContainsKey(floats[i] + "Prefixed"))
            {
                offset[i] = 1;
                typeof(Amulet).GetField(floats[i]).SetValue(this, Convert.ToInt32(typeof(Amulet).GetField(floats[i]).GetValue(this)) + offset[i]);
            }
        }
        float tsfg = (float)typeof(Rarity).GetField(floats[0] + "Rare").GetValue(rarity);
        for (int i = 0; i < floats.Length;i++)
            gameObject.GetComponent<Slot>().values[links[floats[i]]] += (float)typeof(Amulet).GetField(floats[i]).GetValue(this) != 0 ? (float)typeof(Amulet).GetField(floats[i]+"Summand").GetValue(this) + (offset[i] != 0 && !prefixedStats.ContainsKey(floats[i] + "Prefixed") ? (float)typeof(Rarity).GetField(floats[i]+"Rare").GetValue(rarity) : 0) - offset[0] : 0;
        
        description += "-----Gained by Rarity-----\r\n";
        foreach (FieldInfo field1 in rarityFields)
            if (field1.ToString().StartsWith("System.Single") && itemFieldNames.Contains(field1.Name[..^4]) && (float)((typeof(Amulet).GetField(field1.Name[..^4])).GetValue(gameObject.GetComponent<Amulet>())) != 0)
                description += (float)field1.GetValue(gameObject.GetComponent<Rarity>()) != 0 ? field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Rarity>())) + "</color>" + "</b>" + "\r\n" : "";
            else Debug.Log("Ќе вышло добавить свойство равное нулю.");
        description += "-----Gained by Prefix-----\r\n";
        foreach (FieldInfo field1 in prefixFields)
            if (field1.ToString().StartsWith("System.Single") && itemFieldNames.Contains(field1.Name[..^7]) && (float)((typeof(Amulet).GetField(field1.Name[..^7])).GetValue(gameObject.GetComponent<Amulet>())) != 0)
                description += (float)field1.GetValue(gameObject.GetComponent<Prefixes>()) != 0 ? field1.Name + ": <b>" + "<color=red>" + Convert.ToString((float)field1.GetValue(gameObject.GetComponent<Prefixes>())) + "</color>" + "</b>" + "\r\n" : "";
            else Debug.Log("Ќе вышло добавить свойство равное нулю.");
        gameObject.GetComponent<Slot>().itemDescription = rarity.rarityName + " " + description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

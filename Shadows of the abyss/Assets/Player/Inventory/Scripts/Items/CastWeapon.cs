using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CastWeapon : Weapon
{
    public float baseDamageToSpells;
    public float castSpeed;
    public float critToSpells;
    // Start is called before the first frame update
    void Awake()
    {
        //урон—пелами,спелспид,критыспелами,криты, дамагјтаками, атак—пид, критмульти
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
            int num = rnd.Next(1, 6); //11 is number of parameters down bellow;
            _properties[i] = !_properties.Contains(num) ? num : rnd.Next(1, 100) < 50 ? rnd.Next(num, 6) : rnd.Next(1, num);
        }
        gameObject.GetComponent<Slot>().values[36] += castDamage + castDamageSummand + rarity.castDamageRare;
        gameObject.GetComponent<Slot>().values[37] += castSpeed != 0 || _properties.Contains(1) ? castSpeed + castSpeedSummand + rarity.castSpeedRare : 0;
        gameObject.GetComponent<Slot>().values[38] += castCrit + castCritSummand + rarity.castCritRare;
        gameObject.GetComponent<Slot>().values[15] += globalCrit != 0 || _properties.Contains(2) ? globalCritSummand + rarity.globalCritRare : 0; //
        gameObject.GetComponent<Slot>().values[2] += attackDamage != 0 || _properties.Contains(3) ? attackDamageSummand + rarity.attackDamageRare : 0; //
        gameObject.GetComponent<Slot>().values[18] += attackSpeed != 0 || _properties.Contains(4) ? attackSpeedSummand + rarity.attackSpeedRare : 0; //
        gameObject.GetComponent<Slot>().values[35] += globalCritMulti != 0 || _properties.Contains(5) ? globalCritMultiSummand + rarity.globalCritMultiRare : 0;
        for (int tier = 0; tier < (rarity.rarityTier > 2 ? 1 : rarity.rarityTier > 3 ? 2 : 0) && _properties.Contains(2); tier++)
            for (int i = 0; rarity.rarityTier > 2; i++)
            {
                if (Random.Range(0, 5) == 0) { gameObject.GetComponent<Slot>().values[3] += iceDamage != 0 ? iceDamageSummand + rarity.iceDamageRare : 0; break; }
                if (Random.Range(0, 5) == 1) { gameObject.GetComponent<Slot>().values[4] += igniteDamage != 0 ? igniteDamageSummand + rarity.igniteDamageRare : 0; break; }
                if (Random.Range(0, 5) == 2) { gameObject.GetComponent<Slot>().values[5] += lightningDamage != 0 ? lightningDamageSummand + rarity.lightningDamageRare : 0; break; }
                if (Random.Range(0, 5) == 3) { gameObject.GetComponent<Slot>().values[6] += poisonDamage != 0 ? poisonDamageSummand + rarity.poisonDamageRare : 0; break; }
                if (Random.Range(0, 5) == 4) { gameObject.GetComponent<Slot>().values[7] += voidDamage != 0 ? voidDamageSummand + rarity.voidDamageRare : 0; break; }
                if (Random.Range(0, 5) == 5) { gameObject.GetComponent<Slot>().values[8] += pureDamage != 0 ? pureDamageSummand + rarity.pureDamageRare : 0; break; }
            }
        gameObject.GetComponent<Slot>().values[29] += inscSlots + inscSlotsSummand + rarity.inscSlotsRare;
        //
        for (int k = 0; k < gameObject.GetComponent<Slot>().values.Length; k++)
            if (gameObject.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30 && k != 31 && k != 32)
                description += gameObject.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + gameObject.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
        gameObject.GetComponent<Slot>().itemDescription = rarity.rarityName + " " + description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

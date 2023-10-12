using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;
using static UnityEngine.ParticleSystem;

public class Bow : Weapon
{
    // Start is called before the first frame update
    void Awake()
    {
        //криты, дамагјтаками, атак—пид, критмульти
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
        //
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
        //
        float[] _properties = new float[rarity.propertiesNum];

        for(int i = 0; i<_properties.Length;i++)
        {
            int num = rnd.Next(1, 7); //7 is number of parameters down bellow;
            _properties[i] = !_properties.Contains(num) ? num : rnd.Next(1,100) < 50 ? rnd.Next(num,7) : rnd.Next(1, num);
        }
        gameObject.GetComponent<Slot>().values[15] += baseCrit + criticalChanceSummand + rarity.globalCrit + rarity.attackCrit;
        gameObject.GetComponent<Slot>().values[2] += baseDamageToAttack + damageSummand + rarity.attackDamage; //
        gameObject.GetComponent<Slot>().values[18] += baseAttackSpeed + attackSpeedSummand + rarity.attackSpeed; //

        gameObject.GetComponent<Slot>().values[35] += globalCritMulti != 0 || _properties.Contains(1) ? globalCritMulti + critMultiSummand + rarity.globalCritMulti : 0;
        gameObject.GetComponent<Slot>().values[3] += baseIceDamageSummand != 0 || _properties.Contains(2) ? baseIceDamageSummand + iceDamageSummand + rarity.iceDamage : 0;
        gameObject.GetComponent<Slot>().values[4] += baseIgniteDamageSummand != 0 || _properties.Contains(3) ? baseIgniteDamageSummand + igniteDamageSummand + rarity.igniteDamage : 0;
        gameObject.GetComponent<Slot>().values[5] += baseLightningDamageSummand != 0 || _properties.Contains(4) ? baseLightningDamageSummand + lightningDamageSummand + rarity.lightningDamage : 0;
        gameObject.GetComponent<Slot>().values[6] += basePoisonDamageSummand != 0 || _properties.Contains(5) ? basePoisonDamageSummand + poisonDamageSummand + rarity.poisonDamage : 0;
        gameObject.GetComponent<Slot>().values[7] += baseVoidDamageSummand != 0 || _properties.Contains(6) ? baseVoidDamageSummand + voidDamageSummand + rarity.voidDamage : 0;
        gameObject.GetComponent<Slot>().values[8] += basePureDamageSummand != 0 || _properties.Contains(7) ? basePureDamageSummand + pureDamageSummand + rarity.pureDamage : 0;
        gameObject.GetComponent<Slot>().values[29] += incripSlots + inscSummand + rarity.inscriptionAdded;
        //
        for (int k = 0; k < gameObject.GetComponent<Slot>().values.Length; k++)
            if (gameObject.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30 && k != 31 && k != 32)
                description += gameObject.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + gameObject.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
        gameObject.GetComponent<Slot>().itemDescription = rarity.rarityName+" "+description;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastWeapon : Weapon
{
    public float baseDamageToSpells;
    public float castSpeed;
    public float critToSpells;
    // Start is called before the first frame update
    void Awake()
    {
        //�����������,��������,������������,�����, ������������, ��������, ����������
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
        gameObject.GetComponent<Slot>().values[36] += baseDamageToSpells + baseSpellDamageSummand;
        gameObject.GetComponent<Slot>().values[37] += castSpeed + baseCastSpeedSummand;
        gameObject.GetComponent<Slot>().values[38] += critToSpells + baseSpellCritSummand;
        gameObject.GetComponent<Slot>().values[15] += baseCrit + criticalChanceSummand;
        gameObject.GetComponent<Slot>().values[2] += baseDamageToAttack + damageSummand;
        gameObject.GetComponent<Slot>().values[18] += baseAttackSpeed + attackSpeedSummand;
        gameObject.GetComponent<Slot>().values[35] += globalCritMulti + critMultiSummand;
        gameObject.GetComponent<Slot>().values[3] += baseIceDamageSummand + iceDamageSummand;
        gameObject.GetComponent<Slot>().values[4] += baseIgniteDamageSummand + igniteDamageSummand;
        gameObject.GetComponent<Slot>().values[5] += baseLightningDamageSummand + lightningDamageSummand;
        gameObject.GetComponent<Slot>().values[6] += basePoisonDamageSummand + poisonDamageSummand;
        gameObject.GetComponent<Slot>().values[7] += baseVoidDamageSummand + voidDamageSummand;
        gameObject.GetComponent<Slot>().values[8] += basePureDamageSummand + pureDamageSummand;
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

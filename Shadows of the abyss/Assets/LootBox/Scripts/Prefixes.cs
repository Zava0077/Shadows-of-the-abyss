using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Prefixes : MonoBehaviour
{
    float rememberDamage;
    public float damageSummand;
    public float iceDamageSummand;
    public float igniteDamageSummand;
    public float voidDamageSummand;
    public float pureDamageSummand;
    public float poisonDamageSummand;
    public float lightningDamageSummand;
    public float iceResistSummand;
    public float igniteResistSummand;
    public float voidResistSummand;
    public float pureResistSummand;
    public float poisonResistSummand;
    public float lightningResistSummand;
    public float criticalChanceSummand;
    public float evasionChanceSummand;
    public float maxHpSummand;
    public float defenceSummand;
    public string extraDescription = "";
    public static Prefixes self;
    public Prefixes()
    {
        self = this;
    }
    public void PrefixChooser(string prefix, float rememberDamage, GameObject item)
    {
        switch (prefix)
        {
            case "Distructive":
                damageSummand = 5;
                iceDamageSummand = 0;
                igniteDamageSummand = 0;
                voidDamageSummand = 0;
                pureDamageSummand = 0;
                poisonDamageSummand = 0;
                lightningResistSummand = 0;
                iceResistSummand = 0;
                igniteResistSummand = 0;
                voidResistSummand = 0;
                pureResistSummand = 0;
                poisonResistSummand = 0;
                lightningResistSummand = 0;
                maxHpSummand = 0;
                defenceSummand = 0;
                criticalChanceSummand = 10;
                break;
            case "Great":
                damageSummand = 10;
                iceDamageSummand = 0;
                igniteDamageSummand = 0;
                voidDamageSummand = 0;
                pureDamageSummand = 0;
                poisonDamageSummand = 0;
                lightningDamageSummand = 0;
                iceResistSummand = 0;
                igniteResistSummand = 0;
                voidResistSummand = 0;
                pureResistSummand = 0;
                poisonResistSummand = 0;
                lightningResistSummand = 0;
                maxHpSummand = 0;
                defenceSummand = 0;
                criticalChanceSummand = 5;
                break;
            case "Ignite":
                damageSummand = 0;
                iceDamageSummand = 0;
                igniteDamageSummand = rememberDamage + 5;
                voidDamageSummand = 0;
                pureDamageSummand = 0;
                poisonDamageSummand = 0;
                lightningDamageSummand = 0;
                iceResistSummand = 0;
                igniteResistSummand = 0;
                voidResistSummand = 0;
                pureResistSummand = 0;
                poisonResistSummand = 0;
                lightningResistSummand = 0;
                maxHpSummand = 0;
                defenceSummand = 0;
                criticalChanceSummand = 5;
                extraDescription = "Converts all physical damage into damage by fire";
                DamageDefaulter(item);
                break;
        }
    }
    void DamageDefaulter(GameObject item)
    {
        item.GetComponent<Slot>().damage = 0;
        item.GetComponent<Slot>().iceDamage = 0;
        item.GetComponent<Slot>().voidDamage = 0;
        item.GetComponent<Slot>().igniteDamage = 0;
        item.GetComponent<Slot>().pureDamage = 0;
        item.GetComponent<Slot>().lightningDamage = 0;
    }
}

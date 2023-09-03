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
    public int manaCostSummand;
    public int weaponSizeSummand;
    public int attackSpeedSummand;
    public int secondUsageChanceSummand;
    public int tripleAttackChanceSummand;
    public int explChanceSummand;
    public int explTypeEqualer;
    public float weaponCooldownSummand;
    public float createProjectileChanceSummand;
    public int spikes;
    public int inscSummand;
    public string extraDescription = "";
    public static Prefixes self;
    public Prefixes()
    {
        self = this;
    }
    public void PrefixChooser(string prefix, float rememberDamage, GameObject item)
    {
        damageSummand = 0;
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
        criticalChanceSummand = 0;
        evasionChanceSummand = 0;
        maxHpSummand = 0;
        defenceSummand = 0;
        manaCostSummand = 0;
        weaponSizeSummand = 0;
        attackSpeedSummand = 0;
        secondUsageChanceSummand = 0;
        tripleAttackChanceSummand = 0;
        explChanceSummand = 0;
        explTypeEqualer = 0;
        weaponCooldownSummand = 0;
        createProjectileChanceSummand = 0;
        spikes = 0;
        inscSummand = 0;
        extraDescription = "";
        switch (prefix)
        {
            case "Distructive":
                damageSummand = 5;
                criticalChanceSummand = 10;
                inscSummand = -1;
                extraDescription = "";
                break;
            case "Great":
                damageSummand = 10;
                criticalChanceSummand = 5;
                inscSummand = -2;
                extraDescription = "";
                break;
            case "Ignite":
                igniteDamageSummand = rememberDamage + 5;
                criticalChanceSummand = 5;
                inscSummand = 1;
                extraDescription = "Converts all physical damage into damage by fire";
                DamageDefaulter(item);
                break;
            case "Fast":
                damageSummand = -2;
                attackSpeedSummand = -item.GetComponent<Slot>().attackSpeed / 2;
                weaponCooldownSummand = -0.2f;
                inscSummand = 1;
                extraDescription = "Small, but Fast!";
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

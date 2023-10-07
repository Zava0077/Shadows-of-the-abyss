using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Prefixes : MonoBehaviour
{
    float rememberDamage;
    public float movementSpeedSummand;
    public float critMultiSummand;
    public float hpRegenSummand;
    public float manaRegenSummand;
    public float projSpeedSummand;
    public float baseSpellDamageSummand;
    public float baseSpellCritSummand;
    public float baseCastSpeedSummand;
    public float maxHpPercSummand;
    public float maxManaPercSummand;
    public float maxHpSummand;
    public float maxManaSummand;
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
    public float defenceSummand;
    public float manaCostSummand;
    public float weaponSizeSummand;
    public float attackSpeedSummand;
    public float secondUsageChanceSummand;
    public float tripleAttackChanceSummand;
    public float explChanceSummand;
    public float explTypeEqualer;
    public float weaponCooldownSummand;
    public float createProjectileChanceSummand;
    public float spikes;
    public float inscSummand;
    public string extraDescription = "";
    public static Prefixes self;
    public string qualityColor;
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
        qualityColor = "#b80000";
        extraDescription = "";
        switch (prefix)
        {
            case "Distructive":
                damageSummand = 5;
                criticalChanceSummand = 10;
                inscSummand = -1;
                qualityColor = "#ff1e00";
                extraDescription = "";
                break;
            case "Great":
                damageSummand = 10;
                criticalChanceSummand = 5;
                inscSummand = -2;
                qualityColor = "#9000ff";
                attackSpeedSummand = 15;
                extraDescription = "";
                break;
            case "Ignite":
                igniteDamageSummand = rememberDamage + 5;
                criticalChanceSummand = 5;
                inscSummand = 1;
                extraDescription = "<color=#ff5d00>Converts all physical damage into damage by fire</color>";
                DamageDefaulter(item);
                break;
            case "Fast":
                damageSummand = -2;
                attackSpeedSummand = -item.GetComponent<Slot>().values[18] / 2;
                weaponCooldownSummand = -0.2f;
                inscSummand = 1;
                extraDescription = "<color=#2100a6>Small, but Fast!</color>";
                break;
            case "Electric":
                lightningDamageSummand = rememberDamage + 5;
                criticalChanceSummand = 5;
                inscSummand = 1;
                qualityColor = "#73abff";
                extraDescription = "<color=#ff5d00>Converts all physical damage into damage by lightning</color>";
                DamageDefaulter(item);
                break;
        }
    }
    void DamageDefaulter(GameObject item)
    {
        item.GetComponent<Slot>().values[2] = 0;
        item.GetComponent<Slot>().values[3] = 0;
        item.GetComponent<Slot>().values[7] = 0;
        item.GetComponent<Slot>().values[4] = 0;
        item.GetComponent<Slot>().values[8] = 0;
        item.GetComponent<Slot>().values[5] = 0;
        item.GetComponent<Slot>().values[6] = 0;
    }
}

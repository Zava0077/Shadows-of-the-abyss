using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class Prefixes : MonoBehaviour
{
    public float globalCritSummand;
    public float globalCritMultiSummand;
    public float attackCritSummand;
    public float castCritSummand;
    public float globalDamageSummand;
    public float attackDamageSummand;
    public float castDamageSummand;
    public float iceDamageSummand;
    public float igniteDamageSummand;
    public float lightningDamageSummand;
    public float poisonDamageSummand;
    public float pureDamageSummand;
    public float voidDamageSummand;
    public float attackSpeedSummand;
    public float castSpeedSummand;
    public float projSpeedSummand;
    public float maxHPSummand;
    public float maxHPPercentSummand;
    public float maxMPSummand;
    public float maxMPPercentSummand;
    public float HPRegenSummand;
    public float MPRegenSummand;
    public float defenceSummand;
    public float iceResistSummand;
    public float igniteResistSummand;
    public float lightningResistSummand;
    public float poisonResistSummand;
    public float evasionChanceSummand;
    public float voidResistSummand;
    public float moveSpeedSummand;
    public float luckSummand;
    public float manaCostSummand;
    public float weaponSizeSummand;
    public float secondUsageChanceSummand;
    public float tripleAttackChanceSummand;
    public float explChanceSummand;
    public float explTypeEqualer;
    public float weaponCooldownSummand;
    public float createProjectileChanceSummand;
    public float spikesEqualer;
    public float inscSlotsSummand;
    public string extraDescription = "";
    public static Prefixes self;
    public string qualityColor;
    public Dictionary<string, float> prefixedStats = new Dictionary<string, float>
    {

    };
    public Prefixes()
    {
        self = this;
    }
    public void PrefixChooser(string prefix, float rememberDamage, GameObject item)
    {
        attackDamageSummand = 0;
        iceDamageSummand = 0;
        igniteDamageSummand = 0;
        voidDamageSummand = 0;
        pureDamageSummand = 0;
        poisonDamageSummand = 0;
        lightningDamageSummand = 0;
        iceResistSummand = 0;
        igniteResistSummand = 0;
        voidResistSummand = 0;
        poisonResistSummand = 0;
        lightningResistSummand = 0;
        globalCritSummand = 0;
        attackCritSummand = 0;
        castCritSummand = 0;
        evasionChanceSummand = 0;
        maxHPSummand = 0;
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
        spikesEqualer = 0;
        inscSlotsSummand = 0;
        moveSpeedSummand = 0;
        globalCritMultiSummand = 0;
        HPRegenSummand = 0;
        MPRegenSummand = 0;
        projSpeedSummand = 0;
        castDamageSummand = 0;
        castCritSummand = 0;
        castSpeedSummand = 0;
        maxMPPercentSummand = 0;
        maxMPPercentSummand = 0;
        maxHPSummand = 0;
        maxMPPercentSummand = 0;

        qualityColor = "#b80000";
        extraDescription = "";
        switch (prefix)
        {
            case "Distructive":
                attackDamageSummand = 5;
                attackCritSummand = 10;
                castCritSummand = 10;
                inscSlotsSummand = -1;
                qualityColor = "#ff1e00";
                extraDescription = "";
                break;
            case "Great":
                attackDamageSummand = 10;
                attackCritSummand = 10;
                castCritSummand = 10;
                inscSlotsSummand = -2;
                qualityColor = "#9000ff";
                attackSpeedSummand = 0.5f;
                extraDescription = "";
                break;
            case "Ignite":
                igniteDamageSummand = 5; prefixedStats["igniteDamagePrefixed"] = 3;
                attackCritSummand = 5;
                castCritSummand = 5;
                igniteResistSummand = 5; prefixedStats["igniteResistPrefixed"] = 3;
                luckSummand = 2;
                inscSlotsSummand = 1;
                DamageDefaulter(item);
                break;
            case "Fast":
                attackDamageSummand = -2;
                castDamageSummand = -2;
                castSpeedSummand = 1;
                attackSpeedSummand = 1;
                weaponCooldownSummand = -0.2f;
                inscSlotsSummand = 1;
                break;
            case "Electric":
                lightningDamageSummand = 5; prefixedStats["lightningDamagePrefixed"] = 3;
                lightningResistSummand = 5; prefixedStats["lightningResistPrefixed"] = 3;
                attackCritSummand = 5;
                castCritSummand = 5;
                inscSlotsSummand = 1;
                qualityColor = "#73abff";
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

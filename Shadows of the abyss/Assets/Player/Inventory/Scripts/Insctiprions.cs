using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insctiprions : MonoBehaviour
{
    public float[] hp = new float[10];
    public float[] damage = new float[10];
    public float[] iceDamage = new float[10];
    public float[] igniteDamage = new float[10];
    public float[] lightningDamage = new float[10];
    public float[] poisonDamage = new float[10];
    public float[] voidDamage = new float[10];
    public float[] pureDamage = new float[10];
    public float[] defence = new float[10];
    public float[] iceResist = new float[10];
    public float[] igniteResist = new float[10];
    public float[] lightningResist = new float[10];
    public float[] poisonResist = new float[10];
    public float[] voidResist = new float[10];
    public float[] pureResist = new float[10];
    public float[] evasionChance = new float[10];
    public float[] criticalChance = new float[10];
    public int[] stackAmount = new int[10];
    public int[] stacksAlready = new int[10];
    public int[] kind = new int[10];
    public string[] type = new string[10];
    public Sprite[] sprite = new Sprite[10];
    public int[] idItem = new int[10];
    public string[] itemDescription = new string[10];
    public int[] manaCost = new int[10];
    public int[] weaponSize = new int[10];
    public int[] attackSpeed = new int[10];
    public int[] tripleAttackChance = new int[10];
    public int[] secondUsageChance = new int[10];
    public int[] explosionChance = new int[10];
    public int[] explosionType = new int[10];
    public float[] weaponCooldown = new float[10];
    public float[] createProjectileChance = new float[10];
    public int[] spikes = new int[10];
    public int[] pierce = new int[10];
    public int[] extraPierceChance = new int[10];

    public float hpValue;
    public float damageValue;
    public float iceDamageValue;
    public float igniteDamageValue;
    public float lightningDamageValue;
    public float poisonDamageValue;
    public float voidDamageValue;
    public float pureDamageValue;
    public float defenceValue;
    public float iceResistValue;
    public float igniteResistValue;
    public float lightningResistValue;
    public float poisonResistValue;
    public float voidResistValue;
    public float pureResistValue;
    public float evasionChanceValue;
    public float criticalChanceValue;
    public int stackAmountValue;
    public int stacksAlreadyValue;
    public int kindValue;
    public string typeValue;
    public Sprite spriteValue;
    public int idItemValue;
    public string itemDescriptionValue;
    public int manaCostValue;
    public int weaponSizeValue;
    public int attackSpeedValue;
    public int tripleAttackChanceValue;
    public int secondUsageChanceValue;
    public int explosionChanceValue;
    public int explosionTypeValue;
    public float weaponCooldownValue;
    public float createProjectileChanceValue;
    public int spikesValue;
    public int pierceValue;
    public int extraPierceChanceValue;
    private void Update()
    {
        InscriptionParametersAccept();
    }
    void InscriptionParametersAccept()
    {
        hpValue = 0;
        damageValue = 0;
        iceDamageValue = 0;
        igniteDamageValue = 0;
        lightningDamageValue = 0;
        poisonDamageValue = 0;
        voidDamageValue = 0;
        pureDamageValue = 0;
        defenceValue = 0;
        iceResistValue = 0;
        igniteResistValue = 0;
        lightningResistValue = 0;
        poisonResistValue = 0;
        voidResistValue = 0;
        pureResistValue = 0;
        evasionChanceValue = 0;
        criticalChanceValue = 0;
        manaCostValue = 0;
        weaponSizeValue = 0;
        attackSpeedValue = 0;
        tripleAttackChanceValue = 0;
        secondUsageChanceValue = 0;
        explosionChanceValue = 0;
        explosionTypeValue = 0;
        weaponCooldownValue = 0;
        createProjectileChanceValue = 0;
        spikesValue = 0;
        pierceValue = 0;
        extraPierceChanceValue = 0;
        for (int i = 0; i < 10; i++)
        {
            hpValue += hp[i];
            damageValue += damage[i];
            iceDamageValue += iceDamage[i];
            igniteDamageValue += igniteDamage[i];
            lightningDamageValue += lightningDamage[i];
            poisonDamageValue += poisonDamage[i];
            voidDamageValue += voidDamage[i];
            pureDamageValue += pureDamage[i];
            defenceValue += defence[i];
            iceResistValue += iceResist[i];
            igniteResistValue += igniteResist[i];
            lightningResistValue += lightningResist[i];
            poisonResistValue += poisonResist[i];
            voidResistValue += voidResist[i];
            pureResistValue += pureResist[i];
            evasionChanceValue += evasionChance[i];
            criticalChanceValue += criticalChance[i];
            manaCostValue += manaCost[i];
            weaponSizeValue += weaponSize[i];
            attackSpeedValue += attackSpeed[i];
            tripleAttackChanceValue += tripleAttackChance[i];
            secondUsageChanceValue += secondUsageChance[i];
            explosionChanceValue += explosionChance[i];
            explosionTypeValue += explosionType[i];
            weaponCooldownValue += weaponCooldown[i];
            createProjectileChanceValue += createProjectileChance[i];
            spikesValue += spikes[i];
            pierceValue += pierce[i];
            extraPierceChanceValue += extraPierceChance[i];
        }
    }
}

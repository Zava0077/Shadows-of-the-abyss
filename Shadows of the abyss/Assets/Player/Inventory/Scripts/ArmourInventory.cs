using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmourInventory : MonoBehaviour
{
    public static List<Slot> objects = new List<Slot>();
    public static List<Slot> armourSlots = new List<Slot>();
    [SerializeField] GameObject gO;

    public float[] hp;
    public float[] damage;
    public float[] iceDamage;
    public float[] igniteDamage;
    public float[] lightningDamage;
    public float[] poisonDamage;
    public float[] voidDamage;
    public float[] pureDamage;
    public float[] defence;
    public float[] iceResist;
    public float[] igniteResist;
    public float[] lightningResist;
    public float[] poisonResist;
    public float[] voidResist;
    public float[] pureResist;
    public float[] evasionChance;
    public float[] criticalChance;
    public float[] manaCost;
    public float[] weaponSize;
    public float[] tripleAttackChance;
    public float[] secondUsageChance;
    public float[] attackSpeed;
    public float[] explosionChance; //
    public float[] explosionType;//
    public float[] createProjectileChance;//
    public float[] weaponCooldown;
    public float[] spikes;//
    public float[] pierce;//
    public float[] extraPierceChance;//
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
    public string rareName;
    public float manaCostValue;
    public float weaponSizeValue;
    public float tripleAttackChanceValue;
    public float secondUsageChanceValue;
    public float attackSpeedValue;
    public float explosionChanceValue;//
    public float explosionTypeValue;//
    public float weaponCooldownValue;
    public string weaponType;
    public float createProjectileChanceValue;//
    public float spikesValue;//
    public float pierceValue;//
    public float extraPierceChanceValue;//

    public static ArmourInventory self;
    public ArmourInventory()
    {
        self = this;
    }
    private void Awake()
    {
        objects = GetComponentsInChildren<Slot>().ToList<Slot>();

        for (int i = 0; i < objects.Count; i++)
        {
            if (i % 2 == 0)
                armourSlots.Add(objects[i]);
        }
        hp = new float[armourSlots.Count];
        damage = new float[armourSlots.Count];
        iceDamage = new float[armourSlots.Count];
        igniteDamage = new float[armourSlots.Count];
        lightningDamage = new float[armourSlots.Count];
        poisonDamage = new float[armourSlots.Count];
        voidDamage = new float[armourSlots.Count];
        pureDamage = new float[armourSlots.Count];
        defence = new float[armourSlots.Count];
        iceResist = new float[armourSlots.Count];
        igniteResist = new float[armourSlots.Count];
        lightningResist = new float[armourSlots.Count];
        poisonResist = new float[armourSlots.Count];
        voidResist = new float[armourSlots.Count];
        pureResist = new float[armourSlots.Count];
        evasionChance = new float[armourSlots.Count];
        criticalChance = new float[armourSlots.Count];
        manaCost = new float[armourSlots.Count];
        weaponSize = new float[armourSlots.Count];
        tripleAttackChance = new float[armourSlots.Count];
        secondUsageChance = new float[armourSlots.Count];
        attackSpeed = new float[armourSlots.Count];
        explosionChance = new float[armourSlots.Count];
        explosionType = new float[armourSlots.Count];
        weaponCooldown = new float[armourSlots.Count];
        createProjectileChance = new float[armourSlots.Count];
        spikes = new float[armourSlots.Count];
        pierce = new float[armourSlots.Count];
        extraPierceChance = new float[armourSlots.Count];
        transform.parent.gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        CalculateStats(); //Метод вызываемый во время файтов, во время получения урона
    }
    void CheckItems()
    {
        for (int i = 0; i < armourSlots.Count; i++)
        {
            hp[i] = Inventory.slots[i + 16].values[0];
            damage[i] = Inventory.slots[i + 16].values[2];
            iceDamage[i] = Inventory.slots[i + 16].values[3];
            igniteDamage[i] = Inventory.slots[i + 16].values[4];
            lightningDamage[i] = Inventory.slots[i + 16].values[5];
            poisonDamage[i] = Inventory.slots[i + 16].values[6];
            voidDamage[i] = Inventory.slots[i + 16].values[7];
            pureDamage[i] = Inventory.slots[i + 16].values[8];
            defence[i] = Inventory.slots[i + 16].values[1];
            iceResist[i] = Inventory.slots[i + 16].values[9];
            igniteResist[i] = Inventory.slots[i + 16].values[10];
            lightningResist[i] = Inventory.slots[i + 16].values[11];
            poisonResist[i] = Inventory.slots[i + 16].values[12];
            voidResist[i] = Inventory.slots[i + 16].values[13];
            evasionChance[i] = Inventory.slots[i + 16].values[14];
            criticalChance[i] = Inventory.slots[i + 16].values[15];
            manaCost[i] = Inventory.slots[i + 16].values[16];
            weaponSize[i] = Inventory.slots[i + 16].values[17];
            attackSpeed[i] = Inventory.slots[i + 16].values[18];
            tripleAttackChance[i] = Inventory.slots[i + 16].values[19];
            secondUsageChance[i] = Inventory.slots[i + 16].values[20];
            explosionChance[i] = Inventory.slots[i + 16].values[21];
            explosionType[i] = Inventory.slots[i + 16].values[22];
            weaponCooldown[i] = Inventory.slots[i + 16].values[23];
            createProjectileChance[i] = Inventory.slots[i + 16].values[24];
            spikes[i] = Inventory.slots[i + 16].values[25];
            pierce[i] = Inventory.slots[i + 16].values[26];
            extraPierceChance[i] = Inventory.slots[i + 16].values[27];
        }
    }
    void CalculateStats()
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
        CheckItems();
        for (int i = 0; i < armourSlots.Count; i++)
        {
            if (i == 0 || i == 4 || i == 8)
            {
                damageValue += damage[i] + Inventory.slots[i + 16].inscriptions.damageValue;
                iceDamageValue += iceDamage[i] + Inventory.slots[i + 16].inscriptions.iceDamageValue;
                igniteDamageValue += igniteDamage[i] + Inventory.slots[i + 16].inscriptions.igniteDamageValue;
                lightningDamageValue += lightningDamage[i] + Inventory.slots[i + 16].inscriptions.lightningDamageValue;
                poisonDamageValue += poisonDamage[i] + Inventory.slots[i + 16].inscriptions.poisonDamageValue;
                voidDamageValue += voidDamage[i] + Inventory.slots[i + 16].inscriptions.voidDamageValue;
                pureDamageValue += pureDamage[i] + Inventory.slots[i + 16].inscriptions.pureDamageValue;
                manaCostValue += manaCost[i] + Inventory.slots[i + 16].inscriptions.manaCostValue;
                weaponSizeValue += weaponSize[i] + Inventory.slots[i + 16].inscriptions.weaponSizeValue;
                attackSpeedValue += attackSpeed[i] + Inventory.slots[i + 16].inscriptions.attackSpeedValue;
                tripleAttackChanceValue += tripleAttackChance[i] + Inventory.slots[i + 16].inscriptions.tripleAttackChanceValue;
                secondUsageChanceValue += secondUsageChance[i] + Inventory.slots[i + 16].inscriptions.secondUsageChanceValue;
                explosionChanceValue += explosionChance[i] + Inventory.slots[i + 16].inscriptions.explosionChanceValue;
                explosionTypeValue += explosionType[i] + Inventory.slots[i + 16].inscriptions.explosionTypeValue;
                weaponCooldownValue += weaponCooldown[i] + Inventory.slots[i + 16].inscriptions.weaponCooldownValue;
                criticalChanceValue += criticalChance[i] + Inventory.slots[i + 16].inscriptions.criticalChanceValue;
                createProjectileChanceValue += createProjectileChance[i] + Inventory.slots[i + 16].inscriptions.createProjectileChanceValue;
                pierceValue += pierce[i] + Inventory.slots[i + 16].inscriptions.pierceValue;
                extraPierceChanceValue += extraPierceChance[i] + Inventory.slots[i + 16].inscriptions.extraPierceChanceValue;
            }
            if (i == 0 || i == 8 || i == 2 || i == 6 || i == 10 || i == 1 || i == 5 || i == 9)
            {
                defenceValue += defence[i] + Inventory.slots[i + 16].inscriptions.defenceValue;
                iceResistValue += iceResist[i] + Inventory.slots[i + 16].inscriptions.iceResistValue;
                igniteResistValue += igniteResist[i] + Inventory.slots[i + 16].inscriptions.igniteResistValue;
                lightningResistValue += lightningResist[i] + Inventory.slots[i + 16].inscriptions.lightningResistValue;
                poisonResistValue += poisonResist[i] + Inventory.slots[i + 16].inscriptions.poisonResistValue;
                voidResistValue += voidResist[i] + Inventory.slots[i + 16].inscriptions.voidResistValue;
                pureResistValue += pureResist[i] + Inventory.slots[i + 16].inscriptions.pureResistValue;
                evasionChanceValue += evasionChance[i] + Inventory.slots[i + 16].inscriptions.evasionChanceValue;
                spikesValue += spikes[i] + Inventory.slots[i + 16].inscriptions.spikesValue;
            }
            hpValue += hp[i] + Inventory.slots[i + 16].inscriptions.hpValue;
            weaponType = armourSlots[4].type;
        }
        CapParameters();
    }
    void CapParameters()
    {
        if (weaponCooldownValue <= 0.1f || Inventory.slots[4].type != "Empty")
            weaponCooldownValue = 0.1f;
    }
}

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
    public int[] manaCost;
    public int[] weaponSize;
    public int[] tripleAttackChance;
    public int[] secondUsageChance;
    public int[] attackSpeed;
    public int[] explosionChance; //
    public int[] explosionType;//
    public float[] createProjectileChance;//
    public float[] weaponCooldown;
    public int[] spikes;//
    public int[] pierce;//
    public int[] extraPierceChance;//
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
    public int manaCostValue;
    public int weaponSizeValue;
    public int tripleAttackChanceValue;
    public int secondUsageChanceValue;
    public float attackSpeedValue;
    public int explosionChanceValue;//
    public int explosionTypeValue;//
    public float weaponCooldownValue;
    public string weaponType;
    public float createProjectileChanceValue;//
    public int spikesValue;//
    public int pierceValue;//
    public int extraPierceChanceValue;//

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
        manaCost = new int[armourSlots.Count];
        weaponSize = new int[armourSlots.Count];
        tripleAttackChance = new int[armourSlots.Count];
        secondUsageChance = new int[armourSlots.Count];
        attackSpeed = new int[armourSlots.Count];
        explosionChance = new int[armourSlots.Count];
        explosionType = new int[armourSlots.Count];
        weaponCooldown = new float[armourSlots.Count];
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
            hp[i] = Inventory.slots[i + 16].hp;
            damage[i] = Inventory.slots[i + 16].damage;
            iceDamage[i] = Inventory.slots[i + 16].iceDamage;
            igniteDamage[i] = Inventory.slots[i + 16].igniteDamage;
            lightningDamage[i] = Inventory.slots[i + 16].lightningDamage;
            poisonDamage[i] = Inventory.slots[i + 16].poisonDamage;
            voidDamage[i] = Inventory.slots[i + 16].voidDamage;
            pureDamage[i] = Inventory.slots[i + 16].pureDamage;
            defence[i] = Inventory.slots[i + 16].defence;
            iceResist[i] = Inventory.slots[i + 16].iceResist;
            igniteResist[i] = Inventory.slots[i + 16].igniteResist;
            lightningResist[i] = Inventory.slots[i + 16].lightningResist;
            poisonResist[i] = Inventory.slots[i + 16].poisonResist;
            voidResist[i] = Inventory.slots[i + 16].voidResist;
            pureResist[i] = Inventory.slots[i + 16].pureResist;
            evasionChance[i] = Inventory.slots[i + 16].evasionChance;
            criticalChance[i] = Inventory.slots[i + 16].criticalChance;
            manaCost[i] = Inventory.slots[i + 16].manaCost;
            weaponSize[i] = Inventory.slots[i + 16].weaponSize;
            attackSpeed[i] = Inventory.slots[i + 16].attackSpeed;
            tripleAttackChance[i] = Inventory.slots[i + 16].tripleAttackChance;
            secondUsageChance[i] = Inventory.slots[i + 16].secondUsageChance;
            explosionChance[i] = Inventory.slots[i + 16].explosionChance;
            explosionType[i] = Inventory.slots[i + 16].explosionType;
            weaponCooldown[i] = Inventory.slots[i + 16].weaponCooldown;
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
        CheckItems();
        for (int i = 0; i < armourSlots.Count; i++)
        {
            if (i == 0 || i == 4 || i == 8)
            {
                damageValue += Inventory.slots[i + 16].damage + Inventory.slots[i + 16].inscriptions.damageValue;
                iceDamageValue += Inventory.slots[i + 16].iceDamage + Inventory.slots[i + 16].inscriptions.iceDamageValue;
                igniteDamageValue += Inventory.slots[i + 16].igniteDamage + Inventory.slots[i + 16].inscriptions.igniteDamageValue;
                lightningDamageValue += Inventory.slots[i + 16].lightningDamage + Inventory.slots[i + 16].inscriptions.lightningDamageValue;
                poisonDamageValue += Inventory.slots[i + 16].poisonDamage + Inventory.slots[i + 16].inscriptions.poisonDamageValue;
                voidDamageValue += Inventory.slots[i + 16].voidDamage + Inventory.slots[i + 16].inscriptions.voidDamageValue;
                pureDamageValue += Inventory.slots[i + 16].pureDamage + Inventory.slots[i + 16].inscriptions.pureDamageValue;
                manaCostValue += Inventory.slots[i + 16].manaCost + Inventory.slots[i + 16].inscriptions.manaCostValue;
                weaponSizeValue += Inventory.slots[i + 16].weaponSize + Inventory.slots[i + 16].inscriptions.weaponSizeValue;
                attackSpeedValue += (float)Inventory.slots[i + 16].attackSpeed / 100 + (float)Inventory.slots[i + 16].inscriptions.attackSpeedValue / 100;
                tripleAttackChanceValue += Inventory.slots[i + 16].tripleAttackChance + Inventory.slots[i + 16].inscriptions.tripleAttackChanceValue;
                secondUsageChanceValue += Inventory.slots[i + 16].secondUsageChance + Inventory.slots[i + 16].inscriptions.secondUsageChanceValue;
                explosionChanceValue += Inventory.slots[i + 16].explosionChance + Inventory.slots[i + 16].inscriptions.explosionChanceValue;
                explosionTypeValue += Inventory.slots[i + 16].explosionType + Inventory.slots[i + 16].inscriptions.explosionTypeValue;
                weaponCooldownValue += Inventory.slots[i + 16].weaponCooldown + Inventory.slots[i + 16].inscriptions.weaponCooldownValue;
                criticalChanceValue += Inventory.slots[i + 16].criticalChance + Inventory.slots[i + 16].inscriptions.criticalChanceValue;
            }
            if (i == 0 || i == 8 || i == 2 || i == 6 || i == 10 || i == 1 || i == 5 || i == 9)
            {
                defenceValue += Inventory.slots[i + 16].defence + Inventory.slots[i + 16].inscriptions.defenceValue;
                iceResistValue += Inventory.slots[i + 16].iceResist + Inventory.slots[i + 16].inscriptions.iceResistValue;
                igniteResistValue += Inventory.slots[i + 16].igniteResist + Inventory.slots[i + 16].inscriptions.igniteResistValue;
                lightningResistValue += Inventory.slots[i + 16].lightningResist + Inventory.slots[i + 16].inscriptions.lightningResistValue;
                poisonResistValue += Inventory.slots[i + 16].poisonResist + Inventory.slots[i + 16].inscriptions.poisonResistValue;
                voidResistValue += Inventory.slots[i + 16].voidResist + Inventory.slots[i + 16].inscriptions.voidResistValue;
                pureResistValue += Inventory.slots[i + 16].pureResist + Inventory.slots[i + 16].inscriptions.pureResistValue;
                evasionChanceValue += Inventory.slots[i + 16].evasionChance + Inventory.slots[i + 16].inscriptions.evasionChanceValue;
            }
            hpValue += Inventory.slots[i + 16].hp + Inventory.slots[i + 16].inscriptions.hpValue;
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

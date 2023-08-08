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
                armourSlots.Add(objects[i]);// нужен для мат вычислений бафов со слотов
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
        CheckItems();
        for (int i = 0; i < armourSlots.Count; i++)
        {
            hpValue += Inventory.slots[i + 16].hp;
            damageValue += Inventory.slots[i + 16].damage;
            iceDamageValue += Inventory.slots[i + 16].iceDamage;
            igniteDamageValue += Inventory.slots[i + 16].igniteDamage;
            lightningDamageValue += Inventory.slots[i + 16].lightningDamage;
            poisonDamageValue += Inventory.slots[i + 16].poisonDamage;
            voidDamageValue += Inventory.slots[i + 16].voidDamage;
            pureDamageValue += Inventory.slots[i + 16].pureDamage;
            defenceValue += Inventory.slots[i + 16].defence;
            iceResistValue += Inventory.slots[i + 16].iceResist;
            igniteResistValue += Inventory.slots[i + 16].igniteResist;
            lightningResistValue += Inventory.slots[i + 16].lightningResist;
            poisonResistValue += Inventory.slots[i + 16].poisonResist;
            voidResistValue += Inventory.slots[i + 16].voidResist;
            pureResistValue += Inventory.slots[i + 16].pureResist;
            evasionChanceValue += Inventory.slots[i + 16].evasionChance;
            criticalChanceValue += Inventory.slots[i + 16].criticalChance;
        }
    }
}

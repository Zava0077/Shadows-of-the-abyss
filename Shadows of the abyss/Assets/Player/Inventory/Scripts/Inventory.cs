using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using System;

public class Inventory : MonoBehaviour
{
    public static List<Slot> objects = new List<Slot>();
    public static List<Slot> armourObjects = new List<Slot>();
    public static List<Slot> slots = new List<Slot>();
    static bool isFull = false;
    public static Inventory self;
    public Inventory()
    {
        self = this;
    }
    static int firstFreeSlot;
    private void Awake()
    {
        objects = GetComponentsInChildren<Slot>().ToList<Slot>();
        armourObjects = ArmourInventory.self.GetComponentsInChildren<Slot>().ToList<Slot>();
        for (int i = 0; i < objects.Count; i++)
        {
            if (i % 2 == 0)
                slots.Add(objects[i]);
        }
        for (int i = 0; i < armourObjects.Count; i++)
        {
            if (i % 2 == 0)
                slots.Add(armourObjects[i]);
        }
    }
    private void FixedUpdate()
    {
       
    }
    public static bool IsInventoryFull(GameObject item)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].type == "Empty" || (slots[i].type == item.GetComponent<Slot>().type && slots[i].stacksAlready < slots[i].stackAmount))
            {
                FreeSlotDetector(i);
                return isFull = false;
            }
            if (i == slots.Count - 1)
                return isFull = true;
        }
        return isFull;
    }
    public void PickUpItem(GameObject item) //
    {
        System.Random rnd = new System.Random();
        string rareName = "";
        if (slots[firstFreeSlot].stacksAlready < slots[firstFreeSlot].stackAmount && slots[firstFreeSlot].stacksAlready > 0 && slots[firstFreeSlot].idItem == item.GetComponent<Slot>().idItem)
            slots[firstFreeSlot].stacksAlready++;
        else
        {
            slots[firstFreeSlot].damage = item.GetComponent<Slot>().damage;
            slots[firstFreeSlot].iceDamage = item.GetComponent<Slot>().iceDamage;
            slots[firstFreeSlot].igniteDamage = item.GetComponent<Slot>().igniteDamage;
            slots[firstFreeSlot].lightningDamage = item.GetComponent<Slot>().lightningDamage;
            slots[firstFreeSlot].poisonDamage = item.GetComponent<Slot>().poisonDamage;
            slots[firstFreeSlot].voidDamage = item.GetComponent<Slot>().voidDamage;
            slots[firstFreeSlot].pureDamage = item.GetComponent<Slot>().pureDamage;
            slots[firstFreeSlot].defence = item.GetComponent<Slot>().defence;
            slots[firstFreeSlot].iceResist = item.GetComponent<Slot>().iceResist;
            slots[firstFreeSlot].igniteResist = item.GetComponent<Slot>().igniteResist;
            slots[firstFreeSlot].lightningResist = item.GetComponent<Slot>().lightningResist;
            slots[firstFreeSlot].poisonResist = item.GetComponent<Slot>().poisonResist;
            slots[firstFreeSlot].voidResist = item.GetComponent<Slot>().voidResist;
            slots[firstFreeSlot].pureResist = item.GetComponent<Slot>().pureResist;
            slots[firstFreeSlot].type = item.GetComponent<Slot>().type;
            slots[firstFreeSlot].sprite = item.GetComponent<Slot>().sprite;
            slots[firstFreeSlot].hp = item.GetComponent<Slot>().hp;
            slots[firstFreeSlot].evasionChance = item.GetComponent<Slot>().evasionChance;
            slots[firstFreeSlot].criticalChance = item.GetComponent<Slot>().criticalChance;
            slots[firstFreeSlot].kind = item.GetComponent<Slot>().kind;
            slots[firstFreeSlot].stackAmount = item.GetComponent<Slot>().stackAmount;
            slots[firstFreeSlot].stacksAlready = item.GetComponent<Slot>().stacksAlready;
            slots[firstFreeSlot].idItem = item.GetComponent<Slot>().idItem;
            slots[firstFreeSlot].itemDescription = item.GetComponent<Slot>().itemDescription.Replace("\\n","\n");
            slots[firstFreeSlot].rareList = item.GetComponent<Slot>().rareList;
            slots[firstFreeSlot].rareChances = item.GetComponent<Slot>().rareChances;
            slots[firstFreeSlot].rareName = rareName;
            slots[firstFreeSlot].manaCost = item.GetComponent<Slot>().manaCost;
            slots[firstFreeSlot].weaponSize = item.GetComponent<Slot>().weaponSize;
            slots[firstFreeSlot].attackSpeed = item.GetComponent<Slot>().attackSpeed;
            slots[firstFreeSlot].tripleAttackChance = item.GetComponent<Slot>().tripleAttackChance;
            slots[firstFreeSlot].secondUsageChance = item.GetComponent<Slot>().secondUsageChance;
            slots[firstFreeSlot].explosionChance = item.GetComponent<Slot>().explosionChance;
            slots[firstFreeSlot].explosionType = item.GetComponent<Slot>().explosionType;
            slots[firstFreeSlot].weaponCooldown = item.GetComponent<Slot>().weaponCooldown;
        }
    }
    static void FreeSlotDetector(int freeSlotId)
    {
        firstFreeSlot = freeSlotId;
    }
}

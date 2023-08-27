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
    public static List<Slot> inscriptionObjects = new List<Slot>();
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
        inscriptionObjects = InscriptionsInventory.self.GetComponentsInChildren<Slot>().ToList<Slot>();
        for (int i = 0; i < objects.Count; i++)
        {
            if (i % 2 == 0)
            {
                slots.Add(objects[i]);
                for(int k = 0; k<10;k++)
                {
                    //GameObject insc = Instantiate(inscriptionShape);
                    //slots[slots.Count - 1].inscriptions[k] = insc;
                }
            }

        }
        for (int i = 0; i < armourObjects.Count; i++)
        {
            if (i % 2 == 0)
            {
                slots.Add(armourObjects[i]);
                for (int k = 0; k < 10; k++)
                {
                    //GameObject insc = Instantiate(inscriptionShape);
                    //slots[slots.Count - 1].inscriptions[k] = insc;
                }
            }

        }
        for (int i = 0; i < inscriptionObjects.Count; i++)
        {
            if (i % 2 == 0)
            {
                slots.Add(inscriptionObjects[i]);
                for (int k = 0; k < 10; k++)
                {
                    //GameObject insc = Instantiate(inscriptionShape);
                    //slots[slots.Count - 1].inscriptions[k] = insc;
                }
            }

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
            slots[firstFreeSlot].weaponSprite = item.GetComponent<Slot>().weaponSprite;
            slots[firstFreeSlot].projectileSprite = item.GetComponent<Slot>().projectileSprite;
            slots[firstFreeSlot].createProjectileChance = item.GetComponent<Slot>().createProjectileChance;
            slots[firstFreeSlot].spikes = item.GetComponent<Slot>().spikes;
            slots[firstFreeSlot].pierce = item.GetComponent<Slot>().pierce;
            slots[firstFreeSlot].extraPierceChance = item.GetComponent<Slot>().extraPierceChance;
            slots[firstFreeSlot].inscriptionNum = item.GetComponent<Slot>().inscriptionNum;
            //
            slots[firstFreeSlot].inscriptions.iceDamage = item.GetComponent<Slot>().inscriptions.iceDamage;
            slots[firstFreeSlot].inscriptions.igniteDamage = item.GetComponent<Slot>().inscriptions.igniteDamage;
            slots[firstFreeSlot].inscriptions.lightningDamage = item.GetComponent<Slot>().inscriptions.lightningDamage;
            slots[firstFreeSlot].inscriptions.poisonDamage = item.GetComponent<Slot>().inscriptions.poisonDamage;
            slots[firstFreeSlot].inscriptions.voidDamage = item.GetComponent<Slot>().inscriptions.voidDamage;
            slots[firstFreeSlot].inscriptions.pureDamage = item.GetComponent<Slot>().inscriptions.pureDamage;
            slots[firstFreeSlot].inscriptions.defence = item.GetComponent<Slot>().inscriptions.defence;
            slots[firstFreeSlot].inscriptions.iceResist = item.GetComponent<Slot>().inscriptions.iceResist;
            slots[firstFreeSlot].inscriptions.igniteResist = item.GetComponent<Slot>().inscriptions.igniteResist;
            slots[firstFreeSlot].inscriptions.lightningResist = item.GetComponent<Slot>().inscriptions.lightningResist;
            slots[firstFreeSlot].inscriptions.poisonResist = item.GetComponent<Slot>().inscriptions.poisonResist;
            slots[firstFreeSlot].inscriptions.voidResist = item.GetComponent<Slot>().inscriptions.voidResist;
            slots[firstFreeSlot].inscriptions.pureResist = item.GetComponent<Slot>().inscriptions.pureResist;
            slots[firstFreeSlot].inscriptions.type = item.GetComponent<Slot>().inscriptions.type;
            slots[firstFreeSlot].inscriptions.sprite = item.GetComponent<Slot>().inscriptions.sprite;
            slots[firstFreeSlot].inscriptions.hp = item.GetComponent<Slot>().inscriptions.hp;
            slots[firstFreeSlot].inscriptions.evasionChance = item.GetComponent<Slot>().inscriptions.evasionChance;
            slots[firstFreeSlot].inscriptions.criticalChance = item.GetComponent<Slot>().inscriptions.criticalChance;
            slots[firstFreeSlot].inscriptions.kind = item.GetComponent<Slot>().inscriptions.kind;
            slots[firstFreeSlot].inscriptions.stackAmount = item.GetComponent<Slot>().inscriptions.stackAmount;
            slots[firstFreeSlot].inscriptions.idItem = item.GetComponent<Slot>().inscriptions.idItem;
            slots[firstFreeSlot].inscriptions.itemDescription = item.GetComponent<Slot>().inscriptions.itemDescription;
            slots[firstFreeSlot].inscriptions.manaCost = item.GetComponent<Slot>().inscriptions.manaCost;
            slots[firstFreeSlot].inscriptions.weaponSize = item.GetComponent<Slot>().inscriptions.weaponSize;
            slots[firstFreeSlot].inscriptions.attackSpeed = item.GetComponent<Slot>().inscriptions.attackSpeed;
            slots[firstFreeSlot].inscriptions.tripleAttackChance = item.GetComponent<Slot>().inscriptions.tripleAttackChance;
            slots[firstFreeSlot].inscriptions.secondUsageChance = item.GetComponent<Slot>().inscriptions.secondUsageChance;
            slots[firstFreeSlot].inscriptions.explosionChance = item.GetComponent<Slot>().inscriptions.explosionChance;
            slots[firstFreeSlot].inscriptions.explosionType = item.GetComponent<Slot>().inscriptions.explosionType;
            slots[firstFreeSlot].inscriptions.weaponCooldown = item.GetComponent<Slot>().inscriptions.weaponCooldown;
            slots[firstFreeSlot].inscriptions.createProjectileChance = item.GetComponent<Slot>().inscriptions.createProjectileChance;
            slots[firstFreeSlot].inscriptions.spikes = item.GetComponent<Slot>().inscriptions.spikes;
            slots[firstFreeSlot].inscriptions.pierce = item.GetComponent<Slot>().inscriptions.pierce;
            slots[firstFreeSlot].inscriptions.extraPierceChance = item.GetComponent<Slot>().inscriptions.extraPierceChance;
            //
        }
    }
    static void FreeSlotDetector(int freeSlotId)
    {
        firstFreeSlot = freeSlotId;
    }
    public void GetFeatures()
    {
        //if (slots[SlotInteraction.hoveredId].GetComponent<Slot>().inscriptions.Count != 0)
            for (int i = 0; i < InscriptionsInventory.inscriptionSlots.Count; i++)
            {
                //
                InscriptionsInventory.inscriptionSlots[i].damage = slots[SlotInteraction.hoveredId].inscriptions.damage[i];
                InscriptionsInventory.inscriptionSlots[i].iceDamage = slots[SlotInteraction.hoveredId].inscriptions.iceDamage[i];
                InscriptionsInventory.inscriptionSlots[i].igniteDamage = slots[SlotInteraction.hoveredId].inscriptions.igniteDamage[i];
                InscriptionsInventory.inscriptionSlots[i].lightningDamage = slots[SlotInteraction.hoveredId].inscriptions.lightningDamage[i];
                InscriptionsInventory.inscriptionSlots[i].poisonDamage = slots[SlotInteraction.hoveredId].inscriptions.poisonDamage[i];
                InscriptionsInventory.inscriptionSlots[i].voidDamage = slots[SlotInteraction.hoveredId].inscriptions.voidDamage[i];
                InscriptionsInventory.inscriptionSlots[i].pureDamage = slots[SlotInteraction.hoveredId].inscriptions.pureDamage[i];
                InscriptionsInventory.inscriptionSlots[i].defence = slots[SlotInteraction.hoveredId].inscriptions.defence[i];
                InscriptionsInventory.inscriptionSlots[i].iceResist = slots[SlotInteraction.hoveredId].inscriptions.iceResist[i];
                InscriptionsInventory.inscriptionSlots[i].igniteResist = slots[SlotInteraction.hoveredId].inscriptions.igniteResist[i];
                InscriptionsInventory.inscriptionSlots[i].lightningResist = slots[SlotInteraction.hoveredId].inscriptions.lightningResist[i];
                InscriptionsInventory.inscriptionSlots[i].poisonResist = slots[SlotInteraction.hoveredId].inscriptions.poisonResist[i];
                InscriptionsInventory.inscriptionSlots[i].voidResist = slots[SlotInteraction.hoveredId].inscriptions.voidResist[i];
                InscriptionsInventory.inscriptionSlots[i].pureResist = slots[SlotInteraction.hoveredId].inscriptions.pureResist[i];
                InscriptionsInventory.inscriptionSlots[i].type = slots[SlotInteraction.hoveredId].inscriptions.type[i];
                InscriptionsInventory.inscriptionSlots[i].sprite = slots[SlotInteraction.hoveredId].inscriptions.sprite[i];
                InscriptionsInventory.inscriptionSlots[i].hp = slots[SlotInteraction.hoveredId].inscriptions.hp[i];
                InscriptionsInventory.inscriptionSlots[i].evasionChance = slots[SlotInteraction.hoveredId].inscriptions.evasionChance[i];
                InscriptionsInventory.inscriptionSlots[i].criticalChance = slots[SlotInteraction.hoveredId].inscriptions.criticalChance[i]; 
                InscriptionsInventory.inscriptionSlots[i].kind = slots[SlotInteraction.hoveredId].inscriptions.kind[i];
                InscriptionsInventory.inscriptionSlots[i].stackAmount = slots[SlotInteraction.hoveredId].inscriptions.stackAmount[i];
                InscriptionsInventory.inscriptionSlots[i].stacksAlready = slots[SlotInteraction.hoveredId].inscriptions.stacksAlready[i];
                InscriptionsInventory.inscriptionSlots[i].idItem = slots[SlotInteraction.hoveredId].inscriptions.idItem[i];
                InscriptionsInventory.inscriptionSlots[i].itemDescription = slots[SlotInteraction.hoveredId].inscriptions.itemDescription[i];
                InscriptionsInventory.inscriptionSlots[i].manaCost = slots[SlotInteraction.hoveredId].inscriptions.manaCost[i];
                InscriptionsInventory.inscriptionSlots[i].weaponSize = slots[SlotInteraction.hoveredId].inscriptions.weaponSize[i];
                InscriptionsInventory.inscriptionSlots[i].attackSpeed = slots[SlotInteraction.hoveredId].inscriptions.attackSpeed[i];
                InscriptionsInventory.inscriptionSlots[i].tripleAttackChance = slots[SlotInteraction.hoveredId].inscriptions.tripleAttackChance[i];
                InscriptionsInventory.inscriptionSlots[i].secondUsageChance = slots[SlotInteraction.hoveredId].inscriptions.secondUsageChance[i];
                InscriptionsInventory.inscriptionSlots[i].explosionChance = slots[SlotInteraction.hoveredId].inscriptions.explosionChance[i];
                InscriptionsInventory.inscriptionSlots[i].explosionType = slots[SlotInteraction.hoveredId].inscriptions.explosionType[i];
                InscriptionsInventory.inscriptionSlots[i].weaponCooldown = slots[SlotInteraction.hoveredId].inscriptions.weaponCooldown[i];
                InscriptionsInventory.inscriptionSlots[i].createProjectileChance = slots[SlotInteraction.hoveredId].inscriptions.createProjectileChance[i];
                InscriptionsInventory.inscriptionSlots[i].spikes = slots[SlotInteraction.hoveredId].inscriptions.spikes[i];
                InscriptionsInventory.inscriptionSlots[i].pierce = slots[SlotInteraction.hoveredId].inscriptions.pierce[i];
                InscriptionsInventory.inscriptionSlots[i].extraPierceChance = slots[SlotInteraction.hoveredId].inscriptions.extraPierceChance[i];
            }
    }
    public void AcceptFeatures()
    {
        //if (slots[SlotInteraction.hoveredId].inscriptions.Count != 0)
            for (int i = 0; i < InscriptionsInventory.inscriptionSlots.Count; i++)
            {
                slots[SlotInteraction.hoveredId].inscriptions.damage[i] = InscriptionsInventory.inscriptionSlots[i].damage;
                slots[SlotInteraction.hoveredId].inscriptions.iceDamage[i] = InscriptionsInventory.inscriptionSlots[i].iceDamage;
                slots[SlotInteraction.hoveredId].inscriptions.igniteDamage[i] = InscriptionsInventory.inscriptionSlots[i].igniteDamage;
                slots[SlotInteraction.hoveredId].inscriptions.lightningDamage[i] = InscriptionsInventory.inscriptionSlots[i].lightningDamage;
                slots[SlotInteraction.hoveredId].inscriptions.poisonDamage[i] = InscriptionsInventory.inscriptionSlots[i].poisonDamage;
                slots[SlotInteraction.hoveredId].inscriptions.voidDamage[i] = InscriptionsInventory.inscriptionSlots[i].voidDamage;
                slots[SlotInteraction.hoveredId].inscriptions.pureDamage[i] = InscriptionsInventory.inscriptionSlots[i].pureDamage;
                slots[SlotInteraction.hoveredId].inscriptions.defence[i] = InscriptionsInventory.inscriptionSlots[i].defence;
                slots[SlotInteraction.hoveredId].inscriptions.iceResist[i] = InscriptionsInventory.inscriptionSlots[i].iceResist;
                slots[SlotInteraction.hoveredId].inscriptions.igniteResist[i] = InscriptionsInventory.inscriptionSlots[i].igniteResist;
                slots[SlotInteraction.hoveredId].inscriptions.lightningResist[i] = InscriptionsInventory.inscriptionSlots[i].lightningResist;
                slots[SlotInteraction.hoveredId].inscriptions.poisonResist[i] = InscriptionsInventory.inscriptionSlots[i].poisonResist;
                slots[SlotInteraction.hoveredId].inscriptions.voidResist[i] = InscriptionsInventory.inscriptionSlots[i].voidResist;
                slots[SlotInteraction.hoveredId].inscriptions.pureResist[i] = InscriptionsInventory.inscriptionSlots[i].pureResist;
                slots[SlotInteraction.hoveredId].inscriptions.type[i] = InscriptionsInventory.inscriptionSlots[i].type;
                slots[SlotInteraction.hoveredId].inscriptions.sprite[i] = InscriptionsInventory.inscriptionSlots[i].sprite;
                slots[SlotInteraction.hoveredId].inscriptions.hp[i] = InscriptionsInventory.inscriptionSlots[i].hp;
                slots[SlotInteraction.hoveredId].inscriptions.evasionChance[i] = InscriptionsInventory.inscriptionSlots[i].evasionChance;
                slots[SlotInteraction.hoveredId].inscriptions.criticalChance[i] = InscriptionsInventory.inscriptionSlots[i].criticalChance;
                slots[SlotInteraction.hoveredId].inscriptions.kind[i] = InscriptionsInventory.inscriptionSlots[i].kind;
                slots[SlotInteraction.hoveredId].inscriptions.stackAmount[i] = InscriptionsInventory.inscriptionSlots[i].stackAmount;
                slots[SlotInteraction.hoveredId].inscriptions.stacksAlready[i] = InscriptionsInventory.inscriptionSlots[i].stacksAlready;
                slots[SlotInteraction.hoveredId].inscriptions.idItem[i] = InscriptionsInventory.inscriptionSlots[i].idItem;
                slots[SlotInteraction.hoveredId].inscriptions.itemDescription[i] = InscriptionsInventory.inscriptionSlots[i].itemDescription;
                slots[SlotInteraction.hoveredId].inscriptions.manaCost[i] = InscriptionsInventory.inscriptionSlots[i].manaCost;
                slots[SlotInteraction.hoveredId].inscriptions.weaponSize[i] = InscriptionsInventory.inscriptionSlots[i].weaponSize;
                slots[SlotInteraction.hoveredId].inscriptions.attackSpeed[i] = InscriptionsInventory.inscriptionSlots[i].attackSpeed;
                slots[SlotInteraction.hoveredId].inscriptions.tripleAttackChance[i] = InscriptionsInventory.inscriptionSlots[i].tripleAttackChance;
                slots[SlotInteraction.hoveredId].inscriptions.secondUsageChance[i] = InscriptionsInventory.inscriptionSlots[i].secondUsageChance;
                slots[SlotInteraction.hoveredId].inscriptions.explosionChance[i] = InscriptionsInventory.inscriptionSlots[i].explosionChance;
                slots[SlotInteraction.hoveredId].inscriptions.explosionType[i] = InscriptionsInventory.inscriptionSlots[i].explosionType;
                slots[SlotInteraction.hoveredId].inscriptions.weaponCooldown[i] = InscriptionsInventory.inscriptionSlots[i].weaponCooldown;
                slots[SlotInteraction.hoveredId].inscriptions.createProjectileChance[i] = InscriptionsInventory.inscriptionSlots[i].createProjectileChance;
                slots[SlotInteraction.hoveredId].inscriptions.spikes[i] = InscriptionsInventory.inscriptionSlots[i].spikes;
                slots[SlotInteraction.hoveredId].inscriptions.pierce[i] = InscriptionsInventory.inscriptionSlots[i].pierce;
                slots[SlotInteraction.hoveredId].inscriptions.extraPierceChance[i] = InscriptionsInventory.inscriptionSlots[i].extraPierceChance;
                //
            }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

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
                slots.Add(objects[i]);

        }
        for (int i = 0; i < armourObjects.Count; i++)
        {
            if (i % 2 == 0)
                slots.Add(armourObjects[i]);

        }
        for (int i = 0; i < inscriptionObjects.Count; i++)
        {
            if (i % 2 == 0)
                slots.Add(inscriptionObjects[i]);
        }
    }
    private void FixedUpdate()
    {

    }
    public static bool IsInventoryFull(GameObject item)
    {
        for (int i = 0; i < 16; i++)
        {
            if (slots[i].type == "Empty" || (slots[i].type == item.GetComponent<Slot>().type && slots[i].values[32] < slots[i].values[31]))
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
        if (slots[firstFreeSlot].values[32] < slots[firstFreeSlot].values[31] && slots[firstFreeSlot].values[32] > 0 && slots[firstFreeSlot].values[28] == item.GetComponent<Slot>().values[28])
            slots[firstFreeSlot].values[32]++;
        else
        {
            slots[firstFreeSlot].values = item.GetComponent<Slot>().values; //Применение всех свойств
            slots[firstFreeSlot].type = item.GetComponent<Slot>().type;
            slots[firstFreeSlot].sprite = item.GetComponent<Slot>().sprite;
            slots[firstFreeSlot].itemDescription = item.GetComponent<Slot>().itemDescription.Replace("\\n","\n");
            slots[firstFreeSlot].rareList = item.GetComponent<Slot>().rareList;
            slots[firstFreeSlot].rareChances = item.GetComponent<Slot>().rareChances;
            slots[firstFreeSlot].rareName = rareName;
            slots[firstFreeSlot].weaponSprite = item.GetComponent<Slot>().weaponSprite;
            slots[firstFreeSlot].projectileSprite = item.GetComponent<Slot>().projectileSprite;
            slots[firstFreeSlot].useEvent = item.GetComponent<Slot>().useEvent;
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
        for (int i = 0; i < InscriptionsInventory.inscriptionSlots.Count; i++)
        {
            //
            InscriptionsInventory.inscriptionSlots[i].values[2] = slots[SlotInteraction.hoveredId].inscriptions.damage[i];
            InscriptionsInventory.inscriptionSlots[i].values[3] = slots[SlotInteraction.hoveredId].inscriptions.iceDamage[i];
            InscriptionsInventory.inscriptionSlots[i].values[4] = slots[SlotInteraction.hoveredId].inscriptions.igniteDamage[i];
            InscriptionsInventory.inscriptionSlots[i].values[5] = slots[SlotInteraction.hoveredId].inscriptions.lightningDamage[i];
            InscriptionsInventory.inscriptionSlots[i].values[6] = slots[SlotInteraction.hoveredId].inscriptions.poisonDamage[i];
            InscriptionsInventory.inscriptionSlots[i].values[7] = slots[SlotInteraction.hoveredId].inscriptions.voidDamage[i];
            InscriptionsInventory.inscriptionSlots[i].values[8] = slots[SlotInteraction.hoveredId].inscriptions.pureDamage[i];
            InscriptionsInventory.inscriptionSlots[i].values[1] = slots[SlotInteraction.hoveredId].inscriptions.defence[i];
            InscriptionsInventory.inscriptionSlots[i].values[9] = slots[SlotInteraction.hoveredId].inscriptions.iceResist[i];
            InscriptionsInventory.inscriptionSlots[i].values[10] = slots[SlotInteraction.hoveredId].inscriptions.igniteResist[i];
            InscriptionsInventory.inscriptionSlots[i].values[11] = slots[SlotInteraction.hoveredId].inscriptions.lightningResist[i];
            InscriptionsInventory.inscriptionSlots[i].values[12] = slots[SlotInteraction.hoveredId].inscriptions.poisonResist[i];
            InscriptionsInventory.inscriptionSlots[i].values[13] = slots[SlotInteraction.hoveredId].inscriptions.voidResist[i];
            InscriptionsInventory.inscriptionSlots[i].type = slots[SlotInteraction.hoveredId].inscriptions.type[i];
            InscriptionsInventory.inscriptionSlots[i].sprite = slots[SlotInteraction.hoveredId].inscriptions.sprite[i];
            InscriptionsInventory.inscriptionSlots[i].values[0] = slots[SlotInteraction.hoveredId].inscriptions.hp[i];
            InscriptionsInventory.inscriptionSlots[i].values[14] = slots[SlotInteraction.hoveredId].inscriptions.evasionChance[i];
            InscriptionsInventory.inscriptionSlots[i].values[15] = slots[SlotInteraction.hoveredId].inscriptions.criticalChance[i];
            InscriptionsInventory.inscriptionSlots[i].values[30] = slots[SlotInteraction.hoveredId].inscriptions.kind[i];
            InscriptionsInventory.inscriptionSlots[i].values[31] = slots[SlotInteraction.hoveredId].inscriptions.stackAmount[i];
            InscriptionsInventory.inscriptionSlots[i].values[32] = slots[SlotInteraction.hoveredId].inscriptions.stacksAlready[i];
            InscriptionsInventory.inscriptionSlots[i].values[28] = slots[SlotInteraction.hoveredId].inscriptions.idItem[i];
            InscriptionsInventory.inscriptionSlots[i].itemDescription = slots[SlotInteraction.hoveredId].inscriptions.itemDescription[i];
            InscriptionsInventory.inscriptionSlots[i].values[16] = slots[SlotInteraction.hoveredId].inscriptions.manaCost[i];
            InscriptionsInventory.inscriptionSlots[i].values[17] = slots[SlotInteraction.hoveredId].inscriptions.weaponSize[i];
            InscriptionsInventory.inscriptionSlots[i].values[18] = slots[SlotInteraction.hoveredId].inscriptions.attackSpeed[i];
            InscriptionsInventory.inscriptionSlots[i].values[19] = slots[SlotInteraction.hoveredId].inscriptions.tripleAttackChance[i];
            InscriptionsInventory.inscriptionSlots[i].values[20] = slots[SlotInteraction.hoveredId].inscriptions.secondUsageChance[i];
            InscriptionsInventory.inscriptionSlots[i].values[21] = slots[SlotInteraction.hoveredId].inscriptions.explosionChance[i];
            InscriptionsInventory.inscriptionSlots[i].values[22] = slots[SlotInteraction.hoveredId].inscriptions.explosionType[i];
            InscriptionsInventory.inscriptionSlots[i].values[23] = slots[SlotInteraction.hoveredId].inscriptions.weaponCooldown[i];
            InscriptionsInventory.inscriptionSlots[i].values[24] = slots[SlotInteraction.hoveredId].inscriptions.createProjectileChance[i];
            InscriptionsInventory.inscriptionSlots[i].values[25] = slots[SlotInteraction.hoveredId].inscriptions.spikes[i];
            InscriptionsInventory.inscriptionSlots[i].values[26] = slots[SlotInteraction.hoveredId].inscriptions.pierce[i];
            InscriptionsInventory.inscriptionSlots[i].values[27] = slots[SlotInteraction.hoveredId].inscriptions.extraPierceChance[i];      
        }
    }
    public void AcceptFeatures()
    {
            for (int i = 0; i < InscriptionsInventory.inscriptionSlots.Count; i++)
            {
                slots[SlotInteraction.hoveredId].inscriptions.damage[i] = InscriptionsInventory.inscriptionSlots[i].values[2];
                slots[SlotInteraction.hoveredId].inscriptions.iceDamage[i] = InscriptionsInventory.inscriptionSlots[i].values[3];
                slots[SlotInteraction.hoveredId].inscriptions.igniteDamage[i] = InscriptionsInventory.inscriptionSlots[i].values[4];
                slots[SlotInteraction.hoveredId].inscriptions.lightningDamage[i] = InscriptionsInventory.inscriptionSlots[i].values[5];
                slots[SlotInteraction.hoveredId].inscriptions.poisonDamage[i] = InscriptionsInventory.inscriptionSlots[i].values[6];
                slots[SlotInteraction.hoveredId].inscriptions.voidDamage[i] = InscriptionsInventory.inscriptionSlots[i].values[7];
                slots[SlotInteraction.hoveredId].inscriptions.pureDamage[i] = InscriptionsInventory.inscriptionSlots[i].values[8];
                slots[SlotInteraction.hoveredId].inscriptions.defence[i] = InscriptionsInventory.inscriptionSlots[i].values[1];
                slots[SlotInteraction.hoveredId].inscriptions.iceResist[i] = InscriptionsInventory.inscriptionSlots[i].values[9];
                slots[SlotInteraction.hoveredId].inscriptions.igniteResist[i] = InscriptionsInventory.inscriptionSlots[i].values[10];
                slots[SlotInteraction.hoveredId].inscriptions.lightningResist[i] = InscriptionsInventory.inscriptionSlots[i].values[11];
                slots[SlotInteraction.hoveredId].inscriptions.poisonResist[i] = InscriptionsInventory.inscriptionSlots[i].values[12];
                slots[SlotInteraction.hoveredId].inscriptions.voidResist[i] = InscriptionsInventory.inscriptionSlots[i].values[13];
                slots[SlotInteraction.hoveredId].inscriptions.type[i] = InscriptionsInventory.inscriptionSlots[i].type;
                slots[SlotInteraction.hoveredId].inscriptions.sprite[i] = InscriptionsInventory.inscriptionSlots[i].sprite;
                slots[SlotInteraction.hoveredId].inscriptions.hp[i] = InscriptionsInventory.inscriptionSlots[i].values[0];
                slots[SlotInteraction.hoveredId].inscriptions.evasionChance[i] = InscriptionsInventory.inscriptionSlots[i].values[14];
                slots[SlotInteraction.hoveredId].inscriptions.criticalChance[i] = InscriptionsInventory.inscriptionSlots[i].values[15];
                slots[SlotInteraction.hoveredId].inscriptions.kind[i] = InscriptionsInventory.inscriptionSlots[i].kind;
                slots[SlotInteraction.hoveredId].inscriptions.stackAmount[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[31];
                slots[SlotInteraction.hoveredId].inscriptions.stacksAlready[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[32];
                slots[SlotInteraction.hoveredId].inscriptions.idItem[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[28];
                slots[SlotInteraction.hoveredId].inscriptions.itemDescription[i] = InscriptionsInventory.inscriptionSlots[i].itemDescription;
                slots[SlotInteraction.hoveredId].inscriptions.manaCost[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[16];
                slots[SlotInteraction.hoveredId].inscriptions.weaponSize[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[17];
                slots[SlotInteraction.hoveredId].inscriptions.attackSpeed[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[18];
                slots[SlotInteraction.hoveredId].inscriptions.tripleAttackChance[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[19];
                slots[SlotInteraction.hoveredId].inscriptions.secondUsageChance[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[20];
                slots[SlotInteraction.hoveredId].inscriptions.explosionChance[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[21];
                slots[SlotInteraction.hoveredId].inscriptions.explosionType[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[22];
                slots[SlotInteraction.hoveredId].inscriptions.weaponCooldown[i] = InscriptionsInventory.inscriptionSlots[i].values[23];
                slots[SlotInteraction.hoveredId].inscriptions.createProjectileChance[i] = InscriptionsInventory.inscriptionSlots[i].values[24];
                slots[SlotInteraction.hoveredId].inscriptions.spikes[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[25];
                slots[SlotInteraction.hoveredId].inscriptions.pierce[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[26];
                slots[SlotInteraction.hoveredId].inscriptions.extraPierceChance[i] = (int)InscriptionsInventory.inscriptionSlots[i].values[27];
                //
            }
    }
}

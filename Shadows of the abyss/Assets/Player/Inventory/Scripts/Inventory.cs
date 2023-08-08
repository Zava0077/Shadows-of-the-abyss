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
    [SerializeField] GameObject gO;
    static bool isFull = false;
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
        for (int i = 0; i < 4; i++)
        { //шаблон для выдачи предметов
            IsInventoryFull();
            if (slots[firstFreeSlot].stacksAlready < slots[firstFreeSlot].stackAmount && slots[firstFreeSlot].stacksAlready>0)
                slots[firstFreeSlot].stacksAlready++;
            else
            {
                slots[firstFreeSlot].damage = gO.GetComponent<Slot>().damage;
                slots[firstFreeSlot].iceDamage = gO.GetComponent<Slot>().iceDamage;
                slots[firstFreeSlot].igniteDamage = gO.GetComponent<Slot>().igniteDamage;
                slots[firstFreeSlot].lightningDamage = gO.GetComponent<Slot>().lightningDamage;
                slots[firstFreeSlot].poisonDamage = gO.GetComponent<Slot>().poisonDamage;
                slots[firstFreeSlot].voidDamage = gO.GetComponent<Slot>().voidDamage;
                slots[firstFreeSlot].pureDamage = gO.GetComponent<Slot>().pureDamage;
                slots[firstFreeSlot].defence = gO.GetComponent<Slot>().defence;
                slots[firstFreeSlot].iceResist = gO.GetComponent<Slot>().iceResist;
                slots[firstFreeSlot].igniteResist = gO.GetComponent<Slot>().igniteResist;
                slots[firstFreeSlot].lightningResist = gO.GetComponent<Slot>().lightningResist;
                slots[firstFreeSlot].poisonResist = gO.GetComponent<Slot>().poisonResist;
                slots[firstFreeSlot].voidResist = gO.GetComponent<Slot>().voidResist;
                slots[firstFreeSlot].pureResist = gO.GetComponent<Slot>().pureResist;
                slots[firstFreeSlot].type = gO.GetComponent<Slot>().type;
                slots[firstFreeSlot].sprite = gO.GetComponent<Slot>().sprite;
                slots[firstFreeSlot].hp = gO.GetComponent<Slot>().hp;
                slots[firstFreeSlot].evasionChance = gO.GetComponent<Slot>().evasionChance;
                slots[firstFreeSlot].criticalChance = gO.GetComponent<Slot>().criticalChance;
                slots[firstFreeSlot].kind = gO.GetComponent<Slot>().kind;
                slots[firstFreeSlot].stackAmount = gO.GetComponent<Slot>().stackAmount;
                slots[firstFreeSlot].stacksAlready = gO.GetComponent<Slot>().stacksAlready;
                slots[firstFreeSlot].idItem = gO.GetComponent<Slot>().idItem;
                slots[firstFreeSlot].itemDescription = gO.GetComponent<Slot>().itemDescription;
                slots[firstFreeSlot].rareList = gO.GetComponent<Slot>().rareList;
                slots[firstFreeSlot].rareChances = gO.GetComponent<Slot>().rareChances;
                slots[firstFreeSlot].rareName = gO.GetComponent<Slot>().rareName;
            }
        }
    }
    private void FixedUpdate()
    {
        //if (!IsInventoryFull()) //
        //{
       //}
    }
    static bool IsInventoryFull()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].type == "Empty" || slots[i].stacksAlready < slots[i].stackAmount)
            {
                FreeSlotDetector(i);
                return isFull = false;
            }
            if (i == slots.Count - 1)
                return isFull = true;
        }
        return isFull;
    }
    void LootItem(GameObject[] gameObjects, int idItem, int chance) //
    {
        System.Random rnd = new System.Random();
        string[] rareList = gameObjects[idItem].GetComponent<Slot>().rareList;
        int[] rareChances = gameObjects[idItem].GetComponent<Slot>().rareChances;
        int rareChance = rnd.Next(0,100);
        string rareName = "";
        for(int i = 0;i<rareList.Length ;i++ )
        {
            if (rareChance < 100 - rareChances[i])
            {
                rareName = rareList[i];
                break;
            }
            else i++;
        }
        if (slots[firstFreeSlot].stacksAlready < slots[firstFreeSlot].stackAmount && slots[firstFreeSlot].stacksAlready > 0 && slots[firstFreeSlot].kind == gameObjects[idItem].GetComponent<Slot>().kind)
            slots[firstFreeSlot].stacksAlready++;
        else
        {
            slots[firstFreeSlot].damage = gameObjects[idItem].GetComponent<Slot>().damage;
            slots[firstFreeSlot].iceDamage = gameObjects[idItem].GetComponent<Slot>().iceDamage;
            slots[firstFreeSlot].igniteDamage = gameObjects[idItem].GetComponent<Slot>().igniteDamage;
            slots[firstFreeSlot].lightningDamage = gameObjects[idItem].GetComponent<Slot>().lightningDamage;
            slots[firstFreeSlot].poisonDamage = gameObjects[idItem].GetComponent<Slot>().poisonDamage;
            slots[firstFreeSlot].voidDamage = gameObjects[idItem].GetComponent<Slot>().voidDamage;
            slots[firstFreeSlot].pureDamage = gameObjects[idItem].GetComponent<Slot>().pureDamage;
            slots[firstFreeSlot].defence = gameObjects[idItem].GetComponent<Slot>().defence;
            slots[firstFreeSlot].iceResist = gameObjects[idItem].GetComponent<Slot>().iceResist;
            slots[firstFreeSlot].igniteResist = gameObjects[idItem].GetComponent<Slot>().igniteResist;
            slots[firstFreeSlot].lightningResist = gameObjects[idItem].GetComponent<Slot>().lightningResist;
            slots[firstFreeSlot].poisonResist = gameObjects[idItem].GetComponent<Slot>().poisonResist;
            slots[firstFreeSlot].voidResist = gameObjects[idItem].GetComponent<Slot>().voidResist;
            slots[firstFreeSlot].pureResist = gameObjects[idItem].GetComponent<Slot>().pureResist;
            slots[firstFreeSlot].type = gameObjects[idItem].GetComponent<Slot>().type;
            slots[firstFreeSlot].sprite = gameObjects[idItem].GetComponent<Slot>().sprite;
            slots[firstFreeSlot].hp = gameObjects[idItem].GetComponent<Slot>().hp;
            slots[firstFreeSlot].evasionChance = gameObjects[idItem].GetComponent<Slot>().evasionChance;
            slots[firstFreeSlot].criticalChance = gameObjects[idItem].GetComponent<Slot>().criticalChance;
            slots[firstFreeSlot].kind = gameObjects[idItem].GetComponent<Slot>().kind;
            slots[firstFreeSlot].stackAmount = gameObjects[idItem].GetComponent<Slot>().stackAmount;
            slots[firstFreeSlot].stacksAlready = gameObjects[idItem].GetComponent<Slot>().stacksAlready;
            slots[firstFreeSlot].idItem = gameObjects[idItem].GetComponent<Slot>().idItem;
            slots[firstFreeSlot].rareList = gameObjects[idItem].GetComponent<Slot>().rareList;
            slots[firstFreeSlot].rareChances = gameObjects[idItem].GetComponent<Slot>().rareChances;
            slots[firstFreeSlot].rareName = rareName;
        }
    }
    static void FreeSlotDetector(int freeSlotId)
    {
        firstFreeSlot = freeSlotId;
    }
}

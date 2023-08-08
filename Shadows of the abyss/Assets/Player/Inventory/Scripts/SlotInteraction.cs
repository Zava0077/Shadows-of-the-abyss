using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotInteraction : Slot, IPointerClickHandler
{
    Vector3 cursor;
    bool isFolowing;
    public static float bufferHp;
    public static float bufferDamage;
    public static float bufferIceDamage;
    public static float bufferIgniteDamage;
    public static float bufferLightningDamage;
    public static float bufferPoisonDamage;
    public static float bufferVoidDamage;
    public static float bufferPureDamage;
    public static float bufferDefence;
    public static float bufferIceResist;
    public static float bufferIgniteResist;
    public static float bufferLightningResist;
    public static float bufferPoisonResist;
    public static float bufferVoidResist;
    public static float bufferPureResist;
    public static float bufferEvasionChance;
    public static float bufferCriticalChance;
    public static int bufferStackAmount;
    public static int bufferStacksAlready;
    public static int bufferKind;
    public static string bufferType;
    public static int bufferIdItem;
    public static string bufferDescription;
    public static string[] bufferRareList;
    public static string bufferRareName;
    public static int[] bufferRareChances;
    static GameObject bufferWeapon;
    static Sprite bufferEmptySprite;
    public static Sprite bufferSprite;
    [SerializeField] GameObject defaultSlot;
    private void Update()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.z = 0;
    }
    private void FixedUpdate()
    {
        
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                DragItem();
                break;
            case PointerEventData.InputButton.Right:
                RightClick();
                break;
        }
    }
    public void DragItem()
    {
        if(slots[gameObject.GetComponent<Slot>().id].stackAmount != 0 && slots[gameObject.GetComponent<Slot>().id].stacksAlready <= slots[gameObject.GetComponent<Slot>().id].stackAmount && CursorSlot.self.idItem == slots[gameObject.GetComponent<Slot>().id].idItem)
        {
            if(slots[gameObject.GetComponent<Slot>().id].stacksAlready < slots[gameObject.GetComponent<Slot>().id].stackAmount)
            {//отдать РАЗНИЦУ stacksAlready в слот инвентаря, когда stacksAlready У КУРСОРА становится 0 написать метод сводящий все переменные в дефолт
                for(; slots[gameObject.GetComponent<Slot>().id].stacksAlready < slots[gameObject.GetComponent<Slot>().id].stackAmount && CursorSlot.self.stacksAlready > 0;)
                {
                    slots[gameObject.GetComponent<Slot>().id].stacksAlready++;
                    if(CursorSlot.self.stacksAlready - 1 >= 0)
                        CursorSlot.self.stacksAlready--;
                }
            }
            else if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == slots[gameObject.GetComponent<Slot>().id].stackAmount)
                Switch();
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря
      
        }
        else if((CursorSlot.self.kind == slots[gameObject.GetComponent<Slot>().id].kind && gameObject.GetComponent<Slot>().id > 16) || (CursorSlot.self.type == "Empty" || gameObject.GetComponent<Slot>().id < 16))
        {
            Switch();
        }
    }
    void RightClick()
    {
        if (slots[gameObject.GetComponent<Slot>().id].stacksAlready > 0 && ((CursorSlot.self.stacksAlready < CursorSlot.self.stackAmount && CursorSlot.self.type != "Empty")))
        {
            CursorSlot.self.damage = slots[gameObject.GetComponent<Slot>().id].damage;
            CursorSlot.self.iceDamage = slots[gameObject.GetComponent<Slot>().id].iceDamage;
            CursorSlot.self.igniteDamage = slots[gameObject.GetComponent<Slot>().id].igniteDamage;
            CursorSlot.self.lightningDamage = slots[gameObject.GetComponent<Slot>().id].lightningDamage;
            CursorSlot.self.poisonDamage = slots[gameObject.GetComponent<Slot>().id].poisonDamage;
            CursorSlot.self.voidDamage = slots[gameObject.GetComponent<Slot>().id].voidDamage;
            CursorSlot.self.pureDamage = slots[gameObject.GetComponent<Slot>().id].pureDamage;
            CursorSlot.self.defence = slots[gameObject.GetComponent<Slot>().id].defence;
            CursorSlot.self.iceResist = slots[gameObject.GetComponent<Slot>().id].iceResist;
            CursorSlot.self.igniteResist = slots[gameObject.GetComponent<Slot>().id].igniteResist;
            CursorSlot.self.lightningResist = slots[gameObject.GetComponent<Slot>().id].lightningResist;
            CursorSlot.self.poisonResist = slots[gameObject.GetComponent<Slot>().id].poisonResist;
            CursorSlot.self.voidResist = slots[gameObject.GetComponent<Slot>().id].voidResist;
            CursorSlot.self.pureResist = slots[gameObject.GetComponent<Slot>().id].pureResist;
            CursorSlot.self.type = slots[gameObject.GetComponent<Slot>().id].type;
            CursorSlot.self.sprite = slots[gameObject.GetComponent<Slot>().id].sprite;
            CursorSlot.self.hp = slots[gameObject.GetComponent<Slot>().id].hp;
            CursorSlot.self.evasionChance = slots[gameObject.GetComponent<Slot>().id].evasionChance;
            CursorSlot.self.criticalChance = slots[gameObject.GetComponent<Slot>().id].criticalChance;
            CursorSlot.self.kind = slots[gameObject.GetComponent<Slot>().id].kind;
            CursorSlot.self.stackAmount = slots[gameObject.GetComponent<Slot>().id].stackAmount;
            CursorSlot.self.idItem = slots[gameObject.GetComponent<Slot>().id].idItem;
            CursorSlot.self.itemDescription = slots[gameObject.GetComponent<Slot>().id].itemDescription;
            CursorSlot.self.rareList = slots[gameObject.GetComponent<Slot>().id].rareList;
            CursorSlot.self.rareChances = slots[gameObject.GetComponent<Slot>().id].rareChances;
            CursorSlot.self.rareName = slots[gameObject.GetComponent<Slot>().id].rareName;
            CursorSlot.self.stacksAlready++;
            slots[gameObject.GetComponent<Slot>().id].stacksAlready--;
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (CursorSlot.self.type == "Empty")
        {
            CursorSlot.self.damage = slots[gameObject.GetComponent<Slot>().id].damage;
            CursorSlot.self.iceDamage = slots[gameObject.GetComponent<Slot>().id].iceDamage;
            CursorSlot.self.igniteDamage = slots[gameObject.GetComponent<Slot>().id].igniteDamage;
            CursorSlot.self.lightningDamage = slots[gameObject.GetComponent<Slot>().id].lightningDamage;
            CursorSlot.self.poisonDamage = slots[gameObject.GetComponent<Slot>().id].poisonDamage;
            CursorSlot.self.voidDamage = slots[gameObject.GetComponent<Slot>().id].voidDamage;
            CursorSlot.self.pureDamage = slots[gameObject.GetComponent<Slot>().id].pureDamage;
            CursorSlot.self.defence = slots[gameObject.GetComponent<Slot>().id].defence;
            CursorSlot.self.iceResist = slots[gameObject.GetComponent<Slot>().id].iceResist;
            CursorSlot.self.igniteResist = slots[gameObject.GetComponent<Slot>().id].igniteResist;
            CursorSlot.self.lightningResist = slots[gameObject.GetComponent<Slot>().id].lightningResist;
            CursorSlot.self.poisonResist = slots[gameObject.GetComponent<Slot>().id].poisonResist;
            CursorSlot.self.voidResist = slots[gameObject.GetComponent<Slot>().id].voidResist;
            CursorSlot.self.pureResist = slots[gameObject.GetComponent<Slot>().id].pureResist;
            CursorSlot.self.type = slots[gameObject.GetComponent<Slot>().id].type;
            CursorSlot.self.sprite = slots[gameObject.GetComponent<Slot>().id].sprite;
            CursorSlot.self.hp = slots[gameObject.GetComponent<Slot>().id].hp;
            CursorSlot.self.evasionChance = slots[gameObject.GetComponent<Slot>().id].evasionChance;
            CursorSlot.self.criticalChance = slots[gameObject.GetComponent<Slot>().id].criticalChance;
            CursorSlot.self.kind = slots[gameObject.GetComponent<Slot>().id].kind;
            CursorSlot.self.stackAmount = slots[gameObject.GetComponent<Slot>().id].stackAmount;
            CursorSlot.self.idItem = slots[gameObject.GetComponent<Slot>().id].idItem;
            CursorSlot.self.itemDescription = slots[gameObject.GetComponent<Slot>().id].itemDescription;
            CursorSlot.self.rareList = slots[gameObject.GetComponent<Slot>().id].rareList;
            CursorSlot.self.rareChances = slots[gameObject.GetComponent<Slot>().id].rareChances;
            CursorSlot.self.rareName = slots[gameObject.GetComponent<Slot>().id].rareName;
            CursorSlot.self.stacksAlready += Mathf.CeilToInt((float)(slots[gameObject.GetComponent<Slot>().id].stacksAlready) / 2);
            slots[gameObject.GetComponent<Slot>().id].stacksAlready -= Mathf.CeilToInt((float)(slots[gameObject.GetComponent<Slot>().id].stacksAlready) / 2);
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (slots[gameObject.GetComponent<Slot>().id].type == "Empty")
        {
            slots[gameObject.GetComponent<Slot>().id].iceDamage= CursorSlot.self.iceDamage;
            slots[gameObject.GetComponent<Slot>().id].igniteDamage = CursorSlot.self.igniteDamage;
            slots[gameObject.GetComponent<Slot>().id].lightningDamage= CursorSlot.self.lightningDamage;
            slots[gameObject.GetComponent<Slot>().id].poisonDamage= CursorSlot.self.poisonDamage;
            slots[gameObject.GetComponent<Slot>().id].voidDamage =   CursorSlot.self.voidDamage;
            slots[gameObject.GetComponent<Slot>().id].pureDamage= CursorSlot.self.pureDamage;
            slots[gameObject.GetComponent<Slot>().id].defence= CursorSlot.self.defence;
            slots[gameObject.GetComponent<Slot>().id].iceResist=CursorSlot.self.iceResist;
            slots[gameObject.GetComponent<Slot>().id].igniteResist= CursorSlot.self.igniteResist;
            slots[gameObject.GetComponent<Slot>().id].lightningResist= CursorSlot.self.lightningResist;
            slots[gameObject.GetComponent<Slot>().id].poisonResist= CursorSlot.self.poisonResist;
            slots[gameObject.GetComponent<Slot>().id].voidResist= CursorSlot.self.voidResist;
            slots[gameObject.GetComponent<Slot>().id].pureResist =CursorSlot.self.pureResist;
            slots[gameObject.GetComponent<Slot>().id].type= CursorSlot.self.type;
            slots[gameObject.GetComponent<Slot>().id].sprite =CursorSlot.self.sprite;
            slots[gameObject.GetComponent<Slot>().id].hp =CursorSlot.self.hp;
            slots[gameObject.GetComponent<Slot>().id].evasionChance =CursorSlot.self.evasionChance;
            slots[gameObject.GetComponent<Slot>().id].criticalChance = CursorSlot.self.criticalChance;
            slots[gameObject.GetComponent<Slot>().id].kind =CursorSlot.self.kind;
            slots[gameObject.GetComponent<Slot>().id].stackAmount =CursorSlot.self.stackAmount;
            slots[gameObject.GetComponent<Slot>().id].idItem=CursorSlot.self.idItem;
            slots[gameObject.GetComponent<Slot>().id].itemDescription = CursorSlot.self.itemDescription;
            slots[gameObject.GetComponent<Slot>().id].rareList = CursorSlot.self.rareList;
            slots[gameObject.GetComponent<Slot>().id].rareChances = CursorSlot.self.rareChances;
            slots[gameObject.GetComponent<Slot>().id].rareName = CursorSlot.self.rareName;
            CursorSlot.self.stacksAlready--;
            slots[gameObject.GetComponent<Slot>().id].stacksAlready++;
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else
            Switch();
    }
    void Switch()
    {
        bufferDamage = CursorSlot.self.damage;
        bufferIceDamage = CursorSlot.self.iceDamage;
        bufferIgniteDamage = CursorSlot.self.igniteDamage;
        bufferLightningDamage = CursorSlot.self.lightningDamage;
        bufferPoisonDamage = CursorSlot.self.poisonDamage;
        bufferVoidDamage = CursorSlot.self.voidDamage;
        bufferPureDamage = CursorSlot.self.pureDamage;
        bufferDefence = CursorSlot.self.defence;
        bufferIceResist = CursorSlot.self.iceResist;
        bufferIgniteResist = CursorSlot.self.igniteResist;
        bufferLightningResist = CursorSlot.self.lightningResist;
        bufferPoisonResist = CursorSlot.self.poisonResist;
        bufferVoidResist = CursorSlot.self.voidResist;
        bufferPureResist = CursorSlot.self.pureResist;
        bufferType = CursorSlot.self.type;
        bufferSprite = CursorSlot.self.sprite;
        bufferHp = CursorSlot.self.hp;
        bufferEvasionChance = CursorSlot.self.evasionChance;
        bufferCriticalChance = CursorSlot.self.criticalChance;
        bufferKind = CursorSlot.self.kind;
        bufferStackAmount = CursorSlot.self.stackAmount;
        bufferStacksAlready = CursorSlot.self.stacksAlready;
        bufferIdItem = CursorSlot.self.idItem;
        bufferDescription = CursorSlot.self.itemDescription;
        bufferRareList = CursorSlot.self.rareList;
        bufferRareChances = CursorSlot.self.rareChances;
        bufferRareName = CursorSlot.self.rareName;
        //

        CursorSlot.self.damage = slots[gameObject.GetComponent<Slot>().id].damage;
        CursorSlot.self.iceDamage = slots[gameObject.GetComponent<Slot>().id].iceDamage;
        CursorSlot.self.igniteDamage = slots[gameObject.GetComponent<Slot>().id].igniteDamage;
        CursorSlot.self.lightningDamage = slots[gameObject.GetComponent<Slot>().id].lightningDamage;
        CursorSlot.self.poisonDamage = slots[gameObject.GetComponent<Slot>().id].poisonDamage;
        CursorSlot.self.voidDamage = slots[gameObject.GetComponent<Slot>().id].voidDamage;
        CursorSlot.self.pureDamage = slots[gameObject.GetComponent<Slot>().id].pureDamage;
        CursorSlot.self.defence = slots[gameObject.GetComponent<Slot>().id].defence;
        CursorSlot.self.iceResist = slots[gameObject.GetComponent<Slot>().id].iceResist;
        CursorSlot.self.igniteResist = slots[gameObject.GetComponent<Slot>().id].igniteResist;
        CursorSlot.self.lightningResist = slots[gameObject.GetComponent<Slot>().id].lightningResist;
        CursorSlot.self.poisonResist = slots[gameObject.GetComponent<Slot>().id].poisonResist;
        CursorSlot.self.voidResist = slots[gameObject.GetComponent<Slot>().id].voidResist;
        CursorSlot.self.pureResist = slots[gameObject.GetComponent<Slot>().id].pureResist;
        CursorSlot.self.type = slots[gameObject.GetComponent<Slot>().id].type;
        CursorSlot.self.sprite = slots[gameObject.GetComponent<Slot>().id].sprite;
        CursorSlot.self.hp = slots[gameObject.GetComponent<Slot>().id].hp;
        CursorSlot.self.evasionChance = slots[gameObject.GetComponent<Slot>().id].evasionChance;
        CursorSlot.self.criticalChance = slots[gameObject.GetComponent<Slot>().id].criticalChance;
        CursorSlot.self.kind = slots[gameObject.GetComponent<Slot>().id].kind;
        CursorSlot.self.stackAmount = slots[gameObject.GetComponent<Slot>().id].stackAmount;
        CursorSlot.self.stacksAlready = slots[gameObject.GetComponent<Slot>().id].stacksAlready;
        CursorSlot.self.idItem = slots[gameObject.GetComponent<Slot>().id].idItem;
        CursorSlot.self.itemDescription = slots[gameObject.GetComponent<Slot>().id].itemDescription;
        CursorSlot.self.rareChances = slots[gameObject.GetComponent<Slot>().id].rareChances;
        CursorSlot.self.rareList = slots[gameObject.GetComponent<Slot>().id].rareList;
        CursorSlot.self.rareName = slots[gameObject.GetComponent<Slot>().id].rareName;
        //
        slots[gameObject.GetComponent<Slot>().id].damage = bufferDamage;
        slots[gameObject.GetComponent<Slot>().id].iceDamage = bufferIceDamage;
        slots[gameObject.GetComponent<Slot>().id].igniteDamage = bufferIgniteDamage;
        slots[gameObject.GetComponent<Slot>().id].lightningDamage = bufferLightningDamage;
        slots[gameObject.GetComponent<Slot>().id].poisonDamage = bufferPoisonDamage;
        slots[gameObject.GetComponent<Slot>().id].voidDamage = bufferVoidDamage;
        slots[gameObject.GetComponent<Slot>().id].pureDamage = bufferPureDamage;
        slots[gameObject.GetComponent<Slot>().id].defence = bufferDefence;
        slots[gameObject.GetComponent<Slot>().id].iceResist = bufferIceResist;
        slots[gameObject.GetComponent<Slot>().id].igniteResist = bufferIgniteResist;
        slots[gameObject.GetComponent<Slot>().id].lightningResist = bufferLightningResist;
        slots[gameObject.GetComponent<Slot>().id].poisonResist = bufferPoisonResist;
        slots[gameObject.GetComponent<Slot>().id].voidResist = bufferVoidResist;
        slots[gameObject.GetComponent<Slot>().id].pureResist = bufferPureResist;
        slots[gameObject.GetComponent<Slot>().id].type = bufferType;
        slots[gameObject.GetComponent<Slot>().id].sprite = bufferSprite;
        slots[gameObject.GetComponent<Slot>().id].hp = bufferHp;
        slots[gameObject.GetComponent<Slot>().id].evasionChance = bufferEvasionChance;
        slots[gameObject.GetComponent<Slot>().id].criticalChance = bufferCriticalChance;
        slots[gameObject.GetComponent<Slot>().id].kind = bufferKind;
        slots[gameObject.GetComponent<Slot>().id].stackAmount = bufferStackAmount;
        slots[gameObject.GetComponent<Slot>().id].stacksAlready = bufferStacksAlready;
        slots[gameObject.GetComponent<Slot>().id].idItem = bufferIdItem;
        slots[gameObject.GetComponent<Slot>().id].itemDescription = bufferDescription;
        slots[gameObject.GetComponent<Slot>().id].rareList = bufferRareList;
        slots[gameObject.GetComponent<Slot>().id].rareChances = bufferRareChances;
        slots[gameObject.GetComponent<Slot>().id].rareName = bufferRareName;
    } 
    void ToDefault(int i)
    {
        switch (i)
        {
            case 0:
                CursorSlot.self.damage = defaultSlot.GetComponent<Slot>().damage;
                CursorSlot.self.iceDamage = defaultSlot.GetComponent<Slot>().iceDamage;
                CursorSlot.self.igniteDamage = defaultSlot.GetComponent<Slot>().igniteDamage;
                CursorSlot.self.lightningDamage = defaultSlot.GetComponent<Slot>().lightningDamage;
                CursorSlot.self.poisonDamage = defaultSlot.GetComponent<Slot>().poisonDamage;
                CursorSlot.self.voidDamage = defaultSlot.GetComponent<Slot>().voidDamage;
                CursorSlot.self.pureDamage = defaultSlot.GetComponent<Slot>().pureDamage;
                CursorSlot.self.defence = defaultSlot.GetComponent<Slot>().defence;
                CursorSlot.self.iceResist = defaultSlot.GetComponent<Slot>().iceResist;
                CursorSlot.self.igniteResist = defaultSlot.GetComponent<Slot>().igniteResist;
                CursorSlot.self.lightningResist = defaultSlot.GetComponent<Slot>().lightningResist;
                CursorSlot.self.poisonResist = defaultSlot.GetComponent<Slot>().poisonResist;
                CursorSlot.self.voidResist = defaultSlot.GetComponent<Slot>().voidResist;
                CursorSlot.self.pureResist = defaultSlot.GetComponent<Slot>().pureResist;
                CursorSlot.self.type = defaultSlot.GetComponent<Slot>().type;
                CursorSlot.self.sprite = defaultSlot.GetComponent<Slot>().sprite;
                CursorSlot.self.hp = defaultSlot.GetComponent<Slot>().hp;
                CursorSlot.self.evasionChance = defaultSlot.GetComponent<Slot>().evasionChance;
                CursorSlot.self.criticalChance = defaultSlot.GetComponent<Slot>().criticalChance;
                CursorSlot.self.kind = defaultSlot.GetComponent<Slot>().kind;
                CursorSlot.self.stackAmount = defaultSlot.GetComponent<Slot>().stackAmount;
                CursorSlot.self.stacksAlready = defaultSlot.GetComponent<Slot>().stacksAlready;
                CursorSlot.self.idItem = defaultSlot.GetComponent<Slot>().idItem;
                CursorSlot.self.itemDescription = defaultSlot.GetComponent<Slot>().itemDescription;
                CursorSlot.self.rareList = defaultSlot.GetComponent<Slot>().rareList;
                CursorSlot.self.rareChances = defaultSlot.GetComponent<Slot>().rareChances;
                CursorSlot.self.rareName = defaultSlot.GetComponent<Slot>().rareName;
                break;
            case 1:
                slots[gameObject.GetComponent<Slot>().id].damage = defaultSlot.GetComponent<Slot>().damage;
                slots[gameObject.GetComponent<Slot>().id].iceDamage = defaultSlot.GetComponent<Slot>().iceDamage;
                slots[gameObject.GetComponent<Slot>().id].igniteDamage = defaultSlot.GetComponent<Slot>().igniteDamage;
                slots[gameObject.GetComponent<Slot>().id].lightningDamage = defaultSlot.GetComponent<Slot>().lightningDamage;
                slots[gameObject.GetComponent<Slot>().id].poisonDamage = defaultSlot.GetComponent<Slot>().poisonDamage;
                slots[gameObject.GetComponent<Slot>().id].voidDamage = defaultSlot.GetComponent<Slot>().voidDamage;
                slots[gameObject.GetComponent<Slot>().id].pureDamage = defaultSlot.GetComponent<Slot>().pureDamage;
                slots[gameObject.GetComponent<Slot>().id].defence = defaultSlot.GetComponent<Slot>().defence;
                slots[gameObject.GetComponent<Slot>().id].iceResist = defaultSlot.GetComponent<Slot>().iceResist;
                slots[gameObject.GetComponent<Slot>().id].igniteResist = defaultSlot.GetComponent<Slot>().igniteResist;
                slots[gameObject.GetComponent<Slot>().id].lightningResist = defaultSlot.GetComponent<Slot>().lightningResist;
                slots[gameObject.GetComponent<Slot>().id].poisonResist = defaultSlot.GetComponent<Slot>().poisonResist;
                slots[gameObject.GetComponent<Slot>().id].voidResist = defaultSlot.GetComponent<Slot>().voidResist;
                slots[gameObject.GetComponent<Slot>().id].pureResist = defaultSlot.GetComponent<Slot>().pureResist;
                slots[gameObject.GetComponent<Slot>().id].type = defaultSlot.GetComponent<Slot>().type;
                slots[gameObject.GetComponent<Slot>().id].sprite = defaultSlot.GetComponent<Slot>().sprite;
                slots[gameObject.GetComponent<Slot>().id].hp = defaultSlot.GetComponent<Slot>().hp;
                slots[gameObject.GetComponent<Slot>().id].evasionChance = defaultSlot.GetComponent<Slot>().evasionChance;
                slots[gameObject.GetComponent<Slot>().id].criticalChance = defaultSlot.GetComponent<Slot>().criticalChance;
                slots[gameObject.GetComponent<Slot>().id].kind = defaultSlot.GetComponent<Slot>().kind;
                slots[gameObject.GetComponent<Slot>().id].stackAmount = defaultSlot.GetComponent<Slot>().stackAmount;
                slots[gameObject.GetComponent<Slot>().id].stacksAlready = defaultSlot.GetComponent<Slot>().stacksAlready;
                slots[gameObject.GetComponent<Slot>().id].idItem = defaultSlot.GetComponent<Slot>().idItem;
                slots[gameObject.GetComponent<Slot>().id].itemDescription = defaultSlot.GetComponent<Slot>().itemDescription;
                slots[gameObject.GetComponent<Slot>().id].rareList = defaultSlot.GetComponent<Slot>().rareList;
                slots[gameObject.GetComponent<Slot>().id].rareChances = defaultSlot.GetComponent<Slot>().rareChances;
                slots[gameObject.GetComponent<Slot>().id].rareName = defaultSlot.GetComponent<Slot>().rareName;
                break;
        }
    }
}

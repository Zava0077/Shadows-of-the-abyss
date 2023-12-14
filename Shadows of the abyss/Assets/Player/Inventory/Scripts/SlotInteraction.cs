using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotInteraction : Slot, IPointerClickHandler
{
    Vector3 cursor;
    bool isFolowing;

    [SerializeField] public float[] bufferValues;
    public Usable.UsableEvent bufferUseEvent;
    #region OldCode
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

 
    //
    public static int bufferManaCost;
    public static int bufferWeaponSize;
    public static int bufferAttackSpeed;
    public static int bufferTripleAttackChance;
    public static int bufferSecondUsageChance;
    public static int bufferExplosionChance;
    public static int bufferExplosionType;
    public static float bufferWeaponCooldown;

    public static int hoveredId;
    public static bool isHovered;

    public static float bufferCreateProjectileChance;
    public static int bufferSpikes;
    public static int bufferPierce;
    public static int bufferExtraPierceChance;
    #endregion
    public static string bufferDescription;
    public static string[] bufferRareList;
    public static string bufferRareName;
    public static int[] bufferRareChances;
    public static Insctiprions bufferInscriptions = new Insctiprions();
    public static int bufferInscNum;
    //
    static GameObject bufferWeapon;
    static Sprite bufferEmptySprite;
    public static Sprite bufferSprite;
    public static Sprite bufferProjSprite;
    public static Sprite bufferWeaponSprite;
    static bool isSwitching;
    [SerializeField] GameObject defaultSlot; //поменять
    
    private void Update()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.z = 0;
        if (Input.GetButtonDown("Fire1") && !isHovered && CursorSlot.self.type == "Usable")
        {
            CursorSlot.self.useEvent(); //Вызывать метод ивент прямо из класса объекта
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case PointerEventData.InputButton.Left:
                if (!isSwitching)
                {
                    isSwitching = true;
                    DragItem();
                } 
                break;
            case PointerEventData.InputButton.Right:
                if (!isSwitching)
                {
                    isSwitching = true;
                    RightClick();
                }
                break;
        }
    }
    public void DragItem()
    {
        if (slots[gameObject.GetComponent<Slot>().id].values[31] != 0 && slots[gameObject.GetComponent<Slot>().id].values[32] <= slots[gameObject.GetComponent<Slot>().id].values[31] && CursorSlot.self.values[28] == slots[gameObject.GetComponent<Slot>().id].values[28] && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
        {
            if (slots[gameObject.GetComponent<Slot>().id].values[32] < slots[gameObject.GetComponent<Slot>().id].values[31])
            {
                for (; slots[gameObject.GetComponent<Slot>().id].values[32] < slots[gameObject.GetComponent<Slot>().id].values[31] && CursorSlot.self.values[32] > 0;)
                {
                    if (slots[gameObject.GetComponent<Slot>().id].gameObject != CursorSlot.self.gameObject)
                    {
                        slots[gameObject.GetComponent<Slot>().id].values[32]++;
                        if (CursorSlot.self.values[32] - 1 >= 0)
                            CursorSlot.self.values[32]--;
                    }
                }
            }
            else if (slots[gameObject.GetComponent<Slot>().id].values[32] == slots[gameObject.GetComponent<Slot>().id].values[31])
                Switch();
            if (CursorSlot.self.values[32] == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].values[32] == 0)
                ToDefault(1);//удалять предмет с инвентаря

        }
        else if (gameObject.GetComponent<Slot>().id < 16 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch();
        else if (gameObject.GetComponent<Slot>().id > 27 + slots[hoveredId].values[29])
            ;
        else if (CursorSlot.self.values[30] == slots[gameObject.GetComponent<Slot>().id].staticKind && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch();
        else if (CursorSlot.self.type == "Empty" && gameObject.GetComponent<Slot>().id >= 16 && slots[gameObject.GetComponent<Slot>().id].values[30] != 0 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
           Switch();
        isSwitching = false;
    }
    void RightClick()
    {
        if (slots[gameObject.GetComponent<Slot>().id].values[32] > 0 && ((CursorSlot.self.values[32] < CursorSlot.self.values[31] && CursorSlot.self.type != "Empty") && gameObject.GetComponent<Slot>().id < 16))
        {
            for (int i = 0; i < CursorSlot.self.values.Length; i++)
                if (CursorSlot.self.valuesNames[i] != "StacksAlready")
                    CursorSlot.self.values[i] = slots[gameObject.GetComponent<Slot>().id].values[i]; //Применение всех свойств
            CursorSlot.self.type = slots[gameObject.GetComponent<Slot>().id].type;
            CursorSlot.self.sprite = slots[gameObject.GetComponent<Slot>().id].sprite;
            CursorSlot.self.itemDescription = slots[gameObject.GetComponent<Slot>().id].itemDescription;
            CursorSlot.self.rareList = slots[gameObject.GetComponent<Slot>().id].rareList;
            CursorSlot.self.rareChances = slots[gameObject.GetComponent<Slot>().id].rareChances;
            CursorSlot.self.rareName = slots[gameObject.GetComponent<Slot>().id].rareName;
            CursorSlot.self.weaponSprite = slots[gameObject.GetComponent<Slot>().id].weaponSprite;
            CursorSlot.self.projectileSprite = slots[gameObject.GetComponent<Slot>().id].projectileSprite;
            //
            CursorSlot.self.inscriptions.damage = slots[gameObject.GetComponent<Slot>().id].inscriptions.damage;
            CursorSlot.self.inscriptions.iceDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.iceDamage;
            CursorSlot.self.inscriptions.igniteDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteDamage;
            CursorSlot.self.inscriptions.lightningDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningDamage;
            CursorSlot.self.inscriptions.poisonDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonDamage;
            CursorSlot.self.inscriptions.voidDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.voidDamage;
            CursorSlot.self.inscriptions.pureDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.pureDamage;
            CursorSlot.self.inscriptions.defence = slots[gameObject.GetComponent<Slot>().id].inscriptions.defence;
            CursorSlot.self.inscriptions.iceResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.iceResist;
            CursorSlot.self.inscriptions.igniteResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteResist;
            CursorSlot.self.inscriptions.lightningResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningResist;
            CursorSlot.self.inscriptions.poisonResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonResist;
            CursorSlot.self.inscriptions.voidResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.voidResist;
            CursorSlot.self.inscriptions.pureResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.pureResist;
            CursorSlot.self.inscriptions.type = slots[gameObject.GetComponent<Slot>().id].inscriptions.type;
            CursorSlot.self.inscriptions.sprite = slots[gameObject.GetComponent<Slot>().id].inscriptions.sprite;
            CursorSlot.self.inscriptions.hp = slots[gameObject.GetComponent<Slot>().id].inscriptions.hp;
            CursorSlot.self.inscriptions.evasionChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.evasionChance;
            CursorSlot.self.inscriptions.criticalChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.criticalChance;
            CursorSlot.self.inscriptions.kind = slots[gameObject.GetComponent<Slot>().id].inscriptions.kind;
            CursorSlot.self.inscriptions.stackAmount = slots[gameObject.GetComponent<Slot>().id].inscriptions.stackAmount;
            CursorSlot.self.inscriptions.idItem = slots[gameObject.GetComponent<Slot>().id].inscriptions.idItem;
            CursorSlot.self.inscriptions.itemDescription = slots[gameObject.GetComponent<Slot>().id].inscriptions.itemDescription;
            CursorSlot.self.inscriptions.manaCost = slots[gameObject.GetComponent<Slot>().id].inscriptions.manaCost;
            CursorSlot.self.inscriptions.weaponSize = slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponSize;
            CursorSlot.self.inscriptions.attackSpeed = slots[gameObject.GetComponent<Slot>().id].inscriptions.attackSpeed;
            CursorSlot.self.inscriptions.tripleAttackChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.tripleAttackChance;
            CursorSlot.self.inscriptions.secondUsageChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.secondUsageChance;
            CursorSlot.self.inscriptions.explosionChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionChance;
            CursorSlot.self.inscriptions.explosionType = slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionType;
            CursorSlot.self.inscriptions.weaponCooldown = slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponCooldown;
            CursorSlot.self.inscriptions.createProjectileChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.createProjectileChance;
            CursorSlot.self.inscriptions.spikes = slots[gameObject.GetComponent<Slot>().id].inscriptions.spikes;
            CursorSlot.self.inscriptions.pierce = slots[gameObject.GetComponent<Slot>().id].inscriptions.pierce;
            CursorSlot.self.inscriptions.extraPierceChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.extraPierceChance;
            CursorSlot.self.values[32]++;
            slots[gameObject.GetComponent<Slot>().id].values[32]--;
            if (CursorSlot.self.values[32] == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].values[32] == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (CursorSlot.self.type == "Empty" && slots[gameObject.GetComponent<Slot>().id].values[31] > 0 && gameObject.GetComponent<Slot>().id < 16)
        {
            for (int i = 0; i < CursorSlot.self.values.Length; i++)
                if (CursorSlot.self.valuesNames[i] != "StacksAlready")
                    CursorSlot.self.values[i] = slots[gameObject.GetComponent<Slot>().id].values[i]; //Применение всех свойств

            CursorSlot.self.type = slots[gameObject.GetComponent<Slot>().id].type;
            CursorSlot.self.sprite = slots[gameObject.GetComponent<Slot>().id].sprite;
            CursorSlot.self.itemDescription = slots[gameObject.GetComponent<Slot>().id].itemDescription;
            CursorSlot.self.rareList = slots[gameObject.GetComponent<Slot>().id].rareList;
            CursorSlot.self.rareChances = slots[gameObject.GetComponent<Slot>().id].rareChances;
            CursorSlot.self.rareName = slots[gameObject.GetComponent<Slot>().id].rareName;
            CursorSlot.self.weaponSprite = slots[gameObject.GetComponent<Slot>().id].weaponSprite;
            CursorSlot.self.projectileSprite = slots[gameObject.GetComponent<Slot>().id].projectileSprite;
            //
            CursorSlot.self.inscriptions.damage = slots[gameObject.GetComponent<Slot>().id].inscriptions.damage;
            CursorSlot.self.inscriptions.iceDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.iceDamage;
            CursorSlot.self.inscriptions.igniteDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteDamage;
            CursorSlot.self.inscriptions.lightningDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningDamage;
            CursorSlot.self.inscriptions.poisonDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonDamage;
            CursorSlot.self.inscriptions.voidDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.voidDamage;
            CursorSlot.self.inscriptions.pureDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.pureDamage;
            CursorSlot.self.inscriptions.defence = slots[gameObject.GetComponent<Slot>().id].inscriptions.defence;
            CursorSlot.self.inscriptions.iceResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.iceResist;
            CursorSlot.self.inscriptions.igniteResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteResist;
            CursorSlot.self.inscriptions.lightningResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningResist;
            CursorSlot.self.inscriptions.poisonResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonResist;
            CursorSlot.self.inscriptions.voidResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.voidResist;
            CursorSlot.self.inscriptions.pureResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.pureResist;
            CursorSlot.self.inscriptions.type = slots[gameObject.GetComponent<Slot>().id].inscriptions.type;
            CursorSlot.self.inscriptions.sprite = slots[gameObject.GetComponent<Slot>().id].inscriptions.sprite;
            CursorSlot.self.inscriptions.hp = slots[gameObject.GetComponent<Slot>().id].inscriptions.hp;
            CursorSlot.self.inscriptions.evasionChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.evasionChance;
            CursorSlot.self.inscriptions.criticalChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.criticalChance;
            CursorSlot.self.inscriptions.kind = slots[gameObject.GetComponent<Slot>().id].inscriptions.kind;
            CursorSlot.self.inscriptions.stackAmount = slots[gameObject.GetComponent<Slot>().id].inscriptions.stackAmount;
            CursorSlot.self.inscriptions.idItem = slots[gameObject.GetComponent<Slot>().id].inscriptions.idItem;
            CursorSlot.self.inscriptions.itemDescription = slots[gameObject.GetComponent<Slot>().id].inscriptions.itemDescription;
            CursorSlot.self.inscriptions.manaCost = slots[gameObject.GetComponent<Slot>().id].inscriptions.manaCost;
            CursorSlot.self.inscriptions.weaponSize = slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponSize;
            CursorSlot.self.inscriptions.attackSpeed = slots[gameObject.GetComponent<Slot>().id].inscriptions.attackSpeed;
            CursorSlot.self.inscriptions.tripleAttackChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.tripleAttackChance;
            CursorSlot.self.inscriptions.secondUsageChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.secondUsageChance;
            CursorSlot.self.inscriptions.explosionChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionChance;
            CursorSlot.self.inscriptions.explosionType = slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionType;
            CursorSlot.self.inscriptions.weaponCooldown = slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponCooldown;
            CursorSlot.self.inscriptions.createProjectileChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.createProjectileChance;
            CursorSlot.self.inscriptions.spikes = slots[gameObject.GetComponent<Slot>().id].inscriptions.spikes;
            CursorSlot.self.inscriptions.pierce = slots[gameObject.GetComponent<Slot>().id].inscriptions.pierce;
            CursorSlot.self.inscriptions.extraPierceChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.extraPierceChance;

            CursorSlot.self.values[32] += Mathf.CeilToInt((float)(slots[gameObject.GetComponent<Slot>().id].values[32]) / 2);
            slots[gameObject.GetComponent<Slot>().id].values[32] -= Mathf.CeilToInt((float)(slots[gameObject.GetComponent<Slot>().id].values[32]) / 2);
            if (CursorSlot.self.values[32] == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].values[32] == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (CursorSlot.self.values[31] > 0 /*&& slots[gameObject.GetComponent<Slot>().id].stackAmount > 0 */&& slots[gameObject.GetComponent<Slot>().id].type == "Empty" && CursorSlot.self.type != "Empty" && gameObject.GetComponent<Slot>().id < 16)
        {
            for (int i = 0; i < CursorSlot.self.values.Length; i++)
                if (CursorSlot.self.valuesNames[i] != "StacksAlready")
                    slots[gameObject.GetComponent<Slot>().id].values[i] = CursorSlot.self.values[i];

            slots[gameObject.GetComponent<Slot>().id].type = CursorSlot.self.type;
            slots[gameObject.GetComponent<Slot>().id].sprite = CursorSlot.self.sprite;
            slots[gameObject.GetComponent<Slot>().id].itemDescription = CursorSlot.self.itemDescription;
            slots[gameObject.GetComponent<Slot>().id].rareList = CursorSlot.self.rareList;
            slots[gameObject.GetComponent<Slot>().id].rareChances = CursorSlot.self.rareChances;
            slots[gameObject.GetComponent<Slot>().id].rareName = CursorSlot.self.rareName;
            slots[gameObject.GetComponent<Slot>().id].weaponSprite = CursorSlot.self.weaponSprite;
            slots[gameObject.GetComponent<Slot>().id].projectileSprite = CursorSlot.self.projectileSprite;
            //
            slots[gameObject.GetComponent<Slot>().id].inscriptions.iceDamage = CursorSlot.self.inscriptions.iceDamage;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteDamage = CursorSlot.self.inscriptions.igniteDamage;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningDamage = CursorSlot.self.inscriptions.lightningDamage;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonDamage = CursorSlot.self.inscriptions.poisonDamage;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.voidDamage = CursorSlot.self.inscriptions.voidDamage;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.pureDamage = CursorSlot.self.inscriptions.pureDamage;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.defence = CursorSlot.self.inscriptions.defence;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.iceResist = CursorSlot.self.inscriptions.iceResist;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteResist = CursorSlot.self.inscriptions.igniteResist;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningResist = CursorSlot.self.inscriptions.lightningResist;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonResist = CursorSlot.self.inscriptions.poisonResist;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.voidResist = CursorSlot.self.inscriptions.voidResist;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.pureResist = CursorSlot.self.inscriptions.pureResist;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.type = CursorSlot.self.inscriptions.type;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.sprite = CursorSlot.self.inscriptions.sprite;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.hp = CursorSlot.self.inscriptions.hp;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.evasionChance = CursorSlot.self.inscriptions.evasionChance;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.criticalChance = CursorSlot.self.inscriptions.criticalChance;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.kind = CursorSlot.self.inscriptions.kind;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.stackAmount = CursorSlot.self.inscriptions.stackAmount;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.idItem = CursorSlot.self.inscriptions.idItem;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.itemDescription = CursorSlot.self.inscriptions.itemDescription;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.manaCost = CursorSlot.self.inscriptions.manaCost;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponSize = CursorSlot.self.inscriptions.weaponSize;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.attackSpeed = CursorSlot.self.inscriptions.attackSpeed;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.tripleAttackChance = CursorSlot.self.inscriptions.tripleAttackChance;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.secondUsageChance = CursorSlot.self.inscriptions.secondUsageChance;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionChance = CursorSlot.self.inscriptions.explosionChance;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionType = CursorSlot.self.inscriptions.explosionType;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponCooldown = CursorSlot.self.inscriptions.weaponCooldown;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.createProjectileChance = CursorSlot.self.inscriptions.createProjectileChance;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.spikes = CursorSlot.self.inscriptions.spikes;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.pierce = CursorSlot.self.inscriptions.pierce;
            slots[gameObject.GetComponent<Slot>().id].inscriptions.extraPierceChance = CursorSlot.self.inscriptions.extraPierceChance;

            CursorSlot.self.values[32]--;
            slots[gameObject.GetComponent<Slot>().id].values[32]++;
            if (CursorSlot.self.values[32] == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].values[32] == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (gameObject.GetComponent<Slot>().id < 16 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch();
        else if (gameObject.GetComponent<Slot>().id > 27 + slots[hoveredId].values[29])
            ;
        else if (CursorSlot.self.values[30] == slots[gameObject.GetComponent<Slot>().id].staticKind && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch();
        else if (CursorSlot.self.type == "Empty" && gameObject.GetComponent<Slot>().id >= 16 && slots[gameObject.GetComponent<Slot>().id].values[30] != 0 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch();
        isSwitching = false;
    }
    void SwitchReset()
    {
        isSwitching = false;
    }
    void Switch()
    {
        bufferValues = CursorSlot.self.values;
        bufferType = CursorSlot.self.type;
        bufferSprite = CursorSlot.self.sprite;

        bufferWeaponSprite = CursorSlot.self.weaponSprite;
        bufferProjSprite = CursorSlot.self.projectileSprite;

        bufferDescription = CursorSlot.self.itemDescription;
        bufferRareList = CursorSlot.self.rareList;
        bufferRareChances = CursorSlot.self.rareChances;
        bufferRareName = CursorSlot.self.rareName;
        bufferUseEvent = CursorSlot.self.useEvent;

        bufferInscriptions.damage = CursorSlot.self.inscriptions.damage;
        bufferInscriptions.iceDamage = CursorSlot.self.inscriptions.iceDamage;
        bufferInscriptions.igniteDamage = CursorSlot.self.inscriptions.igniteDamage;
        bufferInscriptions.lightningDamage = CursorSlot.self.inscriptions.lightningDamage;
        bufferInscriptions.poisonDamage = CursorSlot.self.inscriptions.poisonDamage;
        bufferInscriptions.voidDamage = CursorSlot.self.inscriptions.voidDamage;
        bufferInscriptions.pureDamage = CursorSlot.self.inscriptions.pureDamage;
        bufferInscriptions.defence = CursorSlot.self.inscriptions.defence;
        bufferInscriptions.iceResist = CursorSlot.self.inscriptions.iceResist;
        bufferInscriptions.igniteResist = CursorSlot.self.inscriptions.igniteResist;
        bufferInscriptions.lightningResist = CursorSlot.self.inscriptions.lightningResist;
        bufferInscriptions.poisonResist = CursorSlot.self.inscriptions.poisonResist;
        bufferInscriptions.voidResist = CursorSlot.self.inscriptions.voidResist;
        bufferInscriptions.pureResist = CursorSlot.self.inscriptions.pureResist;
        bufferInscriptions.type = CursorSlot.self.inscriptions.type;
        bufferInscriptions.sprite = CursorSlot.self.inscriptions.sprite;
        bufferInscriptions.hp = CursorSlot.self.inscriptions.hp;
        bufferInscriptions.evasionChance = CursorSlot.self.inscriptions.evasionChance;
        bufferInscriptions.criticalChance = CursorSlot.self.inscriptions.criticalChance;
        bufferInscriptions.kind = CursorSlot.self.inscriptions.kind;
        bufferInscriptions.stackAmount = CursorSlot.self.inscriptions.stackAmount;
        bufferInscriptions.idItem = CursorSlot.self.inscriptions.idItem;
        bufferInscriptions.itemDescription = CursorSlot.self.inscriptions.itemDescription;
        bufferInscriptions.manaCost = CursorSlot.self.inscriptions.manaCost;
        bufferInscriptions.weaponSize = CursorSlot.self.inscriptions.weaponSize;
        bufferInscriptions.attackSpeed = CursorSlot.self.inscriptions.attackSpeed;
        bufferInscriptions.tripleAttackChance = CursorSlot.self.inscriptions.tripleAttackChance;
        bufferInscriptions.secondUsageChance = CursorSlot.self.inscriptions.secondUsageChance;
        bufferInscriptions.explosionChance = CursorSlot.self.inscriptions.explosionChance;
        bufferInscriptions.explosionType = CursorSlot.self.inscriptions.explosionType;
        bufferInscriptions.weaponCooldown = CursorSlot.self.inscriptions.weaponCooldown;
        bufferInscriptions.createProjectileChance = CursorSlot.self.inscriptions.createProjectileChance;
        bufferInscriptions.spikes = CursorSlot.self.inscriptions.spikes;
        bufferInscriptions.pierce = CursorSlot.self.inscriptions.pierce;
        bufferInscriptions.extraPierceChance = CursorSlot.self.inscriptions.extraPierceChance;
        //
        CursorSlot.self.values = slots[gameObject.GetComponent<Slot>().id].values;

        CursorSlot.self.type = slots[gameObject.GetComponent<Slot>().id].type;
        CursorSlot.self.sprite = slots[gameObject.GetComponent<Slot>().id].sprite;
        CursorSlot.self.itemDescription = slots[gameObject.GetComponent<Slot>().id].itemDescription;
        CursorSlot.self.rareChances = slots[gameObject.GetComponent<Slot>().id].rareChances;
        CursorSlot.self.rareList = slots[gameObject.GetComponent<Slot>().id].rareList;
        CursorSlot.self.rareName = slots[gameObject.GetComponent<Slot>().id].rareName;
        CursorSlot.self.weaponSprite = slots[gameObject.GetComponent<Slot>().id].weaponSprite;
        CursorSlot.self.projectileSprite = slots[gameObject.GetComponent<Slot>().id].projectileSprite;
        CursorSlot.self.useEvent = slots[gameObject.GetComponent<Slot>().id].useEvent;
        //
        CursorSlot.self.inscriptions.damage = slots[gameObject.GetComponent<Slot>().id].inscriptions.damage;
        CursorSlot.self.inscriptions.iceDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.iceDamage;
        CursorSlot.self.inscriptions.igniteDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteDamage;
        CursorSlot.self.inscriptions.lightningDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningDamage;
        CursorSlot.self.inscriptions.poisonDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonDamage;
        CursorSlot.self.inscriptions.voidDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.voidDamage;
        CursorSlot.self.inscriptions.pureDamage = slots[gameObject.GetComponent<Slot>().id].inscriptions.pureDamage;
        CursorSlot.self.inscriptions.defence = slots[gameObject.GetComponent<Slot>().id].inscriptions.defence;
        CursorSlot.self.inscriptions.iceResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.iceResist;
        CursorSlot.self.inscriptions.igniteResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteResist;
        CursorSlot.self.inscriptions.lightningResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningResist;
        CursorSlot.self.inscriptions.poisonResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonResist;
        CursorSlot.self.inscriptions.voidResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.voidResist;
        CursorSlot.self.inscriptions.pureResist = slots[gameObject.GetComponent<Slot>().id].inscriptions.pureResist;
        CursorSlot.self.inscriptions.type = slots[gameObject.GetComponent<Slot>().id].inscriptions.type;
        CursorSlot.self.inscriptions.sprite = slots[gameObject.GetComponent<Slot>().id].inscriptions.sprite;
        CursorSlot.self.inscriptions.hp = slots[gameObject.GetComponent<Slot>().id].inscriptions.hp;
        CursorSlot.self.inscriptions.evasionChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.evasionChance;
        CursorSlot.self.inscriptions.criticalChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.criticalChance;
        CursorSlot.self.inscriptions.kind = slots[gameObject.GetComponent<Slot>().id].inscriptions.kind;
        CursorSlot.self.inscriptions.stackAmount = slots[gameObject.GetComponent<Slot>().id].inscriptions.stackAmount;
        CursorSlot.self.inscriptions.idItem = slots[gameObject.GetComponent<Slot>().id].inscriptions.idItem;
        CursorSlot.self.inscriptions.itemDescription = slots[gameObject.GetComponent<Slot>().id].inscriptions.itemDescription;
        CursorSlot.self.inscriptions.manaCost = slots[gameObject.GetComponent<Slot>().id].inscriptions.manaCost;
        CursorSlot.self.inscriptions.weaponSize = slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponSize;
        CursorSlot.self.inscriptions.attackSpeed = slots[gameObject.GetComponent<Slot>().id].inscriptions.attackSpeed;
        CursorSlot.self.inscriptions.tripleAttackChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.tripleAttackChance;
        CursorSlot.self.inscriptions.secondUsageChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.secondUsageChance;
        CursorSlot.self.inscriptions.explosionChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionChance;
        CursorSlot.self.inscriptions.explosionType = slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionType;
        CursorSlot.self.inscriptions.weaponCooldown = slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponCooldown;
        CursorSlot.self.inscriptions.createProjectileChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.createProjectileChance;
        CursorSlot.self.inscriptions.spikes = slots[gameObject.GetComponent<Slot>().id].inscriptions.spikes;
        CursorSlot.self.inscriptions.pierce = slots[gameObject.GetComponent<Slot>().id].inscriptions.pierce;
        CursorSlot.self.inscriptions.extraPierceChance = slots[gameObject.GetComponent<Slot>().id].inscriptions.extraPierceChance;
        //
        slots[gameObject.GetComponent<Slot>().id].values = bufferValues;

        slots[gameObject.GetComponent<Slot>().id].type = bufferType;
        slots[gameObject.GetComponent<Slot>().id].sprite = bufferSprite;
        slots[gameObject.GetComponent<Slot>().id].itemDescription = bufferDescription;
        slots[gameObject.GetComponent<Slot>().id].rareList = bufferRareList;
        slots[gameObject.GetComponent<Slot>().id].rareChances = bufferRareChances;
        slots[gameObject.GetComponent<Slot>().id].rareName = bufferRareName;
        slots[gameObject.GetComponent<Slot>().id].weaponSprite = bufferWeaponSprite;
        slots[gameObject.GetComponent<Slot>().id].projectileSprite = bufferProjSprite;
        slots[gameObject.GetComponent<Slot>().id].useEvent = bufferUseEvent;
        //
        slots[gameObject.GetComponent<Slot>().id].inscriptions.damage = bufferInscriptions.damage;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.iceDamage = bufferInscriptions.iceDamage;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteDamage = bufferInscriptions.igniteDamage;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningDamage = bufferInscriptions.lightningDamage;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonDamage = bufferInscriptions.poisonDamage;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.voidDamage = bufferInscriptions.voidDamage;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.pureDamage = bufferInscriptions.pureDamage;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.defence = bufferInscriptions.defence;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.iceResist = bufferInscriptions.iceResist;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.igniteResist = bufferInscriptions.igniteResist;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.lightningResist = bufferInscriptions.lightningResist;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.poisonResist = bufferInscriptions.poisonResist;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.voidResist = bufferInscriptions.voidResist;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.pureResist = bufferInscriptions.pureResist;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.type = bufferInscriptions.type;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.sprite = bufferInscriptions.sprite;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.hp = bufferInscriptions.hp;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.evasionChance = bufferInscriptions.evasionChance;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.criticalChance = bufferInscriptions.criticalChance;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.kind = bufferInscriptions.kind;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.stackAmount = bufferInscriptions.stackAmount;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.idItem = bufferInscriptions.idItem;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.itemDescription = bufferInscriptions.itemDescription;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.manaCost = bufferInscriptions.manaCost;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponSize = bufferInscriptions.weaponSize;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.attackSpeed = bufferInscriptions.attackSpeed;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.tripleAttackChance = bufferInscriptions.tripleAttackChance;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.secondUsageChance = bufferInscriptions.secondUsageChance;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionChance = bufferInscriptions.explosionChance;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.explosionType = bufferInscriptions.explosionType;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.weaponCooldown = bufferInscriptions.weaponCooldown;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.createProjectileChance = bufferInscriptions.createProjectileChance;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.spikes = bufferInscriptions.spikes;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.pierce = bufferInscriptions.pierce;
        slots[gameObject.GetComponent<Slot>().id].inscriptions.extraPierceChance = bufferInscriptions.extraPierceChance;
        //
    } 
    public void ToDefault(int i)
    {
        GameObject defSlot = defaultSlot;
        switch (i)
        {
            case 0:
                CursorSlot.self.values = new float[CursorSlot.self.values.Length];
                CursorSlot.self.type = defSlot.GetComponent<Slot>().type;
                CursorSlot.self.sprite = defSlot.GetComponent<Slot>().sprite;
                CursorSlot.self.itemDescription = defSlot.GetComponent<Slot>().itemDescription;
                CursorSlot.self.rareList = defSlot.GetComponent<Slot>().rareList;
                CursorSlot.self.rareChances = defSlot.GetComponent<Slot>().rareChances;
                CursorSlot.self.rareName = defSlot.GetComponent<Slot>().rareName;
                CursorSlot.self.weaponSprite = defSlot.GetComponent<Slot>().weaponSprite;
                CursorSlot.self.projectileSprite = defSlot.GetComponent<Slot>().projectileSprite;
                CursorSlot.self.useEvent = defSlot.GetComponent<Slot>().useEvent;
                break;
            case 1:
                slots[gameObject.GetComponent<Slot>().id].values = new float[slots[gameObject.GetComponent<Slot>().id].values.Length];
                slots[gameObject.GetComponent<Slot>().id].type = defSlot.GetComponent<Slot>().type;
                slots[gameObject.GetComponent<Slot>().id].sprite = defSlot.GetComponent<Slot>().sprite;
                slots[gameObject.GetComponent<Slot>().id].itemDescription = defSlot.GetComponent<Slot>().itemDescription;
                slots[gameObject.GetComponent<Slot>().id].rareList = defSlot.GetComponent<Slot>().rareList;
                slots[gameObject.GetComponent<Slot>().id].rareChances = defSlot.GetComponent<Slot>().rareChances;
                slots[gameObject.GetComponent<Slot>().id].rareName = defSlot.GetComponent<Slot>().rareName;
                slots[gameObject.GetComponent<Slot>().id].weaponSprite = defSlot.GetComponent<Slot>().weaponSprite;
                slots[gameObject.GetComponent<Slot>().id].projectileSprite = defSlot.GetComponent<Slot>().projectileSprite;
                slots[gameObject.GetComponent<Slot>().id].useEvent = defSlot.GetComponent<Slot>().useEvent;
                break;
        }
    }
}

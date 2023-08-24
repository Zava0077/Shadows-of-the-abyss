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
    public static Insctiprions bufferInscriptions = new Insctiprions();
    public static int bufferInscNum;
    //
    public static Sprite bufferProjSprite;
    public static Sprite bufferWeaponSprite;
    [SerializeField] GameObject defaultSlot;
    private void Update()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.z = 0;
        if (Input.GetButtonDown("Fire1") && !isHovered && CursorSlot.self.type == "Usable")
        {
            ;//Event Switch case
        }
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
        if (slots[gameObject.GetComponent<Slot>().id].stackAmount != 0 && slots[gameObject.GetComponent<Slot>().id].stacksAlready <= slots[gameObject.GetComponent<Slot>().id].stackAmount && CursorSlot.self.idItem == slots[gameObject.GetComponent<Slot>().id].idItem && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
        {
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready < slots[gameObject.GetComponent<Slot>().id].stackAmount)
            {//отдать РАЗНИЦУ stacksAlready в слот инвентаря, когда stacksAlready У КУРСОРА становится 0 написать метод сводящий все переменные в дефолт
                for (; slots[gameObject.GetComponent<Slot>().id].stacksAlready < slots[gameObject.GetComponent<Slot>().id].stackAmount && CursorSlot.self.stacksAlready > 0;)
                {
                    slots[gameObject.GetComponent<Slot>().id].stacksAlready++;
                    if (CursorSlot.self.stacksAlready - 1 >= 0)
                        CursorSlot.self.stacksAlready--;
                }
            }
            else if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == slots[gameObject.GetComponent<Slot>().id].stackAmount)
                Switch(true);
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря

        }
        else if (gameObject.GetComponent<Slot>().id < 16 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch(true);
        else if (gameObject.GetComponent<Slot>().id > 27 + slots[hoveredId].inscriptionNum)
            ;
        else if (CursorSlot.self.kind == slots[gameObject.GetComponent<Slot>().id].kind && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch(true);
        else if (CursorSlot.self.type == "Empty" && slots[gameObject.GetComponent<Slot>().id].kind != 0 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch(false);
    }
    void RightClick()
    {
        if (slots[gameObject.GetComponent<Slot>().id].stacksAlready > 0 && ((CursorSlot.self.stacksAlready < CursorSlot.self.stackAmount && CursorSlot.self.type != "Empty") && gameObject.GetComponent<Slot>().id < 16))
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
            CursorSlot.self.manaCost = slots[gameObject.GetComponent<Slot>().id].manaCost;
            CursorSlot.self.weaponSize = slots[gameObject.GetComponent<Slot>().id].weaponSize;
            CursorSlot.self.attackSpeed = slots[gameObject.GetComponent<Slot>().id].attackSpeed;
            CursorSlot.self.tripleAttackChance = slots[gameObject.GetComponent<Slot>().id].tripleAttackChance;
            CursorSlot.self.secondUsageChance = slots[gameObject.GetComponent<Slot>().id].secondUsageChance;
            CursorSlot.self.explosionChance = slots[gameObject.GetComponent<Slot>().id].explosionChance;
            CursorSlot.self.explosionType = slots[gameObject.GetComponent<Slot>().id].explosionType;
            CursorSlot.self.weaponCooldown = slots[gameObject.GetComponent<Slot>().id].weaponCooldown;
            CursorSlot.self.weaponSprite = slots[gameObject.GetComponent<Slot>().id].weaponSprite;
            CursorSlot.self.projectileSprite = slots[gameObject.GetComponent<Slot>().id].projectileSprite;
            CursorSlot.self.createProjectileChance = slots[gameObject.GetComponent<Slot>().id].createProjectileChance;
            CursorSlot.self.spikes = slots[gameObject.GetComponent<Slot>().id].spikes;
            CursorSlot.self.pierce = slots[gameObject.GetComponent<Slot>().id].pierce;
            CursorSlot.self.extraPierceChance = slots[gameObject.GetComponent<Slot>().id].extraPierceChance;
            CursorSlot.self.inscriptionNum = slots[gameObject.GetComponent<Slot>().id].inscriptionNum;
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
            CursorSlot.self.stacksAlready++;
            slots[gameObject.GetComponent<Slot>().id].stacksAlready--;
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (CursorSlot.self.type == "Empty" && slots[gameObject.GetComponent<Slot>().id].stackAmount > 0 && gameObject.GetComponent<Slot>().id < 16)
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
            CursorSlot.self.manaCost = slots[gameObject.GetComponent<Slot>().id].manaCost;
            CursorSlot.self.weaponSize = slots[gameObject.GetComponent<Slot>().id].weaponSize;
            CursorSlot.self.attackSpeed = slots[gameObject.GetComponent<Slot>().id].attackSpeed;
            CursorSlot.self.tripleAttackChance = slots[gameObject.GetComponent<Slot>().id].tripleAttackChance;
            CursorSlot.self.secondUsageChance = slots[gameObject.GetComponent<Slot>().id].secondUsageChance;
            CursorSlot.self.explosionChance = slots[gameObject.GetComponent<Slot>().id].explosionChance;
            CursorSlot.self.explosionType = slots[gameObject.GetComponent<Slot>().id].explosionType;
            CursorSlot.self.weaponCooldown = slots[gameObject.GetComponent<Slot>().id].weaponCooldown;
            CursorSlot.self.weaponSprite = slots[gameObject.GetComponent<Slot>().id].weaponSprite;
            CursorSlot.self.projectileSprite = slots[gameObject.GetComponent<Slot>().id].projectileSprite;
            CursorSlot.self.createProjectileChance = slots[gameObject.GetComponent<Slot>().id].createProjectileChance;
            CursorSlot.self.spikes = slots[gameObject.GetComponent<Slot>().id].spikes;
            CursorSlot.self.pierce = slots[gameObject.GetComponent<Slot>().id].pierce;
            CursorSlot.self.extraPierceChance = slots[gameObject.GetComponent<Slot>().id].extraPierceChance;
            //CursorSlot.self.inscriptions = slots[gameObject.GetComponent<Slot>().id].inscriptions;
            CursorSlot.self.inscriptionNum = slots[gameObject.GetComponent<Slot>().id].inscriptionNum;
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

            CursorSlot.self.stacksAlready += Mathf.CeilToInt((float)(slots[gameObject.GetComponent<Slot>().id].stacksAlready) / 2);
            slots[gameObject.GetComponent<Slot>().id].stacksAlready -= Mathf.CeilToInt((float)(slots[gameObject.GetComponent<Slot>().id].stacksAlready) / 2);
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (CursorSlot.self.stackAmount > 0 /*&& slots[gameObject.GetComponent<Slot>().id].stackAmount > 0 */&& slots[gameObject.GetComponent<Slot>().id].type == "Empty" && CursorSlot.self.type != "Empty" && gameObject.GetComponent<Slot>().id < 16)
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
            slots[gameObject.GetComponent<Slot>().id].manaCost = CursorSlot.self.manaCost;
            slots[gameObject.GetComponent<Slot>().id].weaponSize = CursorSlot.self.weaponSize;
            slots[gameObject.GetComponent<Slot>().id].attackSpeed = CursorSlot.self.attackSpeed;
            slots[gameObject.GetComponent<Slot>().id].tripleAttackChance = CursorSlot.self.tripleAttackChance;
            slots[gameObject.GetComponent<Slot>().id].secondUsageChance = CursorSlot.self.secondUsageChance;
            slots[gameObject.GetComponent<Slot>().id].explosionChance = CursorSlot.self.explosionChance;
            slots[gameObject.GetComponent<Slot>().id].explosionType = CursorSlot.self.explosionType;
            slots[gameObject.GetComponent<Slot>().id].weaponCooldown = CursorSlot.self.weaponCooldown;
            slots[gameObject.GetComponent<Slot>().id].weaponSprite = CursorSlot.self.weaponSprite;
            slots[gameObject.GetComponent<Slot>().id].projectileSprite = CursorSlot.self.projectileSprite;
            slots[gameObject.GetComponent<Slot>().id].createProjectileChance = CursorSlot.self.createProjectileChance;
            slots[gameObject.GetComponent<Slot>().id].spikes = CursorSlot.self.spikes;
            slots[gameObject.GetComponent<Slot>().id].pierce = CursorSlot.self.pierce;
            slots[gameObject.GetComponent<Slot>().id].extraPierceChance = CursorSlot.self.extraPierceChance;
            slots[gameObject.GetComponent<Slot>().id].inscriptionNum = CursorSlot.self.inscriptionNum;
            //[gameObject.GetComponent<Slot>().id].inscriptions = CursorSlot.self.inscriptions;
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

            CursorSlot.self.stacksAlready--;
            slots[gameObject.GetComponent<Slot>().id].stacksAlready++;
            if (CursorSlot.self.stacksAlready == 0)
                ToDefault(0);//удалять предмет с курсора
            if (slots[gameObject.GetComponent<Slot>().id].stacksAlready == 0)
                ToDefault(1);//удалять предмет с инвентаря
        }
        else if (gameObject.GetComponent<Slot>().id < 16 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch(true);
        else if (gameObject.GetComponent<Slot>().id > 27 + slots[hoveredId].inscriptionNum)
            ;
        else if (CursorSlot.self.kind == slots[gameObject.GetComponent<Slot>().id].kind && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch(true);
        else if (CursorSlot.self.type == "Empty" && slots[gameObject.GetComponent<Slot>().id].kind != 0 && slots[gameObject.GetComponent<Slot>().id].canBeReplaced)
            Switch(false);
    }
    void Switch(bool puttingAsArmour)
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
        bufferManaCost = CursorSlot.self.manaCost;
        bufferWeaponSize = CursorSlot.self.weaponSize;
        bufferAttackSpeed = CursorSlot.self.attackSpeed;
        bufferTripleAttackChance = CursorSlot.self.tripleAttackChance;
        bufferSecondUsageChance = CursorSlot.self.secondUsageChance;
        bufferExplosionChance = CursorSlot.self.explosionChance;
        bufferExplosionType = CursorSlot.self.explosionType;
        bufferWeaponCooldown = CursorSlot.self.weaponCooldown;
        bufferWeaponSprite = CursorSlot.self.weaponSprite;
        bufferProjSprite = CursorSlot.self.projectileSprite;
        bufferCreateProjectileChance = CursorSlot.self.createProjectileChance;
        bufferSpikes = CursorSlot.self.spikes;
        bufferPierce = CursorSlot.self.pierce;
        bufferExtraPierceChance = CursorSlot.self.extraPierceChance;
        bufferInscNum = CursorSlot.self.inscriptionNum;
        //bufferInscriptions = CursorSlot.self.inscriptions;
        //
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
        CursorSlot.self.manaCost = slots[gameObject.GetComponent<Slot>().id].manaCost;
        CursorSlot.self.weaponSize = slots[gameObject.GetComponent<Slot>().id].weaponSize;
        CursorSlot.self.attackSpeed = slots[gameObject.GetComponent<Slot>().id].attackSpeed;
        CursorSlot.self.tripleAttackChance = slots[gameObject.GetComponent<Slot>().id].tripleAttackChance;
        CursorSlot.self.secondUsageChance = slots[gameObject.GetComponent<Slot>().id].secondUsageChance;
        CursorSlot.self.explosionChance = slots[gameObject.GetComponent<Slot>().id].explosionChance;
        CursorSlot.self.explosionType = slots[gameObject.GetComponent<Slot>().id].explosionType;
        CursorSlot.self.weaponCooldown = slots[gameObject.GetComponent<Slot>().id].weaponCooldown;
        CursorSlot.self.weaponSprite = slots[gameObject.GetComponent<Slot>().id].weaponSprite;
        CursorSlot.self.projectileSprite = slots[gameObject.GetComponent<Slot>().id].projectileSprite;
        CursorSlot.self.createProjectileChance = slots[gameObject.GetComponent<Slot>().id].createProjectileChance;
        CursorSlot.self.spikes = slots[gameObject.GetComponent<Slot>().id].spikes;
        CursorSlot.self.pierce = slots[gameObject.GetComponent<Slot>().id].pierce;
        CursorSlot.self.extraPierceChance = slots[gameObject.GetComponent<Slot>().id].extraPierceChance;
        CursorSlot.self.inscriptionNum = slots[gameObject.GetComponent<Slot>().id].inscriptionNum;
        //CursorSlot.self.inscriptions = slots[gameObject.GetComponent<Slot>().id].inscriptions;
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
        if (puttingAsArmour)
            slots[gameObject.GetComponent<Slot>().id].kind = bufferKind;
        slots[gameObject.GetComponent<Slot>().id].stackAmount = bufferStackAmount;
        slots[gameObject.GetComponent<Slot>().id].stacksAlready = bufferStacksAlready;
        slots[gameObject.GetComponent<Slot>().id].idItem = bufferIdItem;
        slots[gameObject.GetComponent<Slot>().id].itemDescription = bufferDescription;
        slots[gameObject.GetComponent<Slot>().id].rareList = bufferRareList;
        slots[gameObject.GetComponent<Slot>().id].rareChances = bufferRareChances;
        slots[gameObject.GetComponent<Slot>().id].rareName = bufferRareName;
        slots[gameObject.GetComponent<Slot>().id].manaCost = bufferManaCost;
        slots[gameObject.GetComponent<Slot>().id].weaponSize = bufferWeaponSize;
        slots[gameObject.GetComponent<Slot>().id].attackSpeed = bufferAttackSpeed;
        slots[gameObject.GetComponent<Slot>().id].tripleAttackChance = bufferTripleAttackChance;
        slots[gameObject.GetComponent<Slot>().id].secondUsageChance = bufferSecondUsageChance;
        slots[gameObject.GetComponent<Slot>().id].explosionChance = bufferExplosionChance;
        slots[gameObject.GetComponent<Slot>().id].explosionType = bufferExplosionType;
        slots[gameObject.GetComponent<Slot>().id].weaponCooldown = bufferWeaponCooldown;
        slots[gameObject.GetComponent<Slot>().id].weaponSprite = bufferWeaponSprite;
        slots[gameObject.GetComponent<Slot>().id].projectileSprite = bufferProjSprite;
        slots[gameObject.GetComponent<Slot>().id].createProjectileChance = bufferCreateProjectileChance;
        slots[gameObject.GetComponent<Slot>().id].spikes = bufferSpikes;
        slots[gameObject.GetComponent<Slot>().id].pierce = bufferPierce;
        slots[gameObject.GetComponent<Slot>().id].extraPierceChance = bufferExtraPierceChance;
        slots[gameObject.GetComponent<Slot>().id].inscriptionNum = bufferInscNum;
        //slots[gameObject.GetComponent<Slot>().id].inscriptions = bufferInscriptions;
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
                CursorSlot.self.manaCost = defaultSlot.GetComponent<Slot>().manaCost;
                CursorSlot.self.weaponSize = defaultSlot.GetComponent<Slot>().weaponSize;
                CursorSlot.self.attackSpeed = defaultSlot.GetComponent<Slot>().attackSpeed;
                CursorSlot.self.tripleAttackChance = defaultSlot.GetComponent<Slot>().tripleAttackChance;
                CursorSlot.self.secondUsageChance = defaultSlot.GetComponent<Slot>().secondUsageChance;
                CursorSlot.self.explosionChance = defaultSlot.GetComponent<Slot>().explosionChance;
                CursorSlot.self.explosionType = defaultSlot.GetComponent<Slot>().explosionType;
                CursorSlot.self.weaponCooldown = defaultSlot.GetComponent<Slot>().weaponCooldown;
                CursorSlot.self.weaponSprite = defaultSlot.GetComponent<Slot>().weaponSprite;
                CursorSlot.self.projectileSprite = defaultSlot.GetComponent<Slot>().projectileSprite;
                CursorSlot.self.createProjectileChance = defaultSlot.GetComponent<Slot>().createProjectileChance;
                CursorSlot.self.spikes = defaultSlot.GetComponent<Slot>().spikes;
                CursorSlot.self.pierce = defaultSlot.GetComponent<Slot>().pierce;
                CursorSlot.self.extraPierceChance = defaultSlot.GetComponent<Slot>().extraPierceChance;
                CursorSlot.self.inscriptions = defaultSlot.GetComponent<Slot>().inscriptions;
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
                slots[gameObject.GetComponent<Slot>().id].manaCost = defaultSlot.GetComponent<Slot>().manaCost;
                slots[gameObject.GetComponent<Slot>().id].weaponSize = defaultSlot.GetComponent<Slot>().weaponSize;
                slots[gameObject.GetComponent<Slot>().id].attackSpeed = defaultSlot.GetComponent<Slot>().attackSpeed;
                slots[gameObject.GetComponent<Slot>().id].tripleAttackChance = defaultSlot.GetComponent<Slot>().tripleAttackChance;
                slots[gameObject.GetComponent<Slot>().id].secondUsageChance = defaultSlot.GetComponent<Slot>().secondUsageChance;
                slots[gameObject.GetComponent<Slot>().id].explosionChance = defaultSlot.GetComponent<Slot>().explosionChance;
                slots[gameObject.GetComponent<Slot>().id].explosionType = defaultSlot.GetComponent<Slot>().explosionType;
                slots[gameObject.GetComponent<Slot>().id].weaponCooldown = defaultSlot.GetComponent<Slot>().weaponCooldown;
                slots[gameObject.GetComponent<Slot>().id].weaponSprite = defaultSlot.GetComponent<Slot>().weaponSprite;
                slots[gameObject.GetComponent<Slot>().id].projectileSprite = defaultSlot.GetComponent<Slot>().projectileSprite;
                slots[gameObject.GetComponent<Slot>().id].createProjectileChance = defaultSlot.GetComponent<Slot>().createProjectileChance;
                slots[gameObject.GetComponent<Slot>().id].spikes = defaultSlot.GetComponent<Slot>().spikes;
                slots[gameObject.GetComponent<Slot>().id].pierce = defaultSlot.GetComponent<Slot>().pierce;
                slots[gameObject.GetComponent<Slot>().id].extraPierceChance = defaultSlot.GetComponent<Slot>().extraPierceChance;
                slots[gameObject.GetComponent<Slot>().id].inscriptions = defaultSlot.GetComponent<Slot>().inscriptions;
                break;
        }
    }
}

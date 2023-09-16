using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class Slot : Inventory
{
    [SerializeField] public float[] values;
    [SerializeField] public string[] valuesNames;
    #region oldCode
    [SerializeField] public float hp;
    [SerializeField] public float damage;
    [SerializeField] public float iceDamage;
    [SerializeField] public float igniteDamage;
    [SerializeField] public float lightningDamage;
    [SerializeField] public float poisonDamage;
    [SerializeField] public float voidDamage;
    [SerializeField] public float pureDamage;
    [SerializeField] public float defence;
    [SerializeField] public float iceResist;
    [SerializeField] public float igniteResist;
    [SerializeField] public float lightningResist;
    [SerializeField] public float poisonResist;
    [SerializeField] public float voidResist;
    [SerializeField] public float pureResist;
    [SerializeField] public float evasionChance;
    [SerializeField] public float criticalChance;
    [SerializeField] public int stackAmount;
    public int stacksAlready;
    [SerializeField] public int kind;
    [SerializeField] public string type;
    [SerializeField] public int id;
    [SerializeField] public int idItem;
    [SerializeField] public string itemDescription;
    [SerializeField] public string[] rareList;
    [SerializeField] public int[] rareChances;
    [SerializeField] public int manaCost;
    [SerializeField] public int weaponSize;
    [SerializeField] public int attackSpeed;
    [SerializeField] public int tripleAttackChance;
    [SerializeField] public int secondUsageChance;
    [SerializeField] public int explosionChance;
    [SerializeField] public int explosionType;
    [SerializeField] public float weaponCooldown;
    [SerializeField] public float createProjectileChance;
    [SerializeField] public int spikes;
    [SerializeField] public int pierce;
    [SerializeField] public int extraPierceChance;
    [SerializeField] public int inscriptionNum;
    [SerializeField] public Insctiprions inscriptions = new Insctiprions();
    [SerializeField] public Sprite projectileSprite;
    [SerializeField] public Sprite weaponSprite;
    [SerializeField] GameObject weapon;
    [SerializeField] Sprite emptySprite;
    [SerializeField] public Sprite sprite;
    #endregion
    public bool canBeReplaced = true;
    public string rareName;
    Text stacks;
    //Cold Lightning Fire Phys Poison Void Pure
    public Slot()
    {
        insctance = this;
    }
    public static Slot insctance;
    Image sR;
    private void Awake()
    {
        if (GetComponentInChildren<Text>() != null)
            stacks = GetComponentInChildren<Text>();
        sR = GetComponent<Image>();
        if (sprite != null)
            sR.sprite = sprite;
    }
    private void Update()
    {
        if (type == "Empty")
            sR.sprite = emptySprite;
        else sR.sprite = sprite;
        if(slots[id].stacksAlready != 0)
            stacks.text = slots[id].stacksAlready.ToString();
        if (stacksAlready == 0 && stacks != null)
            stacks.enabled = false;
        else if(stacks != null) stacks.enabled = true;
    }
}

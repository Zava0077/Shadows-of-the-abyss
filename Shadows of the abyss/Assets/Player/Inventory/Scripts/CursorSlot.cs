using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;


public class CursorSlot : MonoBehaviour
{
    [SerializeField] public float[] values;
    [SerializeField] public string[] valuesNames;
    #region OldCode
    //[SerializeField] public float hp;
    //[SerializeField] public float damage;
    //[SerializeField] public float iceDamage;
    //[SerializeField] public float igniteDamage;
    //[SerializeField] public float lightningDamage;
    //[SerializeField] public float poisonDamage;
    //[SerializeField] public float voidDamage;
    //[SerializeField] public float pureDamage;
    //[SerializeField] public float defence;
    //[SerializeField] public float iceResist;
    //[SerializeField] public float igniteResist;
    //[SerializeField] public float lightningResist;
    //[SerializeField] public float poisonResist;
    //[SerializeField] public float voidResist;
    //[SerializeField] public float pureResist;
    //[SerializeField] public float evasionChance;
    //[SerializeField] public float criticalChance;
    //[SerializeField] public int stackAmount;
    //public int stacksAlready;
    [SerializeField] public int kind;
    [SerializeField] public string type;
    [SerializeField] GameObject weapon;
    [SerializeField] Sprite emptySprite;
    [SerializeField] public Sprite sprite;
    [SerializeField] public Sprite bufferSprite;
    //[SerializeField] public int idItem;
    [SerializeField] public string itemDescription;
    [SerializeField] public string[] rareList;
    [SerializeField] public int[] rareChances;
    //
    //[SerializeField] public int manaCost;
    //[SerializeField] public int weaponSize;
    //[SerializeField] public int attackSpeed;
    //[SerializeField] public int tripleAttackChance;
    //[SerializeField] public int secondUsageChance;
    //[SerializeField] public int explosionChance;
    //[SerializeField] public int explosionType;
    //[SerializeField] public float weaponCooldown;
    //[SerializeField] public float createProjectileChance;
    //[SerializeField] public int spikes;
    //[SerializeField] public int pierce;
    //[SerializeField] public int extraPierceChance;
    //[SerializeField] public GameObject[] inscriptions;
    [SerializeField] public Insctiprions inscriptions = new Insctiprions();
    //
    [SerializeField] public Sprite projectileSprite;
    [SerializeField] public Sprite weaponSprite;
    //
    //[SerializeField] public int inscriptionNum;
    #endregion
    public string rareName;
    Vector3 cursor;
    SpriteRenderer sR;
    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        if (sprite != null)
            sR.sprite = sprite;
    }
    public CursorSlot()
    {
        self = this;
    }
    public static CursorSlot self;
    private void Update()
    {
        if (type == "Empty")
            sR.sprite = emptySprite;
        else sR.sprite = sprite;
        if (type == "Empty")
            values = new float[values.Length];
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.z = 0;
        gameObject.transform.position = cursor;
    }
}

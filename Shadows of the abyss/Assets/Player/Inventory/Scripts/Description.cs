using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class Description : MonoBehaviour
{
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
    [SerializeField] GameObject weapon;
    [SerializeField] public int idItem;
    [SerializeField] GameObject parentPanel;
    static public bool isChoosingSlot;
    Vector3 cursor;
    Vector2 position;
    SpriteRenderer sR;
    private void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
        gameObject.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
        gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
    }
    public Description()
    {
        self = this;
    }
    public static Description self;
    private void Update()
    {
        cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.z = 0;
        position = new Vector2(cursor.x,cursor.y);
        GetComponent<Transform>().position = position;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Text text = parentPanel.transform.Find("CursorSlot").GetComponentInChildren<Text>();
    
        if (collision.gameObject.tag == "Inventory" || collision.gameObject.tag == "ArmourInventory")
        {
            SlotInteraction.isHovered = true;
            if(isChoosingSlot)
            {
                SlotInteraction.hoveredId = collision.gameObject.GetComponent<Slot>().id;
                isChoosingSlot = false;
                Inventory.self.GetFeatures();
                if (Inventory.slots[SlotInteraction.hoveredId].values[29] > 0)
                {
                    Inventory.slots[SlotInteraction.hoveredId].canBeReplaced = false;
                    CameraBinds.self.inscMenu.SetActive(true);
                }
               
            }
            text.text = collision.gameObject.GetComponent<Slot>().itemDescription;
            GameObject item = collision.gameObject;
            for (int i = 0; i < item.GetComponent<Slot>().values[29]; i++) // позже отдельно добавлять доп описание:
            {
                if (item.GetComponent<Slot>().inscriptions.type[i] == "Empty") text.text += "\r\n" + "Slot " + (i + 1) + ": <Empty>";
                else
                {
                    if (item.GetComponent<Slot>().inscriptions.hp[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": Hp +" + item.GetComponent<Slot>().inscriptions.hp[i];
                    if (item.GetComponent<Slot>().inscriptions.damage[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": Damage +" + item.GetComponent<Slot>().inscriptions.damage[i];
                    if (item.GetComponent<Slot>().inscriptions.iceDamage[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": IceDamage +" + item.GetComponent<Slot>().inscriptions.iceDamage[i];
                    if (item.GetComponent<Slot>().inscriptions.igniteDamage[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": IgniteDamage +" + item.GetComponent<Slot>().inscriptions.igniteDamage[i];
                    if (item.GetComponent<Slot>().inscriptions.lightningDamage[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": LightningDamage +" + item.GetComponent<Slot>().inscriptions.lightningDamage[i];
                    if (item.GetComponent<Slot>().inscriptions.poisonDamage[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": PoisonDamage +" + item.GetComponent<Slot>().inscriptions.poisonDamage[i];
                    if (item.GetComponent<Slot>().inscriptions.voidDamage[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": VoidDamage +" + item.GetComponent<Slot>().inscriptions.voidDamage[i];
                    if (item.GetComponent<Slot>().inscriptions.pureDamage[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + " PureDamage +x" + item.GetComponent<Slot>().inscriptions.pureDamage[i];
                    if (item.GetComponent<Slot>().inscriptions.defence[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + " Defence +" + item.GetComponent<Slot>().inscriptions.defence[i];
                    if (item.GetComponent<Slot>().inscriptions.iceResist[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + " IceResist +" + item.GetComponent<Slot>().inscriptions.iceResist[i];
                    if (item.GetComponent<Slot>().inscriptions.igniteResist[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + " IgniteResist +" + item.GetComponent<Slot>().inscriptions.igniteResist[i];
                    if (item.GetComponent<Slot>().inscriptions.lightningResist[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": LightningResist +" + item.GetComponent<Slot>().inscriptions.lightningResist[i];
                    if (item.GetComponent<Slot>().inscriptions.poisonResist[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": PoisonResist +" + item.GetComponent<Slot>().inscriptions.poisonResist[i];
                    if (item.GetComponent<Slot>().inscriptions.voidResist[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": VoidResist +" + item.GetComponent<Slot>().inscriptions.voidResist[i];
                    if (item.GetComponent<Slot>().inscriptions.pureResist[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": PureResist +" + item.GetComponent<Slot>().inscriptions.pureResist[i];
                    if (item.GetComponent<Slot>().inscriptions.evasionChance[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": EvasionChance +" + item.GetComponent<Slot>().inscriptions.evasionChance[i];
                    if (item.GetComponent<Slot>().inscriptions.criticalChance[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": CriticalChance +" + item.GetComponent<Slot>().inscriptions.criticalChance[i];
                    if (item.GetComponent<Slot>().inscriptions.manaCost[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": ManaCost +" + item.GetComponent<Slot>().inscriptions.manaCost[i];
                    if (item.GetComponent<Slot>().inscriptions.weaponSize[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": WeaponSize +" + item.GetComponent<Slot>().inscriptions.weaponSize[i];
                    if (item.GetComponent<Slot>().inscriptions.attackSpeed[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": AttackSpeed +" + item.GetComponent<Slot>().inscriptions.attackSpeed[i];
                    if (item.GetComponent<Slot>().inscriptions.tripleAttackChance[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": TripleAttackChance +" + item.GetComponent<Slot>().inscriptions.tripleAttackChance[i];
                    if (item.GetComponent<Slot>().inscriptions.secondUsageChance[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": SecondUsageChance +" + item.GetComponent<Slot>().inscriptions.secondUsageChance[i];
                    if (item.GetComponent<Slot>().inscriptions.explosionChance[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": ExplosionChance +" + item.GetComponent<Slot>().inscriptions.explosionChance[i];
                    if (item.GetComponent<Slot>().inscriptions.explosionType[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": ExplosionType +" + item.GetComponent<Slot>().inscriptions.explosionType[i];
                    if (item.GetComponent<Slot>().inscriptions.weaponCooldown[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": WeaponCooldown +" + item.GetComponent<Slot>().inscriptions.weaponCooldown[i];
                    if (item.GetComponent<Slot>().inscriptions.createProjectileChance[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": CreateProjectileChance +" + item.GetComponent<Slot>().inscriptions.createProjectileChance[i];
                    if (item.GetComponent<Slot>().inscriptions.spikes[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": Spikes +" + item.GetComponent<Slot>().inscriptions.spikes[i];
                    if (item.GetComponent<Slot>().inscriptions.pierce[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": Pierce +" + item.GetComponent<Slot>().inscriptions.pierce[i];
                    if (item.GetComponent<Slot>().inscriptions.extraPierceChance[i] != 0) text.text += "\r\n" + "Slot " + (i + 1) + ": ExtraPierceChance +" + item.GetComponent<Slot>().inscriptions.extraPierceChance[i];
                }
            }
            parentPanel.transform.Find("CursorSlot").GetComponentInChildren<Text>().enabled = true;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[0].enabled = true;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
            gameObject.transform.localScale = new Vector3(text.preferredWidth + 4f, text.preferredHeight + 1f, 0f);
        }
        if (text.text == "Empty")
        {
            parentPanel.transform.Find("CursorSlot").GetComponentInChildren<Text>().enabled = false;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inventory" || collision.gameObject.tag == "ArmourInventory")
        {
            SlotInteraction.isHovered = false;
            parentPanel.transform.Find("CursorSlot").GetComponentInChildren<Text>().enabled = false;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[0].enabled = false;
            gameObject.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        }
    }
}

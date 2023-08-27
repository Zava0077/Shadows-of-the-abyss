using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
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
                Inventory.slots[SlotInteraction.hoveredId].canBeReplaced = false;
                isChoosingSlot = false;
                Inventory.self.GetFeatures();
            }
            if (collision.gameObject.GetComponent<Slot>().rareName != "")
                text.text += " <b><color=red>" + collision.gameObject.GetComponent<Slot>().rareName + "</color></b>";
            text.text = collision.gameObject.GetComponent<Slot>().itemDescription;
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

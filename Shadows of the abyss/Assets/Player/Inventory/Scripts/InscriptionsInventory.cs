using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InscriptionsInventory : MonoBehaviour
{
    public static List<Slot> objects = new List<Slot>();
    public static List<Slot> inscriptionSlots = new List<Slot>();
    public GameObject[] inscriptions;
    public static InscriptionsInventory self;
    public InscriptionsInventory()
    {
        self = this;
    }
    private void Awake()
    {
        objects = GetComponentsInChildren<Slot>().ToList();
        for (int i = 0; i < objects.Count; i++)
        {
            if (i % 2 == 0)
                inscriptionSlots.Add(objects[i]);
        }
    }
    private void Update()
    {
        transform.parent.transform.parent.GetComponentsInChildren<Image>()[2].sprite = Inventory.slots[SlotInteraction.hoveredId].sprite;
    }
    private void FixedUpdate()
    {

    }
}

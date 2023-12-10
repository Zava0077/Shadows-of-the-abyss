using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Breastplate : Armour
{
    
    void Awake()
    {
        CurrentItem(this);
        string[] floats = new string[] { "maxHP", "defence", "inscSlots", "igniteResist", "iceResist", "lightningResist", "poisonResist", "evasionChance", "voidResist" };
        EquipmentConstructor(floats);
    }
}

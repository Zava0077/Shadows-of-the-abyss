using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Amulet : Jewelery
{
    public float globalCrit;
    public float maxMPPercent;
    // Start is called before the first frame update
    void Awake()
    {
        CurrentItem(this);
        string[] floats = new string[] {"maxHP","maxMP","maxMPPercent","globalCrit", "luck", "igniteResist","iceResist","lightningResist","poisonResist","evasionChance","voidResist"};
        EquipmentConstructor(floats);
    }
}

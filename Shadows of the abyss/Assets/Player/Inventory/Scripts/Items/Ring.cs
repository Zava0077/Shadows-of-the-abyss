using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class Ring : Jewelery
{
    public float attackSpeed;
    public float castSpeed;
    public float regenHP;
    public float regenMP;
    // Start is called before the first frame update
    void Awake()
    {
        CurrentItem(this);
        string[] floats = new string[] { "maxHP", "maxMP", "attackSpeed", "castSpeed", "regenHP", "regenMP", "luck", "igniteResist", "iceResist", "lightningResist", "poisonResist", "evasionChance", "voidResist" , "inscSlots"};
        EquipmentConstructor(floats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

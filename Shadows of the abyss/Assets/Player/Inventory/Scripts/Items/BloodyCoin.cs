using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyCoin : Trinkets
{
    public float bloodyCoin = 1;
    private void Awake()
    {
        CurrentItem(this);
        string[] floats = new string[] { "bloodyCoin" };
        EquipmentConstructor(floats);
    }
}

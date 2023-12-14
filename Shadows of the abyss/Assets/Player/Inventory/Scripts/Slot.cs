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
    [SerializeField] public bool[] settings;
    [SerializeField] public string[] settingsNames;
    public Usable.UsableEvent useEvent;
    #region oldCode
    [SerializeField] public int kind;
    [SerializeField] public string type;
    [SerializeField] public int id;
    [SerializeField] public int staticKind;
    [SerializeField] public string itemDescription;
    [SerializeField] public string[] rareList;
    [SerializeField] public int[] rareChances;
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
        if (slots[id].values[32] != 0)
            stacks.text = slots[id].values[32].ToString();
        if (values[32] == 0 && stacks != null)
            stacks.enabled = false;
        else if(stacks != null) stacks.enabled = true;
    }
}

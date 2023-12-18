using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class PrefixChanger : Usable
{
    GameObject _thisSlot;
    public GameObject _defaultSlot;
    public GameObject cursor;
    public static PrefixChanger _self;
    public PrefixChanger()
    {
        _self = this;
    }
    UsableEvent prfChanger = (GameObject thisSlot) =>
    {
        Debug.Log("Вызван метод из класса PrefixChanger");
        GameObject _event = Instantiate(_self.cursor);
        _event.AddComponent<PrefixChangerObject>();
        var script = _event.GetComponent<PrefixChangerObject>();
        script.curSprite = thisSlot.GetComponent<Slot>().sprite;
        script.@event = _self.prfChangerEvent;
        CursorSlot.self.enabled = false;
        Description.self.enabled = false;
    };
    private void Awake()
    {
        GetComponent<Slot>().useEvent = prfChanger;
        GetComponent<Slot>().defaultSlot = _defaultSlot;
    }
    UsableEvent prfChangerEvent = (GameObject thisSlot) =>
    {
        if (thisSlot.GetComponent<Slot>().type == "Usable") //сместить провреку в момент вызова делегата
        {
            Debug.Log("Невозможно использовать на данном предмете!");
            return;
        }
        thisSlot.GetComponent<Slot>().originalItem.SetActive(true);
        self.PickUpItem(thisSlot.GetComponent<Slot>().originalItem);
        thisSlot.GetComponent<Slot>().originalItem.SetActive(false);
        slotInt.ToDefault(1,PrefixChangerObject._id);
        Debug.Log("Вы успешно сменили зачарование на предмете!");
    };
}

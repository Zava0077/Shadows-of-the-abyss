using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Heal : Usable
{
    public GameObject _defaultSlot;
    UsableEvent heal = (GameObject thisSlot) =>
    {
        Debug.Log("Вызван метод из класса Heal");
        slotInt.ToDefault(1);
    };
    private void Awake()
    {
        GetComponent<Slot>().useEvent = heal;
        GetComponent<Slot>().defaultSlot = _defaultSlot;
    }
}

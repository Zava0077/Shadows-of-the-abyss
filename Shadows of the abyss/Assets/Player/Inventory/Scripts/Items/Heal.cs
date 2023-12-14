using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Heal : Usable
{
    UsableEvent heal = () =>
    {
        Debug.Log("Вызван метод из класса Heal");
    };
    private void Awake()
    {
        GetComponent<Slot>().useEvent = heal;
    }
}

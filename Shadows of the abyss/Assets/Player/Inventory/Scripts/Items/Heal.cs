using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Heal : Usable
{
    UsableEvent heal = () =>
    {
        Debug.Log("������ ����� �� ������ Heal");
    };
    private void Awake()
    {
        GetComponent<Slot>().useEvent = heal;
    }
}

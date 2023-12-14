using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Usable : Slot
{
    //public virtual void UsableEvent()
    //{
    //    Debug.Log("Вызван метод из родительского класса");
    //}
    public delegate void UsableEvent();
}

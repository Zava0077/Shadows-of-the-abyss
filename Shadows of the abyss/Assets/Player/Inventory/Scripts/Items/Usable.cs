using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Usable : SlotInteraction
{
    
    public delegate void UsableEvent([Optional] GameObject thisSlot);
}

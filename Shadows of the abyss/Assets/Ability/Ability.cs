using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public GameObject[] abilities = new GameObject[10];
    int abilityLevel = 0;
    public void UseAbility(GameObject ability)
    {
        for(int i = 0; i < abilities.Length;i++)
            if (ability == abilities[i])
                switch(i)
                {
                    case 0:
                        break;
                }
    }
}

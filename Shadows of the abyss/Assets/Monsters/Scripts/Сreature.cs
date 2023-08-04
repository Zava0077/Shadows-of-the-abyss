using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int Health;
    public int Armor;
    public float Speed;
    public double FireRes;
    public double LightningRes;
    public double ColdRes;
    public double PoisonRes;
    public double PhysicalRes;
    public double VoidRes;
    public int Evasion;
    public void Die()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    public int MaxHealth;
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

    public void Limits()
    {
        #region Ограничения
        if (FireRes > 0.75)
        {
            FireRes = 0.75;
        }
        if (ColdRes > 0.75)
        {
            ColdRes = 0.75;
        }
        if (LightningRes > 0.75)
        {
            LightningRes = 0.75;
        }
        if (PhysicalRes > 0.75)
        {
            PhysicalRes = 0.75;
        }
        if (PoisonRes > 0.75)
        {
            PoisonRes = 0.75;
        }
        if (VoidRes > 0.75)
        {
            VoidRes = 0.75;
        }
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if (Evasion > 50)
        {
            Evasion = 50;
        }
        #endregion
        if (Health <= 0)
        {
            if(gameObject.tag == "Player")
            {
                Debug.Log("hhui");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageType : MonoBehaviour
{
    public enum DamageTypes
    {
        Cold,
        Lightning,
        Fire,
        Physical,
        Poison,
        Void,
        Pure
    }

    static public void GetDamage(Creature TakeDamage, int Damage, DamageTypes type)
    {
        if (UnityEngine.Random.Range(1, 100) > PlayerScript.self.Evasion)
        {
            double TakingDamage = 0;
            switch (type)
            {
                case DamageTypes.Cold:
                    {
                        TakingDamage = Damage * (1 - TakeDamage.ColdRes) - TakeDamage.Armor;
                        break;
                    }
                case DamageTypes.Lightning:
                    {
                        TakingDamage = Damage * (1 - TakeDamage.LightningRes) - TakeDamage.Armor;
                        break;
                    }
                case DamageTypes.Fire:
                    {
                        TakingDamage = Damage * (1 - TakeDamage.FireRes) - TakeDamage.Armor;
                        break;
                    }
                case DamageTypes.Physical:
                    {
                        TakingDamage = Damage * (1 - TakeDamage.PhysicalRes) - TakeDamage.Armor;
                        break;
                    }
                case DamageTypes.Poison:
                    {
                        TakingDamage = Damage * (1 - TakeDamage.PoisonRes) - TakeDamage.Armor;
                        break;
                    }
                case DamageTypes.Void:
                    {
                        TakingDamage = Damage * (1 - TakeDamage.VoidRes) - TakeDamage.Armor;
                        break;
                    }
                case DamageTypes.Pure:
                    {
                        TakingDamage = Damage;
                        break;
                    }
            }
            TakingDamage = Math.Ceiling(TakingDamage);
            if (TakingDamage <= 0)
            {
                TakeDamage.Health -= 1;
            }
            else
            {
                TakeDamage.Health -= (int)TakingDamage;
            }
        }
    }
}

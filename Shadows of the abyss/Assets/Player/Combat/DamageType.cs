using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageType : MonoBehaviour
{
    public enum DamageTypes
    {
        Melee,
        Spell,
        Range
    }
    
    static public void GetDamage(Entity TakeDamage, Entity DoDamage, DamageTypes type)
    {
        if (UnityEngine.Random.Range(1, 100) > PlayerScript.self.Evasion)
        {
            float AllIncreas = DoDamage.increasedAllDamage;
            switch (type)
            {
                case DamageTypes.Melee:
                    {
                        AllIncreas += DoDamage.increasedMeleeDamage;
                        break;
                    }
                case DamageTypes.Spell:
                    {
                        AllIncreas += DoDamage.increasedSpellDamage;
                        break;
                    }
                case DamageTypes.Range:
                    {
                        
                        break;
                    }
            }
            if(DoDamage.FireDamage > 0)
            {
                if (DoDamage.increasedAllDamage == 0 && DoDamage.increasedFireDamage == 0)
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.FireRes) * DoDamage.FireDamage);
                }
                else
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.FireRes) * (DoDamage.FireDamage + (DoDamage.FireDamage * ((AllIncreas + DoDamage.increasedFireDamage) / 100))));
                }
            }
            if (DoDamage.ColdDamage > 0)
            {
                if (DoDamage.increasedAllDamage == 0 && DoDamage.increasedColdDamage == 0)
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.VoidRes) * DoDamage.ColdDamage);
                }
                else
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.ColdRes) * (DoDamage.ColdDamage + (DoDamage.ColdDamage * ((AllIncreas + DoDamage.increasedColdDamage) / 100))));
                }
            }
            if (DoDamage.VoidDamage > 0)
            {
                if (DoDamage.increasedAllDamage == 0 && DoDamage.increasedVoidDamage == 0)
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.VoidRes) * DoDamage.VoidDamage);
                }
                else
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.VoidRes) * (DoDamage.VoidDamage + (DoDamage.VoidDamage * ((AllIncreas + DoDamage.increasedVoidDamage) / 100))));
                }
            }
            if (DoDamage.PhysicalDamage > 0)
            {
                if(DoDamage.increasedAllDamage == 0 && DoDamage.increasedPhysicalDamage == 0)
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.PhysicalRes) * DoDamage.PhysicalDamage);
                }
                else
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.PhysicalRes) * (DoDamage.PhysicalDamage + (DoDamage.PhysicalDamage * ((AllIncreas + DoDamage.increasedPhysicalDamage) / 100))));
                }
            }
            if (DoDamage.LightningDamage > 0)
            {
                if (DoDamage.increasedAllDamage == 0 && DoDamage.increasedLightningDamage == 0)
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.LightningRes) * DoDamage.LightningDamage);
                }
                else
                {
                    TakeDamage.Health -= Convert.ToInt32((1 - TakeDamage.LightningRes) * (DoDamage.LightningDamage + (DoDamage.LightningDamage * ((AllIncreas + DoDamage.increasedLightningDamage) / 100))));
                }
            }
            if (DoDamage.PoisonDamage > 0)
            {
                
            }
        }
    }
}

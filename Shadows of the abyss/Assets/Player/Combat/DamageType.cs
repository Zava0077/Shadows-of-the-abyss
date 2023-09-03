using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.AI;

public class DamageType : MonoBehaviour
{
    float oneSecTimer;
    public static DamageType self;
    public DamageType()
    {
        self = this;
    }
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

    static public void GetDamage(Entity TakeDamage, float Damage, DamageTypes type)
    {
        if (UnityEngine.Random.Range(1, 100) > PlayerScript.self.Evasion)
        {
            double damage = 0;
            if (ArmourInventory.self.damageValue != 0) damage += CalculateDamage(TakeDamage, ArmourInventory.self.damageValue, DamageTypes.Physical);
            if (ArmourInventory.self.iceDamageValue != 0) damage += CalculateDamage(TakeDamage, ArmourInventory.self.iceDamageValue, DamageTypes.Cold);
            if (ArmourInventory.self.igniteDamageValue != 0) damage += CalculateDamage(TakeDamage, ArmourInventory.self.igniteDamageValue, DamageTypes.Fire);
            if (ArmourInventory.self.lightningDamageValue != 0) damage += CalculateDamage(TakeDamage, ArmourInventory.self.lightningDamageValue, DamageTypes.Lightning);
            if (ArmourInventory.self.poisonDamageValue != 0) damage += CalculateDamage(TakeDamage, ArmourInventory.self.poisonDamageValue, DamageTypes.Poison);
            if (ArmourInventory.self.pureDamageValue != 0) damage += CalculateDamage(TakeDamage, ArmourInventory.self.pureDamageValue, DamageTypes.Pure);
            if (ArmourInventory.self.voidDamageValue != 0) damage += CalculateDamage(TakeDamage, ArmourInventory.self.voidDamageValue, DamageTypes.Void);
            if (damage <= 0)
            {
                TakeDamage.gameObject.GetComponent<Entity>().PushActivator(TakeDamage.Health, 1, PlayerScript.self.gameObject.transform.position, TakeDamage.transform.position);
                TakeDamage.Health -= 1;
            }
            else
            {
                TakeDamage.gameObject.GetComponent<Entity>().PushActivator(TakeDamage.Health, (int)damage, PlayerScript.self.gameObject.transform.position, TakeDamage.transform.position);
                TakeDamage.Health -= (int)damage;
            }
        }
    }
    static double CalculateDamage(Entity TakeDamage, float Damage, DamageTypes type)
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
        return TakingDamage;
    }
}

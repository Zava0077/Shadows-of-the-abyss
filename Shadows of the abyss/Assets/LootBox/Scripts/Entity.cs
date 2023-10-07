using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Entity : MonoBehaviour
{
    [SerializeField] GameObject inscription;
    public int MaxHealth;
    public int Health;
    public int Armor;
    public float Speed;
    public double FireRes;
    public bool isInvincible;
    public float LightningRes;
    public float ColdRes;
    public float PoisonRes;
    public float PhysicalRes;
    public float VoidRes;
    public int Evasion;
    public float CriticalChance;
    public float CriticalMultiplier;
    public float LifeRegeneration;
    public float ManaRegeneration;
    public int Mana;
    public int MaxMana;
    public float ProjectileSpeed;

    public float PoisonDuration;
    public float increasedPoisonDuration;
    public float increasedDuration;

    public float StatusResistance;
    public float Luck;

    public float PhysicalDamage;
    public float FireDamage;
    public float LightningDamage;
    public float ColdDamage;
    public float PoisonDamage;
    public float VoidDamage;

    public float increasedAllDamage;
    public float increasedPhysicalDamage;
    public float increasedFireDamage;
    public float increasedLightningDamage;
    public float increasedColdDamage;
    public float increasedPoisonDamage;
    public float increasedVoidDamage;
    public float increasedMeleeDamage;
    public float increasedSpellDamage;

    public float increasedAttackSpeed;
    public float increasedCastSpeed;
    public float icreasedSpeed;
    public float increasedArmor;
    public float increasedEvasion;
    public float increasedHealth;

    public void Limits()
    {
        #region Îãðàíè÷åíèÿ
        if (FireRes > 0.75)
        {
            FireRes = 0.75;
        }
        if (ColdRes > 0.75)
        {
            ColdRes = 0.75f;
        }
        if (LightningRes > 0.75)
        {
            LightningRes = 0.75f;
        }
        if (PhysicalRes > 0.75)
        {
            PhysicalRes = 0.75f;
        }
        if (PoisonRes > 0.75)
        {
            PoisonRes = 0.75f;
        }
        if (VoidRes > 0.75)
        {
            VoidRes = 0.75f;
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
            if (gameObject.tag == "Player")
            {
                Debug.Log("hhui");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void Die(GameObject[] gameObjects, int[] chance)
    {
        Drop(gameObjects, chance);
        Destroy(this.gameObject);
    }
    public void Drop(GameObject[] gameObjects, int[] chance)
    {
        System.Random rnd = new System.Random();
        int rareChance = rnd.Next(0, 100);
        int dropChance = rnd.Next(0, 100);

        string rareName = "";
        for (int i = 0; i < gameObjects.Length; i++)
        {
            int secondDropChance = 100;
            GameObject article;
            string[] rareList = gameObjects[i].GetComponent<Slot>().rareList;
            int[] rareChances = gameObjects[i].GetComponent<Slot>().rareChances;
            Vector2 randomDirection;
            int ifChance = 0;
            if (dropChance < chance[i])
            {
                article = Instantiate(gameObjects[i], gameObject.transform.position, Quaternion.identity);
                string description = "";
                rareName = "";
                Prefixes.self.extraDescription = "";
                if (gameObjects[i].GetComponent<Slot>().type != "Usable" && gameObjects[i].GetComponent<Slot>().type != "Empty" && gameObjects[i].GetComponent<Slot>().type != "Scroll")
                    for (int k = 0; k < rareList.Length; k++)
                    {
                        ifChance += rareChances[k];
                        if (rareChance < ifChance)
                        {
                            rareName = rareList[k];
                            ifChance = 0;
                            break;
                        }
                        else continue;
                    }
                article.GetComponent<Slot>().sprite = gameObjects[i].GetComponent<Slot>().sprite;
                article.GetComponent<Slot>().rareName = rareName;
                Prefixes.self.PrefixChooser(rareName, gameObjects[i].GetComponent<Slot>().damage, article);
                description += article.GetComponent<Slot>().rareName + " " + article.GetComponent<Slot>().itemDescription + "\r\n";
                article.GetComponent<Slot>().damage += Prefixes.self.damageSummand;
                if (article.GetComponent<Slot>().damage > 0)
                    description += "Damage: " + article.GetComponent<Slot>().damage + "\r\n";
                article.GetComponent<Slot>().iceDamage += Prefixes.self.iceDamageSummand;
                if (article.GetComponent<Slot>().iceDamage > 0)
                    description += "Ice damage: " + article.GetComponent<Slot>().iceDamage + "\r\n";
                article.GetComponent<Slot>().igniteDamage += Prefixes.self.igniteDamageSummand;
                if (article.GetComponent<Slot>().igniteDamage > 0)
                    description += "Ignite damage: " + article.GetComponent<Slot>().igniteDamage + "\r\n";
                article.GetComponent<Slot>().lightningDamage += Prefixes.self.lightningDamageSummand;
                if (article.GetComponent<Slot>().lightningDamage > 0)
                    description += "Ligtning damage: " + article.GetComponent<Slot>().lightningDamage + "\r\n";
                article.GetComponent<Slot>().poisonDamage += Prefixes.self.poisonDamageSummand;
                if (article.GetComponent<Slot>().poisonDamage > 0)
                    description += "Poison damage: " + article.GetComponent<Slot>().poisonDamage + "\r\n";
                article.GetComponent<Slot>().voidDamage += Prefixes.self.voidDamageSummand;
                if (article.GetComponent<Slot>().voidDamage > 0)
                    description += "Void damage: " + article.GetComponent<Slot>().voidDamage + "\r\n";
                article.GetComponent<Slot>().pureDamage += Prefixes.self.pureDamageSummand;
                if (article.GetComponent<Slot>().pureDamage > 0)
                    description += "Pure damage: " + article.GetComponent<Slot>().damage + "\r\n";
                article.GetComponent<Slot>().defence += Prefixes.self.defenceSummand;
                if (article.GetComponent<Slot>().defence > 0)
                    description += "Defence: " + article.GetComponent<Slot>().defence + "\r\n";
                article.GetComponent<Slot>().iceResist += Prefixes.self.iceResistSummand;
                if (article.GetComponent<Slot>().iceResist > 0)
                    description += "Ice resist: " + article.GetComponent<Slot>().iceResist + "\r\n";
                article.GetComponent<Slot>().igniteResist += Prefixes.self.igniteResistSummand;
                if (article.GetComponent<Slot>().igniteResist > 0)
                    description += "Ignite resist: " + article.GetComponent<Slot>().igniteResist + "\r\n";
                article.GetComponent<Slot>().lightningResist += Prefixes.self.lightningResistSummand;
                if (article.GetComponent<Slot>().lightningResist > 0)
                    description += "Lightning resist: " + article.GetComponent<Slot>().lightningResist + "\r\n";
                article.GetComponent<Slot>().poisonResist += Prefixes.self.poisonResistSummand;
                if (article.GetComponent<Slot>().poisonResist > 0)
                    description += "Poison resist: " + article.GetComponent<Slot>().poisonResist + "\r\n";
                article.GetComponent<Slot>().voidResist += Prefixes.self.voidResistSummand;
                if (article.GetComponent<Slot>().voidResist > 0)
                    description += "Void resist: " + article.GetComponent<Slot>().voidResist + "\r\n";
                article.GetComponent<Slot>().pureResist += Prefixes.self.pureResistSummand;
                if (article.GetComponent<Slot>().pureResist > 0)
                    description += "Pure resist: " + article.GetComponent<Slot>().pureResist + "\r\n";
                article.GetComponent<Slot>().hp += Prefixes.self.maxHpSummand;
                if (article.GetComponent<Slot>().hp > 0)
                    description += "Max HP: +" + article.GetComponent<Slot>().hp + "\r\n";
                article.GetComponent<Slot>().evasionChance += Prefixes.self.evasionChanceSummand;
                if (article.GetComponent<Slot>().evasionChance > 0)
                    description += "Evasion chance: " + article.GetComponent<Slot>().evasionChance + "\r\n";
                article.GetComponent<Slot>().criticalChance += Prefixes.self.criticalChanceSummand;
                if (article.GetComponent<Slot>().criticalChance > 0)
                    description += "Critical chance: " + article.GetComponent<Slot>().criticalChance + "\r\n";
                article.GetComponent<Slot>().kind = gameObjects[i].GetComponent<Slot>().kind;
                article.GetComponent<Slot>().idItem = gameObjects[i].GetComponent<Slot>().idItem;
                //
                article.GetComponent<Slot>().manaCost += Prefixes.self.manaCostSummand;
                if (article.GetComponent<Slot>().manaCost > 0)
                    description += "Mana cost: " + article.GetComponent<Slot>().manaCost + "\r\n";
                article.GetComponent<Slot>().weaponSize += Prefixes.self.weaponSizeSummand;
                if (article.GetComponent<Slot>().weaponSize > 0)
                    description += "Size: " + article.GetComponent<Slot>().weaponSize + "\r\n";
                article.GetComponent<Slot>().attackSpeed += Prefixes.self.attackSpeedSummand;
                if (article.GetComponent<Slot>().attackSpeed > 0)
                    description += "Speed: " + article.GetComponent<Slot>().attackSpeed + "\r\n";
                article.GetComponent<Slot>().secondUsageChance += Prefixes.self.secondUsageChanceSummand;
                if (article.GetComponent<Slot>().secondUsageChance > 0)
                    description += "Second usage: " + article.GetComponent<Slot>().secondUsageChance + "\r\n";
                article.GetComponent<Slot>().tripleAttackChance += Prefixes.self.tripleAttackChanceSummand;
                if (article.GetComponent<Slot>().tripleAttackChance > 0)
                    description += "Triple attack: " + article.GetComponent<Slot>().tripleAttackChance + "\r\n";
                article.GetComponent<Slot>().explosionChance += Prefixes.self.explChanceSummand;
                if (article.GetComponent<Slot>().explosionChance > 0)
                    description += "Chance of explotion: " + article.GetComponent<Slot>().explosionChance + "\r\n";
                article.GetComponent<Slot>().explosionType = Prefixes.self.explTypeEqualer;
                article.GetComponent<Slot>().weaponCooldown += Prefixes.self.weaponCooldownSummand;
                if (article.GetComponent<Slot>().weaponCooldown > 0)
                    description += "Cooldown: " + article.GetComponent<Slot>().weaponCooldown + "\r\n";
                article.GetComponent<Slot>().createProjectileChance += Prefixes.self.createProjectileChanceSummand;
                if (article.GetComponent<Slot>().createProjectileChance > 0)
                    description += "Create projectile chance: " + article.GetComponent<Slot>().createProjectileChance + "\r\n";

                article.GetComponent<Slot>().spikes += Prefixes.self.spikes;
                if (article.GetComponent<Slot>().spikes > 0)
                    description += "Spikes: " + article.GetComponent<Slot>().spikes + "\r\n";
                //
                for (int j = 0; j < 10; j++)
                {
                    //GameObject insc = Instantiate(inscription);
                    //article.GetComponent<Slot>().inscriptions[j] = insc; //создать скрипт инскрипшионс, там хранить все переменные в виде массива [10] в скрипте слот создать список, хранящий этот скрипт, таким образом он будет переносится из слота в слот.
                }
                //
                if (description == "" && article.GetComponent<Slot>().itemDescription == "")
                    description = "Empty";
                else if (description == "")
                    article.GetComponent<Slot>().itemDescription = article.GetComponent<Slot>().itemDescription;
                else
                    article.GetComponent<Slot>().itemDescription = description + Prefixes.self.extraDescription;
                if (gameObjects[i].GetComponent<Slot>().stackAmount > 0)
                    secondDropChance /= 2;
                else secondDropChance = 0;
                article.transform.position += new Vector3((float)rnd.Next(-2, 2) / 10, (float)rnd.Next(-2, 2) / 10);
            }
            gameObject.transform.localScale /= 100;
            if (rnd.Next(0, 100) < secondDropChance)
                i--;

        }
    }
}

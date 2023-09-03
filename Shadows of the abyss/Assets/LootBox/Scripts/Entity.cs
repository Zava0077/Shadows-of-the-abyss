using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.Mathematics;

public class Entity : MonoBehaviour
{
    [SerializeField] GameObject inscription;
    [SerializeField] GameObject damageNumber;
    public bool isPushing;
    bool _numberCreated;
    Vector2 pushFrom;
    Vector2 pushTo;
    float pushingCoefficient;
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
    }
    public void PushActivator(int health, int damage, Vector2 from, Vector2 to)
    {
        float coefficient;
        if (isPushing == false)
            isPushing = true;
        if (damage > health)
            damage = health;
        if (!_numberCreated)
        {
            GameObject number = Instantiate(damageNumber, transform.position + new Vector3((float)UnityEngine.Random.Range(-100, 100) / 200, (float)UnityEngine.Random.Range(-100, 100) / 200), Quaternion.identity, transform);
            number.GetComponentsInChildren<Text>()[0].text = damage.ToString();
            number.GetComponentsInChildren<Text>()[1].text = damage.ToString();
            _numberCreated = true;
        }
        coefficient = (float)damage / (float)health;
        pushingCoefficient = 1 + coefficient;
        pushFrom = from;
        pushTo = to;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        if(Attack.isAbleToAttack)
            _numberCreated = false;
        Invoke(nameof(PushReset), 0.1f);
    }
    public void Pushing()
    {
        if (isPushing)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            transform.position += Vector3.Lerp(Vector3.zero, new Vector2(pushTo.x - pushFrom.x, pushTo.y - pushFrom.y).normalized * pushingCoefficient, Time.deltaTime);
        }         
    }
    void PushReset()
    {
        isPushing = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = true;
        _numberCreated = false;
        CancelInvoke(nameof(PushReset));
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
                    description += "Damage: " + "<color=red>" +article.GetComponent<Slot>().damage + "</color>" + "\r\n";
                article.GetComponent<Slot>().iceDamage += Prefixes.self.iceDamageSummand;
                if (article.GetComponent<Slot>().iceDamage > 0)
                    description += "Ice damage: " + "<color=red>" +article.GetComponent<Slot>().iceDamage + "</color>" + "\r\n";
                article.GetComponent<Slot>().igniteDamage += Prefixes.self.igniteDamageSummand;
                if (article.GetComponent<Slot>().igniteDamage > 0)
                    description += "Ignite damage: " + "<color=red>" +article.GetComponent<Slot>().igniteDamage + "</color>" + "\r\n";
                article.GetComponent<Slot>().lightningDamage += Prefixes.self.lightningDamageSummand;
                if (article.GetComponent<Slot>().lightningDamage > 0)
                    description += "Ligtning damage: " + "<color=red>" +article.GetComponent<Slot>().lightningDamage + "</color>" + "\r\n";
                article.GetComponent<Slot>().poisonDamage += Prefixes.self.poisonDamageSummand;
                if (article.GetComponent<Slot>().poisonDamage > 0)
                    description += "Poison damage: " + "<color=red>" +article.GetComponent<Slot>().poisonDamage + "</color>" + "\r\n";
                article.GetComponent<Slot>().voidDamage += Prefixes.self.voidDamageSummand;
                if (article.GetComponent<Slot>().voidDamage > 0)
                    description += "Void damage: " + "<color=red>" +article.GetComponent<Slot>().voidDamage + "</color>" + "\r\n";
                article.GetComponent<Slot>().pureDamage += Prefixes.self.pureDamageSummand;
                if (article.GetComponent<Slot>().pureDamage > 0)
                    description += "Pure damage: " + "<color=red>" +article.GetComponent<Slot>().damage + "</color>" + "\r\n";
                article.GetComponent<Slot>().defence += Prefixes.self.defenceSummand;
                if (article.GetComponent<Slot>().defence > 0)
                    description += "Defence: " + "<color=red>" +article.GetComponent<Slot>().defence + "</color>" + "\r\n";
                article.GetComponent<Slot>().iceResist += Prefixes.self.iceResistSummand;
                if (article.GetComponent<Slot>().iceResist > 0)
                    description += "Ice resist: " + "<color=red>" +article.GetComponent<Slot>().iceResist + "</color>" + "\r\n";
                article.GetComponent<Slot>().igniteResist += Prefixes.self.igniteResistSummand;
                if (article.GetComponent<Slot>().igniteResist > 0)
                    description += "Ignite resist: " + "<color=red>" +article.GetComponent<Slot>().igniteResist + "</color>" + "\r\n";
                article.GetComponent<Slot>().lightningResist += Prefixes.self.lightningResistSummand;
                if (article.GetComponent<Slot>().lightningResist > 0)
                    description += "Lightning resist: " + "<color=red>" +article.GetComponent<Slot>().lightningResist + "</color>" + "\r\n";
                article.GetComponent<Slot>().poisonResist += Prefixes.self.poisonResistSummand;
                if (article.GetComponent<Slot>().poisonResist > 0)
                    description += "Poison resist: " + "<color=red>" +article.GetComponent<Slot>().poisonResist + "</color>" + "\r\n";
                article.GetComponent<Slot>().voidResist += Prefixes.self.voidResistSummand;
                if (article.GetComponent<Slot>().voidResist > 0)
                    description += "Void resist: " + "<color=red>" +article.GetComponent<Slot>().voidResist + "</color>" + "\r\n";
                article.GetComponent<Slot>().pureResist += Prefixes.self.pureResistSummand;
                if (article.GetComponent<Slot>().pureResist > 0)
                    description += "Pure resist: " + "<color=red>" +article.GetComponent<Slot>().pureResist + "</color>" + "\r\n";
                article.GetComponent<Slot>().hp += Prefixes.self.maxHpSummand;
                if (article.GetComponent<Slot>().hp > 0)
                    description += "Max HP: +" + article.GetComponent<Slot>().hp + "</color>" + "\r\n";
                article.GetComponent<Slot>().evasionChance += Prefixes.self.evasionChanceSummand;
                if (article.GetComponent<Slot>().evasionChance > 0)
                    description += "Evasion chance: " + "<color=red>" +article.GetComponent<Slot>().evasionChance + "</color>" + "\r\n";
                article.GetComponent<Slot>().criticalChance += Prefixes.self.criticalChanceSummand;
                if (article.GetComponent<Slot>().criticalChance > 0)
                    description += "Critical chance: " + "<color=red>" +article.GetComponent<Slot>().criticalChance + "</color>" + "\r\n";
                article.GetComponent<Slot>().kind = gameObjects[i].GetComponent<Slot>().kind;
                article.GetComponent<Slot>().idItem = gameObjects[i].GetComponent<Slot>().idItem;
                //
                article.GetComponent<Slot>().manaCost += Prefixes.self.manaCostSummand;
                if (article.GetComponent<Slot>().manaCost > 0)
                    description += "Mana cost: " + "<color=red>" +article.GetComponent<Slot>().manaCost + "</color>" + "\r\n";
                article.GetComponent<Slot>().weaponSize += Prefixes.self.weaponSizeSummand;
                if (article.GetComponent<Slot>().weaponSize > 0)
                    description += "Size: " + "<color=red>" +article.GetComponent<Slot>().weaponSize + "</color>" + "\r\n";
                article.GetComponent<Slot>().attackSpeed += Prefixes.self.attackSpeedSummand;
                if (article.GetComponent<Slot>().attackSpeed > 0)
                    description += "Speed: " + "<color=red>" +article.GetComponent<Slot>().attackSpeed + "</color>" + "\r\n";
                article.GetComponent<Slot>().secondUsageChance += Prefixes.self.secondUsageChanceSummand;
                if (article.GetComponent<Slot>().secondUsageChance > 0)
                    description += "Second usage: " + "<color=red>" +article.GetComponent<Slot>().secondUsageChance + "</color>" + "\r\n";
                article.GetComponent<Slot>().tripleAttackChance += Prefixes.self.tripleAttackChanceSummand;
                if (article.GetComponent<Slot>().tripleAttackChance > 0)
                    description += "Triple attack: " + "<color=red>" +article.GetComponent<Slot>().tripleAttackChance + "</color>" + "\r\n";
                article.GetComponent<Slot>().explosionChance += Prefixes.self.explChanceSummand;
                if (article.GetComponent<Slot>().explosionChance > 0)
                    description += "Chance of explotion: " + "<color=red>" +article.GetComponent<Slot>().explosionChance + "</color>" + "\r\n";
                article.GetComponent<Slot>().explosionType = Prefixes.self.explTypeEqualer;
                article.GetComponent<Slot>().weaponCooldown += Prefixes.self.weaponCooldownSummand;
                if (article.GetComponent<Slot>().weaponCooldown > 0)
                    description += "Cooldown: " + "<color=red>" +article.GetComponent<Slot>().weaponCooldown + "</color>" + "\r\n";
                article.GetComponent<Slot>().createProjectileChance += Prefixes.self.createProjectileChanceSummand;
                if (article.GetComponent<Slot>().createProjectileChance > 0)
                    description += "Create projectile chance: " + "<color=red>" +article.GetComponent<Slot>().createProjectileChance + "</color>" + "\r\n";

                article.GetComponent<Slot>().spikes += Prefixes.self.spikes;
                if (article.GetComponent<Slot>().spikes > 0)
                    description += "Spikes: " + "<color=red>" +article.GetComponent<Slot>().spikes + "</color>" + "\r\n";
                article.GetComponent<Slot>().inscriptionNum += Prefixes.self.inscSummand;
                if (article.GetComponent<Slot>().inscriptionNum > 0)
                    description += "Incrtiption slots: " + "<color=red>" +article.GetComponent<Slot>().inscriptionNum + "</color>" + "\r\n";
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

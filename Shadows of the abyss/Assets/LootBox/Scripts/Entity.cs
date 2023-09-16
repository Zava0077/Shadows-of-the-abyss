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
                Prefixes.self.PrefixChooser(rareName, gameObjects[i].GetComponent<Slot>().values[2], article);
                description += "<color=" + Prefixes.self.qualityColor + ">" + article.GetComponent<Slot>().rareName + "</color>" + " " + article.GetComponent<Slot>().itemDescription + "\r\n";
                article.GetComponent<Slot>().values[2] += Prefixes.self.damageSummand;
                //if (article.GetComponent<Slot>().damage > 0) description += "Damage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().damage + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[3] += Prefixes.self.iceDamageSummand;
                //if (article.GetComponent<Slot>().iceDamage > 0) description += "Ice damage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().iceDamage + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[4] += Prefixes.self.igniteDamageSummand;
                //if (article.GetComponent<Slot>().igniteDamage > 0) description += "Ignite damage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().igniteDamage + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[5] += Prefixes.self.lightningDamageSummand;
                //if (article.GetComponent<Slot>().lightningDamage > 0) description += "Ligtning damage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().lightningDamage + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[6] += Prefixes.self.poisonDamageSummand;
                //if (article.GetComponent<Slot>().poisonDamage > 0) description += "Poison damage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().poisonDamage + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[7] += Prefixes.self.voidDamageSummand;
                //if (article.GetComponent<Slot>().voidDamage > 0) description += "Void damage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().voidDamage + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[8] += Prefixes.self.pureDamageSummand;
                //if (article.GetComponent<Slot>().pureDamage > 0) description += "Pure damage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().damage + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[1] += Prefixes.self.defenceSummand;
                //if (article.GetComponent<Slot>().defence > 0) description += "Defence: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().defence + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[9] += Prefixes.self.iceResistSummand;
                //if (article.GetComponent<Slot>().iceResist > 0) description += "Ice resist: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().iceResist + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[10] += Prefixes.self.igniteResistSummand;
                //if (article.GetComponent<Slot>().igniteResist > 0) description += "Ignite resist: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().igniteResist + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[11] += Prefixes.self.lightningResistSummand;
                //if (article.GetComponent<Slot>().lightningResist > 0) description += "Lightning resist: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().lightningResist + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[12] += Prefixes.self.poisonResistSummand;
                //if (article.GetComponent<Slot>().poisonResist > 0) description += "Poison resist: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().poisonResist + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[13] += Prefixes.self.voidResistSummand;
                //if (article.GetComponent<Slot>().voidResist > 0) description += "Void resist: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().voidResist + "</color>" + "</b>" + "\r\n";
                //if (article.GetComponent<Slot>().pureResist > 0) description += "Pure resist: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().pureResist + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[0] += Prefixes.self.maxHpSummand;
                //if (article.GetComponent<Slot>().hp > 0) description += "Max HP: +" + article.GetComponent<Slot>().hp + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[14] += Prefixes.self.evasionChanceSummand;
                //if (article.GetComponent<Slot>().evasionChance > 0) description += "Evasion chance: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().evasionChance + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[15] += Prefixes.self.criticalChanceSummand;
                //if (article.GetComponent<Slot>().criticalChance > 0) description += "Critical chance: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().criticalChance + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[30] = gameObjects[i].GetComponent<Slot>().kind;
                article.GetComponent<Slot>().values[28] = gameObjects[i].GetComponent<Slot>().values[28];
                //
                article.GetComponent<Slot>().values[16] += Prefixes.self.manaCostSummand;
                //if (article.GetComponent<Slot>().manaCost > 0) description += "Mana cost: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().manaCost + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[17] += Prefixes.self.weaponSizeSummand;
                //if (article.GetComponent<Slot>().weaponSize > 0) description += "Size: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().weaponSize + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[18] += Prefixes.self.attackSpeedSummand;
                //if (article.GetComponent<Slot>().attackSpeed > 0) description += "Speed: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().attackSpeed + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[19] += Prefixes.self.secondUsageChanceSummand;
                //if (article.GetComponent<Slot>().secondUsageChance > 0) description += "Second usage: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().secondUsageChance + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[20] += Prefixes.self.tripleAttackChanceSummand;
                //if (article.GetComponent<Slot>().tripleAttackChance > 0) description += "Triple attack: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().tripleAttackChance + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[21] += Prefixes.self.explChanceSummand;
                //if (article.GetComponent<Slot>().explosionChance > 0) description += "Chance of explotion: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().explosionChance + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[22] = Prefixes.self.explTypeEqualer;
                article.GetComponent<Slot>().values[23] += Prefixes.self.weaponCooldownSummand;
                //  if (article.GetComponent<Slot>().weaponCooldown > 0) description += "Cooldown: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().weaponCooldown + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[24] += Prefixes.self.createProjectileChanceSummand;
                //  if (article.GetComponent<Slot>().createProjectileChance > 0) description += "Create projectile chance: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().createProjectileChance + "</color>" + "</b>" + "\r\n";

                article.GetComponent<Slot>().values[25] += Prefixes.self.spikes;
                //if (article.GetComponent<Slot>().spikes > 0) description += "Spikes: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().spikes + "</color>" + "</b>" + "\r\n";
                article.GetComponent<Slot>().values[29] += Prefixes.self.inscSummand;
                //if (article.GetComponent<Slot>().inscriptionNum > 0) description += "Incrtiption slots: " + "<b>" + "<color=red>" + article.GetComponent<Slot>().inscriptionNum + "</color>" + "</b>" + "\r\n";
                //
                for (int k = 0; k < article.GetComponent<Slot>().values.Length; k++)
                    if (article.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30)
                        description += article.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + article.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
                if (description == "" && article.GetComponent<Slot>().itemDescription == "")
                    description = "Empty";
                else if (description == "")
                    article.GetComponent<Slot>().itemDescription = article.GetComponent<Slot>().itemDescription;
                else
                    article.GetComponent<Slot>().itemDescription = description + Prefixes.self.extraDescription;
                if (gameObjects[i].GetComponent<Slot>().values[31] > 0)
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

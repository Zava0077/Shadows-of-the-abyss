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
    
    public bool isInvincible;
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
    
    public double LightningRes;
    public double ColdRes;
    public double PoisonRes;
    public double PhysicalRes;
    public double FireRes;
    public double VoidRes;
    public int Evasion;

    public void Limits()
    {
        #region Limit
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
        if (Attack.isAbleToAttack)
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

        for (int i = 0; i < gameObjects.Length; i++)
        {
            int rareChance = rnd.Next(0, 100);
            int dropChance = rnd.Next(0, 100);
            string rareName = "";
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
                //if (gameObjects[i].GetComponent<Slot>().type != "Usable" && gameObjects[i].GetComponent<Slot>().type != "Empty" && gameObjects[i].GetComponent<Slot>().type != "Scroll")
                //    for (int k = 0; k < rareList.Length; k++)
                //    {
                //        ifChance += rareChances[k];
                //        if (rareChance < ifChance)
                //        {
                //            rareName = rareList[k];
                //            ifChance = 0;
                //            break;
                //        }
                //        else continue;
                //    }
                article.GetComponent<Slot>().sprite = gameObjects[i].GetComponent<Slot>().sprite;
                //article.GetComponent<Slot>().rareName = rareName;
                //Prefixes.self.PrefixChooser(rareName, gameObjects[i].GetComponent<Slot>().values[2], article);
                //description += "<color=" + Prefixes.self.qualityColor + ">" + article.GetComponent<Slot>().rareName + "</color>" + " " + article.GetComponent<Slot>().itemDescription + "\r\n";
                //article.GetComponent<Slot>().values[2] += Prefixes.self.damageSummand;
                //article.GetComponent<Slot>().values[3] += Prefixes.self.iceDamageSummand;
                //article.GetComponent<Slot>().values[4] += Prefixes.self.igniteDamageSummand;
                //article.GetComponent<Slot>().values[5] += Prefixes.self.lightningDamageSummand;
                //article.GetComponent<Slot>().values[6] += Prefixes.self.poisonDamageSummand;
                //article.GetComponent<Slot>().values[7] += Prefixes.self.voidDamageSummand;
                //article.GetComponent<Slot>().values[8] += Prefixes.self.pureDamageSummand;
                //article.GetComponent<Slot>().values[1] += Prefixes.self.defenceSummand;
                //article.GetComponent<Slot>().values[9] += Prefixes.self.iceResistSummand;
                //article.GetComponent<Slot>().values[10] += Prefixes.self.igniteResistSummand;
                //article.GetComponent<Slot>().values[11] += Prefixes.self.lightningResistSummand;
                //article.GetComponent<Slot>().values[12] += Prefixes.self.poisonResistSummand;
                //article.GetComponent<Slot>().values[13] += Prefixes.self.voidResistSummand;
                //article.GetComponent<Slot>().values[0] += Prefixes.self.maxHpSummand;
                //article.GetComponent<Slot>().values[14] += Prefixes.self.evasionChanceSummand;
                //article.GetComponent<Slot>().values[15] += Prefixes.self.criticalChanceSummand;
                article.GetComponent<Slot>().values[30] = gameObjects[i].GetComponent<Slot>().kind;
                article.GetComponent<Slot>().values[28] = gameObjects[i].GetComponent<Slot>().values[28];
                //
                //article.GetComponent<Slot>().values[16] += Prefixes.self.manaCostSummand;
                //article.GetComponent<Slot>().values[17] += Prefixes.self.weaponSizeSummand;
                //article.GetComponent<Slot>().values[18] += Prefixes.self.attackSpeedSummand;
                //article.GetComponent<Slot>().values[19] += Prefixes.self.secondUsageChanceSummand;
                //article.GetComponent<Slot>().values[20] += Prefixes.self.tripleAttackChanceSummand;
                //article.GetComponent<Slot>().values[21] += Prefixes.self.explChanceSummand;
                //article.GetComponent<Slot>().values[22] = Prefixes.self.explTypeEqualer;
                //article.GetComponent<Slot>().values[23] += Prefixes.self.weaponCooldownSummand;
                //article.GetComponent<Slot>().values[24] += Prefixes.self.createProjectileChanceSummand;

                //article.GetComponent<Slot>().values[25] += Prefixes.self.spikes;
                //article.GetComponent<Slot>().values[29] += Prefixes.self.inscSummand;
                //for (int k = 0; k < article.GetComponent<Slot>().values.Length; k++)
                //    if (article.GetComponent<Slot>().values[k] != 0 && k != 28 && k != 30 && k != 31 && k != 32)
                //        description += article.GetComponent<Slot>().valuesNames[k] + ": <b>" + "<color=red>" + article.GetComponent<Slot>().values[k] + "</color>" + "</b>" + "\r\n";
                //if (description == "" && article.GetComponent<Slot>().itemDescription == "")
                //    description = "Empty";
                //else if (description == "")
                //    article.GetComponent<Slot>().itemDescription = article.GetComponent<Slot>().itemDescription;
                //else
                //    article.GetComponent<Slot>().itemDescription = description + Prefixes.self.extraDescription;
                //if (gameObjects[i].GetComponent<Slot>().values[31] > 0) тут хуйня какая-то
                article.GetComponent<Slot>().spikes += Prefixes.self.spikes;
                if (article.GetComponent<Slot>().spikes > 0)
                    description += "Spikes: " + article.GetComponent<Slot>().spikes + "\r\n";
                //
                for (int j = 0; j < 10; j++)
                {
                    //GameObject insc = Instantiate(inscription);
                    //article.GetComponent<Slot>().inscriptions[j] = insc; //ÑÐ¾Ð·Ð´Ð°ÑÑ ÑÐºÑÐ¸Ð¿Ñ Ð¸Ð½ÑÐºÑÐ¸Ð¿ÑÐ¸Ð¾Ð½Ñ, ÑÐ°Ð¼ Ñ
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
            if (rnd.Next(1, 100) < secondDropChance)
                i--;

        }
    }
}

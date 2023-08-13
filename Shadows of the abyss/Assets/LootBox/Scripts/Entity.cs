using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Entity : MonoBehaviour
{
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
                if (gameObjects[i].GetComponent<Slot>().type != "Usable" && gameObjects[i].GetComponent<Slot>().type != "Empty")
                    for (int k = 0; k < rareList.Length; k++)
                    {
                        ifChance += rareChances[k];
                        if (rareChance < ifChance)
                        {
                            rareName = rareList[k];
                            ifChance = 0;
                            break;
                        }
                        else k++;
                    }

                article.GetComponent<Slot>().sprite = gameObjects[i].GetComponent<Slot>().sprite;
                article.GetComponent<Slot>().rareName = rareName;
                Prefixes.self.PrefixChooser(rareName, gameObjects[i].GetComponent<Slot>().damage, article);
                description += article.GetComponent<Slot>().rareName + " " + article.GetComponent<Slot>().itemDescription + "\r\n";
                article.GetComponent<Slot>().damage +=  Prefixes.self.damageSummand;
                if (article.GetComponent<Slot>().damage > 0)
                    description += "Damage: " + article.GetComponent<Slot>().damage + "\r\n";
                article.GetComponent<Slot>().iceDamage += Prefixes.self.iceDamageSummand;
                if (article.GetComponent<Slot>().iceDamage > 0)
                    description += "Ice damage: " + article.GetComponent<Slot>().iceDamage + "\r\n";
                article.GetComponent<Slot>().igniteDamage +=  Prefixes.self.igniteDamageSummand; 
                if (article.GetComponent<Slot>().igniteDamage > 0)
                    description += "Ignite damage: " + article.GetComponent<Slot>().igniteDamage + "\r\n";
                article.GetComponent<Slot>().lightningDamage +=  Prefixes.self.lightningDamageSummand;
                if (article.GetComponent<Slot>().lightningDamage > 0)
                    description += "Ligtning damage: " + article.GetComponent<Slot>().lightningDamage + "\r\n";
                article.GetComponent<Slot>().poisonDamage +=  Prefixes.self.poisonDamageSummand;
                if (article.GetComponent<Slot>().poisonDamage > 0)
                    description += "Poison damage: " + article.GetComponent<Slot>().poisonDamage + "\r\n";
                article.GetComponent<Slot>().voidDamage +=  Prefixes.self.voidDamageSummand;
                if (article.GetComponent<Slot>().voidDamage > 0)
                    description += "Void damage: " + article.GetComponent<Slot>().voidDamage + "\r\n";
                article.GetComponent<Slot>().pureDamage +=  Prefixes.self.pureDamageSummand;
                if (article.GetComponent<Slot>().pureDamage > 0)
                    description += "Pure damage: " + article.GetComponent<Slot>().damage + "\r\n";
                article.GetComponent<Slot>().defence +=  Prefixes.self.defenceSummand;
                if (article.GetComponent<Slot>().defence > 0)
                    description += "Defence: " + article.GetComponent<Slot>().defence + "\r\n";
                article.GetComponent<Slot>().iceResist +=  Prefixes.self.iceResistSummand;
                if (article.GetComponent<Slot>().iceResist > 0)
                    description += "Ice resist: " + article.GetComponent<Slot>().iceResist + "\r\n";
                article.GetComponent<Slot>().igniteResist +=  Prefixes.self.igniteResistSummand;
                if (article.GetComponent<Slot>().igniteResist > 0)
                    description += "Ignite resist: " + article.GetComponent<Slot>().igniteResist + "\r\n";
                article.GetComponent<Slot>().lightningResist += Prefixes.self.lightningResistSummand;
                if (article.GetComponent<Slot>().lightningResist > 0)
                    description += "Lightning resist: " + article.GetComponent<Slot>().lightningResist + "\r\n";
                article.GetComponent<Slot>().poisonResist +=  Prefixes.self.poisonResistSummand;
                if (article.GetComponent<Slot>().poisonResist > 0)
                    description += "Poison resist: " + article.GetComponent<Slot>().poisonResist + "\r\n";
                article.GetComponent<Slot>().voidResist +=  Prefixes.self.voidResistSummand;
                if (article.GetComponent<Slot>().voidResist > 0)
                    description += "Void resist: " + article.GetComponent<Slot>().voidResist + "\r\n";
                article.GetComponent<Slot>().pureResist +=  Prefixes.self.pureResistSummand;
                if (article.GetComponent<Slot>().pureResist > 0)
                    description += "Pure resist: " + article.GetComponent<Slot>().pureResist + "\r\n";
                article.GetComponent<Slot>().hp +=  Prefixes.self.maxHpSummand;
                if (article.GetComponent<Slot>().hp > 0)
                    description += "Max HP: +" + article.GetComponent<Slot>().hp + "\r\n";
                article.GetComponent<Slot>().evasionChance +=  Prefixes.self.evasionChanceSummand;
                if (article.GetComponent<Slot>().evasionChance > 0)
                    description += "Evasion chance: " + article.GetComponent<Slot>().evasionChance + "\r\n";
                article.GetComponent<Slot>().criticalChance +=  Prefixes.self.criticalChanceSummand;
                if (article.GetComponent<Slot>().criticalChance > 0)
                    description += "Critical chance: " + article.GetComponent<Slot>().criticalChance + "\r\n";
                article.GetComponent<Slot>().kind = gameObjects[i].GetComponent<Slot>().kind;
                article.GetComponent<Slot>().idItem = gameObjects[i].GetComponent<Slot>().idItem;
                if (description == "" && article.GetComponent<Slot>().itemDescription == "")
                    description = "Empty";
                else if (description == "")
                    article.GetComponent<Slot>().itemDescription = article.GetComponent<Slot>().itemDescription;
                else
                    article.GetComponent<Slot>().itemDescription = description + Prefixes.self.extraDescription;
                if (gameObjects[i].GetComponent<Slot>().stackAmount > 0)
                    secondDropChance /= 2;
                else secondDropChance = 0;
                article.transform.position += new Vector3((float)rnd.Next(-2,2)/10, (float)rnd.Next(-2, 2)/10);
            }
            gameObject.transform.localScale /= 100;
            if (rnd.Next(0, 100) < secondDropChance)
                i--;

        }
    }
}

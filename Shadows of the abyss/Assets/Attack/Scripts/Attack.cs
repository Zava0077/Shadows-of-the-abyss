using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Attack : MonoBehaviour
{
    public GameObject meleeProjectiler;
    public GameObject rangeProjectiler;
    public GameObject[] projectilers = new GameObject[4096];
    Sprite weaponSprite;
    public static Attack self;
    public int e = 0;
    public int n = 1;
    public bool isAttacking;
    public bool isAbleToAttack;
    public Attack()
    {
        self = this;
    }
    int tripleAttackChance;
    public int id;
    private void Awake()
    {
        tripleAttackChance = 0;
        isAbleToAttack = true;
        id = 0;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && !SlotInteraction.isHovered && CursorSlot.self.type == "Empty" && isAbleToAttack)
        {
            weaponSprite = ArmourInventory.armourSlots[4].weaponSprite;
            isAttacking = true;
        }
    }
    private void FixedUpdate()
    {
        if(isAttacking)
        {
            Attacking();
            Invoke(nameof(IsAbleToAttackReseter), ArmourInventory.self.attackSpeedValue + ArmourInventory.self.weaponCooldownValue);
            isAttacking = false;
        }
    } 
    void IsAbleToAttackReseter()
    {
        isAbleToAttack = true;
        MeleeProjectiler.self.isCreated = false;
        RangedProjectiler.self.isCreated = false;
    }
    public void AttackInvoker()
    {
        Invoke(nameof(Attacking), ArmourInventory.self.attackSpeedValue);
    }
    public void Attacking()
    {
        if (e == projectilers.Length || id == projectilers.Length)
            ;
        else
        {
            switch(ArmourInventory.self.weaponType)
            {
                case "Melee":
                    MeleeAttack();
                    break;
                case "Ranged":
                    RangeAttack();
                    break;
                case "Mage":
                    if (PlayerScript.self.Mana > 0)
                    {
                        PlayerScript.self.Mana -= ArmourInventory.self.manaCostValue;
                        RangeAttack();
                    }
                    else Debug.Log("Нехватка маны!");
                    break;
                    
            }
        }
        CancelInvoke(nameof(Attacking));
    }
    void MeleeAttack()
    {
        isAbleToAttack = false;
        projectilers[id] = meleeProjectiler;
        if (Random.Range(1, 100) <= ArmourInventory.self.tripleAttackChanceValue)
        {
            id++;
            projectilers[id] = meleeProjectiler;
            id++;
            projectilers[id] = meleeProjectiler;
        }
        id++;
        for (int i = 0; i < projectilers.Length - 1; i++)
        {
            if (i % 2 == 0 && projectilers[i] != null && projectilers[e] != null)
            {
                GameObject weapon = Instantiate(projectilers[e], transform.position, Quaternion.identity, transform);
                weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
                projectilers[e].GetComponent<MeleeProjectiler>().rotateZShift = 30 * e;
                e++;
            }
            else if (projectilers[i] != null && projectilers[e] != null)
            {
                GameObject weapon = Instantiate(projectilers[e], transform.position, Quaternion.identity, transform);
                weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
                projectilers[e].GetComponent<MeleeProjectiler>().rotateZShift = -30 * n;
                n++;
            }
        }
    }
    void RangeAttack()
    {
        isAbleToAttack = false;
        projectilers[id] = rangeProjectiler;
        if (Random.Range(1, 100) <= ArmourInventory.self.tripleAttackChanceValue)
        {
            id++;
            projectilers[id] = rangeProjectiler;
            id++;
            projectilers[id] = rangeProjectiler;
        }
        id++;
        for (int i = 0; i < projectilers.Length - 1; i++)
        {
            if (i % 2 == 0 && projectilers[i] != null && projectilers[e] != null)
            {
                GameObject weapon = Instantiate(projectilers[e], transform.position, Quaternion.identity, transform);
                weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
                projectilers[e].GetComponent<RangedProjectiler>().rotateZShift = 30 * e;
                e++;
            }
            else if (projectilers[i] != null && projectilers[e] != null)
            {
                GameObject weapon = Instantiate(projectilers[e], transform.position, Quaternion.identity, transform);
                weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
                projectilers[e].GetComponent<RangedProjectiler>().rotateZShift = -30 * n;
                n++;
            }
        }
    }
}

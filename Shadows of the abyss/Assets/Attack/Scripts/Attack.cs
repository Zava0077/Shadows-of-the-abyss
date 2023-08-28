using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.TerrainTools;

public class Attack : MonoBehaviour
{
    public GameObject meleeProjectiler;
    public GameObject rangeProjectiler;
    public GameObject[] projectilers;
    Sprite weaponSprite;
    public static Attack self;
    public int e = 0;
    public bool isAttacking;
    public static bool isAbleToAttack;
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
        projectilers = new GameObject[4096];
        e = 0;
        id = 0;
    }
    private void Update()
    {
        if (Input.GetButton("Fire1") && !SlotInteraction.isHovered && CursorSlot.self.type == "Empty" && isAbleToAttack && (ArmourInventory.self.weaponType != "Empty" && ArmourInventory.self.weaponType != ""))
        {
            weaponSprite = ArmourInventory.armourSlots[4].weaponSprite;
            isAttacking = true;
        }
        if (ArmourInventory.self.weaponType == "Empty" || ArmourInventory.self.weaponType == "")
            isAbleToAttack = true;
    }
    private void FixedUpdate()
    {
        if(isAttacking)
        {
            Attacking();
            isAbleToAttack = false;
            isAttacking = false;
        }
    } 
    public void IsAbleToAttackInvoker()
    {
        Invoke(nameof(IsAbleToAttackReseter), ArmourInventory.self.weaponCooldownValue);
    }
    void IsAbleToAttackReseter()
    {
        isAbleToAttack = true;
        MeleeProjectiler.self.isCreated = false;
        RangedProjectiler.self.isCreated = false;
        CancelInvoke(nameof(IsAbleToAttackReseter));
    }
    public void AttackInvoker()
    {
        Invoke(nameof(Attacking), ArmourInventory.self.attackSpeedValue);
    }
    public void Attacking()
    {
        if (e >= projectilers.Length - 1 || id >= projectilers.Length - 1)
        {
            e = 0;
            id = 0;
            projectilers = new GameObject[4096];
        }
        switch (ArmourInventory.self.weaponType)
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
        CancelInvoke(nameof(Attacking));
    }
    void MeleeAttack()
    {
        isAbleToAttack = false;
        projectilers[id] = meleeProjectiler;
        TripleAttack(ArmourInventory.self.tripleAttackChanceValue, meleeProjectiler, 2, Random.Range(1, 100));
        id++;
        for (int i = 0; i < projectilers.Length - 1; i++)
        {
            //if (e >= projectilers.Length - 1 || id >= projectilers.Length - 1)
            //{
            //    e = 0;
            //    id = 0;
            //    projectilers = new GameObject[4096];
            //}
            if (/*i % 2 == 0 && */projectilers[i] != null && projectilers[e] != null)
            {
                GameObject weapon = Instantiate(projectilers[e], transform.position, Quaternion.identity, transform);
                weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
                projectilers[e].GetComponent<MeleeProjectiler>().rotateZShift = 30 * e - id/2;
                e++;
            }
        }
    }
    void RangeAttack()
    {
        isAbleToAttack = false;
        projectilers[id] = rangeProjectiler;
        TripleAttack(ArmourInventory.self.tripleAttackChanceValue, rangeProjectiler,2, Random.Range(1,100));
        id++;
        for (int i = 0; i < projectilers.Length - 1; i++)
        {
            if (e >= projectilers.Length)
                e = 0;
          if (/*i % 2 == 0 && */projectilers[i] != null && projectilers[e] != null)
            {
                GameObject weapon = Instantiate(projectilers[e], transform.position, Quaternion.identity, transform);
                weapon.GetComponentInChildren<SpriteRenderer>().sprite = weaponSprite;
                projectilers[e].GetComponent<RangedProjectiler>().rotateZShift = 30 * e - id/2;
                e++;
            }
        }
    }
    void TripleAttack(float chance,GameObject projectiler, int attacksNum, int slotMachineNumber)
    {
        if (slotMachineNumber <= chance)
        {
            if (slotMachineNumber <= chance * 0.5f)
                attacksNum++;
            if (id < projectilers.Length && id + 1 < projectilers.Length && id + 2 < projectilers.Length)
            {
                for (int i = 0; i < attacksNum; i++)
                {
                    id++;
                    projectilers[id] = projectiler;
                }
            }
            else
            {
                projectilers = new GameObject[4096];
                id = 0;
                projectilers[id] = projectiler;
                id++;
                projectilers[id] = projectiler;
            }
        }
    }
}

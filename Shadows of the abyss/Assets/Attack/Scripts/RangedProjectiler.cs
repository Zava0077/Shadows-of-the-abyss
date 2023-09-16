using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RangedProjectiler : MonoBehaviour
{
    public static RangedProjectiler self;
    public RangedProjectiler()
    {
        self = this;
    }
    public float rotateZShift;
    public float secondAttackChance;
    public bool isCreated;
    public GameObject projectile;
    Sprite bulletSprite;
    private void Awake()
    {
        secondAttackChance = ArmourInventory.self.secondUsageChanceValue;
        bulletSprite = ArmourInventory.armourSlots[4].projectileSprite;
    }
    void Update()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + rotateZShift);
        if (!isCreated)
        {
            GameObject weapon = Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent.transform.parent);
            weapon.GetComponent<SpriteRenderer>().sprite = bulletSprite;
            isCreated = true;
        }
        if (Random.Range(1, 100) <= secondAttackChance)
        {
            Invoke(nameof(Destroying), ArmourInventory.self.attackSpeedValue);
        }
        else
            Invoke(nameof(FullDestroying), ArmourInventory.self.attackSpeedValue);
    }
    void FullDestroying()
    {
        Attack.self.projectilers = new GameObject[4096];
        Attack.self.e = 0;
        Attack.self.id = 0;
        Attack.self.isAttacking = false;
        Destroy(gameObject);
        Attack.self.IsAbleToAttackInvoker();
        CancelInvoke(nameof(FullDestroying));
    }
    void Destroying()
    {
        Destroy(gameObject);
        Attack.self.isAttacking = true;
        CancelInvoke(nameof(Destroying));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DamageEvent
    }
}

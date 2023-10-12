using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class MeleeProjectiler : MonoBehaviour
{
    public static MeleeProjectiler self;
    public MeleeProjectiler()
    {
        self = this;
    }
    List<GameObject> enemies = new List<GameObject>();
    public float rotateZShift;
    public int secondAttackChance;
    public int createWaveChance;
    public bool isCreated;
    public GameObject wave;
    private void Awake()
    {
        secondAttackChance = Random.Range(1, 100);
        createWaveChance = Random.Range(1, 100);
    }
    void Update()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + rotateZShift);
        if (createWaveChance <= ArmourInventory.self.createProjectileChanceValue && !isCreated)
        {
            GameObject bullet = Instantiate(wave, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent.parent);
            bullet.GetComponentInChildren<SpriteRenderer>().sprite = ArmourInventory.armourSlots[4].projectileSprite;
            isCreated = true;
        }
        if (secondAttackChance <= ArmourInventory.self.secondUsageChanceValue)
        {
            Invoke(nameof(Destroying), 1/ArmourInventory.self.attackSpeedValue);
        }
        else
            Invoke(nameof(FullDestroying), 1/ArmourInventory.self.attackSpeedValue);

    }
    void FullDestroying()
    {
        Attack.self.projectilers = new GameObject[4096];
        Attack.self.e = 0;
        Attack.self.id = 0;
        Attack.self.isAttacking = false;
        Destroy(gameObject);
        enemies.Clear();
        Attack.self.IsAbleToAttackInvoker();
        CancelInvoke(nameof(FullDestroying));
    }
    void Destroying()
    {
        Destroy(gameObject);
        Attack.self.isAttacking = true;
        enemies.Clear();
        CancelInvoke(nameof(Destroying));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DamageEvent
        if (collision.tag == "Enemy" && !enemies.Contains(collision.gameObject))
        {
            enemies.Add(collision.gameObject);
            //if (ArmourInventory.self.damageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.damageValue, DamageType.DamageTypes.Physical);
            //if (ArmourInventory.self.iceDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.iceDamageValue, DamageType.DamageTypes.Cold);
            //if (ArmourInventory.self.igniteDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.igniteDamageValue, DamageType.DamageTypes.Fire);
            //if (ArmourInventory.self.lightningDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.lightningDamageValue, DamageType.DamageTypes.Lightning);
            //if (ArmourInventory.self.poisonDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.poisonDamageValue, DamageType.DamageTypes.Poison);
            //if (ArmourInventory.self.pureDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.pureDamageValue, DamageType.DamageTypes.Pure);
            //if (ArmourInventory.self.voidDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.voidDamageValue, DamageType.DamageTypes.Void);
        }
    }
}

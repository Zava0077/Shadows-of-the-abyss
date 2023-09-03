using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWaveProj : MonoBehaviour
{
    float projectileSpeed = 15f;
    Rigidbody2D rb;
    int piercesCount; //
    int pierceChance; //
    List<GameObject> enemies = new List<GameObject>();
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = transform.right * projectileSpeed;
        piercesCount = ArmourInventory.self.pierceValue;
        pierceChance = ArmourInventory.self.extraPierceChanceValue;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall" || collision.tag == "Door")
            Destroy(gameObject);
        if (collision.tag == "Enemy" && !enemies.Contains(collision.gameObject) && piercesCount > 0)
        {
            if (Random.Range(1, 100) <= pierceChance)
                ;
            else piercesCount--;
            enemies.Add(collision.gameObject);
            if (ArmourInventory.self.damageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.damageValue, DamageType.DamageTypes.Physical);
            if (ArmourInventory.self.iceDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.iceDamageValue, DamageType.DamageTypes.Cold);
            if (ArmourInventory.self.igniteDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.igniteDamageValue, DamageType.DamageTypes.Fire);
            if (ArmourInventory.self.lightningDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.lightningDamageValue, DamageType.DamageTypes.Lightning);
            if (ArmourInventory.self.poisonDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.poisonDamageValue, DamageType.DamageTypes.Poison);
            if (ArmourInventory.self.pureDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.pureDamageValue, DamageType.DamageTypes.Pure);
            if (ArmourInventory.self.voidDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.voidDamageValue, DamageType.DamageTypes.Void);
            if (piercesCount == 0)
                Destroy(gameObject);
        }
    }
}

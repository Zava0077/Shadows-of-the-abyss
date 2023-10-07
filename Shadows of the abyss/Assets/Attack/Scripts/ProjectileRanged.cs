using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRanged : MonoBehaviour
{
    float projectileSpeed = 15f;
    Rigidbody2D rb;
    public GameObject projectile;
    float piercesCount; //
    float pierceChance; //
    public bool isCrit;
    int direction;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        piercesCount = ArmourInventory.self.pierceValue;
        pierceChance = ArmourInventory.self.extraPierceChanceValue;
    }
    List<GameObject> enemies = new List<GameObject>();
    void Update()
    {
        if (isCrit && Random.Range(1, 100) < ArmourInventory.self.createProjectileChanceValue)
        {
            int rand = Random.Range(-1, 1);
            if (rand == 0)
                direction = 1;
            else direction = -1;
            GameObject bullet = Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0f, 0f, transform.rotation.z + 30 * direction), this.gameObject.transform.parent);
            bullet.GetComponent<SpriteRenderer>().sprite = transform.gameObject.GetComponent<SpriteRenderer>().sprite;
            isCrit = false;
        }
        rb.velocity = transform.right * projectileSpeed;
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
            //mojno dobavit' mechaniku tip vse pierce prevrashautsa v mnojitel urona.
            //if (ArmourInventory.self.damageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.damageValue, DamageType.DamageTypes.Physical);
            //if (ArmourInventory.self.iceDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.iceDamageValue, DamageType.DamageTypes.Cold);
            //if (ArmourInventory.self.igniteDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.igniteDamageValue, DamageType.DamageTypes.Fire);
            //if (ArmourInventory.self.lightningDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.lightningDamageValue, DamageType.DamageTypes.Lightning);
            //if (ArmourInventory.self.poisonDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.poisonDamageValue, DamageType.DamageTypes.Poison);
            //if (ArmourInventory.self.pureDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.pureDamageValue, DamageType.DamageTypes.Pure);
            //if (ArmourInventory.self.voidDamageValue != 0) DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.voidDamageValue, DamageType.DamageTypes.Void);
            if (piercesCount == 0)
                Destroy(gameObject);
        }
    }
}

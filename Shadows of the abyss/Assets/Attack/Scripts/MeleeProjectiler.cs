using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeProjectiler : MonoBehaviour
{
    public static MeleeProjectiler self;
    public MeleeProjectiler()
    {
        self = this;
    }
    public float rotateZShift;
    public int secondAttackChance;
    public int createWaveChance;
    public bool isCreated;
    public GameObject wave;
    private void Awake()
    {
        secondAttackChance = 0;
        createWaveChance = 0;
    }
    void Update()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + rotateZShift);
        if (Random.Range(1, 100) <= ArmourInventory.self.createProjectileChanceValue && !isCreated)
        {
            Instantiate(wave, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent.transform.parent);
            isCreated = true;
        }
        if (Random.Range(1, 100) <= ArmourInventory.self.secondUsageChanceValue)
        {
            //Attack.self.AttackInvoker();
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
        if(collision.tag == "Enemy")
        {
            DamageType.GetDamage(collision.gameObject.GetComponent<Entity>(), ArmourInventory.self.damageValue, DamageType.DamageTypes.Physical);
        }
    }
}

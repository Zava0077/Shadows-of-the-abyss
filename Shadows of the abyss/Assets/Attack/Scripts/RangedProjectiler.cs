using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectiler : MonoBehaviour
{
    public static RangedProjectiler self;
    public RangedProjectiler()
    {
        self = this;
    }
    public float rotateZShift;
    public int secondAttackChance;
    public bool isCreated;
    public GameObject projectile;
    private void Awake()
    {
        secondAttackChance = 0;
    }
    void Update()
    {
        Vector3 diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + rotateZShift);
        if (!isCreated)
        {
            Instantiate(projectile, this.gameObject.transform.position, this.gameObject.transform.rotation, this.gameObject.transform.parent.transform.parent);
            isCreated = true;
        }
        if (Random.Range(1, 100) <= secondAttackChance)
        {
            Attack.self.AttackInvoker();
            Invoke(nameof(Destroying), PlayerScript.self.attackSpeed);
        }
        else
            Invoke(nameof(FullDestroying), PlayerScript.self.attackSpeed);
    }
    void FullDestroying()
    {
        Attack.self.projectilers = new GameObject[4096];
        Attack.self.e = 0;
        Attack.self.n = 1;
        Attack.self.id = 0;
        Destroy(gameObject);
        CancelInvoke(nameof(Destroying));
    }
    void Destroying()
    {
        Destroy(gameObject);
        CancelInvoke(nameof(Destroying));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DamageEvent
    }
}

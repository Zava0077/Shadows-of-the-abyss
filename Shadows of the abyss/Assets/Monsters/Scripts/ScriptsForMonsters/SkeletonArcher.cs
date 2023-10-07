using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.Mathematics;

public class SkeletonArcher : Mob
{
    System.Random rand = new System.Random();
    Rigidbody2D rb;
    [SerializeField] private Transform target;
    NavMeshAgent agent;
    private Image HealthBar;
    bool aggro = false;
    float timeLeft = 5f;
    float timeShoot = 3f;
    public GameObject projectile;
    void Start()
    {
        Health = 10;
        MaxHealth = 10;
        Armor = 1;
        Speed = 2;
        //resistance
        FireRes = 0.1f;
        ColdRes = 0.1f;
        LightningRes = 0.1f;
        PhysicalRes = 0.1f;
        PoisonRes = 0.1f;
        VoidRes = 0.1f;
        ProjectileSpeed = 4f;
        PhysicalDamage = 5f;
        increasedPhysicalDamage = 50;

        agent = GetComponent<NavMeshAgent>();
        target = PlayerScript.self.gameObject.transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Speed;
        HealthBar = GetComponentInChildren<Image>();
        rb = GetComponent<Rigidbody2D>();
    }

    private bool CanShoot(RaycastHit2D hit2D)
    {
        if (hit2D.collider == PlayerScript.self.GetComponent<Collider2D>())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Update()
    {
        RotateMob();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (target.position - transform.position) / (target.position - transform.position).magnitude, Vector2.Distance(target.position, transform.position));
        if (Vector2.Distance(target.position,transform.position) <= 5 && hit.collider == target.GetComponent<Collider2D>())
        {
            aggro = true;
        }
        else
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft >= 0)
            {
                timeLeft = 5f;
                aggro = false;
            }
        }
        if (aggro)
        {
            agent.SetDestination(target.position);
            timeShoot -= Time.deltaTime;
            if (CanShoot(hit) && timeShoot < 0)
            {
                timeShoot = 3f;
                var direction = transform.position - target.position;
                var angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
                MonsterProjectile arrow = Instantiate(projectile, transform.position, Quaternion.Euler(0,0,angle)).GetComponent<MonsterProjectile>();
                arrow.Create(this);
            }
        }
    }
}

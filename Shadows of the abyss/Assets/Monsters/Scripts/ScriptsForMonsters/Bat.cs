using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Unity.Mathematics;

public class Bat : Mob
{
    System.Random rand = new System.Random();
    Rigidbody2D rb;
    [SerializeField] private Transform target;
    public GameObject[] drop;
    public int[] dropChances;
    NavMeshAgent agent;
    bool CanDash = true;
    bool isDashing = false;
    private Vector2 playerPos;
    private int Damage = 5;
    float _oneSecTimer = 0;
    private Image HealthBar;
    float DashCooldown = 2f;
    float DashingTime = 0.2f;
    float DashToPlayer = 3;
    void Start()
    {
        Health = 30;
        MaxHealth = 30;
        Armor = 5;
        Speed = 2;
        //resistance
        FireRes = 0.1f;
        ColdRes = 0.1f;
        LightningRes = 0.1f;
        PhysicalRes = 0.1f;
        PoisonRes = 0.1f;
        VoidRes = 0.1f;

        agent = GetComponent<NavMeshAgent>();
        target = PlayerScript.self.gameObject.transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Speed;
        HealthBar = GetComponentInChildren<Image>();
        rb = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        Limits();
        GetComponentInChildren<SpriteRenderer>().color = Color.black;
        Pushing();
        if (Health <= 0)
            Die(drop,dropChances);
        HealthBar.fillAmount = (float)Health / (float)MaxHealth;
        agent.SetDestination(target.position);
        DashToPlayer -= Time.deltaTime;
        if(DashToPlayer <= 0)
        {
            agent.speed = Speed * 3;
        }
        if(DashToPlayer <= -1)
        {
            agent.speed = Speed;
            DashToPlayer = 3;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Attack")
        {
            _oneSecTimer += Time.deltaTime;
            if (_oneSecTimer >= 1f)
            {
                DamageType.GetDamage(PlayerScript.self, this, DamageType.DamageTypes.Melee);
                _oneSecTimer -= 1f;
            }
        }
    }

    IEnumerator Dash()
    {
        CanDash = false;
        isDashing = true;
        if (Math.Abs((Math.Abs(gameObject.transform.position.y) - Math.Abs(PlayerScript.self.transform.position.y))) > Math.Abs(Math.Abs(gameObject.transform.position.x) - Math.Abs(PlayerScript.self.transform.position.x)))
        {
            if (rand.Next(0, 2) == 0)
            {
                rb.velocity = new Vector2(transform.localScale.x * 10f, 0f);
                yield return new WaitForSeconds(DashingTime);
                rb.velocity = new Vector2(0f, 0f);
            }
            else
            {
                rb.velocity = new Vector2(transform.localScale.x * -10f, 0f);
                yield return new WaitForSeconds(DashingTime);
                rb.velocity = new Vector2(0f, 0f);
            }
        }
        else
        {
            if (rand.Next(0, 2) == 0)
            {
                rb.velocity = new Vector2(0.2f, transform.localScale.x * -10f);
                yield return new WaitForSeconds(DashingTime);
                rb.velocity = new Vector2(0f,0f);
            }
            else
            {
                rb.velocity = new Vector2(0.2f, transform.localScale.x * 10f);
                yield return new WaitForSeconds(DashingTime);
                rb.velocity = new Vector2(0f, 0f);
            }
        }
        isDashing = false;
        yield return new WaitForSeconds(DashCooldown);
        CanDash = true;
    }

    private void OnMouseEnter()
    {
        if(CanDash)
        {
            StartCoroutine(Dash());
        }
    }
}

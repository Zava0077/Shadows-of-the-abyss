using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Bat : Entity
{
    [SerializeField] private Transform target;
    public GameObject[] drop;
    public int[] dropChances;
    NavMeshAgent agent;
    private Vector2 playerPos;
    private int Damage = 5;
    float _oneSecTimer = 0;
    private Image HealthBar;
    void Start()
    {
        Health = 30;
        MaxHealth = 30;
        Armor = 5;
        Speed = 2;
        //resistance
        FireRes = 0.1;
        ColdRes = 0.1;
        LightningRes = 0.1;
        PhysicalRes = 0.1;
        PoisonRes = 0.1;
        VoidRes = 0.1;

        agent = GetComponent<NavMeshAgent>();
        target = PlayerScript.self.gameObject.transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = Speed;
        HealthBar = GetComponentInChildren<Image>();
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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
}

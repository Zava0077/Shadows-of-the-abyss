using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Bat : Creature
{
    [SerializeField] private Transform target;
    NavMeshAgent agent;
    private Vector2 playerPos;
    private int Damage = 5;
    float _oneSecTimer = 0;
    private Image HealthBar;
    void Start()
    {
        Health = 10;
        MaxHealth = 10;
        Armor = 1;
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
        HealthBar.fillAmount = (float)Health / (float)MaxHealth;
        agent.SetDestination(target.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _oneSecTimer += Time.deltaTime;
        if(_oneSecTimer >= 1f)
        {
            _oneSecTimer -= 1f;
            if (collision.transform == target.transform)
            {
                DamageType.GetDamage(PlayerScript.self, 5, DamageType.DamageTypes.Physical);
            }
        }
    }
}

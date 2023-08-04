using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Creature
{
    private Transform target;
    private Vector2 playerPos;
    private int Damage = 5;
    float _oneSecTimer = 0;
    void Start()
    {
        Health = 10;
        Armor = 3;
        Speed = 3;
        //resistance
        FireRes = 10;
        ColdRes = 10;
        LightningRes = 10;
        PhysicalRes = 10;
        PoisonRes = 10;
        VoidRes = 10;


        target = PlayerScript.self.gameObject.transform;
    }

    void Update()
    {
        if(Health <= 0)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        playerPos = new Vector2(target.position.x, target.position.y);
        Vector3 pos = Vector3.Lerp(transform.position, transform.position + new Vector3(playerPos.x - transform.position.x, playerPos.y - transform.position.y).normalized, Time.deltaTime);
        transform.position = pos;
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

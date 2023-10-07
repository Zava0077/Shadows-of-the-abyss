using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class MonsterProjectile : MonoBehaviour
{
    public Entity Entity;
    public float projectileSpeed = 4f;
    Vector3 target;
    public float rotateZShift;
    Rigidbody2D rb;
    float time = 0f;
    public void Create(Mob entity)
    {
        Entity = entity;
        projectileSpeed = entity.ProjectileSpeed;
        GetComponent<SpriteRenderer>().flipX = true;
        target = PlayerScript.self.transform.position;
    }


    void Update()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target, +4f * Time.deltaTime);
        transform.position += transform.right * Time.deltaTime * -projectileSpeed;
        time += Time.deltaTime;
        if(time > 4f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider == PlayerScript.self.GetComponent<Collider2D>())
        {
            DamageType.GetDamage(PlayerScript.self, Entity, DamageType.DamageTypes.Range);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

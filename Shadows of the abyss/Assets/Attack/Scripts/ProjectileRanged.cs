using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRanged : MonoBehaviour
{
    float projectileSpeed = 15f;
    Rigidbody2D rb;
    public GameObject projectile;
    int createProjChance;
    int piercesCount; //
    int pierceChance; //
    public bool isCrit;
    int direction;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
        createProjChance = 100;
    }
    void Update()
    {
        if (isCrit && Random.Range(1, 100) < createProjChance)
        {
            int rand = Random.Range(-1, 1);
            if (rand == 0)
                direction = 1;
            else direction = -1;
            Instantiate(projectile, gameObject.transform.position, Quaternion.Euler(0f,0f, transform.rotation.z + 30 * direction), this.gameObject.transform.parent);
            isCrit = false;
        }
        rb.velocity = transform.right * projectileSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
            Destroy(gameObject);
    }
}

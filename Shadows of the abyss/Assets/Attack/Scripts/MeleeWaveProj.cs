using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWaveProj : MonoBehaviour
{
    float projectileSpeed = 15f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = transform.right * projectileSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
            Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    Vector2 position;
    Vector2 playerPosition;
    Vector2 direction;
    float pickUpSpeed = 10f;
    private void Update()
    {
        position = transform.position;
        transform.GetComponent<Rigidbody2D>().position = (Vector2)transform.position + direction;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && collision.isTrigger == true)
        {
            playerPosition = PlayerScript.self.rb2d.position;
            direction = Vector2.Lerp(Vector2.zero, new Vector2(playerPosition.x - position.x, playerPosition.y - position.y).normalized,Time.deltaTime*pickUpSpeed);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !collision.collider.isTrigger)
        {
            Inventory.IsInventoryFull(collision.otherCollider.gameObject);
            Inventory.self.PickUpItem(collision.otherCollider.gameObject);
            Destroy(collision.otherCollider.gameObject);
        }
    }
}

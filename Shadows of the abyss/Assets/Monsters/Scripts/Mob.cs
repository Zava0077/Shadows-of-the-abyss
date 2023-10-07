using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : Entity
{
    Entity target = PlayerScript.self;
    public SpriteRenderer sprite;

    // Update is called once per frame
    public void RotateMob()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        if (target.transform.position.x < this.transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}

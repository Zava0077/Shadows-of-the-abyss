using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoomDoors : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Door")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}

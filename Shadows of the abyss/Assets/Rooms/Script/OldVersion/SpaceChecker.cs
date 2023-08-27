using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceChecker : MonoBehaviour
{
    public static bool isTriggered;
    public static SpaceChecker self;
    public SpaceChecker()
    {
        self = this;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpaceChecker")
        {
            InvokeDestroyMap();
            isTriggered = true;
        }
    }
    public void InvokeDestroyMap()
    {
        StartCoroutine(DestroyMap());
    }
    IEnumerator DestroyMap()
    {
        for (int i = 0; i < MapGeneration.roomsNum; i++)
        {
            for (int j = 0; j < MapGeneration.roomsNum; j++)
            {
                Destroy(MapGeneration.roomsQueue[i, j]);
                MapGeneration.roomsQueue[i, j] = null;
            }
        }
        MapGeneration.rebuild = true;
        yield return new WaitForSeconds(2);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpaceChecker")
            isTriggered = false;
        Debug.Log(collision.gameObject);
    }
}

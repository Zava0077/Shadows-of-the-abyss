using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpaceChecker : MonoBehaviour
{
    public static bool isTriggered;
    public static SpaceChecker self;
    static int cycles;
    static int retries;
    public SpaceChecker()
    {
        self = this;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SpaceChecker")
        {
            StartCoroutine(DestroyMap());
            isTriggered = true;
        }
        retries = 0;
        cycles = 0;
    
    }
    IEnumerator DestroyMap()
    {
        MapGeneration.roomsLeft = MapGeneration.roomsNum;
        for (int i = 0; i < MapGeneration.roomsNum; i++)
        {
            for (int j = 0; j < MapGeneration.roomsNum; j++)
            {
                Destroy(MapGeneration.roomsQueue[i, j]);
                MapGeneration.roomsQueue[i, j] = null;
                MapGeneration.roomNum = -1;
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

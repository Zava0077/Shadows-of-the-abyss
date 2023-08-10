using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : Entity
{
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] int[] chance;
    private void Awake()
    {
        Die(gameObjects,chance);
    }
}

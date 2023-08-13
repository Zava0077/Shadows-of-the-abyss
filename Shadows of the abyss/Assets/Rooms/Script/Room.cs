using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] public string type;
    public string theOnlyNeighbourRelDirection;
    public GameObject DoorL;
    public GameObject DoorR;
    public GameObject DoorU;
    public GameObject DoorD;
    public GameObject WallL;
    public GameObject WallR;
    public GameObject WallU;
    public GameObject WallD;
    public int doorCount;

}

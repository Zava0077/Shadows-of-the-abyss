using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Net.Http.Headers;

public class MapGeneration : MonoBehaviour
{
    System.Random rnd = new System.Random();
    [SerializeField] GameObject[] rooms;
    public static GameObject[,] roomsQueue;
    public static int roomsNum;
    public static int roomsLeft;
    public static bool rebuilding;
    public static int retries;
    public static int cycles;
    public static int matrixLevel;
    public static int splitsNum;
    public static int splitNum;
    public static int[] splitRoomNumber;
    public static int[] splitedRoomNumber;
    public static int splitRoomsLeft;
    public static int roomNum;
    public static int transitionNumber;
    public static int virtualRoomNum;
    public static int secondVirtualRoomNum;
    public static bool rebuild;
    public static MapGeneration self;
    public MapGeneration()
    {
        self = this;
    }
    private void Awake()
    {
        roomsNum = 12;
        roomNum = -1;
        roomsLeft = roomsNum;
        Debug.Log(roomsNum);
        roomsQueue = new GameObject[roomsNum, roomsNum];
        splitRoomNumber = new int[roomsNum];
        splitedRoomNumber = new int[roomsNum];
        StartCoroutine(MapGenerator());
    }
    public void InvokeAwake()
    {
        Awake();
    }
    private void FixedUpdate()
    {
        if (rebuild == false && roomNum < roomsNum)
            SpaceChecker.self.InvokeDestroyMap();
        if (roomNum > roomsNum)
            SpaceChecker.self.InvokeDestroyMap();
        if (rebuild)
        {
            splitRoomsLeft = 0;
            roomsLeft = roomsNum;
            roomNum = -1;
            StartCoroutine(MapGenerator());
            rebuild = false;
        }
    }
    void Update()
    {

    }
    void Translation(int transitionNumber, int virtualRoomNum)
    {
        if (roomsQueue[transitionNumber, virtualRoomNum].GetComponent<Room>().type == "Split" && matrixLevel % 2 == 0)
        {
            roomsQueue[matrixLevel, roomNum].transform.position = roomsQueue[transitionNumber, virtualRoomNum].transform.Find("1").transform.position;
            roomsQueue[matrixLevel, roomNum].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, roomsQueue[transitionNumber, virtualRoomNum].transform.Find("1").GetComponent<RoomRotate>().roomRotation + roomsQueue[transitionNumber, virtualRoomNum].transform.eulerAngles.z));
        }
        else if (roomsQueue[transitionNumber, virtualRoomNum].GetComponent<Room>().type == "Split" && matrixLevel % 2 != 0)
        {
            roomsQueue[matrixLevel, roomNum].transform.position = roomsQueue[transitionNumber, virtualRoomNum].transform.Find("2").transform.position;
            roomsQueue[matrixLevel, roomNum].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, roomsQueue[transitionNumber, virtualRoomNum].transform.Find("2").GetComponent<RoomRotate>().roomRotation + roomsQueue[transitionNumber, virtualRoomNum].transform.eulerAngles.z));
        }
        if (roomsQueue[transitionNumber, virtualRoomNum].GetComponent<Room>().type == "Left" || roomsQueue[matrixLevel, virtualRoomNum].GetComponent<Room>().type == "Right" || roomsQueue[matrixLevel, virtualRoomNum].GetComponent<Room>().type == "Straight")
        {
            roomsQueue[matrixLevel, roomNum].transform.position = roomsQueue[transitionNumber, virtualRoomNum].transform.Find("Out").transform.position;
            roomsQueue[matrixLevel, roomNum].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, roomsQueue[transitionNumber, virtualRoomNum].transform.Find("Out").GetComponent<RoomRotate>().roomRotation + roomsQueue[transitionNumber, virtualRoomNum].transform.eulerAngles.z));
        }
        //if (SpaceChecker.isTriggered == true)
        //{
        //    if (cycles > 2)
        //    {
        //        roomsLeft = 0;
        //        Destroy(roomsQueue[matrixLevel, roomNum]);
        //        return;
        //    }
        //    retries++;
        //    roomsLeft++;
        //    Destroy(roomsQueue[matrixLevel, roomNum]);
        //    SpaceChecker.isTriggered = false;
        //    if (retries > 2)
        //    {
        //        Destroy(roomsQueue[matrixLevel, roomNum - 1]);
        //        roomsLeft++;
        //        retries = 0;
        //        cycles++;
        //        roomsQueue[matrixLevel, roomNum - 1] = null;
        //    }
        //    roomsQueue[matrixLevel, roomNum] = null;
        //    rebuilding = true;
        //    return;
        //}
    }
    public IEnumerator MapGenerator()
    {
        for (; roomsLeft > 0 + splitRoomsLeft; roomsLeft--)
        {
            roomNum++;
            roomsLeft--;
            rebuilding = false;
            int roomType = rnd.Next(0, rooms.Length);
            roomsQueue[matrixLevel, roomNum] = Instantiate(rooms[roomType]);
            if (roomsQueue[matrixLevel, roomNum].GetComponent<Room>().type == "Split")
            {
                splitRoomNumber[splitsNum] = roomNum;
                splitsNum++;
                splitRoomsLeft += roomsLeft / 2;
            }
            if (roomNum > 0 && !rebuilding)
            {
                transitionNumber = matrixLevel;
                virtualRoomNum = roomNum - 1;
                secondVirtualRoomNum = virtualRoomNum;
                if (matrixLevel > 0 && roomsQueue[matrixLevel, roomNum - 1] == null)
                {
                    transitionNumber = splitNum;
                    virtualRoomNum = splitRoomNumber[transitionNumber];
                    splitsNum--;
                    splitNum++;
                }
                Translation(transitionNumber, virtualRoomNum);
            }
            //if (SpaceChecker.isTriggered == true)
            //{
            //    if (cycles > 2)
            //    {
            //        roomsLeft = 0;
            //        Destroy(roomsQueue[matrixLevel, roomNum - 1]);
            //        yield break;
            //    }
            //    retries++;
            //    roomsLeft++;
            //    Destroy(roomsQueue[matrixLevel, roomNum - 1]);
            //    SpaceChecker.isTriggered = false;
            //    if (retries > 2)
            //    {
            //        Destroy(roomsQueue[matrixLevel, roomNum - 2]);
            //        roomsLeft++;
            //        retries = 0;
            //        cycles++;
            //        roomsQueue[matrixLevel, roomNum - 2] = null;
            //    }
            //    roomsQueue[matrixLevel, roomNum - 1] = null;
            //    rebuilding = true;
            //}
        }
        if (splitRoomsLeft > 0 && matrixLevel < roomsNum)
        {
            matrixLevel++;
            splitRoomsLeft = 0;
            splitedRoomNumber[matrixLevel] = roomNum;
            roomNum = splitRoomNumber[matrixLevel];
            StartCoroutine(MapGenerator());
        }
        yield return new WaitForSeconds(2);
    }
}



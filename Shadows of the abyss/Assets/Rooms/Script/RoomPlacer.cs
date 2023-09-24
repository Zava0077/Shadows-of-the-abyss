using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RoomPlacer : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMash;
    int[,] biomMap;
    public Room wall;
    public Room room;
    public Room magicRoom;
    public Room hellRoom;
    public Room voidRoom;
    int neighbours = 0;
    int[] treasureNeighbours;
    int dungeonSize;
    private Room[,] dungeon;
    private void Start()
    {
        dungeonSize = 32;
        dungeon = new Room[dungeonSize, dungeonSize];
        BiomGeneration(4);
        MapGenerator(dungeonSize);
        Invoke(nameof(NavMeshBulider), Time.deltaTime);
    }
    void NavMeshBulider()
    {
        navMash.BuildNavMesh();
        CancelInvoke(nameof(NavMeshBulider));
    }
    void BiomGeneration(int biomCount)
    {
        bool biomPlaced;
        float biomSize = (float)dungeonSize / biomCount;
        int[,] biomMap = new int[Mathf.CeilToInt(biomCount / 2), Mathf.FloorToInt(biomCount / 2)];
        int[,] bigBiomMap = new int[dungeonSize, dungeonSize];
        for (int biom = 1; biom <= biomCount; biom++)
        {
            biomPlaced = false;
            for (int x = 0; !biomPlaced;)
            {

                for (int y = 0; !biomPlaced; y++)
                {
                    int random = Random.Range(1, 100);
                    if (y == biomMap.GetLength(1))
                    {
                        x++;
                        y = 0;
                        if (x == biomMap.GetLength(0))
                            x = 0;
                    }
                    if (random < 10 && biomMap[x, y] == 0)
                    {
                        biomMap[x, y] = biom;
                        biomPlaced = true;
                    }
                    else continue;
                }
            }
        }
        for (int x = 0; x < bigBiomMap.GetLength(0); x++)
            for (int y = 0; y < bigBiomMap.GetLength(1); y++)
            {
                if (biomMap[Mathf.FloorToInt(x / (bigBiomMap.GetLength(0) / biomMap.GetLength(0))), Mathf.FloorToInt(y / (bigBiomMap.GetLength(1) / biomMap.GetLength(1)))] != 0)
                    bigBiomMap[x, y] = biomMap[Mathf.FloorToInt(x / (bigBiomMap.GetLength(0) / biomMap.GetLength(0))), Mathf.FloorToInt(y / (bigBiomMap.GetLength(1) / biomMap.GetLength(1)))];
            }
        for (int x = 0; x < bigBiomMap.GetLength(0); x++)
            for (int y = 0; y < bigBiomMap.GetLength(1); y++)
            {
                if (x + 1 < bigBiomMap.GetLength(0) && bigBiomMap[x, y] != bigBiomMap[x + 1, y])
                    if (Random.Range(1, 100) < 10) bigBiomMap[x, y] = bigBiomMap[x + 1, y];
                if (y + 1 < bigBiomMap.GetLength(1) && bigBiomMap[x, y] != bigBiomMap[x, y + 1])
                    if (Random.Range(1, 100) < 10) bigBiomMap[x, y] = bigBiomMap[x, y + 1];
                if (x - 1 > 0 && bigBiomMap[x - 1, y] != bigBiomMap[x - 1, y])
                    if (Random.Range(1, 100) < 10) bigBiomMap[x, y] = bigBiomMap[x + 1, y];
                if (y - 1 > 0 && bigBiomMap[x, y] != bigBiomMap[x, y - 1])
                    if (Random.Range(1, 100) < 10) bigBiomMap[x, y] = bigBiomMap[x, y - 1];
            }
        this.biomMap = bigBiomMap;
    }
    private void MapGenerator(int mapSize)
    {
        bool isThereAPlace = true;
        bool isRoomCreated = false;
        bool _roomInRoom;
        int tries = 0;
        List<HashSet<Vector2Int>> rightWalls = new List<HashSet<Vector2Int>>();
        List<HashSet<Vector2Int>> leftWalls = new List<HashSet<Vector2Int>>();
        List<HashSet<Vector2Int>> topWalls = new List<HashSet<Vector2Int>>();
        List<HashSet<Vector2Int>> bottomWalls = new List<HashSet<Vector2Int>>();
        HashSet<Vector2Int> rightEdges = new HashSet<Vector2Int>();
        HashSet<Vector2Int> leftEdges = new HashSet<Vector2Int>();
        HashSet<Vector2Int> topEdges = new HashSet<Vector2Int>();
        HashSet<Vector2Int> bottomEdges = new HashSet<Vector2Int>();
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        //Creating rooms
        for (int room = 0; room < mapSize / 2 && isThereAPlace; room++)
        {
            _roomInRoom = false;
            HashSet<Vector2Int> roomCoordinates = new HashSet<Vector2Int>();
            int[] roomSize = new int[2] { Random.Range(6, mapSize / 12), Random.Range(6, mapSize / 12) };
            int roomCordX = Random.Range(0, mapSize - roomSize[0]);
            int roomCordY = Random.Range(0, mapSize - roomSize[1]);

            if (tries > mapSize)
            {
                isThereAPlace = false;
                break;
            }
            for (int x = roomCordX; x < roomCordX + roomSize[0]; x++)
                for (int y = roomCordY; y < roomCordY + roomSize[1]; y++)
                {
                    if (!vacantPlaces.Contains(new Vector2Int(x, y)))
                    {
                        roomCoordinates.Add(new Vector2Int(x, y));
                    }
                    else
                    {
                        if (Random.Range(1, 100) < 4 && !_roomInRoom)
                            _roomInRoom = true;
                        else
                        {
                            roomCoordinates.Clear();
                            tries++;
                            break;
                        }
                        if (_roomInRoom)
                            continue;
                    }
                    if (x == roomCordX + roomSize[0] - 1 && y == roomCordY + roomSize[1] - 1)
                        isRoomCreated = true;
                }
            if (isRoomCreated)
            {
                for (int i = 0; i < roomCoordinates.Count; i++)
                    vacantPlaces.Add(roomCoordinates.ElementAt(i));
                isRoomCreated = false;
                tries = 0;
            }
        }
        for (int i = 0; i < vacantPlaces.Count; i++)
        {
            Room newRoom = Instantiate(room);
            newRoom.transform.position = new Vector2(vacantPlaces.ElementAt(i).x - dungeonSize / 2, vacantPlaces.ElementAt(i).y - dungeonSize / 2) * 3;
            dungeon[vacantPlaces.ElementAt(i).x, vacantPlaces.ElementAt(i).y] = newRoom;
        }
        //looking for horizontal walls
        for (int y = 0; y < mapSize; y++)
            for (int x = 0; x < mapSize; x++)
            {
                if (y < mapSize && dungeon[x, y] != null && dungeon[x, y + 1] == null)
                    bottomEdges.Add(new Vector2Int(x, y)); //нижние края
                else if (y > 0 && dungeon[x, y] != null && dungeon[x, y - 1] == null)
                    topEdges.Add(new Vector2Int(x, y)); //верхние края
                else
                {
                    HashSet<Vector2Int> bottom = new HashSet<Vector2Int>();
                    HashSet<Vector2Int> top = new HashSet<Vector2Int>();
                    for (int i = 0; i < bottomEdges.Count; i++)
                        bottom.Add(bottomEdges.ElementAt(i));
                    for (int i = 0; i < topEdges.Count; i++)
                        bottom.Add(topEdges.ElementAt(i));

                    if (bottomEdges.Count != 0)
                        bottomWalls.Add(bottom);
                    if (topEdges.Count != 0)
                        topWalls.Add(top);
                    bottomEdges.Clear();
                    topEdges.Clear();
                    continue;
                }
            }
        //looking for vertical walls
        for (int x = 0; x < mapSize; x++)
            for (int y = 0; y < mapSize; y++)
            {
                if (x > 0 && dungeon[x, y] != null && dungeon[x - 1, y] == null)
                    leftEdges.Add(new Vector2Int(x, y)); //левые края
                else if (x < mapSize && dungeon[x, y] != null && dungeon[x + 1, y] == null)
                    rightEdges.Add(new Vector2Int(x, y)); //правые края
                else
                {
                    HashSet<Vector2Int> left = new HashSet<Vector2Int>();
                    HashSet<Vector2Int> right = new HashSet<Vector2Int>();
                    for (int i = 0; i < leftEdges.Count; i++)
                        left.Add(leftEdges.ElementAt(i));
                    for (int i = 0; i < rightEdges.Count; i++)
                        right.Add(rightEdges.ElementAt(i));

                    if (leftEdges.Count != 0)
                        leftWalls.Add(left);
                    if (rightEdges.Count != 0)
                        rightWalls.Add(right);
                    leftEdges.Clear();
                    rightEdges.Clear();
                    continue;
                }
            }
        //creating straight canals
        HashSet<Vector2Int> paths = new HashSet<Vector2Int>();
        for (int side = 0; side < 4; side++)
        {
            HashSet<Vector2Int> path = new HashSet<Vector2Int>();
            List<HashSet<Vector2Int>> walls = new List<HashSet<Vector2Int>>();
            switch (side)
            {
                case 0:
                    walls = rightWalls;
                    break;
                case 1:
                    walls = bottomWalls;
                    break;
                case 2:
                    walls = leftWalls;
                    break;
                case 3:
                    walls = topWalls;
                    break;
            }
            for (int wall = 0; wall < walls.Count; wall++)
            {
                HashSet<Vector2Int> startWall = walls.ElementAt(Random.Range(0, walls.Count));
                for(int i =0;i<startWall.Count;i++)
                {
                    bool _end = false;
                    Vector2Int start = startWall.ElementAt(i);
                    switch (side)
                    {
                        case 0: //right
                            for (int x = 1; start.x + x < mapSize; x++)
                            {
                                if (dungeon[start.x + x, start.y] == null)
                                    path.Add(new Vector2Int(start.x + x, start.y));
                                else if (start.x + x == mapSize - 1)
                                {
                                    path.Clear();
                                    break;
                                }
                                else
                                {
                                    _end = true;
                                    break;
                                }
                            }
                            break;
                        case 1: //bottom
                            for (int y = 1; start.y + y < mapSize; y++)
                            {
                                if (dungeon[start.x, start.y + y] == null)
                                    path.Add(new Vector2Int(start.x, start.y + y));
                                else if (start.y + y == mapSize - 1)
                                {
                                    path.Clear();
                                    break;
                                }
                                else
                                {
                                    _end = true;
                                    break;
                                }
                            }
                            break;
                        case 2: //left
                            for (int x = 1; start.x - x > 0; x++)
                            {
                                if (dungeon[start.x - x, start.y] == null)
                                    path.Add(new Vector2Int(start.x - x, start.y));
                                else if (start.x - x == 0)
                                {
                                    path.Clear();
                                    break;
                                }
                                else
                                {
                                    _end = true;
                                    break;
                                }
                            }
                            break;
                        case 3: //top
                            for (int y = 1; start.y + y > 0; y++)
                            {
                                if (dungeon[start.x, start.y - y] == null)
                                    path.Add(new Vector2Int(start.x, start.y - y));
                                else if (start.y - y == 0)
                                {
                                    path.Clear();
                                    break;
                                }
                                else
                                {
                                    _end = true;
                                    break;
                                }
                            }
                            break;
                    }
                    if (_end)
                        break;
                }
                walls.Remove(startWall);
                for (int i = 0; i < path.Count; i++)
                    paths.Add(path.ElementAt(i));
            }
        }
        for (int i = 0; i < paths.Count; i++)
        {
            Room newRoom = Instantiate(room);
            newRoom.transform.position = new Vector2(paths.ElementAt(i).x - dungeonSize / 2, paths.ElementAt(i).y - dungeonSize / 2) * 3;
            dungeon[paths.ElementAt(i).x, paths.ElementAt(i).y] = newRoom;
        }

        //
    }
}

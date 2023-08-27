using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public Room[] roomPrefabs;
    public Room secretRoom;
    public Room treasureRoom;
    public Room startingRoom;
    int neighbours = 0;
    int[] treasureNeighbours;
    int dungeonSize;
    private Room[,] spawnedRooms;
    private void Start()
    {
        dungeonSize = 16;
        spawnedRooms = new Room[dungeonSize, dungeonSize];
        spawnedRooms[dungeonSize / 2, dungeonSize / 2] = startingRoom;

        for (int i = 0; i < dungeonSize; i++)
        {
            PlaceOneRoom();
        }
        CreateTheDoor();
        SecretRoom();
        TreasureRoom();
    }
    private void PlaceOneRoom()
    {
        int roomType = Random.Range(0, roomPrefabs.Length);
        string type = roomPrefabs[roomType].type;
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;
                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;
                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y > maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        Room newRoom = Instantiate(roomPrefabs[roomType]);
        Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
        newRoom.transform.position = new Vector2(position.x - dungeonSize / 2, position.y - dungeonSize / 2) * 16;
        spawnedRooms[position.x, position.y] = newRoom;
    }
    private void CreateTheDoor()
    {
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                int chance = 100;
                int voidChance = 25;
                List<Room> filledPlaces = new List<Room>();
                if (spawnedRooms[x, y] == null) continue;
                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;
                if (x > 0 && spawnedRooms[x - 1, y] != null)
                {
                    filledPlaces.Add(spawnedRooms[x - 1, y]);
                    if (/* spawnedRooms[x, y].doorCount > 2 || */ spawnedRooms[x - 1, y].doorCount > 2)
                        chance = 10;
                    if (Random.Range(0, 100) < chance)
                    {
                        Destroy(spawnedRooms[x, y].DoorL);
                        Destroy(filledPlaces[filledPlaces.Count - 1].DoorR);
                        spawnedRooms[x, y].doorCount++;
                        spawnedRooms[x - 1, y].doorCount++;
                        if (Random.Range(0, 100) < voidChance)
                        {
                            Destroy(spawnedRooms[x, y].WallL);
                            Destroy(filledPlaces[filledPlaces.Count - 1].WallR);
                        }
                    }
                }
                if (y > 0 && spawnedRooms[x, y - 1] != null)
                {
                    filledPlaces.Add(spawnedRooms[x, y - 1]);
                    if (/* spawnedRooms[x, y].doorCount > 2 || */ spawnedRooms[x, y - 1].doorCount > 2)
                        chance = 10;
                    if (Random.Range(0, 100) < chance)
                    {
                        Destroy(spawnedRooms[x, y].DoorD);
                        Destroy(filledPlaces[filledPlaces.Count - 1].DoorU);
                        spawnedRooms[x, y].doorCount++;
                        spawnedRooms[x, y - 1].doorCount++;
                        if (Random.Range(0, 100) < voidChance)
                        {
                            Destroy(spawnedRooms[x, y].WallD);
                            Destroy(filledPlaces[filledPlaces.Count - 1].WallU);
                        }
                    }

                }
                if (x < maxX && spawnedRooms[x + 1, y] != null)
                {
                    filledPlaces.Add(spawnedRooms[x + 1, y]);
                    if (/* spawnedRooms[x, y].doorCount > 2 || */ spawnedRooms[x + 1, y].doorCount > 2)
                        chance = 10;
                    if (Random.Range(0, 100) < chance)
                    {
                        Destroy(spawnedRooms[x, y].DoorR);
                        Destroy(filledPlaces[filledPlaces.Count - 1].DoorL);
                        spawnedRooms[x, y].doorCount++;
                        spawnedRooms[x + 1, y].doorCount++;
                        if (Random.Range(0, 100) < voidChance)
                        {
                            Destroy(spawnedRooms[x, y].WallR);
                            Destroy(filledPlaces[filledPlaces.Count - 1].WallL);
                        }
                    }

                }
                if (y > maxY && spawnedRooms[x, y + 1] != null)
                {
                    filledPlaces.Add(spawnedRooms[x, y + 1]);
                    if (/* spawnedRooms[x, y].doorCount > 2 || */ spawnedRooms[x, y + 1].doorCount > 2)
                        chance = 10;
                    if (Random.Range(0, 100) < chance)
                    {
                        Destroy(spawnedRooms[x, y].DoorU);
                        Destroy(filledPlaces[filledPlaces.Count - 1].DoorD);
                        spawnedRooms[x, y].doorCount++;
                        spawnedRooms[x, y + 1].doorCount++;
                        if (Random.Range(0, 100) < voidChance)
                        {
                            Destroy(spawnedRooms[x, y].WallU);
                            Destroy(filledPlaces[filledPlaces.Count - 1].WallD);
                        }
                    }
                }
            }
    }
    void SecretRoom()
    {
        HashSet<Vector2Int> potentianSecretRoomPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                neighbours = 0;
                if (spawnedRooms[x, y] != null) continue;
                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;
                if (x > 0 && spawnedRooms[x - 1, y] != null)
                {
                    neighbours++;
                }
                if (y > 0 && spawnedRooms[x, y - 1] != null)
                {
                    neighbours++;
                }
                if (x < maxX && spawnedRooms[x + 1, y] != null)
                {
                    neighbours++;

                }
                if (y > maxY && spawnedRooms[x, y + 1] != null)
                {
                    neighbours++;
                }
                if (neighbours == 2)
                    potentianSecretRoomPlaces.Add(new Vector2Int(x, y));
            }
        Vector2Int position = potentianSecretRoomPlaces.ElementAt(Random.Range(0, potentianSecretRoomPlaces.Count));
        if (position != new Vector2(0, 0))
        {
            Room newRoom = Instantiate(secretRoom);
            newRoom.transform.position = new Vector2(position.x - dungeonSize / 2, position.y - dungeonSize / 2) * 16;
            spawnedRooms[position.x, position.y] = newRoom;
        }
    }
    void TreasureRoom()
    {
        HashSet<Vector2Int> potentianTreasureRoomPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                neighbours = 0;
                if (spawnedRooms[x, y] != null) continue;
                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;
                if (x > 0 && spawnedRooms[x - 1, y] != null)
                    neighbours++;
                if (y > 0 && spawnedRooms[x, y - 1] != null)
                    neighbours++;
                if (x < maxX && spawnedRooms[x + 1, y] != null)
                    neighbours++;
                if (y > maxY && spawnedRooms[x, y + 1] != null)
                    neighbours++;
                if (neighbours == 1)
                    potentianTreasureRoomPlaces.Add(new Vector2Int(x, y));
            }
        int choosenPlace = Random.Range(0, potentianTreasureRoomPlaces.Count);
        Vector2Int position = potentianTreasureRoomPlaces.ElementAt(choosenPlace);
        Room newRoom = Instantiate(treasureRoom);
        newRoom.transform.position = new Vector2(position.x - dungeonSize / 2, position.y - dungeonSize / 2) * 16;
        spawnedRooms[position.x, position.y] = newRoom;
    }
}

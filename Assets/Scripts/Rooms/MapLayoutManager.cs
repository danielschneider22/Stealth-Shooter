using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayoutManager : MonoBehaviour
{
    public RoomManager initRoomManager;
    public List<GameObject> mediumRoomPrefabs;
    public List<GameObject> largeRoomPrefabs;
    public List<GameObject> treasureRoomPrefabs;
    public List<GameObject> merchantRoomPrefabs;
    public GameObject exitRoom;
    public int numRoomsToGenerate;

    private GameObject newRoom;
    private List<RoomManager> pathFromEntranceToExit;
    private List<RoomManager> pathFromEntranceToExitSiblings;
    private List<RoomManager> allRooms;
    private void Start()
    {
        allRooms = new List<RoomManager>() { initRoomManager };
        CreatePathFromStartToExit();
        CreateSubBranches();
        CreateTreasureRooms();
    }

    private void CreatePathFromStartToExit()
    {
        pathFromEntranceToExit = new List<RoomManager>() { initRoomManager };
        RoomManager currRoom = initRoomManager;
        List<int> badDirections = new List<int>();
        for (var i = 0; i < numRoomsToGenerate; i++)
        {
            bool foundGoodDirection = false;
            newRoom = i == numRoomsToGenerate - 1 ? Instantiate(exitRoom) : Instantiate(GetRandomRoom());
            RoomManager newRoomManager = newRoom.GetComponent<RoomManager>();
            while (!foundGoodDirection)
            {
                int direction = Random.Range(0, 5);
                while (badDirections.Contains(direction))
                {
                    direction = Random.Range(0, 5);
                }
                PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                if (!DoesNewRoomOverlapWithAnyExisting(newRoomManager))
                {
                    pathFromEntranceToExit.Add(newRoomManager);
                    allRooms.Add(newRoomManager);
                    UnhideDoor(direction, currRoom);
                    currRoom = newRoomManager;
                    foundGoodDirection = true;
                    badDirections = new List<int>();
                    badDirections.Add(GetOppositeDirection(direction));
                }
                else
                {
                    newRoomManager.InitializeDoors();
                    badDirections.Add(direction);
                }
                if (badDirections.Count == 4)
                {
                    Destroy(newRoomManager);
                    Destroy(newRoom);
                    badDirections = new List<int>();
                    foundGoodDirection = true;
                    currRoom = initRoomManager;
                }
            }
        }
    }

    private GameObject GetRandomMediumRoom()
    {
        int randomRoom = Random.Range(0, mediumRoomPrefabs.Count);
        return mediumRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomTreasureRoom()
    {
        int randomRoom = Random.Range(0, treasureRoomPrefabs.Count);
        return treasureRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomLargeRoom()
    {
        int randomRoom = Random.Range(0, largeRoomPrefabs.Count);
        return largeRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomMerchantRoom()
    {
        int randomRoom = Random.Range(0, merchantRoomPrefabs.Count);
        return merchantRoomPrefabs[randomRoom];
    }

    private GameObject GetRandomRoom()
    {
        int roomType = Random.Range(0, 4);
        switch(roomType)
        {
            case (0):
            case (1):
                return GetRandomMediumRoom();
            case (2):
                return GetRandomLargeRoom();
        }
        return GetRandomMediumRoom();
    }

    private void CreateSubBranches()
    {
        List<int> badDirections = new List<int>();
        pathFromEntranceToExitSiblings = new List<RoomManager>();
        List<RoomManager> subList = pathFromEntranceToExit.GetRange(1, pathFromEntranceToExit.Count - 2);
        foreach (RoomManager currRoom in subList)
        {
            bool foundGoodDirection = false;
            newRoom = Instantiate(GetRandomRoom());
            RoomManager newRoomManager = newRoom.GetComponent<RoomManager>();
            while (!foundGoodDirection)
            {
                int direction = Random.Range(0, 4);
                while (badDirections.Contains(direction))
                {
                    direction = Random.Range(0, 4);
                }
                
                PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                if (!DoesNewRoomOverlapWithAnyExisting(newRoomManager))
                {
                    allRooms.Add(newRoomManager);
                    foundGoodDirection = true;
                    badDirections = new List<int>();
                    pathFromEntranceToExitSiblings.Add(newRoomManager);
                    badDirections.Add(GetOppositeDirection(direction));
                    UnhideDoor(direction, currRoom);
                }
                else
                {
                    newRoomManager.InitializeDoors();
                    badDirections.Add(direction);
                }
                if (badDirections.Count == 4)
                {
                    Destroy(newRoomManager);
                    Destroy(newRoom);
                    badDirections = new List<int>();
                    foundGoodDirection = true;
                }
            }
        }
    }

    private void CreateTreasureRooms()
    {
        int numTreasureRoomsCreated = 0;
        List<int> badDirections = new List<int>();
        List<RoomManager> subList = pathFromEntranceToExitSiblings.GetRange(0, pathFromEntranceToExitSiblings.Count - 1);
        List<int> roomsLinkedToTreasure = new List<int>();
        for (var i = 0; i <= 4; i=i+1)
        {
            if(roomsLinkedToTreasure.Count < subList.Count)
            {
                bool foundGoodDirection = false;
                newRoom = Instantiate(GetRandomTreasureRoom());
                RoomManager newRoomManager = newRoom.GetComponent<RoomManager>();
                while (!foundGoodDirection)
                {
                    int roomId = Random.Range(0, subList.Count);
                    while (roomsLinkedToTreasure.Contains(roomId))
                    {
                        roomId = Random.Range(0, subList.Count);
                    }
                    RoomManager currRoom = subList[roomId];
                    int direction = Random.Range(0, 4);
                    while (badDirections.Contains(direction))
                    {
                        direction = Random.Range(0, 4);
                    }

                    PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                    if (!DoesNewRoomOverlapWithAnyExisting(newRoomManager))
                    {
                        allRooms.Add(newRoomManager);
                        foundGoodDirection = true;
                        badDirections = new List<int>();
                        UnhideDoor(direction, currRoom);
                        roomsLinkedToTreasure.Add(roomId);
                        numTreasureRoomsCreated++;
                    }
                    else
                    {
                        newRoomManager.InitializeDoors();
                        badDirections.Add(direction);
                    }
                    if (badDirections.Count == 4)
                    {
                        Destroy(newRoomManager);
                        Destroy(newRoom);
                        badDirections = new List<int>();
                        foundGoodDirection = true;
                    }
                }
            }
            
        }

        if (numTreasureRoomsCreated < 2)
        {
            for (var i = allRooms.Count - 2; i > 0; i--)
            {
                RoomManager currRoom = allRooms[i];
                bool foundGoodDirection = false;
                newRoom = Instantiate(GetRandomTreasureRoom());
                RoomManager newRoomManager = newRoom.GetComponent<RoomManager>();
                while (!foundGoodDirection)
                {
                    int direction = Random.Range(0, 4);
                    while (badDirections.Contains(direction))
                    {
                        direction = Random.Range(0, 4);
                    }
                    
                    PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                    if (!DoesNewRoomOverlapWithAnyExisting(newRoomManager))
                    {
                        allRooms.Add(newRoomManager);
                        foundGoodDirection = true;
                        badDirections = new List<int>();
                        UnhideDoor(direction, currRoom);
                        numTreasureRoomsCreated++;
                        if(numTreasureRoomsCreated == 2)
                        {
                            i = 0;
                        }
                    }
                    else
                    {
                        newRoomManager.InitializeDoors();
                        badDirections.Add(direction);
                    }
                    if (badDirections.Count == 4)
                    {
                        Destroy(newRoomManager);
                        Destroy(newRoom);
                        badDirections = new List<int>();
                        foundGoodDirection = true;
                    }
                }
            }
        }
    }

    private void PositionRoomInCorrectDirection(int direction, RoomManager newRoomManager, RoomManager currRoom)
    {
        float diffX, diffY = 0;
        switch (direction)
        {
            // Left
            case (0):
                diffX = newRoomManager.RightDoor.transform.position.x - currRoom.LeftDoor.transform.position.x;
                diffY = newRoomManager.RightDoor.transform.position.y - currRoom.LeftDoor.transform.position.y;
                newRoom.transform.position = new Vector3(newRoom.transform.position.x - diffX, newRoom.transform.position.y - diffY, 0);
                newRoomManager.RightDoor.SetActive(false);
                newRoomManager.RightRoom = currRoom;
                break;
            // Right
            case (1):
                diffX = newRoomManager.LeftDoor.transform.position.x - currRoom.RightDoor.transform.position.x;
                diffY = newRoomManager.LeftDoor.transform.position.y - currRoom.RightDoor.transform.position.y;
                newRoom.transform.position = new Vector3(newRoom.transform.position.x - diffX, newRoom.transform.position.y - diffY, 0);
                newRoomManager.LeftDoor.SetActive(false);
                newRoomManager.LeftRoom = currRoom;
                break;
            // Bottom
            case (2):
                diffX = newRoomManager.TopDoor.transform.position.x - currRoom.BottomDoor.transform.position.x;
                diffY = newRoomManager.TopDoor.transform.position.y - currRoom.BottomDoor.transform.position.y;
                newRoom.transform.position = new Vector3(newRoom.transform.position.x - diffX, newRoom.transform.position.y - diffY, 0);
                newRoomManager.TopDoor.SetActive(false);
                newRoomManager.TopRoom = currRoom;
                break;
            // Top
            case (3):
                diffX = newRoomManager.BottomDoor.transform.position.x - currRoom.TopDoor.transform.position.x;
                diffY = newRoomManager.BottomDoor.transform.position.y - currRoom.TopDoor.transform.position.y;
                newRoom.transform.position = new Vector3(newRoom.transform.position.x - diffX, newRoom.transform.position.y - diffY, 0);
                newRoomManager.BottomDoor.SetActive(false);
                newRoomManager.BottomRoom = currRoom;
                break;
        }
    }

    public int GetOppositeDirection(int direction)
    {
        switch (direction)
        {
            case (0):
            {
                return 1;
            }
            case(1):
            {
                return 0;
            }
            case (2):
            {
                return 3;
            }
            case(3):
            {
                return 2;
            }
        }
        return 0;
    }

    public void UnhideDoor(int direction, RoomManager currRoom)
    {
        switch (direction)
        {
            // Left
            case (0):
                currRoom.LeftDoor.GetComponent<HideDoor>().UnhideDoor();
                break;
            // Right
            case (1):
                currRoom.RightDoor.GetComponent<HideDoor>().UnhideDoor();
                break;
            // Bottom
            case (2):
                currRoom.BottomDoor.GetComponent<HideDoor>().UnhideDoor();
                break;
            // Top
            case (3):
                currRoom.TopDoor.GetComponent<HideDoor>().UnhideDoor();
                break;
        }
    }

    public bool DoesNewRoomOverlapWithAnyExisting(RoomManager newRoomManager)
    {
        foreach(RoomManager roomManager in allRooms)
        {
            if(RoomsOverlap(roomManager, newRoomManager))
            {
                return true;
            }
        }
        return false;
    }

    // https://silentmatt.com/rectangle-intersection/
    public static bool RoomsOverlap(RoomManager room1, RoomManager room2)
    {
        float room1X1 = room1.LeftDoor.transform.position.x;
        float room2X1 = room2.LeftDoor.transform.position.x;
        float room1Y1 = room1.TopDoor.transform.position.y;
        float room2Y1 = room2.TopDoor.transform.position.y;
        float room1X2 = room1.RightDoor.transform.position.x;
        float room2X2 = room2.RightDoor.transform.position.x;
        float room1Y2 = room1.BottomDoor.transform.position.y;
        float room2Y2 = room2.BottomDoor.transform.position.y;

        bool test1 = room1X1 < room2X2 && Mathf.Abs(room1X1 - room2X2) > 2;
        bool test2 = room1X2 > room2X1 && Mathf.Abs(room1X2 - room2X1) > 2;
        bool test3 = room1Y1 > room2Y2 && Mathf.Abs(room1Y1 - room2Y2) > 2;
        bool test4 = room1Y2 < room2Y1 && Mathf.Abs(room1Y2 - room2Y1) > 2;

        return test1 && test2 && test3 && test4;
    }
}

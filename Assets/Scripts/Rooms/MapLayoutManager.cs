using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayoutManager : MonoBehaviour
{
    public RoomManager initRoomManager;
    public GameObject mediumRoomPrefab;
    public int numRoomsToGenerate;

    private GameObject newRoom;
    private List<RoomManager> pathFromEntranceToExit;
    private List<RoomManager> pathFromEntranceToExitSiblings;
    private void Start()
    {
        CreatePathFromStartToExit();
    }

    private void CreatePathFromStartToExit()
    {
        pathFromEntranceToExit = new List<RoomManager>() { initRoomManager };
        RoomManager currRoom = initRoomManager;
        List<int> badDirections = new List<int>();
        for (var i = 0; i < numRoomsToGenerate; i++)
        {
            bool foundGoodDirection = false;
            while (!foundGoodDirection)
            {
                int direction = Random.Range(0, 5);
                while (badDirections.Contains(direction))
                {
                    direction = Random.Range(0, 5);
                }
                newRoom = Instantiate(mediumRoomPrefab);
                RoomManager newRoomManager = newRoom.GetComponent<RoomManager>();
                PositionRoomInCorrectDirection(direction, newRoomManager, currRoom);
                if (!DoesNewRoomOverlapWithAnyExisting(newRoomManager))
                {
                    pathFromEntranceToExit.Add(newRoomManager);
                    currRoom = newRoomManager;
                    foundGoodDirection = true;
                    badDirections = new List<int>();
                    badDirections.Add(GetOppositeDirection(direction));
                    if (i == numRoomsToGenerate - 1)
                    {
                        newRoomManager.IsExit = true;
                    }
                }
                else
                {
                    Destroy(newRoomManager);
                    Destroy(newRoom);
                    badDirections.Add(direction);
                }
                if (badDirections.Count == 4)
                {
                    badDirections = new List<int>();
                    foundGoodDirection = true;
                    currRoom = initRoomManager;
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

    public bool DoesNewRoomOverlapWithAnyExisting(RoomManager newRoomManager)
    {
        foreach(RoomManager roomManager in pathFromEntranceToExit)
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

        return room1X1 < room2X2 && room1X2 > room2X1 && room1Y1 > room2Y2 && room1Y2 < room2Y1;
    }
}

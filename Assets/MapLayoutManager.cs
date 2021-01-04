using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayoutManager : MonoBehaviour
{
    public RoomManager initRoomManager;
    public GameObject mediumRoomPrefab;

    private GameObject newRoom;
    private void Start()
    {
        newRoom = Instantiate(mediumRoomPrefab);
        RoomManager newRoomManager = newRoom.GetComponent<RoomManager>();
        float diffX = newRoomManager.RightDoor.transform.position.x - initRoomManager.LeftDoor.transform.position.x;
        float diffY = newRoomManager.RightDoor.transform.position.y - initRoomManager.LeftDoor.transform.position.y;
        newRoom.transform.position = new Vector3(newRoom.transform.position.x - diffX, newRoom.transform.position.y - diffY, 0);
        newRoomManager.RightDoor.SetActive(false);

        GameObject newRoom2 = Instantiate(mediumRoomPrefab);
        RoomManager newRoomManager2 = newRoom2.GetComponent<RoomManager>();
        diffX = newRoomManager2.TopDoor.transform.position.x - initRoomManager.BottomDoor.transform.position.x;
        diffY = newRoomManager2.TopDoor.transform.position.y - initRoomManager.BottomDoor.transform.position.y;
        newRoom2.transform.position = new Vector3(newRoom2.transform.position.x - diffX, newRoom2.transform.position.y - diffY, 0);
        newRoomManager2.BottomDoor.SetActive(false);
    }
}

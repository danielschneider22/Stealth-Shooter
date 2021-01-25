using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject BottomDoor;
    public GameObject TopDoor;
    public RoomManager LeftRoom;
    public RoomManager RightRoom;
    public RoomManager TopRoom;
    public RoomManager BottomRoom;
    public bool IsEntrance;
    public bool IsExit;

    private void Awake()
    {
        InitializeDoors();
    }

    public void InitializeDoors()
    {
        LeftDoor.gameObject.SetActive(true);
        RightDoor.gameObject.SetActive(true);
        BottomDoor.gameObject.SetActive(true);
        TopDoor.gameObject.SetActive(true);
        LeftDoor.GetComponent<HideDoor>().MakeDoorHidden();
        RightDoor.GetComponent<HideDoor>().MakeDoorHidden();
        BottomDoor.GetComponent<HideDoor>().MakeDoorHidden();
        TopDoor.GetComponent<HideDoor>().MakeDoorHidden();
    }
}

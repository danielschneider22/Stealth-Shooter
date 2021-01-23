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
}

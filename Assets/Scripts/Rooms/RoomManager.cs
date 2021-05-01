using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.NetworkVariable;
using MLAPI.Messaging;

public class RoomManager : NetworkBehaviour // TODO: allenwhitedev not sure if this needs to be NetworkBehavior or can remain MonoBehavior on a GameObject with a NetworkObject component
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
    public GameObject enemies;
    public GameObject lights;
    public GameObject background;
    private NetworkVariableBool AreDoorsInitialized = new NetworkVariableBool(false);

    SpriteRenderer m_Renderer;
    // Use this for initialization
    void Start()
    {
        m_Renderer = background.GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        InitializeDoors(true, true);
    }
    void Update()
    {
        if (m_Renderer.isVisible)
        {
            enemies.SetActive(true);
            lights.SetActive(true);
        }
        else if (!m_Renderer.isVisible)
        {
            enemies.SetActive(false);
            lights.SetActive(false);
        }
    }

    public void InitializeDoors(bool val, bool nextVal)
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

    // - network variable sync
    private void OnEnable() // start listening for door initialization
    {
        AreDoorsInitialized.OnValueChanged += InitializeDoors;
    }
    private void OnDisable() // stop listening for the door initialization
    {
        AreDoorsInitialized.OnValueChanged -= InitializeDoors;
    }

    // - network server RPCs
    [ServerRpc] public void InitializeDoorsServerRpc() { InitializeDoors(true, true); }

}

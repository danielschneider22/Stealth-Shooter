using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
public class HideDoor : NetworkBehaviour
{
    public Animator animator;
    public SpriteRenderer top;
    public SpriteRenderer bottom;
    private Color doorColor;
    private NetworkVariableBool IsDoorHidden = new NetworkVariableBool(true);
    private NetworkVariableBool IsActive = new NetworkVariableBool(true);

    public void Awake()
    {
        doorColor = new Color(0.000f, 0.726f, 0.712f, 1.000f);
    }
    public void MakeDoorHidden()
    {
        animator.enabled = false;
        top.color = Color.white;
        bottom.color = Color.white;

    }

    public void UnhideDoor()
    {
        animator.enabled = true;
        top.color = doorColor;
        bottom.color = doorColor;

    }

    // - network variable sync
    private void OnEnable() // start listening for door initialization, or active toggle
    {
        IsDoorHidden.OnValueChanged += HideOrShowDoor;
        IsActive.OnValueChanged += SetActive;
    }
    private void onDisable() // sto listening for door intiialization, or active toggle
    {
        IsDoorHidden.OnValueChanged -= HideOrShowDoor;
        IsActive.OnValueChanged -= SetActive;
    }
    private void HideOrShowDoor(bool val, bool nextVal)
    {
        if (val == false && nextVal == true)
            MakeDoorHidden();
        else if (val == true && nextVal == false)
            UnhideDoor();
    }
    private void SetActive(bool val, bool nextVal) 
    {
        gameObject.SetActive(nextVal);
    }

    // - network RPCs
    [ServerRpc] public void UnhideDoorServerRpc() { IsDoorHidden.Value = false; }
    [ServerRpc] public void MakeDoorHiddenServerRpc() { IsDoorHidden.Value = true; }
    [ServerRpc] public void SetActiveServerRpc(bool isActive) { IsDoorHidden.Value = isActive; }
}

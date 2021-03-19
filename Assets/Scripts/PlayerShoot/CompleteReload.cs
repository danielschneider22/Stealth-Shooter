using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CompleteReload : NetworkBehaviour
{
    private PlayerGeneralShootController playerShootController;
    private void Awake()
    {
        playerShootController = transform.parent.GetComponent<PlayerGeneralShootController>();
    }
    [Client]
    public void CompleteReloadMethod()
    {
        playerShootController.CompleteReload();
    }
}

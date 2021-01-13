using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteReload : MonoBehaviour
{
    private PlayerGeneralShootController playerShootController;
    private void Awake()
    {
        playerShootController = transform.parent.GetComponent<PlayerGeneralShootController>();
    }
    public void CompleteReloadMethod()
    {
        playerShootController.CompleteReload();
    }
}

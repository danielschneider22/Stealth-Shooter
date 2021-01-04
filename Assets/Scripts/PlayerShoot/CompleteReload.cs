using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteReload : MonoBehaviour
{
    private PlayerShootController playerShootController;
    private void Awake()
    {
        playerShootController = transform.parent.GetComponent<PlayerShootController>();
    }
    public void CompleteReloadMethod()
    {
        playerShootController.CompleteReload();
    }
}

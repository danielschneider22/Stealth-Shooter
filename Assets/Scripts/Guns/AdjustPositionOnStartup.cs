using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPositionOnStartup : MonoBehaviour
{
    public Transform popupPosition;
    public bool adjustToCenter;
    void Start()
    {
        if(popupPosition != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(popupPosition.position);
        } else if (adjustToCenter)
        {
            transform.position = new Vector3(transform.parent.position.x * -1, transform.parent.position.y * -1);
        }
        
    }

}

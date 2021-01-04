using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPositionOnStartup : MonoBehaviour
{
    public Transform popupPosition;
    void Start()
    {
        transform.position = Camera.main.WorldToScreenPoint(popupPosition.position);
    }

}

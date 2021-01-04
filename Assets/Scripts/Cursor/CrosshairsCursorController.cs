using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairsCursorController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        /* if(Cursor.visible)
        {
            Cursor.visible = false;
        }
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);*/

        // Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // transform.position = Vector3.Lerp(transform.position, new Vector3(newPos.x, newPos.y, 0), 5f * Time.deltaTime);
    }
}

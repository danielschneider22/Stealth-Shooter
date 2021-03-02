using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayClick : MonoBehaviour
{
	public Text text;

	public void Play() { // exit main menu into game scene
		Debug.Log("Debug: Play"); 
		Color orangeFromMuzzle = new Color32(253,175,71,255);
		text.color = orangeFromMuzzle;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

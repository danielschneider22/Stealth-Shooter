using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onPlayButtonClick : MonoBehaviour
{
	public Text myText;

	// Start is called before the first frame update
	void Start() {}

	public void Play() { // play game
		myText.text = "Playing";
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) {
			myText.text = "Wowee";
		}
	}
}

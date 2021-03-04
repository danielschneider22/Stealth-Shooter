using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayClick : MonoBehaviour
{
	public TextMeshProUGUI text;

	public void Play() { // exit main menu into game scene
		Debug.Log("Debug: Play"); 
		Color orangeFromMuzzle = new Color32(253,175,71,255);
		text.color = orangeFromMuzzle;
		SceneManager.LoadSceneAsync("Scene 1");
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

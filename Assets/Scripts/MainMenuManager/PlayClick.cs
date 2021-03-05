using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayClick : MonoBehaviour
{
	public TextMeshProUGUI text;
	public Image muzzleFlash;

	public void Play() { // exit main menu into game scene
		Color orangeFromMuzzle = new Color32(253,175,71,255);
		text.color = orangeFromMuzzle;
		SceneManager.LoadSceneAsync("Scene 1");
		// muzzleFlash.enabled = true;
	}
    // Start is called before the first frame update
    void Start()
    {
       // muzzleFlash.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

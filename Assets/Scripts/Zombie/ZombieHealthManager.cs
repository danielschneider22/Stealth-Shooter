using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthManager : MonoBehaviour
{
    public int health = 40;
    private int currHealth;
    public GameObject bloodPoolEffect;
    public GameObject bodyPartsEffect;
    public GameObject healthBarPrefab;
    private Image healthBar;
    private GameObject healthBarObj;

    private AudioManager audioManager; 
	public void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        GameObject healthBarCanvas = GameObject.FindGameObjectWithTag("HealthBarCanvas");
        healthBarObj = Instantiate(healthBarPrefab);
        healthBar = healthBarObj.transform.GetChild(1).GetComponent<Image>();
        healthBarObj.transform.SetParent(healthBarCanvas.transform);
        healthBarObj.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        currHealth = health;
    }
    // Start is called before the first frame update
    public void HealthChange(int changeAmount)
    {
        currHealth = currHealth + changeAmount;
        if (currHealth <= 0) { // die
            GameObject bloodPool = Instantiate(bloodPoolEffect);
            bloodPool.transform.position = transform.position;
            GameObject bodyParts = Instantiate(bodyPartsEffect);
            bodyParts.transform.position = transform.position;
			audioManager.Play("ZombieDie", 0); // TODO: calculate distance from player instead of passing 0
		    audioManager.Stop("ZombieIdle"); // @danielschneider22: don't know how multiple idle zombie sounds will be handled with Brackeys AudioManager	
            Destroy(gameObject);
        }
		else {
			audioManager.Play("ZombieHurt", 0);
		}
        healthBar.fillAmount = ((float)currHealth / (float)health);
    }
    public void Update()
    {
        healthBarObj.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        healthBarObj.transform.position = new Vector3(healthBarObj.transform.position.x, healthBarObj.transform.position.y + 40f, healthBarObj.transform.position.z);
    }
    public void OnDisable()
    {
        if(healthBarObj != null)
        {
            healthBarObj.SetActive(false);
        }
    }
    public void OnEnable()
    {
        healthBarObj.SetActive(true);
    }
    public void OnDestroy()
    {
        Destroy(healthBarObj);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthManager : MonoBehaviour
{
    public int health = 40;
    private int currHealth;
    public GameObject bloodPoolEffect;
    public GameObject bodyPartsEffect;
    public GameObject coinDropEffect;
    public GameObject healthBarPrefab;
    public GameObject ammoDropEffect;
    private Image healthBar;
    private GameObject healthBarObj;
    private GameObject damageTextCanvas;
    private Transform player;

    private AudioManager audioManager; 
	public void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        GameObject healthBarCanvas = GameObject.FindGameObjectWithTag("HealthBarCanvas");
        damageTextCanvas = GameObject.FindGameObjectWithTag("DamageTextCanvas");
        healthBarObj = Instantiate(healthBarPrefab);
        healthBar = healthBarObj.transform.GetChild(1).GetComponent<Image>();
        healthBarObj.transform.SetParent(healthBarCanvas.transform);
        healthBarObj.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        currHealth = health;
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            GameObject coinDrop = Instantiate(coinDropEffect);
            coinDrop.GetComponent<ParticleAttractor>()._attractorTransform = player;
            ParticleSystem.MainModule coinEffectMain = coinDrop.GetComponent<ParticleSystem>().main;
            coinEffectMain.maxParticles = GetCoinDropAmount();
            coinDrop.transform.position = transform.position;
			audioManager.Play("ZombieDie", 0); // TODO: calculate distance from player instead of passing 0
		    audioManager.Stop("ZombieIdle"); // @danielschneider22: don't know how multiple idle zombie sounds will be handled with Brackeys AudioManager
            
            if(gameObject.name.Contains("Soldier"))
            {
                GameObject ammoDrop = Instantiate(ammoDropEffect);
                ammoDrop.GetComponent<ParticleAttractor>()._attractorTransform = player;
                ammoDrop.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
		else {
			audioManager.Play("ZombieHurt", 0);
		}
        healthBar.fillAmount = ((float)currHealth / (float)health);
        for(var i = 0; i < damageTextCanvas.transform.childCount; i++ )
        {
            GameObject child = damageTextCanvas.transform.GetChild(i).gameObject;
            if (!damageTextCanvas.transform.GetChild(i).gameObject.activeSelf)
            {
                child.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y + 20f, child.transform.position.z);
                child.SetActive(true);
                child.GetComponent<TextMeshProUGUI>().text = (changeAmount * -1).ToString();
                break;
            }
        }
    }

    public int GetCoinDropAmount()
    {
        if(gameObject.name.Contains("Zombie"))
        {
            return Random.Range(3, 8);
        } else if(gameObject.name.Contains("Soldier"))
        {
            return Random.Range(20, 30);
        }
        return 0;
    }
    public void FixedUpdate()
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthManager : MonoBehaviour
{
    public int health = 40;
    public GameObject bloodPoolEffect;
    public GameObject bodyPartsEffect;
		private AudioManager audioManager; 
		public void Awake() {audioManager = FindObjectOfType<AudioManager>();}
    // Start is called before the first frame update
    public void HealthChange(int changeAmount)
    {
        health = health + changeAmount;
        if (health <= 0) { // die
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateManager : MonoBehaviour
{
    public enum ZombieState
    {
        Idle,
        Pacing,
        FollowingPlayer
    }
    public ZombieState zombieState;
    public HitManager hitManager;
    public GameObject bloodEffect;
    public GameObject exclamationPoint;
    public Animator animator;

    private ZombieHealthManager healthManager;
    private MoveTowardsPlayer moveTowardsPlayer;
    private Rigidbody2D rigidBody;
		private AudioManager audioManager;
	
    public void Awake()
    {
        if(zombieState == ZombieState.Pacing)
        {
            GetComponent<PaceInCircle>().enabled = true;
        }
        hitManager.OnHitWithBullet += ZombieHitWithBullet;
        healthManager = GetComponent<ZombieHealthManager>();
        moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
        rigidBody = transform.GetComponent<Rigidbody2D>();
				audioManager = FindObjectOfType<AudioManager>();
    }

    public void StartFollowingPlayer()
    {
        GetComponent<MoveTowardsPlayer>().animator.SetTrigger("StartWalking");
        GetComponent<MoveTowardsPlayer>().enabled = true;
        Destroy(GetComponent<ZombieFieldOfViewControl>().fieldOfView.gameObject);
        zombieState = ZombieState.FollowingPlayer;
        GetComponent<PaceInCircle>().enabled = false;
				audioManager.Play("ZombieIdle", 0, true); // loop
    }

    public void ZombieHitWithBullet(GameObject bullet)
    {
        if (!bullet.name.Contains("Enemy")) // zombie is immune to non-player bullets
        {
            var bloodAngle = new Vector3(180f - bullet.transform.eulerAngles.z, 90f, 90f);
            GameObject impactEffectObj = Instantiate(bloodEffect);
            impactEffectObj.transform.position = transform.position;
            impactEffectObj.transform.eulerAngles = bloodAngle;

            moveTowardsPlayer.hitStunTimer = .2f;
            rigidBody.velocity = new Vector2();
            rigidBody.AddForce(bullet.gameObject.transform.right * 400f, ForceMode2D.Impulse);

            animator.SetTrigger("Hit");

            healthManager.HealthChange(Random.Range(6, 10) * -1);

            if (zombieState != ZombieState.FollowingPlayer)
            {
                StartFollowingPlayer();
                GameObject pointObj = Instantiate(exclamationPoint);
                pointObj.GetComponent<FollowCharacter>().characterToFollow = transform;
                pointObj.GetComponent<FollowCharacter>().yOffsetFromChar = 3f;
            }
            Destroy(bullet);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldierStateManager : MonoBehaviour
{
    public enum EnemySoldierState
    {
        Idle,
        Pacing,
        FollowingPlayer
    }
    public EnemySoldierState soldierState;
    public HitManager hitManager;
    public GameObject bloodEffect;
    public GameObject exclamationPoint;
    public Animator animator;

    private ZombieHealthManager healthManager;
    private MoveTowardsPlayer moveTowardsPlayer;
    private Rigidbody2D rigidBody;

    public void Awake()
    {
        if (soldierState == EnemySoldierState.Pacing)
        {
            GetComponent<PaceInCircle>().enabled = true;
        }
        hitManager.OnHitWithBullet += EnemySoldierHitWithBullet;
        healthManager = GetComponent<ZombieHealthManager>();
        moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
        rigidBody = transform.GetComponent<Rigidbody2D>();
    }

    public void StartFollowingPlayer()
    {
        GetComponent<MoveTowardsPlayer>().animator.SetTrigger("StartWalking");
        GetComponent<MoveTowardsPlayer>().enabled = true;
        soldierState = EnemySoldierState.FollowingPlayer;
        GetComponent<PaceInCircle>().enabled = false;
        GetComponent<EnemySoldierShootController>().enabled = true;
    }

    public void EnemySoldierHitWithBullet(GameObject bullet)
    {
        if(!bullet.name.Contains("Enemy"))
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

            if (soldierState != EnemySoldierState.FollowingPlayer)
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

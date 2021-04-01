using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemySoldierStateManager;
using static ZombieStateManager;

public class EnemySoldierFieldOfViewControl : MonoBehaviour
{

    public FieldOfView fieldOfViewPrefab;
    public Transform fieldOfViewPos;
    public GameObject exclamationPoint;
    public FieldOfView fieldOfView;

    private EnemySoldierStateManager soldierStateManager;
    private MoveTowardsPlayer moveTowardsPlayer;
    private void Awake()
    {
        fieldOfView = Instantiate(fieldOfViewPrefab);
        fieldOfView.zombie = gameObject;
        fieldOfView.OnRaycastHit += SoldierRaycastHit;
        fieldOfView.OnPlayerNotFound += PlayerNotFound;
        fieldOfView.viewDistance = 30f;
        soldierStateManager = GetComponent<EnemySoldierStateManager>();
        moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
    }

    void Update()
    {
        fieldOfView.origin = fieldOfViewPos.position;
        // fieldOfView.SetAimDirection(new Vector3(0, 0, transform.parent.eulerAngles.z));
        fieldOfView.startingAngle = transform.eulerAngles.z + 45f;
    }

    private void SoldierRaycastHit(RaycastHit2D raycastHit2D)
    {
        if ((soldierStateManager.soldierState == EnemySoldierState.Idle || soldierStateManager.soldierState == EnemySoldierState.Pacing) &&
            (raycastHit2D.collider.gameObject.name.Contains("Player") ||
            (raycastHit2D.collider.gameObject.name.Contains("ZombieAlertDetector") && raycastHit2D.collider.gameObject.transform.parent.GetComponent<ZombieStateManager>().zombieState == ZombieState.FollowingPlayer) ||
            (raycastHit2D.collider.gameObject.name.Contains("SoldierAlertDetector") && raycastHit2D.collider.gameObject.transform.parent.GetComponent<EnemySoldierStateManager>().soldierState == EnemySoldierState.FollowingPlayer))) {
            soldierStateManager.StartFollowingPlayer();
            GameObject pointObj = Instantiate(exclamationPoint);
            pointObj.GetComponent<FollowCharacter>().characterToFollow = transform;
            pointObj.GetComponent<FollowCharacter>().yOffsetFromChar = 3f;
            fieldOfView.fov = 360f;
            if(moveTowardsPlayer.moveTowardPlayerType != MoveTowardsPlayer.MoveTowardPlayerType.KeepPlayerInView)
            {
                Destroy(fieldOfView.gameObject);
            }
        }
        if (raycastHit2D.collider.gameObject.name.Contains("Player"))
        {
            moveTowardsPlayer.playerVisible = true;
        }

    }

    private void PlayerNotFound()
    {
        moveTowardsPlayer.playerVisible = false;
    }

    private void OnDisable()
    {
        if(fieldOfView != null)
        {
            fieldOfView.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        fieldOfView.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ZombieStateManager;

public class ZombieFieldOfViewControl : MonoBehaviour
{

    public FieldOfView fieldOfViewPrefab;
    public Transform fieldOfViewPos;
    public GameObject exclamationPoint;
    public FieldOfView fieldOfView;
    private MoveTowardsPlayer moveTowardsPlayer;
    private void Awake()
    {
        fieldOfView = Instantiate(fieldOfViewPrefab);
        fieldOfView.zombie = gameObject;
        fieldOfView.OnRaycastHit += ZombieRaycastHit;
        fieldOfView.OnPlayerNotFound += PlayerNotFound;
        moveTowardsPlayer = GetComponent<MoveTowardsPlayer>();
        fieldOfView.viewDistance = 20f;
    }

    void Update()
    {
        fieldOfView.origin = fieldOfViewPos.position;
        // fieldOfView.SetAimDirection(new Vector3(0, 0, transform.parent.eulerAngles.z));
        fieldOfView.startingAngle = transform.eulerAngles.z + 45f;
    }

    private void ZombieRaycastHit(RaycastHit2D raycastHit2D)
    {
        if (raycastHit2D.collider.gameObject.name.Contains("Player") ||
            (raycastHit2D.collider.gameObject.name.Contains("Zombie") && raycastHit2D.collider.gameObject.transform.parent.GetComponent<ZombieStateManager>().zombieState == ZombieState.FollowingPlayer)) {
            GetComponent<ZombieStateManager>().StartFollowingPlayer();
            GameObject pointObj = Instantiate(exclamationPoint);
            pointObj.GetComponent<FollowCharacter>().characterToFollow = transform;
            pointObj.GetComponent<FollowCharacter>().yOffsetFromChar = 3f;
            Destroy(fieldOfView.gameObject);
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
}

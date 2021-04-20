using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ZombieStateManager;

public class PaceInCircle : MonoBehaviour
{
    public float distance;
    public float speed = .1f;
    public Vector3 initPosition;
    public WallColliderManager wallColliderManager;
    private Rigidbody2D m_Rigidbody2D;
    private float totalRotation = 0f;
    private bool isTurning;
    private bool isReversing;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
    }


    // Start is called before the first frame update
    void Start()
    {
        // TODO: @allenwhitedev low-priority network-break fix -> console warning: Paramater 'StartWalking' does not exist
        // GetComponent<MoveTowardsPlayer>().animator.SetTrigger("StartWalking");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isTurning)
        {
            transform.position += transform.right * Time.deltaTime * (speed / 2);
        } else
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
        
        if (Vector3.Distance(initPosition, transform.position) > distance && !isTurning)
        {
            isTurning = true;
            initPosition = transform.position;
        }
        else if (wallColliderManager.willHitWall)
        {

            wallColliderManager.willHitWall = false;
            isTurning = true;
        }
        else if(isTurning)
        {
            transform.eulerAngles += new Vector3(0, 0, 1f);
            totalRotation += 1f;
            if(totalRotation >= 180f)
            {
                isTurning = false;
                totalRotation = 0f;
                isReversing = !isReversing;
            }
        }
    }
}

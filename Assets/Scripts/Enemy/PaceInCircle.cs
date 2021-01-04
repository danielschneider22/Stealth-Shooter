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
        GetComponent<MoveTowardsPlayer>().animator.SetTrigger("StartWalking");
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
        
        if (Vector3.Distance(initPosition, transform.position) > distance && !isTurning && !isReversing)
        {
            isTurning = true;
        }
        else if (Vector3.Distance(initPosition, transform.position) < speed * 2.5 && !isTurning && isReversing)
        {
            isTurning = true;
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

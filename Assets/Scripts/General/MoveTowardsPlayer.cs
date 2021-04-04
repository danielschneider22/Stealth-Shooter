using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;

public class MoveTowardsPlayer : NetworkBehaviour
{

    private Transform player;
    public float speed = .001f;
    public Animator animator;
    int MaxDist = 12;

    private float attackTimer = 0f;
    private float attackWaitTime = 2f;
    private float sinMovementRandModifier;

    public float hitStunTimer = 0f;
    public float distToStayAwayFromPlayer = 5f;
    public bool playerVisible = false;
    public enum MoveTowardPlayerType
    {
        MoveStraightTowards,
        Dodging,
        KeepPlayerInView
    }
    public MoveTowardPlayerType moveTowardPlayerType = MoveTowardPlayerType.MoveStraightTowards;

    private Rigidbody2D m_Rigidbody2D;

    public override void NetworkStart()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        // TODO: @allenwhitedev handle multiple Players (Player tag)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sinMovementRandModifier = Random.Range(-100.0f, 100.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vectorToTarget = new Vector3(player.position.x + 1.25f, player.position.y - 1.25f, 0) - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;

        if ((Vector3.Distance(player.position, m_Rigidbody2D.transform.position) > distToStayAwayFromPlayer && moveTowardPlayerType != MoveTowardPlayerType.KeepPlayerInView || !playerVisible && moveTowardPlayerType == MoveTowardPlayerType.KeepPlayerInView) && hitStunTimer <= 0f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.position, step);
            if(moveTowardPlayerType == MoveTowardPlayerType.Dodging)
            {
                transform.position = transform.position + (transform.up * Mathf.Sin((Time.time * 2f) + sinMovementRandModifier) * .1f);
            }
            
        } else if (hitStunTimer <= 0f && moveTowardPlayerType == MoveTowardPlayerType.Dodging)
        {
            transform.position = transform.position + (transform.up * Mathf.Sin((Time.time * 2f) + sinMovementRandModifier) * .15f);
        }

        if(hitStunTimer < 0f)
        {
            if (m_Rigidbody2D.velocity != Vector2.zero)
            {
                m_Rigidbody2D.velocity = Vector2.zero;
            }
            if (m_Rigidbody2D.angularVelocity != 0f)
            {
                m_Rigidbody2D.angularVelocity = 0f;
            }
        }
        

        if (Vector3.Distance(transform.position, player.position) <= MaxDist && attackTimer <= 0f)
        {
            animator.SetTrigger("Attack");
            attackTimer = attackWaitTime;
        }
        attackTimer -= Time.deltaTime;
        hitStunTimer -= Time.deltaTime;
    }
}

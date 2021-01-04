using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoldierShootController : MonoBehaviour
{
    public Transform gunPosition;
    public GameObject handgunBullet;
    public Animator bodyAnimator;

    public float handgunBulletForce = 5f;

    private float fireTimer = 2f;
    public float fireRate = 2f;

    private void Awake()
    {
        fireTimer = fireRate;
    }

    private void FixedUpdate()
    {
        if(fireTimer <= 0f)
        {
            ShootHandgun();
            fireTimer = fireRate;
        }
        fireTimer -= Time.deltaTime;
    }

    private void ShootHandgun()
    {
        bodyAnimator.SetTrigger("FireHandgun");
        GameObject bullet = Instantiate(handgunBullet, gunPosition.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * handgunBulletForce, ForceMode2D.Impulse);

    }
}

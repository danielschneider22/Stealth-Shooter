using UnityEngine;

public class HitManager : MonoBehaviour
{
    public GameObject bloodEffect;
    public GameObject exclamationPoint;

    public delegate void HitWithBulletAction(GameObject bullet);
    public event HitWithBulletAction OnHitWithBullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Handgun"))
        {
            OnHitWithBullet(collision.gameObject);
        }
    }
}

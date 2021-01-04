using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitWall : MonoBehaviour
{
    public GameObject impactEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Handgun"))
        {
            GameObject impactEffectObj = Instantiate(impactEffect);
            // impactEffectObj.transform.position = globalPositionOfContact;
            impactEffectObj.transform.position = collision.gameObject.transform.position;
            impactEffectObj.transform.eulerAngles = new Vector3(collision.gameObject.transform.eulerAngles.z, 90f, 90f);
            Destroy(collision.gameObject);
        }
    }
}

using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    private float shakeTimeRemaining, shakePower, shakeFadeTime;
    public CinemachineVirtualCamera virtualCamera;

    private void Update()
    {
        /* if(Input.GetKeyDown(KeyCode.K))
        {
            StartShake(.25f, 1f);
        } */
    }
    // Start is called before the first frame update
    private void LateUpdate()
    {
        if(shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xAmount = Random.Range(-.25f, .25f) * shakePower;
            float yAmount = Random.Range(-.25f, .25f) * shakePower;

            // transform.position += new Vector3(xAmount, yAmount, 0f);
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = .5f + xAmount;
            virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = .5f + yAmount;

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
            if(shakeTimeRemaining <= 0)
            {
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = .5f;
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = .5f;
            }
        }
    }

    public void StartShake(float length ,float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;
    }
}

using UnityEngine;
using System.Collections;

public class ParticleAttractor : MonoBehaviour
{

    [SerializeField]
    public Transform _attractorTransform;

    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];
    private float particleTime = 0f;
    public float waitTime = .7f;

    public void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void LateUpdate()
    {
        if (_particleSystem.isPlaying && particleTime > waitTime)
        {
            int length = _particleSystem.GetParticles(_particles);
            Vector3 attractorPosition = _attractorTransform.position;

            for (int i = 0; i < length; i++)
            {
                _particles[i].position = _particles[i].position + (attractorPosition - _particles[i].position) / (_particles[i].remainingLifetime) * Time.deltaTime;
            }
            _particleSystem.SetParticles(_particles, length);
        }
        particleTime += Time.deltaTime;

    }
}
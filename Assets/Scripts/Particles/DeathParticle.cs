using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DeathParticle : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    public void PlayParticle()
    {
        _particleSystem.Play();
    }
}

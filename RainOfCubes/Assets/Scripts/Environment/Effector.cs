using UnityEngine;

public class Effector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _explosionEffect;

    public ParticleSystem SpawnExplosionEffect(Transform point)
    {
        ParticleSystem effect = Instantiate(_explosionEffect, point.position, Quaternion.identity, null);
        return effect;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Effector _effector;
    [SerializeField] private Zoner _zoner;

    private float _explosionRadius = 5;
    private float _explosionForce = 250;

    public void Explode(Transform point)
    {
        _effector.SpawnExplosionEffect(point);

        List<Rigidbody> explodableObjects = _zoner.GetObjectsInRadius(point, _explosionRadius);

        foreach (var explodableObject in explodableObjects)
        {
            explodableObject.AddExplosionForce(_explosionForce, point.position, _explosionRadius);
        }
    }
}
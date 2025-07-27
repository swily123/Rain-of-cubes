using System.Collections.Generic;
using UnityEngine;

public class Zoner : MonoBehaviour
{
    private float _spawnZoneXLength = 8;
    private float _spawnZoneZLength = 8;

    public Vector3 GetRandomPosition()
    {
        float randomPointX = Random.Range(-_spawnZoneXLength, _spawnZoneXLength);
        float randomPointZ = Random.Range(-_spawnZoneZLength, _spawnZoneZLength);

        return new Vector3(randomPointX, transform.position.y, randomPointZ);
    }

    public List<Rigidbody> GetObjectsInRadius(Transform point, float _radius)
    {
        List<Rigidbody> rigidbodies = new List<Rigidbody>();
        Collider[] hits = Physics.OverlapSphere(point.position, _radius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbodies.Add(rigidbody);
            }
        }

        return rigidbodies;
    }
}

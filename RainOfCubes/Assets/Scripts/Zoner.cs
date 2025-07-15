using UnityEngine;

public class Zoner : MonoBehaviour
{
    private float _spawnZoneXLength = 6;
    private float _spawnZoneZLength = 6;

    public Vector3 GetRandomPosition()
    {
        float randomPointX = Random.Range(-_spawnZoneXLength, _spawnZoneXLength);
        float randomPointZ = Random.Range(-_spawnZoneZLength, _spawnZoneZLength);

        return new Vector3(randomPointX, transform.position.y, randomPointZ);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.right * (transform.position.x - _spawnZoneXLength), Vector3.right * (transform.position.x + _spawnZoneXLength));
        Gizmos.DrawLine(Vector3.forward * (transform.position.z - _spawnZoneZLength), Vector3.forward * (transform.position.z + _spawnZoneZLength));
    }
}

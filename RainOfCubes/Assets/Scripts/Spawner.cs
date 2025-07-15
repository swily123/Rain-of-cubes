using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    private int _defualtCapacity = 1;
    private int _poolMaxSize = 10;

    private float _repeatTime = 0.5f;
    private float _defualtMass = 0.05f;
    private float _spawnZoneXLength = 6;
    private float _spawnZoneZLength = 6;

    private ObjectPool<GameObject> _pool;

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube.gameObject);
        cube.Reset();
    }

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => GameObject.CreatePrimitive(PrimitiveType.Cube),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _defualtCapacity,
            maxSize: _poolMaxSize);

    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0, _repeatTime);
    }

    private void ActionOnGet(GameObject cube)
    {
        cube.transform.position = GetRandomPosition();
        cube.transform.rotation = Quaternion.identity;
        cube.transform.localScale = Vector3.one;

        CheckCubeCommponents(cube);
        Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
        cubeRigidbody.velocity = Vector3.zero;
        cubeRigidbody.mass = _defualtMass;

        cube.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void CheckCubeCommponents(GameObject cube)
    {
        if (cube.GetComponent<Rigidbody>() == false)
        {
            cube.AddComponent<Rigidbody>();
        }

        if (cube.GetComponent<Cube>() == false)
        {

            cube.AddComponent<Cube>();
        }
    }

    private Vector3 GetRandomPosition()
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

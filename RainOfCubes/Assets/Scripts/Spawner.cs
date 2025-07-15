using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Zoner _zoner;

    private int _defualtCapacity = 1;
    private int _poolMaxSize = 15;
    private float _repeatTime = 0.5f;

    public static UnityEvent<Cube> CubeDied;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab, _zoner.GetRandomPosition(), Quaternion.identity),
            actionOnGet: (obj) => Configure(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defualtCapacity,
            maxSize: _poolMaxSize);
    }

    private void OnEnable()
    {
        CubeDied.AddListener(ReleaseCube);
    }

    private void OnDisable()
    {
        CubeDied.RemoveAllListeners();
    }

    private void Start()
    {
        StartCoroutine(GetCubesWhile());
    }

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
    }

    private IEnumerator GetCubesWhile()
    {
        var wait = new WaitForSeconds(_repeatTime);

        while(enabled)
        {
            GetCube();
            yield return wait;
        }
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void Configure(Cube cube)
    {
        cube.transform.rotation = Quaternion.identity;
        cube.Rigidbody.velocity = Vector3.zero;

        cube.gameObject.SetActive(true);
    }
}

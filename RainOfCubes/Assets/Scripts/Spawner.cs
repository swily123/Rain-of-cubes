using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeConfigurator _cubeConfigurator;
    [SerializeField] private Zoner _zoner;

    private int _defualtCapacity = 1;
    private int _poolMaxSize = 15;
    private float _repeatTime = 0.5f;

    private ObjectPool<GameObject> _pool;

    public void ReleaseCube(Cube cube)
    {
        _pool.Release(cube.gameObject);
    }

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => GameObject.CreatePrimitive(PrimitiveType.Cube),
            actionOnGet: (obj) => _cubeConfigurator.Configure(obj, _zoner.GetRandomPosition()),
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

    private void GetCube()
    {
        _pool.Get();
    }
}

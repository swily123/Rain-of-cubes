using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawners
{
    public class Spawner <T> : BaseSpawner where T : Component
    {
        [SerializeField] private T _objectPrefab;

        public override event Action<int> AllTimeObjectsChanged;
        public override event Action<int> SpawnObjectChanged;
        public override event Action<int> ActiveObjectsChanged;

        public override int SpawnCountForAllTime { get; protected set; } = 0;

        private int _defualtCapacity = 1;
        private int _poolMaxSize = 30;
        private ObjectPool<T> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<T>(
                createFunc: () => Spawn(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => ActionOnRelease(obj),
                actionOnDestroy: (obj) => Destroy(obj.gameObject),
                collectionCheck: true,
                defaultCapacity: _defualtCapacity,
                maxSize: _poolMaxSize);
        }

        protected void ReleaseObject(T @object)
        {
            _pool.Release(@object);
        }

        protected T GetObject()
        {
            SpawnCountForAllTime++;
            AllTimeObjectsChanged?.Invoke(SpawnCountForAllTime);
            return _pool.Get();
        }

        protected virtual void ActionOnGet(T @object)
        {
            @object.gameObject.SetActive(true);
            ActiveObjectsChanged?.Invoke(_pool.CountActive);
            SpawnObjectChanged?.Invoke(_pool.CountAll);
        }

        protected virtual void ActionOnRelease(T @object)
        {
            @object.gameObject.SetActive(false);
            ActiveObjectsChanged?.Invoke(_pool.CountActive);
        }

        private T Spawn()
        {
            T @object = Instantiate(_objectPrefab);
            return @object;
        }
    }
}

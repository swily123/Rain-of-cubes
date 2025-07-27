using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawners
{
    public class Spawner <T> : BaseSpawner where T : Component
    {
        [SerializeField] private T _objectPrefab;

        public event Action<T> Spawned;
        public event Action<T> Released;

        public override int SpawnCountForAllTime { get; protected set; } = 0;
        public override int SpawnCount => _pool.CountAll;
        public override int ActiveObjectCount => _pool.CountActive;

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
            return _pool.Get();
        }

        private void ActionOnGet(T @object)
        {
            @object.gameObject.SetActive(true);
            Spawned?.Invoke(@object);
        }

        private T Spawn()
        {
            T @object = Instantiate(_objectPrefab);
            return @object;
        }

        private void ActionOnRelease(T @object)
        {
            @object.gameObject.SetActive(false);
            Released?.Invoke(@object);
        }
    }
}
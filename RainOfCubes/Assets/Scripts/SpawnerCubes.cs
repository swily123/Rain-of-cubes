using UnityEngine;
using Spawners;
using System.Collections;
using System;

public class SpawnerCubes : Spawner<Cube>
{
    [SerializeField] protected Zoner _zoner;

    public event Action<Transform> CubeDespawned;
    private float _spawnDelay = 3f;

    private void OnEnable()
    {
        Spawned += Configure;
        Released += AcrionOnCubeDespawn;
    }

    private void OnDisable()
    {
        Spawned -= Configure;
        Released -= AcrionOnCubeDespawn;
    }

    private void Start()
    {
        StartCoroutine(GetCubeWhile());
    }

    private IEnumerator GetCubeWhile()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnDelay);

        while (enabled)
        {
            GetObject();
            yield return wait;
        }
    }

    private void Configure(Cube cube)
    {
        cube.transform.position = _zoner.GetRandomPosition();
        cube.ResetParameters();
        cube.DespawnRequested += ReleaseObject;
    }

    private void AcrionOnCubeDespawn(Cube cube)
    {
        CubeDespawned?.Invoke(cube.transform);
        cube.DespawnRequested -= ReleaseObject;
    }
}
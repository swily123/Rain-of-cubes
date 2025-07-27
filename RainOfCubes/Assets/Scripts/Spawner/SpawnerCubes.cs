using UnityEngine;
using Spawners;
using System.Collections;
using System;

public class SpawnerCubes : Spawner<Cube>
{
    [SerializeField] protected Zoner _zoner;

    public event Action<Transform> CubeDespawned;
    private float _spawnDelay = 1;

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);
        Configure(cube);
    }

    protected override void ActionOnRelease(Cube cube)
    {
        base.ActionOnRelease(cube);
        DespawnCube(cube);
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

    private void DespawnCube(Cube cube)
    {
        CubeDespawned?.Invoke(cube.transform);
        cube.DespawnRequested -= ReleaseObject;
    }
}

using UnityEngine;
using Spawners;

public class SpawnerBombs : Spawner<Bomb>
{
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private void OnEnable()
    {
        _spawnerCubes.CubeDespawned += SpawnBomb;
    }

    private void OnDisable()
    {
        _spawnerCubes.CubeDespawned -= SpawnBomb;
    }

    protected override void ActionOnRelease(Bomb bomb)
    {
        base.ActionOnRelease(bomb);
        DespawnBomb(bomb);
    }

    private void SpawnBomb(Transform point)
    {
        Bomb bomb = GetObject();
        Configure(bomb, point);
    }

    private void Configure(Bomb bomb, Transform point)
    {
        bomb.transform.position = point.position;
        bomb.ResetParameters();
        bomb.ActionOnSpawn();
        bomb.DespawnRequested += ReleaseObject;
    }

    private void DespawnBomb(Bomb bomb)
    {
        bomb.DespawnRequested -= ReleaseObject;
    }
}

using UnityEngine;
using Spawners;

public class SpawnerBombs : Spawner<Bomb>
{
    [SerializeField] private SpawnerCubes _spawnerCubes;

    private void OnEnable()
    {
        _spawnerCubes.CubeDespawned += SpawnBomb;
        Released += AcrionOnBombDespawn;
    }

    private void OnDisable()
    {
        _spawnerCubes.CubeDespawned -= SpawnBomb;
        Released -= AcrionOnBombDespawn;
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

    private void AcrionOnBombDespawn(Bomb bomb)
    {
        bomb.DespawnRequested -= ReleaseObject;
    }
}
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    public abstract int SpawnCountForAllTime { get; protected set; }
    public abstract int SpawnCount { get; }
    public abstract int ActiveObjectCount { get; }
}

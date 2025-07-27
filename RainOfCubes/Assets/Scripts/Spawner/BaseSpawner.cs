using System;
using UnityEngine;

public abstract class BaseSpawner : MonoBehaviour
{
    public abstract event Action<int> AllTimeObjectsChanged;
    public abstract event Action<int> SpawnObjectChanged;
    public abstract event Action<int> ActiveObjectsChanged;

    public abstract int SpawnCountForAllTime { get; protected set; }
}

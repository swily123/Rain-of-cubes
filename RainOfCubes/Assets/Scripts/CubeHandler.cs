using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Colorist _colorist;

    private List<Platform> _subscridedPlatforms = new List<Platform>();

    private void OnEnable()
    {
        foreach (Platform platform in FindObjectsOfType<Platform>())
        {
            platform.CubeOnCollisionEntered += ManageCube;
            _subscridedPlatforms.Add(platform);
        }
    }

    private void OnDisable()
    {
        foreach (Platform platform in _subscridedPlatforms)
        {
            platform.CubeOnCollisionEntered -= ManageCube;
        }
    }

    private void ManageCube(Cube cube)
    {
        ChangeColorCube(cube);
        StartCoroutine(ReleaseCubeAfter(cube));
    }

    private IEnumerator ReleaseCubeAfter(Cube cube)
    {
        int minrange = 2;
        int maxrange = 5;
        int secondToWait = Random.Range(minrange, maxrange + 1);
        var wait = new WaitForSeconds(secondToWait);
        yield return wait;
        _spawner.ReleaseCube(cube);
        cube.Reset();
    }

    private void ChangeColorCube(Cube cube)
    {
        cube.ChangeColor(_colorist.GetRandomColor());
    }
}

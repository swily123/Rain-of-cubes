using System;
using System.Collections;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private Colorist _colorist;

    public static event Action<Cube> CubeDead;

    private void OnEnable()
    {
        Cube.CubeOnCollisionEntered += ManageCube;
    }

    private void OnDisable()
    {
        Cube.CubeOnCollisionEntered -= ManageCube;
    }

    public void ManageCube(Cube cube)
    {
        ChangeColorCube(cube);
        StartCoroutine(ReleaseCubeAfter(cube));
    }

    private IEnumerator ReleaseCubeAfter(Cube cube)
    {
        int minrange = 2;
        int maxrange = 5;
        int secondToWait = UnityEngine.Random.Range(minrange, maxrange + 1);
        yield return new WaitForSeconds(secondToWait);
        CubeDead?.Invoke(cube);
        cube.Reset();
    }

    private void ChangeColorCube(Cube cube)
    {
        cube.ChangeColor(_colorist.GetRandomColor());
    }
}

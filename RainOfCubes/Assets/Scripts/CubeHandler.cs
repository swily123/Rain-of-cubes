using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CubeHandler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Colorist _colorist;

    public static UnityEvent<Cube> CubeOnCollisionEntered;

    private void OnEnable()
    {
        CubeOnCollisionEntered.AddListener(ManageCube);
    }

    private void OnDisable()
    {
        CubeOnCollisionEntered.RemoveAllListeners();
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
        int secondToWait = Random.Range(minrange, maxrange + 1);
        yield return new WaitForSeconds(secondToWait);
        Spawner.CubeDied.Invoke(cube);
        cube.Reset();
    }

    private void ChangeColorCube(Cube cube)
    {
        cube.ChangeColor(_colorist.GetRandomColor());
    }
}

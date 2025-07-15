using UnityEngine;

public class CubeConfigurator : MonoBehaviour
{
    private float _defualtMass = 0.05f;

    public void Configure(GameObject cube, Vector3 position)
    {
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.identity;
        cube.transform.localScale = Vector3.one;

        CheckCubeCommponents(cube);
        Rigidbody cubeRigidbody = cube.GetComponent<Rigidbody>();
        cubeRigidbody.velocity = Vector3.zero;
        cubeRigidbody.mass = _defualtMass;

        cube.SetActive(true);
    }

    private void CheckCubeCommponents(GameObject cube)
    {
        if (cube.GetComponent<Rigidbody>() == false)
        {
            cube.AddComponent<Rigidbody>();
        }

        if (cube.GetComponent<Cube>() == false)
        {
            cube.AddComponent<Cube>();
        }
    }
}

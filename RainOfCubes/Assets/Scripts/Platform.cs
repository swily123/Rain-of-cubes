using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public event Action<Cube, bool> CubeOnCollisionEntered;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Cube>(out Cube cube))
        {
            CubeOnCollisionEntered?.Invoke(cube, cube.IsCollideWithPlatform(gameObject));
            cube.Collide(gameObject);
        }
    }
}

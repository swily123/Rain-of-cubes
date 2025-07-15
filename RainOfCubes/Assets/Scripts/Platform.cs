using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public event Action<Cube> CubeOnCollisionEntered;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Cube>(out Cube cube))
        {
            if (cube.Collided == false)
            {
                CubeOnCollisionEntered?.Invoke(cube);
                cube.Collide();
            }
        }
    }
}

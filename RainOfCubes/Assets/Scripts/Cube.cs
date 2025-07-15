using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defualtColor = Color.white;

    public static event Action<Cube> CubeOnCollisionEntered;

    public bool Collided { get; private set; } = false;
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Collided)
        {
            return;
        }

        if (collision.transform.TryGetComponent<Platform>(out _))
        {
            CubeOnCollisionEntered?.Invoke(this);
            Collided = true;
        }
    }

    public void Reset()
    {
        Collided = false;
        ChangeColor(_defualtColor);
    }
}

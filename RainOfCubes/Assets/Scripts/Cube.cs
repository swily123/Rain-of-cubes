using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defualtColor = Color.white;


    public bool Collided { get; private set; }
    public Rigidbody Rigidbody => GetComponent<Rigidbody>();

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Collided == false)
        {
            return;
        }

        if (collision.transform.TryGetComponent<Platform>(out _))
        {
            CubeHandler.CubeOnCollisionEntered.Invoke(this);
            Collided = true;
        }
    }

    public void Reset()
    {
        Collided = false;
        ChangeColor(_defualtColor);
    }
}

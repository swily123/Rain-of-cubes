using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defualtColor = Color.white;

    private bool _collidedWithPlatform = false;

    public bool Collided => _collidedWithPlatform;

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public void Collide()
    {
        _collidedWithPlatform = true;
    }

    public void Reset()
    {
        _collidedWithPlatform = false;
        ChangeColor(_defualtColor);
    }
}

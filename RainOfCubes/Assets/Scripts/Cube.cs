using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Color _defualtColor = Color.white;
    private List<GameObject> _platformsCollided = new List<GameObject>();
    private bool _collidedWithPlatform = false;

    public bool Collided => _collidedWithPlatform;
    public Color CurrentColor => GetComponent<MeshRenderer>().material.color;

    public void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public void Collide(GameObject platform)
    {
        _collidedWithPlatform = true;
        _platformsCollided.Add(platform);
    }

    public void Reset()
    {
        _collidedWithPlatform = false;
        ChangeColor(_defualtColor);
        _platformsCollided = new List<GameObject>();
    }

    public bool IsCollideWithPlatform(GameObject platform)
    {
        bool isCollide = false;

        foreach (GameObject platformCollided in _platformsCollided)
        {
            if (platform == platformCollided)
            {
                isCollide = true;
            }
        }

        return isCollide;
    }
}

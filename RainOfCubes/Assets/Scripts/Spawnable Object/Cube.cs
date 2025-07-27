using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]

public class Cube : MonoBehaviour
{
    [SerializeField] private Colorist _colorist;
    [SerializeField] private Color _defualtColor = Color.white;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
    private bool _isCollided = false;

    public event Action<Cube> DespawnRequested;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    public void ResetParameters()
    {
        _isCollided = false;
        ChangeColor(_defualtColor);

        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isCollided)
        {
            return;
        }

        if (collision.transform.TryGetComponent<Platform>(out _))
        {
            _isCollided = true;
            ChangeColor(_colorist.GetRandomColor());
            StartCoroutine(ScheduleDespawn());
        }
    }

    private IEnumerator ScheduleDespawn()
    {
        int minrange = 2;
        int maxrange = 5;
        int secondToWait = UnityEngine.Random.Range(minrange, maxrange + 1);
        yield return new WaitForSeconds(secondToWait);
        DespawnRequested?.Invoke(this);
    }
}

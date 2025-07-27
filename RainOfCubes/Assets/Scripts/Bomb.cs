using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]

public class Bomb : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Colorist _colorist;

    public event Action<Bomb> DespawnRequested;

    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ActionOnSpawn()
    {
        int time = GetRandomLifeTime();

        StartCoroutine(ExlpodeWithTime(time));
        StartCoroutine(SmoothlyBecomeTransparent(time));
    }

    public void ResetParameters()
    {
        _colorist.SetAlpha(_meshRenderer, 1);
        transform.rotation = Quaternion.identity;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    private IEnumerator ExlpodeWithTime(float time)
    {
        yield return new WaitForSeconds(time);
        _exploder.Explode(transform);
        DespawnRequested?.Invoke(this);
    }

    private int GetRandomLifeTime()
    {
        int minrange = 2;
        int maxrange = 5;
        int secondToWait = UnityEngine.Random.Range(minrange, maxrange + 1);

        return secondToWait;
    }

    private IEnumerator SmoothlyBecomeTransparent(float duration)
    {
        float startAlpha = 1;
        float step = startAlpha / duration;

        while (_meshRenderer.material.color.a != 0)
        {
            startAlpha = _meshRenderer.material.color.a;
            float alpha = Mathf.MoveTowards(startAlpha, 0, step * Time.deltaTime);
            _colorist.SetAlpha(_meshRenderer, alpha);

            yield return null;
        }
    }
}
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Bomb : MonoBehaviour
{
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Colorist _colorist;

    public event Action<Bomb> DespawnRequested;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ActionOnSpawn()
    {
        int time = GetRandomLifeTime();

        StartCoroutine(ExlpodeWithTime(time));
        StartCoroutine(_colorist.SmoothlyBecomeTransparent(time));
    }

    public void ResetParameters()
    {
        _colorist.SetAlpha(1);
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]

public class Colorist : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private List<Color> _colors = new List<Color>();

    private void Awake()
    {
        _colors = new List<Color>()
        {
            Color.black, Color.blue, Color.red, Color.green, Color.cyan, Color.magenta, Color.clear, Color.yellow, Color.gray, Color.grey
        };
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public Color GetRandomColor()
    {
        Color color = Color.cyan;

        if (_colors.Count > 0)
        {
            int randomIndex = Random.Range(0, _colors.Count);
            color = _colors[randomIndex];
        }

        return color;
    }

    public void SetAlpha(float alpha)
    {
        Color color = _meshRenderer.material.color;
        color.a = alpha;
        _meshRenderer.material.color = color;
    }

    public IEnumerator SmoothlyBecomeTransparent(float duration)
    {
        float startAlpha = 1;
        float step = startAlpha / duration;

        while (_meshRenderer.material.color.a != 0)
        {
            startAlpha = _meshRenderer.material.color.a;
            float alpha = Mathf.MoveTowards(startAlpha, 0, step * Time.deltaTime);
            SetAlpha(alpha);

            yield return null;
        }
    }
}

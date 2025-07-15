using System.Collections.Generic;
using UnityEngine;

public class Colorist : MonoBehaviour
{
    private List<Color> _colors = new List<Color>()
    {
        Color.black, Color.blue, Color.red, Color.green, Color.cyan, Color.magenta, Color.clear, Color.yellow, Color.gray, Color.grey
    };

    public Color GetRandomColor(Color currentColor)
    {
        Color color = Color.cyan;

        if (_colors.Count > 0)
        {
            _colors.Remove(currentColor);
            int randomIndex = Random.Range(0, _colors.Count);
            color = _colors[randomIndex];
            _colors.Add(currentColor);
        }

        return color;
    }
}

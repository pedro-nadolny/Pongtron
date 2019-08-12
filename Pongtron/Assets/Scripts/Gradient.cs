using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gradient : MonoBehaviour
{
    public Color[] colors;

    void Start()
    {
        if (this.colors.Length < 2) {
            Debug.LogWarning("Gradient needs at least 2 colors!");
            return;
        }

        var mesh = GetComponent<MeshFilter>().mesh;
        var uv = mesh.uv;
        var gradient = new Color[uv.Length];
        var colorUvRange = uv.Length / this.colors.Length;

        for (var i = 0; i< this.colors.Length - 1; i++) {
            var uvStart = colorUvRange * i;
            var uvEnd = colorUvRange * (i + 1);

            for (var j = uvStart; j < uvEnd; j++) {
                gradient[j] = Color.Lerp(this.colors[i], this.colors[i + 1], uv[j].x);
            }
        }

        mesh.colors = gradient;
    }
}

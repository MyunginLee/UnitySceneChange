using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Author: Myungin Lee. 
// This codes shows how to change color of material using HSV and random

public class ColorChange : MonoBehaviour
{
     void Start()
    {
        // Color
        // Get the renderer of the spheres and assign colors.
        Renderer monkeyRenderer = GetComponent<Renderer>();
        // HSV color space: https://en.wikipedia.org/wiki/HSL_and_HSV
        float hue = Random.Range(0f,1f); // Hue cycles through 0 to 1 
        // Random: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Random.Range.html
        Color color = Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness
        monkeyRenderer.material.color = color;
    }
}

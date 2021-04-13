using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bacgkround : MonoBehaviour
{

    private float fadeAmount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeControl());
    }

    IEnumerator FadeControl()
    {
        fadeAmount = 0.1f;
        var material = GetComponent<Renderer>().material;
        var color = material.color;

        color.a = fadeAmount;

        while (fadeAmount <= 0f)
        {
            material.color = new Color(color.r, color.g, color.b, fadeAmount + (10f * Time.deltaTime));
            yield return null;
        }
    }
}

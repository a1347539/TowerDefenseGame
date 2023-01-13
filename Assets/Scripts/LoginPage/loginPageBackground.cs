using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loginPageBackground : MonoBehaviour
{
    Image image;
    Color tempColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(changeAlpha());

    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator changeAlpha()
    {
        while (tempColor.a >= 0)
        {
            tempColor = image.color;
            tempColor.a -= 0.01f;
            image.color = tempColor;
            yield return new WaitForSeconds(0.005f);
        }
    }
}

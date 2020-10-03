using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    public float fadeTime = 1;
    private bool fading = false;

    // Update is called once per frame
    void Update()
    {
        if (!fading && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            StartCoroutine(FadeTo(0F, fadeTime));
            fading = true;
        }
    }
    
    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue, t));
            GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}

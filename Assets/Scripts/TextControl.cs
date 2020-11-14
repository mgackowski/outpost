using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class TextControl : MonoBehaviour
{
    private Text text;
    public float fadeDuration = 3000f;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    public void change(string newText)
    {
        text.color = Color.white;
        text.text = newText;
    }

    public void ChangeAndFade(string newText, float fadeDelay)
    {
        change(newText);
        StartCoroutine("FadeOut", fadeDelay);
    }

    private IEnumerator FadeOut(float fadeDelay)
    {
        yield return new WaitForSeconds(fadeDelay);
        for(float t=0f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = Color.Lerp(Color.white, Color.clear, t/fadeDuration);
            yield return null;
        }
        text.color = Color.clear;

    }
}

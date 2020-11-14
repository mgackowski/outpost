using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class ScreenFade : MonoBehaviour
{

    private Image img;
    public float fadeDuration = 3f;

    void Awake()
    {
        img = GetComponent<Image>();
    }

    public void FadeToBlack()
    {
        StartCoroutine("ToBlack", 0);
    }

    public void FadeFromBlack()
    {
        StartCoroutine("FromBlack", 0);
    }

    public void ChangeToBlack()
    {
        img.color = Color.black;
    }

    public void ChangeToClear()
    {
        img.color = Color.clear;
    }

    private IEnumerator ToBlack(float fadeDelay)
    {
        //yield return new WaitForSeconds(fadeDelay);
        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            img.color = Color.Lerp(Color.clear, Color.black, t / fadeDuration);
            yield return null;
        }
        img.color = Color.black;

    }

    private IEnumerator FromBlack(float fadeDelay)
    {
        Debug.Log("Inside coroutine");
        //yield return new WaitForSeconds(fadeDelay);
        for (float t = 0f; t < fadeDuration; t += Time.deltaTime)
        {
            Debug.Log("Inside loop");
            img.color = Color.Lerp(Color.black, Color.clear, t / fadeDuration);
            yield return null;
        }
        img.color = Color.clear;

    }
}

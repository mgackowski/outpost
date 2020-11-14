using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    public float oxygen = 300f;
    public float oxygenDepletionRate = 1f;

    public Text gameplayText;
    public Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoseOxygen");
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = System.TimeSpan.FromSeconds(oxygen).ToString("mm\\:ss");

    }

    IEnumerator LoseOxygen()
    {
        while (oxygen > 0)
        {
            oxygen -= 1f;
            yield return new WaitForSeconds(oxygenDepletionRate);
        }

    }
}

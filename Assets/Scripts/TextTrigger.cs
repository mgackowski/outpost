using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{

    public TextControl gameplayText;
    public string message = "";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerFocus"))
        {
            gameplayText.ChangeAndFade(message, 3f);
            gameObject.SetActive(false);
        }

    }

}

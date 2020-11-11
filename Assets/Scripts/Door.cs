using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    enum State
    {
        Closed,
        Minigame,
        Lockdown,
        Open
    }

    public GameObject doorMinigame;

    private State state = State.Closed;
    private bool interactable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Play minigame");
            state = State.Minigame;
            interactable = false;
            doorMinigame.SetActive(true);

        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if(collision.gameObject.CompareTag("PlayerFocus") && state == State.Closed)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        interactable = false;

        if (state == State.Minigame)
        {
            state = State.Closed;
            doorMinigame.SetActive(false);
        }

    }


}

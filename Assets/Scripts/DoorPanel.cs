using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPanel : MonoBehaviour
{
    public enum State
    {
        Closed,
        Minigame,
        Lockdown,
        Opening,
        Open
    }

    public GameObject doorMinigame;
    public GameObject door;
    public GameLogic logic;

    public State state = State.Closed;
    private bool interactable = false;

    public int oxygenAward = 120;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Play minigame");
            state = State.Minigame;
            interactable = false;
            doorMinigame.SetActive(true);

        }

        if (state == State.Opening)
        {
            door.SetActive(false);
            logic.oxygen += oxygenAward; //TODO: Magic number
            //gameObject.GetComponent<Renderer>().material.color = Color.green;
            state = State.Open;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.CompareTag("PlayerFocus") && state == State.Closed)
        {
            //gameObject.GetComponent<Renderer>().material.color = Color.white;
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //gameObject.GetComponent<Renderer>().material.color = Color.red;
        interactable = false;

        if (state == State.Minigame)
        {
            state = State.Closed;
            doorMinigame.SetActive(false);
        }
        else if (state == State.Lockdown)
        {
            doorMinigame.SetActive(false);
        }

    }
}

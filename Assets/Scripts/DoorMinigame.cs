using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMinigame : MonoBehaviour
{
    public DoorPanel door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Flesh out minigame
        if(door.state == DoorPanel.State.Minigame && Input.GetButton("Fire3"))
        {
            door.state = DoorPanel.State.Opening;
            gameObject.SetActive(false);
        }

        if (door.state == DoorPanel.State.Minigame && Input.GetKey("l"))
        {
            door.state = DoorPanel.State.Lockdown;
        }

    }
}

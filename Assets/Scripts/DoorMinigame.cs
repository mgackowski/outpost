using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMinigame : MonoBehaviour
{
    public Door door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Flesh out minigame
        if(door.state == Door.State.Minigame && Input.GetKey("o"))
        {
            door.state = Door.State.Opening;
            gameObject.SetActive(false);
        }

        if (door.state == Door.State.Minigame && Input.GetKey("l"))
        {
            door.state = Door.State.Lockdown;
        }

    }
}

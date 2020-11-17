using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingMinigame : MonoBehaviour
{
    public NPC npc;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //TODO: Flesh out minigame
        if (npc.state != NPC.NPCState.Dead && Input.GetButton("Fire3"))
        {
            npc.Heal();
            gameObject.SetActive(false);
        }

        //TODO: Flesh out minigame
        if (npc.state != NPC.NPCState.Dead && Input.GetKey("l"))
        {
            npc.health = 0;
            gameObject.SetActive(false);
        }

        if (npc.state == NPC.NPCState.Dead )
        {
            gameObject.SetActive(false);
        }

    }
}

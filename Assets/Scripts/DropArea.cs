using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    public int npcCount = 0;
    public List<NPC> presentNPCs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (NPC npc in presentNPCs) {
            if(!npc.carried && (npc.state != NPC.NPCState.Dead || npc.state != NPC.NPCState.Zombie))
            {
               count += 1;
            }
        }
        npcCount = count;

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            presentNPCs.Add( collision.gameObject.GetComponent<NPC>() );

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            presentNPCs.Remove(collision.gameObject.GetComponent<NPC>());
        }
    }
}

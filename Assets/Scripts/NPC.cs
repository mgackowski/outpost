using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour

{
    public enum NPCState
    {
        Incapacitated,
        Unconscious,
        Convulsing,
        Flatlining,
        Dead,
        Zombie
    }

    public NPCState state = NPCState.Incapacitated;
    public float health = 100;
    public float loseRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoseHealth");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoseHealth()
    {
        while (state != NPCState.Dead)
        {
            if (health > 50) state = NPCState.Incapacitated;
            else if (health > 20) state = NPCState.Unconscious;
            else if (health > 10) state = NPCState.Convulsing;
            else if (health > 0) state = NPCState.Flatlining;
            else state = NPCState.Dead;

            Debug.Log(state);

            health -= loseRate;
            yield return new WaitForSeconds(0.1f);
        }

    }


}

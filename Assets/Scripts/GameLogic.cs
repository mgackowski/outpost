using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{

    public enum GameState
    {
        Intro,
        Play,
        Finish,
        Over

    }

    public GameState state;

    public float oxygen = 300f;
    public float oxygenDepletionRate = 1f;
    public int maxNPCsToSave = 3;

    public TextControl gameplayText;
    public Text timerText;
    public ScreenFade fade;

    public DropArea npcDrop;
    public GameObject player;
    public OxygenGauge oxygenGauge;
    public AudioSource musicSource;

    public DoorPanel[] airlocks = new DoorPanel[3];
    public List<NPC> liveNpcs = new List<NPC>();

    // Start is called before the first frame update
    void Start()
    {
        //ChangeState(GameState.Intro);
        ChangeState(GameState.Play);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (NPC npc in liveNpcs.ToList())
        {
            if (npc.state == NPC.NPCState.Dead)
            {
                liveNpcs.Remove(npc);
            }
        }
        int npcToSave = Mathf.Clamp(liveNpcs.Count, 0, maxNPCsToSave);
        if (npcDrop.npcCount == npcToSave)
        {
            ChangeState(GameState.Finish);
        }
        
        


        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown("t"))
        {
            ChangeState(GameState.Finish);
        }

    }

    private void ChangeState(GameState newState)
    {
        if (state != newState) state = newState;
        switch (newState)
        {
            case GameState.Intro:
                StartCoroutine("PlayIntro");
                break;
            case GameState.Play:
                StartCoroutine("LoseOxygen");
                break;
            case GameState.Finish:
                StartCoroutine("PlayEnding");
                break;
            case GameState.Over:
                StopCoroutine("LoseOxygen");
                player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player.GetComponent<PlayerControls>().enabled = false;
                fade.FadeToBlack();
                break;
        }


    }

    IEnumerator LoseOxygen()
    {
        while (oxygen > 0)
        {
            oxygen -= 1f;
            oxygenGauge.setDisplayedValue(oxygen);
            yield return new WaitForSeconds(oxygenDepletionRate);
        }
        gameplayText.ChangeAndFade("Can't... breathe...",3f);
        ChangeState(GameState.Over);
    }

    IEnumerator PlayIntro()
    {
        StopCoroutine("LoseOxygen");
        fade.ChangeToBlack();
        yield return new WaitForSeconds(1f);
        gameplayText.ChangeAndFade("Got... to... get out...",2f);
        yield return new WaitForSeconds(6f);
        gameplayText.ChangeAndFade("Carry the others to safety...", 2f);
        yield return new WaitForSeconds(6f);
        gameplayText.ChangeAndFade("Before we all suffocate... or worse.", 2f);
        yield return new WaitForSeconds(6f);
        fade.FadeFromBlack();
        yield return new WaitForSeconds(1f);
        ChangeState(GameState.Play);

    }

    IEnumerator PlayEnding()
    {
        StopCoroutine("LoseOxygen");
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.GetComponent<PlayerControls>().enabled = false;
        fade.FadeToBlack();
        yield return new WaitForSeconds(1f);
        gameplayText.ChangeAndFade("We've made it to the safety room... now we have a chance...", 4f);
        yield return new WaitForSeconds(7f);
        musicSource.Stop();


        foreach (NPC npc in liveNpcs)
        {
            npc.ChangeState(NPC.NPCState.Zombie);
            //npc.health = -1f;

        }

        yield return new WaitForSeconds(4);
        gameplayText.ChangeAndFade("\"As long as we have each other, we will never run out of problems...\"", 10f);
        yield return new WaitForSeconds(14);
        gameplayText.change("THE END");


    }

}

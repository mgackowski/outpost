using System.Collections;
using System.Collections.Generic;
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

    public TextControl gameplayText;
    public Text timerText;
    public ScreenFade fade;

    public DropArea npcDrop;
    public GameObject player;
    public OxygenGauge oxygenGauge;

    public DoorPanel[] airlocks = new DoorPanel[3];
    public NPC[] npcs = new NPC[4];

    // Start is called before the first frame update
    void Start()
    {
        //ChangeState(GameState.Intro);
        ChangeState(GameState.Intro);
    }

    // Update is called once per frame
    void Update()
    {
        
        


        if(Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
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
                StopCoroutine("LoseOxygen");
                fade.FadeToBlack();
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
        state = GameState.Play;

    }
}

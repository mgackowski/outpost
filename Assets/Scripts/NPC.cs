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
    public float maxHealth = 100;
    public float health;
    public float loseRate = 1;
    public GameObject cprMinigame;
    public float chanceOfZombie = 0.25f;
    public TextControl textBox;
    public CameraTracker killCam;
    public GameObject player;
    public MonitorAnimation npcScreen;

    private bool interactable = false;
    public bool carried = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        StartCoroutine("LoseHealth");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Play minigame");
            interactable = false;
            cprMinigame.SetActive(true);

        }
        else if (interactable && Input.GetButtonDown("Fire2"))
        {
            if (!carried && player.GetComponent<PlayerControls>().carrying == null)
            {
                Debug.Log("Carry NPC");
                interactable = false;
                carried = true;
                player.GetComponent<PlayerControls>().PickUp(gameObject);
            }
        }
        else if (carried && Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Drop NPC");
            interactable = true;
            carried = false;
            player.GetComponent<PlayerControls>().Drop();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerFocus") && state != NPCState.Dead)
        {
            interactable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;
        cprMinigame.SetActive(false);
    }

    IEnumerator LoseHealth()
    {
        while (state != NPCState.Dead || state != NPCState.Zombie)
        {
            if (health > 50) ChangeState(NPCState.Incapacitated);
            else if (health > 20) ChangeState(NPCState.Unconscious);
            else if (health > 10) ChangeState(NPCState.Convulsing);
            else if (health > 0) ChangeState(NPCState.Flatlining);
            else ChangeState(NPCState.Dead);

            health -= loseRate;
            yield return new WaitForSeconds(0.1f);
        }

    }



    public void ChangeState(NPCState newState)
    {
        if (state != newState)
        {
            state = newState;
            npcScreen.ChangeAnimation(newState);
        }

            //Color newColor;
            switch (newState)
        {
            case NPCState.Incapacitated:
                //ColorUtility.TryParseHtmlString("#FF9200", out newColor);
                //GetComponent<Renderer>().material.color = newColor;
                break;
            case NPCState.Unconscious:
                //ColorUtility.TryParseHtmlString("#C64900", out newColor);
                //GetComponent<Renderer>().material.color = newColor;
                break;
            case NPCState.Convulsing:
                //ColorUtility.TryParseHtmlString("#FF4400", out newColor);
                //GetComponent<Renderer>().material.color = newColor;
                break;
            case NPCState.Flatlining:
                //ColorUtility.TryParseHtmlString("#FF0800", out newColor);
                //GetComponent<Renderer>().material.color = newColor;
                break;
            case NPCState.Dead:
                //ColorUtility.TryParseHtmlString("#444444", out newColor);
                //GetComponent<Renderer>().material.color = newColor;
                StopCoroutine("LoseHealth");
                RiskBecomingZombie();
                break;
            case NPCState.Zombie:
                StopCoroutine("LoseHealth");
                //ColorUtility.TryParseHtmlString("#68826E", out newColor);
                //GetComponent<Renderer>().material.color = newColor;
                break;
        }

    }

    public void Heal()
    {
        health = maxHealth;
    }

    private void RiskBecomingZombie()
    {
        System.Random rnd = new System.Random();
        if(rnd.NextDouble() < chanceOfZombie)
        {
            TurnToZombie();
        }

    }

    private void TurnToZombie()
    {
        state = NPCState.Zombie;
        textBox.ChangeAndFade("One of dead was infected. You run, but you cannot escape.\nGAME OVER", 5f);
        killCam.target = gameObject.transform;
        //Time.timeScale = 0; //GAME OVER

    }

}

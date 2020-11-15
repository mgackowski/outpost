using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControls : MonoBehaviour
{
    public Animator animator;
    private float moveHorizontal;
    private float moveVertical;
    //private bool mainButtonPressed;
    private Vector2 movement;
    private Text debugText;
    private AudioSource playerAudio;

    public float speed;
    public float orientation = 180;
    public CircleCollider2D interactionArea;
    public float interactionRange = 0.6f;
    public float interactionVerticalOffset = -0.3f;
    public GameObject carrying = null;
    public float carryingSpeed = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        debugText = GameObject.Find("DebugText").GetComponent<Text>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        //mainButtonPressed = Input.GetButton("Fire1");

        movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.magnitude > 1) movement = movement.normalized; //slower diagonal mvmt on keyboard

        if (movement.magnitude > 0) orientation = Vector2.SignedAngle(Vector2.up, movement);

        //interactionArea.enabled = mainButtonPressed;
        Vector2 offset = Quaternion.Euler(0, 0, orientation) * Vector2.up * interactionRange;
        offset.y += interactionVerticalOffset;
        interactionArea.offset = offset;
        //debugText.text = orientation.ToString();

        animator.SetFloat("direction", orientation * -1);
        animator.SetFloat("speed", movement.magnitude);

        if (carrying != null)
        {
            Vector2 carriedPosition = interactionArea.offset * 0.5f;
            carriedPosition.y += 0.25f;
            carrying.transform.localPosition = carriedPosition;
            
        }

        if (movement.magnitude > 0)
        {
            playerAudio.enabled = true;
        } else
        {
            playerAudio.enabled = false;
        }

    }

    void FixedUpdate()
    {
        if (carrying != null)
        {
            GetComponent<Rigidbody2D>().velocity = movement * speed * carryingSpeed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = movement * speed;
        }
        
    }

    public void PickUp(GameObject target)
    {
        carrying = target;
        carrying.transform.SetParent(transform);
        carrying.transform.GetChild(0).gameObject.SetActive(false);
        carrying.GetComponent<Collider2D>().enabled = false;
        animator.speed = carryingSpeed;
    }

    public void Drop()
    {
        carrying.transform.SetParent(null);
        carrying.transform.GetChild(0).gameObject.SetActive(true);
        carrying.GetComponent<Collider2D>().enabled = true;
        carrying = null;
        animator.speed = 1f;
    }


}
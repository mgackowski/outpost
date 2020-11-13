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
    private bool mainButtonPressed;
    private Vector2 movement;
    private Text debugText;

    public float speed;
    public float orientation = 180;
    public CircleCollider2D interactionArea;
    public float interactionRange = 0.6f;
    public float interactionVerticalOffset = -0.3f;
    public GameObject carrying = null;

    // Start is called before the first frame update
    void Start()
    {
        debugText = GameObject.Find("DebugText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        mainButtonPressed = Input.GetButton("Fire1");

        movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.magnitude > 1) movement = movement.normalized; //slower diagonal mvmt on keyboard

        if (movement.magnitude > 0) orientation = Vector2.SignedAngle(Vector2.up, movement);

        //interactionArea.enabled = mainButtonPressed;
        Vector2 offset = Quaternion.Euler(0, 0, orientation) * Vector2.up * interactionRange;
        offset.y += interactionVerticalOffset;
        interactionArea.offset = offset;
        debugText.text = orientation.ToString();

        animator.SetFloat("direction", orientation * -1);
        animator.SetFloat("speed", movement.magnitude);

        if (carrying != null)
        {
            Vector2 carriedPosition = interactionArea.offset;
            carriedPosition.y += 0.5f;
            carrying.transform.localPosition = carriedPosition;
            
        }

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement * speed;
    }
    public void PickUp(GameObject target)
    {

        carrying = target;
        carrying.transform.SetParent(transform);
        carrying.transform.GetChild(0).gameObject.SetActive(false);
        carrying.GetComponent<Collider2D>().enabled = false;

    }


}
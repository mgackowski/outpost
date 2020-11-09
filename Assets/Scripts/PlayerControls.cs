using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControls : MonoBehaviour
{
    private float moveHorizontal;
    private float moveVertical;
    private Vector2 movement;
    private Text debugText;

    public float speed;
    public float orientation = 180;
    public CircleCollider2D interactionArea;

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
        movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.magnitude > 1) movement = movement.normalized; //slower diagonal mvmt on keyboard

        if (movement.magnitude > 0) orientation = Vector2.SignedAngle(Vector2.up, movement);

        interactionArea.offset = Quaternion.Euler(0, 0, orientation) * Vector2.up;
        debugText.text = orientation.ToString();

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement * speed;
    }


}
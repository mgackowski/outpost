using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControls : MonoBehaviour
{
    private float moveHorizontal;
    private float moveVertical;
    private Vector2 movement;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetAxis() produces smoothing when played with keyboard
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveHorizontal, moveVertical);
        if (movement.magnitude > 1) movement = movement.normalized;

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = movement * speed;
    }
}

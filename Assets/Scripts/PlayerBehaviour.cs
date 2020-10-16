using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float MaxSpeed;

    private Inputs inputs;
    private Vector2 direction;

    private bool IsOnGround = false;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;


    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;

        myAnimator = GetComponent<Animator>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        var PlayerDirection = new Vector2(direction.x, 0);
        if(myRigidbody2D.velocity.sqrMagnitude < MaxSpeed)
        {
           myRigidbody2D.AddForce(direction * Speed);
        }

        /*var isRunning = PlayerDirection.x != 0;
        myAnimator.SetBool("IsRunning", isRunning);*/ 
        //NEFONCTIONNEPAS
        //ANIMATION DE COURSE NE SE LANCE PAS

        if(direction.x < 0)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            mySpriteRenderer.flipX = true;
        }

        /*var IsAscending = !IsOnGround && myRigidbody2D.velocity.y > 0;
        myAnimator.SetBool("IsJumping", IsAscending);
        var IsDescending = !IsOnGround && myRigidbody2D.velocity.y > 0;
        myAnimator.SetBool("IsFalling", IsDescending);
        myAnimator.SetBool("IsGrounded", IsOnGround);*/
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Ca fonctionne");

        direction = obj.ReadValue<Vector2>();
        Debug.Log(direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }
}

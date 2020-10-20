using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce;

    private Inputs inputs;
    private Vector2 direction;

    private bool IsOnGround = false;

    private Animator myAnimator;
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer mySpriteRenderer;

    private bool isMoving = false;


    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;

        inputs.Player.Jump.performed += OnJumpPerformed;

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

        if(direction.x < 0)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            mySpriteRenderer.flipX = true;
        }

        var isRunning = IsOnGround && direction.x != 0;
        myAnimator.SetBool("IsRunning", isRunning);

        var isJumping = IsOnGround == false && myRigidbody2D.velocity.y > 0;
        myAnimator.SetBool("IsAscending", isJumping);

        var isFalling = IsOnGround == false && myRigidbody2D.velocity.y < 0;
        myAnimator.SetBool("IsDescending", isFalling);
        myAnimator.SetBool("IsStanding", IsOnGround);
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
        Debug.Log(direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        if (IsOnGround == true)
        {
            myRigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            IsOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") == true && other.contacts[0].normal == Vector2.up)
        {
            IsOnGround = true;
        }

        if (other.gameObject.CompareTag("DeathZone"))
        {
            Debug.Log("kill");
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
    }
}

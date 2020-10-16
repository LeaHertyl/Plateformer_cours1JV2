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


    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;
        inputs.Player.Move.canceled += OnMoveCanceled;

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
        var myRigidbody = GetComponent<Rigidbody2D>();
        direction.y = 0;
        if(myRigidbody.velocity.sqrMagnitude < MaxSpeed)
        {
           myRigidbody.AddForce(direction * Speed);
        }
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

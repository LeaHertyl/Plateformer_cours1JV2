using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed; //pour pouvoir modifier la valeur de speed dans l'inspector
    [SerializeField] private float MaxSpeed; //pour pouvoir modifier la valeur de MaxSpeed dans l'inspector
    [SerializeField] private float JumpForce; //pour pouvoir modifier la valeur de JumpForce  dans l'inspector

    private Inputs inputs; //on cree une variable inputs de type Inputs
    private Vector2 direction; //on cree une variable direction de type Vector2

    private bool IsOnGround = false; //on cree un bolléen IsOnGround qu'on instancie comme etant false

    private Animator myAnimator; // on cree une variable pour l'animation du personnage
    private Rigidbody2D myRigidbody2D; //on cree une variable de type Rigidbody2D pour pouvoir agir ensuite sur celui du player
    private SpriteRenderer mySpriteRenderer; // on cree une variable pour modifier le sprite qui va etre affiché pendant les animations

    private void OnEnable()
    {
        //on instancie l'input system créé dans Unity
        inputs = new Inputs();
        inputs.Enable();


        inputs.Player.Move.performed += OnMovePerformed; //quand on appuie sur les inputs de l'action Move de l'action Map player, on lance la fonction OnMovePerformed
        inputs.Player.Move.canceled += OnMoveCanceled; //quand on arrête d'appuyer sur les inputs de l'action Move de l'action Map player, on lance la fonction OnMoveCanceled

        inputs.Player.Jump.performed += OnJumpPerformed; //quand on appuie sur les inputs de l'action Jump de l'action Map player, on lance la fonction OnJumpPerformed

        myAnimator = GetComponent<Animator>(); //on recupere le composant Animator du Player
        myRigidbody2D = GetComponent<Rigidbody2D>(); // on recupere le composant Rigidbody2D du Player
        mySpriteRenderer = GetComponent<SpriteRenderer>(); //on recupere le composant SpriteRenderer du Player

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// utilisation de la fonction FixedUpdate quand on agit sur la physique d'un game object
    /// </summary>
    private void FixedUpdate()
    {
        var PlayerDirection = new Vector2(direction.x, 0);

        //Tant que la vitesse de déplacement du player n'est pas superieure à maxSpeed, on lui ajoute une force en fonction des inputs enclenchés
        if (myRigidbody2D.velocity.sqrMagnitude < MaxSpeed)
        {
           myRigidbody2D.AddForce(direction * Speed);
        }

        //on change l'orientatino du sprite en fonction de sa direction
        if (direction.x < 0)
        {
            mySpriteRenderer.flipX = false;
        }
        else if (direction.x > 0)
        {
            mySpriteRenderer.flipX = true;
        }

        //on lance l'animation isRunning dès que le personnage est en mouvement
        var isRunning = IsOnGround && direction.x != 0;
        myAnimator.SetBool("IsRunning", isRunning);

        //on lance l'animation isJumping quand le personnage n'est pas sur le sol et qu'il effectue un mouvement ascendant sur l'axe y
        var isJumping = IsOnGround == false && myRigidbody2D.velocity.y > 0;
        myAnimator.SetBool("IsAscending", isJumping);

        //on lance l'animation isFalling quand le personnage n'est pas sur le sol et qu'il effectue un mouvement descendant sur l'axe y
        var isFalling = IsOnGround == false && myRigidbody2D.velocity.y < 0;
        myAnimator.SetBool("IsDescending", isFalling);
        myAnimator.SetBool("IsStanding", IsOnGround);
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>(); //la variable direction recupère la position (-1, 0 ou 1) des inputs enclenchés
        Debug.Log(direction);
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero; //quand on arrête d'appuyer sur les inputs, on fait en sorte que le personnage ne reçoive plus aucune force pour qu'il arrête d'avancer
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        //si le bool IsOnGround est vrai, on ajoute une force (JumpForce) vers le haut sur le player
        //puis on repasse le booléen a faux pour arrêter d'ajouter la force même si on continue d'enclencher l'input
        if (IsOnGround == true)
        {
            myRigidbody2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            IsOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //si le Player collisionne avec un GameObject qui a le tag "Ground" le booléen IsOnGround passe en true pour que le Player puisse sauter
        if (other.gameObject.CompareTag("Ground") == true && other.contacts[0].normal == Vector2.up)
        {
            IsOnGround = true;
        }

        //si le Player collisionne avec un GameObject qui a le tag "vide" on lance la fonction GameOver
        if (other.gameObject.CompareTag("DeathZone"))
        {
            Debug.Log("kill");
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(gameObject); //on détruit le gameobject sur lequel le scrip est placé
        SceneManager.LoadScene("GameOverScene"); //on charge et lance la scene qui s'appelle GameOverScene
    }
}

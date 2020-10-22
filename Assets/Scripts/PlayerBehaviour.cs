using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed; //pour pouvoir modifier la valeur de speed dans l'inspector
    [SerializeField] private float MaxSpeed; //pour pouvoir modifier la valeur de MaxSpeed dans l'inspector
    [SerializeField] private float JumpForce; //pour pouvoir modifier la valeur de JumpForce  dans l'inspector

    [SerializeField] private GameObject projectile; //pour pouvoir indiquer quel gameobject correspond à projectile dans l'inspector 

    public GameObject NopeCanvas; //pour pouvoir indiquer dans l'inspector quel gameobject correspond au NopeCanvas et y acceder depuis un autre script

    private Inputs inputs; //on cree une variable inputs de type Inputs
    private Vector2 direction; //on cree une variable direction de type Vector2

    private bool IsOnGround = false; //on cree un bolléen IsOnGround qu'on instancie comme etant false

    private Animator myAnimator; // on cree une variable pour l'animation du personnage
    private Rigidbody2D myRigidbody2D; //on cree une variable de type Rigidbody2D pour pouvoir agir ensuite sur celui du player
    
    public SpriteRenderer mySpriteRenderer; // on cree une variable pour modifier le sprite qui va etre affiché pendant les animations, publique pouvoir y acceder depuis un autre script

    public Vector2 VecteurVisee; //on cree une variable publique pour pouvoir y acceder depuis un autre script

    private int Scorevalue;
    private int Nbcollectibles;

    private void OnEnable()
    {
        //on instancie l'input system créé dans Unity
        inputs = new Inputs();
        inputs.Enable();


        inputs.Player.Move.performed += OnMovePerformed; //quand on appuie sur les inputs de l'action Move de l'action Map player, on lance la fonction OnMovePerformed
        inputs.Player.Move.canceled += OnMoveCanceled; //quand on arrête d'appuyer sur les inputs de l'action Move de l'action Map player, on lance la fonction OnMoveCanceled

        inputs.Player.Jump.performed += OnJumpPerformed; //quand on appuie sur les inputs de l'action Jump de l'action Map player, on lance la fonction OnJumpPerformed

        inputs.Player.Shoot.performed += OnShootPerformed; //quand on appuie sur les inputs de l'action Shoot de l'action Map player, on lance la fonction OnShootPerformed

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
        //on recupere le script Collectible en appelant le perso sur lequel il se trouve avec son tag attribue
        //on recupere les variables ScoreValue et NbCollectiblesScene du script Collectibles
        //on le fait dans l'update pour avoir toujours la bonne valeur correspondant aux variables
        var PlayerGameObject = GameObject.FindWithTag("Player");
        var CollectiblesScript = PlayerGameObject.GetComponent<Collectibles>();

        Scorevalue = CollectiblesScript.ScoreValue;
        Nbcollectibles = CollectiblesScript.NbCollectiblesScene;
    }

    /// <summary>
    /// utilisation de la fonction FixedUpdate quand on agit sur la physique d'un game object
    /// </summary>
    private void FixedUpdate()
    {
        var PlayerDirection = new Vector2(transform.position.x, transform.position.y);
        VecteurVisee = PlayerDirection;

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

    private void OnShootPerformed(InputAction.CallbackContext obj)
    {
        Instantiate(projectile, transform.position, Quaternion.identity); //quand on enclenche l'input, l'objet reference dans projectile est instantie dans la scene
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
            //Debug.Log("kill");
            GameOver();
        }

        //on fait en sorte que le personne ne puisse pas passer au niveau superieur s'il n'a pas ramasse tous les collectibles de la scene
        if(other.gameObject.CompareTag("Level2Launcher") && Scorevalue == Nbcollectibles) 
        {
            //Debug.Log("level2");
            SceneManager.LoadScene("Level2", LoadSceneMode.Single); //si il a tout ramasse, quand il collisionne avec l'objet dont le tag est Level2Launcher, on lance le niveau 2
            NopeCanvas.SetActive(false); //on desactive le NopeCanvas
        }
        else if(other.gameObject.CompareTag("Level2Launcher") && Scorevalue != Nbcollectibles)
        {
            NopeCanvas.SetActive(true); //si on essaye d'acceder au niveau 2 et que tous les collectibles ne sont pas ramasses, on active le NopeCanvas
        }
    }


    private void GameOver()
    {
        Destroy(gameObject); //on détruit le gameobject sur lequel le scrip est placé
        SceneManager.LoadScene("GameOverScene"); //on charge et lance la scene qui s'appelle GameOverScene
    }
}

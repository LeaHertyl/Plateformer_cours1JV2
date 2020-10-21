using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private float speed; // on cree une variable speed modifiable depuis l'inspector

    private Rigidbody2D myRigidbody2D; //on cree une variable de type Rigidbody2D
    private Vector2 VectorMove; //on cree une variable VectorMove de type Vector2
    private bool FlipX; //on cree une variable FlipX de type booleen

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>(); //on recupere le composant Rigidbody2D du projectile pour pouvoir agir dessus

        //on recupere le script PlayerBehaviour en cherchant le gameobject de la scene auquel il est associe a l'aide du Tag de celui-ci
        var PlayerGameObject = GameObject.FindWithTag("Player");
        var PlayerBehaviourScript = PlayerGameObject.GetComponent<PlayerBehaviour>();

        //on donne comme valeur aux variables VectorMove et FlipX des elements du script PlayerBehaviour
        VectorMove = PlayerBehaviourScript.VecteurVisee;
        FlipX = PlayerBehaviourScript.mySpriteRenderer.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        //on ajoute une force au projectile en fonction de l'orientation du sprite du Player pour qu'il tire dans le sens de son orientation
        if(FlipX == true)
        {
            myRigidbody2D.velocity = new Vector2(VectorMove.x * -speed, VectorMove.y);
        }
        else if (FlipX == false)
        {
            myRigidbody2D.velocity = new Vector2(VectorMove.x * speed, VectorMove.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Destroyprojectile");
            Destroy(gameObject);
        }
    }
}

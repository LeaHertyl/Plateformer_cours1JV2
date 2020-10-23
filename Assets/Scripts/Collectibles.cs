using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score; //pour pouvoir appeler le textMesh qui va afficher le score dans l'inspector
    [SerializeField] private GameObject CanvasWin; //pour pouvoir indiquer quel gameobject correspond à CanvasWin dans l'inspector

    public int NbCollectiblesScene; //pour pouvoir modifier la valeur de NbCollectiblesScene dans l'inspectoret y acceder depuis d'autres scripts
    public int ScoreValue; //nombre entier -> le nombre de collectibles ramasses

    private GameObject NopeCanvasToDestroy; //on cree une variable qui s'appelle NopeCanvasToDestroy de type GameObject

    // Start is called before the first frame update
    void Start()
    {
        ScoreValue = 0; //on instancie le score à 0 au lancement de la scene

        //on recupere le script PlayerBehaviour en appelant le perso sur lequel il se trouve avec son tag attribue
        //on recupere le NopeCanvas du script PlayerBehaviour
        var PlayerGameObject = GameObject.FindWithTag("Player");
        var PlayerBehaviourScript = PlayerGameObject.GetComponent<PlayerBehaviour>();

        NopeCanvasToDestroy = PlayerBehaviourScript.NopeCanvas;
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Mouches collectées : " + ScoreValue + "/" + NbCollectiblesScene ; //à chaque frame on met à jour le nombre affiche de collectibles ramasses
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FlyCollectible")) //quand le player trigger un gameobject dont le tag est "FlyCollectible"
        {
            //Debug.Log("youpi");
            Destroy(other.gameObject); //on détruit le gameobjet avec lequel le player a trigger
            ScoreValue++; //on ajoute 1 au score affiché à chaque fois que le player trigger un gameobject dont le tag est "FlyCollectible"
        }

        AllFlies(); //on lance la fonction AllFlies quand l'evenement OnTriggerEnter2D est lancé
        
    }

    private void AllFlies()
    {
        //si les variables ScoreValue et NbCollectiblesScene sont égales, on instantie le CanvasWin et on detruit la variable correspondant au NopeCanvas du script PlayerBehaviour
        if(ScoreValue == NbCollectiblesScene)
        {
            Instantiate(CanvasWin);
            Destroy(NopeCanvasToDestroy);
        }
    }
}

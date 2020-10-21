using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score; //pour pouvoir appeler le textMesh qui va afficher le score dans l'inspector

    public int NbCollectiblesScene; //pour pouvoir modifier la valeur de NbCollectiblesScene dans l'inspectoret y acceder depuis d'autres scripts

    [SerializeField] private GameObject CanvasWin; //pour pouvoir indiquer quel gameobject correspond à CanvasWin dans l'inspector

    public int ScoreValue; //nombre entier -> le nombre de collectibles ramasses

    private GameObject NopeCanvasToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        ScoreValue = 0; //on instancie le score à 0 au lancement de la scene

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

        AllFlies();
        
    }

    private void AllFlies()
    {
        if(ScoreValue == NbCollectiblesScene)
        {
            Instantiate(CanvasWin);
            Destroy(NopeCanvasToDestroy);
        }
    }
}

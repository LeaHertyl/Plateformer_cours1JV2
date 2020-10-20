using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectibles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score; //pour pouvoir appeler le textMesh qui va afficher le score dans l'inspector
    private int ScoreValue; //nombre entier -> le nombre de collectibles ramasses

    // Start is called before the first frame update
    void Start()
    {
        ScoreValue = 0; //on instancie le score à 0 au lancement de la scene
    }

    // Update is called once per frame
    void Update()
    {
        Score.text = "Mouches collectées : " + ScoreValue; //à chaque frame on met à jour le nombre affiche de collectibles ramasses
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("FlyCollectible")) //quand le player trigger un gameobject dont le tag est "FlyCollectible"
        {
            //Debug.Log("youpi");
            Destroy(other.gameObject); //on détruit le gameobjet avec lequel le player a trigger
            ScoreValue++; //on ajoute 1 au score affiché à chaque fois que le player trigger un gameobject dont le tag est "FlyCollectible"
        }
        
    }
}

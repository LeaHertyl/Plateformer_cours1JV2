using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void playBouton()
    {
        Debug.Log("playscene");
        //SceneManager.LoadScene("SampleScene");//Charge la scène du jeu
    }

    //Fonction pour le bouton "Crédits"
    public void creditsBouton()
    {
        Debug.Log("creditscene");
        //SceneManager.LoadScene("Credits");//Charge la scène de crédits
    }

    public void menuScene()
    {
        Debug.Log("menuScene");
        //SceneManager.LoadScene("MenuScene");
    }

    //Fonction pour le bouton "Quitter"
    public void quitBouton()
    {
        Debug.Log("Ferme le jeu");//Code de débug pour voir si le bouton réagit bien.
        //Application.Quit();//Ferme et Arrête l'application
    }
}

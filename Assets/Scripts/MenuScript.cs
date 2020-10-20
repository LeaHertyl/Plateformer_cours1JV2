using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void playBouton() //fonction publique pour pouvoir la sélectionner dans l'evenement OnClick des boutons
    {
        Debug.Log("playscene"); //Code de débug pour voir si le bouton réagit bien.
        SceneManager.LoadScene("SampleScene");//Charge la scène du jeu
    }

    public void creditsBouton()
    {
        Debug.Log("creditscene");
        SceneManager.LoadScene("Credits");//Charge la scène de crédits
    }

    public void menuScene()
    {
        Debug.Log("menuScene");
        SceneManager.LoadScene("MenuScene"); //charge la scene du menu
    }

    public void quitBouton()
    {
        Debug.Log("Ferme le jeu");
        //Application.Quit();//Ferme et Arrête l'application
    }
}

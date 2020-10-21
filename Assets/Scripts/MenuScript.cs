using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    [SerializeField] private GameObject Loading_Screen; // on reference le gameobject dans l'inspector

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

    public void ChargementBeforeScene(string SceneToLoad) // permet de choisir la scene que nous voulons charger lors de l'activation du bouton
    {
        StartCoroutine(Load(SceneToLoad)); //commence le chargement de la scene en parallèle
    }

    private IEnumerator Load(string SceneToLoad)  //permet de lancer les différentes animations faites
    {
        var Loading_ScreenInstance = Instantiate(Loading_Screen); // on instancie le canvas sur laquelle se trouve l'animation
        //DontDestroyOnLoad(Loading_ScreenInstance);  // on lui demande de ne pas supprimer le canvas apres avoir joué l'animation
        var loadingAnimator = Loading_ScreenInstance.GetComponent<Animator>(); // on recupere le composant animator dans le canvas
        var animationTime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;  //cela calcul le temps de l'animation
        
        var loading = SceneManager.LoadSceneAsync(SceneToLoad); //permet de charger a scene en arriere plan pendant l'animation

        loadingAnimator.SetBool("EndLoading", true);
        var animationtime = loadingAnimator.GetCurrentAnimatorStateInfo(0).length;

        loading.allowSceneActivation = false;  // cela descative le canvas où se trouve l'animation

        while (!loading.isDone) // tant que la scene n'a pas atteint 100% de chargement, elle ne lance pas la scene
        {
            if (loading.progress >= 0.9f)
            {
                loading.allowSceneActivation = true;
                loadingAnimator.SetTrigger("EndLoading");
            }

            yield return new WaitForSeconds(animationTime + animationtime); // c'est le temps d'attente que toutes les anims soient jouées
        }

        //Destroy(Loading_Screen);
    }
}

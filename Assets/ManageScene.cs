using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScene : MonoBehaviour
{
    public static void GoToTheNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void StartAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quitter(){
        Application.Quit();
        Debug.Log("L'application s'est bien arrêtée");
    }

}

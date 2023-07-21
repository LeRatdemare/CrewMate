using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonValidate : MonoBehaviour
{
    //public SelectionButton selectionButton;
    public void Validate(){
        bool complet = true;//Initialisation de la variable qui vérifie que le joueur a rentré toutes les infos
        for (int i=0;i<2;i++){
            if (SelectionButton.bouttonPrevious[i]==0){
                complet =false;
            }
        }
        if (complet==true){
            ManageScene.GoToTheNextScene();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Valider : MonoBehaviour
{
    public static List<int> parametrePartie = new List<int>{0,0,0,0,0};
    // il y a 5 valeurs 
    public CheckInt checkInt;
    //public SelectionButton selectionButton;
    // Changer variable du jeton de bool vers int (0/1)
    public void ButtonValider(){
        bool complet = true;
        Debug.Log("here1");
            if (SelectionButton.bouttonPrevious[2]!=0){
                Debug.Log("here2");
                SelectionButton.bouttonPrevious[2] = parametrePartie[2];
                Debug.Log("here3");
                for(int i=0; i<2;i++){
                    if(CheckInt.MissionTentative[i] !=0){
                        Debug.Log("here4");
                        CheckInt.MissionTentative[i] = parametrePartie[3+i];
                        Debug.Log("here5");
                    }
                    else{
                        complet= false;
                        break;
                    }
                }
            }    
            else{
                complet = false;
            }
        if(complet==true){
            SceneManager.LoadScene("Jeu");
        }
    }
}

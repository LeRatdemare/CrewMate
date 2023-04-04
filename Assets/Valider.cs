using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Valider : MonoBehaviour
{
    public static List<int> parametrePartie = new List<int>{0,0,0,0,0};
    public CheckInt checkInt;
    public SelectionButton selectionButton;
    // Changer variable du jeton de bool vers int (0/1)
    public bool ButtonValider(){
        int s=0;
        for (int i=0; i<3;i++){
            if (SelectionButton.bouttonPrevious[i]!=0){
                SelectionButton.bouttonPrevious[i] = parametrePartie[i]; 
                s++;
            }
            else{
                return false;
            }
        }
        for(int i=0; i<2;i++){
            if(CheckInt.MissionTentative[i] ==0){
                CheckInt.MissionTentative[i] = parametrePartie[s+i];
            }
            else{
                return false;
            }   
        }
        SceneManager.LoadScene("");
        return true;

        

        

        
        
        //Vérif que soit 2 soit 5
        //load Scene(nameScene)
    }
}

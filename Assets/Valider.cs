using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Script qui est appelé quand on clique sur le bouton validé,
// et qui vérifie que tous les paramètres Jeux ont été rempis
// avant de load la Scène Jeu
public class Valider : MonoBehaviour
{
    public static List<int> parametrePartie = new List<int>{0,0,0,0,0}; // récupération des param dans une liste vide
    // Liste pour stocker les 5 paramètres du jeu :
    // (0-Nombre de joueurs)
    // (1-Nombre de couleurs)
    // (2-Jeton uttilisé oui = 1 non = 2 )
    // (3-Mission Numéro)
    // (4- Tentative numéro)

    //Script SelectionButton est appelé dans la scène OptionMenu et SettingMenu à chaque fois que le joueur clique 
    //sur un bouton pour rentrer une info
    //Script CheckInt est appelé dans la scène SettingMenu pour les deux InputField
    //Il stocke les infos rentré par l'uttilisateur dans les inputField
    public void ButtonValider(){
        bool complet = true; // complet = false si le form n'a pas été complété entièrement avec les infos
            if (SelectionButton.bouttonPrevious[2]!=0){//si la valeur de cet item est 0 alors c'est que l'uttilisateur n'as pas rempli la case
                //On ne vérifie que pour l'indice 2 car les autres auront été validé dans la scène précédentes
                for (int i=0;i<3;i++){
                    parametrePartie[i]=SelectionButton.bouttonPrevious[i]; //La case a été rempli, sa valeur est passé à la liste paramètrePartie
                }
                for(int i=0; i<2;i++){//Même schéma avec les infos récupéré dans la scène SettingMenu
                    if(CheckInt.MissionTentative[i] !=0){
                        parametrePartie[3+i]=CheckInt.MissionTentative[i] ;
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
            Debug.Log($"Liste[0]: {parametrePartie[0]}, Liste[1]: {parametrePartie[1]}, Liste[2]:{parametrePartie[2]}, Liste[3]:{parametrePartie[3]} , Liste[4]: {parametrePartie[4]}");
            SceneManager.LoadScene("Jeu");
        }
    }
}

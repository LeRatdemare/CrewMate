using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour
{
    //public static int bouttonJoueur;
    //public List<GameObject> boutons = new List<GameObject>{GameObject.Find("ButtonPlayer3"),GameObject.Find("ButtonPlayer4"),GameObject.Find("ButtonPlayer5")}; 

    public void ButtonSelected(int numero){
        //bouttonJoueur = numero;
        GameObject.Find("ButtonPlayer"+$"{numero}").GetComponent<Image>().sprite=Resources.Load<Sprite>("Images/Sybille/BoutonPressed");
    }
        
        
    
}

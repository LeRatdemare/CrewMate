using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMessageIntoTheScene : MonoBehaviour
{
    string nom;
    public static Dictionary<string, GameObject> TextPresentDansLaScene = new Dictionary<string, GameObject>();
   
    void Start(){
        nom = this.gameObject.name;
        Debug.Log(nom);
        TextPresentDansLaScene.Add(nom, this.gameObject);
    }
        
    public void AfficherText(string message){
        GetComponent<TMP_Text>().text = message;
    }
}

//Avoir une classe qui s'appelle textMessageIntoTheScene, avoir une liste static qui pointe vers tous les
//gameObject de cette scène et dès qu'on a une nouvelle instanciation d'un prefab il est mis dans la liste

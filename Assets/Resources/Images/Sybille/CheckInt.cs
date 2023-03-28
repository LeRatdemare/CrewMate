using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInt : MonoBehaviour
{
    public void CheckValeur(string name){
        int i;
        Debug.Log("Bonjour,");
        Text s = GameObject.Find(name).GetComponent<Text>(); 
        Debug.Log("Bonjour2"); 
        Debug.Log("Bonjour3");
        string input = s.text;
        Debug.Log("Bonjour4");
        bool result = int.TryParse(input, out i);
        if (result == false){
            for(int j =0; j<7;j++){
                GameObject.Find("ErreurIntMiss").SetActive(true);// ca va pas le nom de l'objet
                System.Threading.Thread.Sleep(500);
                GameObject.Find("ErreurIntMiss").SetActive(false);
            }

        }
    }
}

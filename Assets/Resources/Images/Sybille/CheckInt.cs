using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInt : MonoBehaviour
{
    public GameObject erreur;
    public InputField b;
    public static List<int> MissionTentative = new List<int>(){0,0};
    public void CheckValeur(){
        int i;
        
        //Debug.Log("You entered :" + message);
        //InputField input = GameObject.Find("InputField2").GetComponent<InputField>();
        //GameObject input = GameObject.Find("InputField2/Text Area");
        //InputField input = GameObject.Find("InputField2/Text Area").GetComponent<InputField>();
        //Debug.Log(GameObject.Find("InputField2/Text Area/Text"));
        //Debug.Log("hé");
        //Debug.Log(input.text);
        //Text s = input.GetComponent<Text>();
        //string input = b.text;
        //Text s = input.textComponent;
        //Debug.Log("Bonjour2.5");
        //Debug.Log(s);
        //Debug.Log("Bonjour2"); 
        //Debug.Log("Bonjour3");
        //string input = s.text;
        //Debug.Log("Bonjour4");
        bool result = int.TryParse(b.text, out i);
        if (result == false || i>50){
            ChangeValueListe(0);
            b.Select();
            b.text ="";
            StartCoroutine(Wait());  
        }
        else{
            ChangeValueListe(i);
        }
        //Debug.Log($"Valeur Liste[0] : {MissionTentative[0]}, Valeur Liste[1] : {MissionTentative[1]}");
    }

        IEnumerator Wait()
        {
            for (int i=0; i<4;i++){
                erreur.SetActive(true);
                yield return new WaitForSeconds(1);   
                erreur.SetActive(false);
                yield return new WaitForSeconds(1);  
            }
            
        }

        public void ChangeValueListe(int nombre){
            if (b.name == "InputField1"){
                MissionTentative[0]= nombre;
            }
            else{
                MissionTentative[1]= nombre;
            }
        }

}

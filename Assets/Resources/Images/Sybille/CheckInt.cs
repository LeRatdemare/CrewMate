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

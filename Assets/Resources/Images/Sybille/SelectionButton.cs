using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour
{
    public static List<int> bouttonPrevious = new List<int>{0,0,0};// potentiellement que ca sert à rien de faire une liste et qu'on peut remplacer ca par un int, car il y a différentes instance du même script (à vérifier)
    public Sprite sp1;
    public Sprite sp2;
    public string typeBoutton;
    public int type;
   
    public void ButtonSelected(int numero){
        if (bouttonPrevious[type] != numero && bouttonPrevious[type] !=0){
            GameObject.Find(typeBoutton+$"{bouttonPrevious[type]}").GetComponent<Image>().overrideSprite=sp2;
        }
        GameObject.Find(typeBoutton+$"{numero}").GetComponent<Image>().overrideSprite=sp1;
        //GameObject.Find(typeBoutton+$"{numero}").GetComponent<Text>().FontStyle.Bold
        bouttonPrevious[type] = numero;
        //Debug.Log($"boutton previous = {bouttonPrevious[0]},{bouttonPrevious[1]},{bouttonPrevious[2]}");
    }  
}
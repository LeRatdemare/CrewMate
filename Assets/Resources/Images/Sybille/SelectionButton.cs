using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionButton : MonoBehaviour
{
    //public static int bouttonJoueur =0;
    //public List<GameObject> boutons = new List<GameObject>{GameObject.Find("ButtonPlayer3"),GameObject.Find("ButtonPlayer4"),GameObject.Find("ButtonPlayer5")}; 
    public static List<int> bouttonPrevious = new List<int>{0,0};
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
    }
        
        
    
}

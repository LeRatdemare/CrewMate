using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionButton : MonoBehaviour
{
    public static int BouttonJoueur; 
    public static List<GameObject> boutons = new List<GameObject> (){GameObject.Find("ButtonPlayer3"),GameObject.Find("ButtonPlayer4"), GameObject.Find("ButtonPlayer5")};

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckInt : MonoBehaviour
{
    public void CheckValue(string name){
        //int i;
        GameObject.Find(name).GetComponent<Text>();  
        //bool result = int.TryParse(s, out i);
    }
}

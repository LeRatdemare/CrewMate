using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstPlayerSelectionButton : MonoBehaviour
{
    public int numPlayer;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // On récupère le message de la popup
        tmp.text = (numPlayer == 0) ? "User" : $"Player {numPlayer}";
    }
    void NextPhase()
    {

    }
}

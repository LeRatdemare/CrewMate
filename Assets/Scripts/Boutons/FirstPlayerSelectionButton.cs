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
        tmp.SetText($"Player {numPlayer}");
    }

    // Update is called once per frame
    void Update()
    {

    }
}

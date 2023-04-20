using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstPlayerSelectionButton : MonoBehaviour
{
    GameManager gameManager;
    TheCrewGame theCrewGame;
    public int numPlayer;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();

        TextMeshProUGUI tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // On récupère le message de la popup
        tmp.text = (numPlayer == 0) ? "User" : $"Player {numPlayer}";
    }
    public void OnClick()
    {
        // On met à jour les valeurs du dictionnaire des joueurs pour que le 1er joueur ait la valeur 0
        theCrewGame.currentPlayer = numPlayer;
        gameManager.BoutonSuivant.SetActive(true);
        Debug.Log((numPlayer == 0) ? "Vous êtes maintenant le 1er joueur." : $"Le 1er joueur est maintenant le joueur {numPlayer}.");
    }
}

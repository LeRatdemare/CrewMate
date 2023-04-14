using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoutonSuivant : MonoBehaviour
{
    TheCrewGame theCrewGame;
    GameManager gameManager;
    void Start()
    {
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void OnClick()
    {
        switch (theCrewGame.GamePhase)
        {
            case TheCrewGame.Phase.UserCardsSelection:
                if (gameManager.handPanel.GetCardsCount() == theCrewGame.NbPlis)
                    theCrewGame.GamePhase = TheCrewGame.Phase.FirstPlayerSelection;
                else
                    return; // Appeler la popup message "Vous n'avez pas sélectionné toutes vos cartes"
                break;
            case TheCrewGame.Phase.FirstPlayerSelection:
                break;
            case TheCrewGame.Phase.UserPlaying:
                break;
            case TheCrewGame.Phase.UserCommunicating:
                break;
            case TheCrewGame.Phase.OtherPlayerPlaying:
                break;
        }
    }
}

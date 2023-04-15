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
                int handSize = gameManager.handPanel.GetCardsCount();
                int nbCardsToDraw = theCrewGame.NbPlis;
                if (handSize == nbCardsToDraw)
                {
                    theCrewGame.GamePhase = TheCrewGame.Phase.FirstPlayerSelection;
                    gameManager.FirstPlayerSelectionPopup.SetActive(true);
                }
                else
                {
                    // Appeler la popup message "Vous n'avez pas sélectionné toutes vos cartes"
                    gameManager.TimedMessagePopup.SetTitle("==Erreur=="); // La 1ère fois qu'on clique cette méthode ne marche pas...
                    gameManager.TimedMessagePopup.SetMessage($"Vous n'avez pas sélectionné toutes vos cartes...\nVous en avez sélectionné {handSize} au lieu de {nbCardsToDraw}.");
                    gameManager.TimedMessagePopup.timeBeforeDeath = 7;
                    gameManager.TimedMessagePopup.SetActive(true);
                }
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
        Debug.Log($"Etape suivante, phase actuelle : {theCrewGame.GamePhase}");
    }
}

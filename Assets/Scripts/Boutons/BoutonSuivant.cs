using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BoutonSuivant : MonoBehaviour
{
    TheCrewGame theCrewGame;
    GameManager gameManager;
    void Start()
    {
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /* !!! RAPPEL !!!
    Remettre la variable Navigation du composant Button à "Automatique"
    pour pouvoir l'utiliser avec une touche du clavier */
    public void OnClick()
    {
        switch (theCrewGame.GamePhase)
        {
            // On vérifie que le joueur ait sélectionné le bon nombre de cartes, avant de passer à la phase suivante
            case TheCrewGame.Phase.UserCardsSelection:
                int handSize = gameManager.handPanel.GetCardsCount();
                int nbCardsToDraw = theCrewGame.NbPlis;
                if (handSize == nbCardsToDraw)
                {
                    theCrewGame.GamePhase = TheCrewGame.Phase.FirstPlayerSelection;
                    gameManager.FirstPlayerSelectionPopup.SetActive(true);
                    // On cache le bouton suivant
                    SetActive(false);
                }
                else
                {
                    // Appeler la popup message "Vous n'avez pas sélectionné toutes vos cartes"
                    gameManager.TimedMessagePopup.SetTitle("==Erreur=="); // La 1ère fois qu'on clique cette méthode ne marche pas...
                    gameManager.TimedMessagePopup.SetMessage($"Vous devez d'abord sélectionner {nbCardsToDraw} cartes.\nNombre de cartes sélectionnées pour le moment : {handSize}.");
                    gameManager.TimedMessagePopup.timeBeforeDeath = 6;
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

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

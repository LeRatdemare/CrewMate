using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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
        // On déclare les variables pour les popups
        string msg;
        string title;
        switch (theCrewGame.GamePhase)
        {
            // On vérifie que le joueur ait sélectionné le bon nombre de cartes, avant de passer à la phase suivante
            case TheCrewGame.Phase.UserCardsSelection:
                int handSize = gameManager.handPanel.GetCardsCount();
                int nbCardsToDraw = theCrewGame.NbPlis;
                if (handSize == nbCardsToDraw)
                {
                    theCrewGame.GamePhase = TheCrewGame.Phase.FirstPlayerSelection;
                    gameManager.tableauCartes.SetState(TableauCartes.State.Hiden);
                    gameManager.FirstPlayerSelectionPopup.SetActive(true);
                    // On cache le bouton suivant
                    SetActive(false);
                }
                else
                {
                    // Appeler la popup message "Vous n'avez pas sélectionné toutes vos cartes"
                    title = "==Erreur==";
                    msg = $"Vous devez d'abord sélectionner {nbCardsToDraw} cartes.\nNombre de cartes sélectionnées pour le moment : {handSize}.";
                    gameManager.ShowMessagePopup(msg, 6, title);
                }
                break;
            /* Ici pas besoin de vérifier quoi que ce soit, le bouton n'apparaît
            que si l'utilisateur a sélectionné au moins 1 bouton.*/

            case TheCrewGame.Phase.FirstPlayerSelection:
                gameManager.FirstPlayerSelectionPopup.SetActive(false);

                // On passe soit au tour de l'utilisateur soit à celui d'un adversaire
                if (theCrewGame.currentPlayer == 0)
                {
                    theCrewGame.GamePhase = TheCrewGame.Phase.UserPlaying;

                    // On annonce au joueur que c'est à lui de jouer
                    title = "Tour de l'utilisateur";
                    msg = $"A toi de jouer !";
                    gameManager.ShowMessagePopup(msg, 2, title, TextAlignmentOptions.Center);
                }
                else
                {
                    theCrewGame.GamePhase = TheCrewGame.Phase.OtherPlayerPlaying;

                    // On annonce au joueur que c'est à lui de jouer
                    title = "Tour des autres";
                    msg = $"Au joueur {theCrewGame.currentPlayer} de jouer.";
                    gameManager.ShowMessagePopup(msg, 2, title, TextAlignmentOptions.Center);
                }
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

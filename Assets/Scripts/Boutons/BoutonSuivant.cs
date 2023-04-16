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
            // Si le joueur a sélectionné le bon nombre de cartes, on passe à la phase suivante
            case TheCrewGame.Phase.UserCardsSelection:
                int handSize = gameManager.handPanel.GetCardsCount();
                int nbCardsToDraw = theCrewGame.NbPlis;
                if (handSize == nbCardsToDraw)
                    theCrewGame.GamePhase = TheCrewGame.Phase.FirstPlayerSelection;
                // Si le joueur n'a pas sélectionné le bon nombre de carte
                else
                {
                    // On le notifie seulement
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
                    theCrewGame.GamePhase = TheCrewGame.Phase.UserPlaying;
                else
                    theCrewGame.GamePhase = TheCrewGame.Phase.OtherPlayerPlaying;
                break;

            case TheCrewGame.Phase.UserPlaying:
                gameManager.BoutonCommuniquer.SetActive(false);

                // On récupère la carte sélectionnée et on la met dans le Slot correspondant du Pli
                Card selectedCard = gameManager.handPanel.GetSelectedCard();
                // gameManager.pli.Slots[0].Activer(selectedCard, Card.ConteneurCarte.Pli);
                gameManager.pli.transform.GetChild(0).GetComponent<Card>().Activer(selectedCard, Card.ConteneurCarte.Pli);
                selectedCard.Desactiver();
                // On déselectionne toutes les cartes
                gameManager.handPanel.DeselectAllCards();

                theCrewGame.GamePhase = TheCrewGame.Phase.OtherPlayerPlaying;
                break;
            case TheCrewGame.Phase.UserCommunicating:
                gameManager.BoutonCommuniquer.SetActive(false);
                // A coder...
                break;
            case TheCrewGame.Phase.OtherPlayerPlaying:
                gameManager.BoutonCommuniquer.SetActive(false);
                // A coder...
                break;
            case TheCrewGame.Phase.OtherPlayerCommunicating:
                gameManager.BoutonCommuniquer.SetActive(false);
                // A coder...
                break;
        }
        Debug.Log($"Etape suivante, phase actuelle : {theCrewGame.GamePhase}");
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

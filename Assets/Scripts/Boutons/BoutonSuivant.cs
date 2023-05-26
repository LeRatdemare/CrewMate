using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BoutonSuivant : MonoBehaviour
{
    TheCrewGame theCrewGame;
    GameManager gameManager;
    public enum InfoJoueur
    {
        Tache=0, Communication=1
    }
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
        Card selectedCard;
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
                theCrewGame.capitaine = theCrewGame.currentPlayer;
                theCrewGame.GamePhase = TheCrewGame.Phase.TasksSelection;
                break;

            case TheCrewGame.Phase.TasksSelection:
                int numeroSlot=0;
                List<Card> lesCartesSelectionnees = gameManager.tableauTache.GetSelectedCard();
                if (lesCartesSelectionnees.Count!=0 || theCrewGame.capitaine!= theCrewGame.currentPlayer )//vérifier qu'une liste vide a bien une longueur de 0
                {
                    foreach(Card maCarte in lesCartesSelectionnees)
                    {
                        //gameManager.pli.transform.GetChild(theCrewGame.currentPlayer).GetComponent<Card>().Activer(maCarte, Card.ConteneurCarte.Pli);
                        /*Il faudra remplacer pli par le nouveau conteneur avec les 3 slots mais modifs un peu la tecnhiques étant
                        donné qu'on ne connait pas le nombre de tâche par personne. Il faudra probablement créer une fonction spécifique 
                        qui attribue en fonction du joueur un slot particulier avec un système 01 11 21 ect avec le premier nombre = le joueur 
                        et le deuxième le numéro du slot qui lui est attribué*/
                        //GameObject.Find("Player"+theCrewGame.currentPlayer)puis trouver le grandChild
                        //faire un truc différent pour le user en prenant PlayerInfoPanel
                        //faire une enum en mode Communication ou Tache avec (int)Tache et (int)Communication
                        GameObject.Find($"Slot{theCrewGame.currentPlayer}{(int)InfoJoueur.Tache}{numeroSlot}").GetComponent<Card>().Activer(maCarte, Card.ConteneurCarte.Tache);
                        //GameObject slot = gameManager.playersInfoPanel.transform.GetChild(theCrewGame.currentPlayer).GetChild((int)Tache).GetChild(i);
                        maCarte.Desactiver();
                        numeroSlot++;
                    }
                    // On déselectionne toutes les cartes
                    gameManager.tableauTache.DeselectAllCards();
                    theCrewGame.NextPlayer();//C'est dans NextPlayer que va se décider si on reste dans la phase TaskSelection ou si on change
                }
                
                //Ca serait cool qu'en fonction de la mission indiquée, cela soit relié à un dictionnaire qui sache combien de tâche par mission sont demandées
        
                else //On s'assure que le joueur capitaine rentre bien une tache
                {
                    title = "==Erreur==";
                    msg = (theCrewGame.capitaine == 0) ? "Vous devez selectionner au moins une tâche car vous êtes le capitaine" : $"Vous devez rentrer au moins une tache pour le joueur {theCrewGame.currentPlayer} car il s'agit du capitaine, vous n'avez pas encore sélectionné ses taches...";
                    gameManager.ShowMessagePopup(msg, 6, title);
                }
            
                // A coder...
                break;

            case TheCrewGame.Phase.UserPlaying:
                // On récupère la carte sélectionnée et on la met dans le Slot correspondant du Pli
                selectedCard = gameManager.handPanel.GetSelectedCard();
                // S'il en a sélectionné une on l'ajoute au Slot correspondant du pli
                if (selectedCard != null)
                {
                    gameManager.pli.transform.GetChild(0).GetComponent<Card>().Activer(selectedCard, Card.ConteneurCarte.Pli);
                    selectedCard.Desactiver();
                    // On déselectionne toutes les cartes
                    gameManager.handPanel.DeselectAllCards();

                    theCrewGame.NextPlayer();
                }
                // Si il n'a pas sélectionné de carte on le notifie  
                else
                {
                    title = "==Erreur==";
                    msg = $"C'est à votre tour de jouer, vous n'avez pas encore sélectionné de carte...";
                    gameManager.ShowMessagePopup(msg, 6, title);
                }
                break;
            case TheCrewGame.Phase.UserCommunicating:
                gameManager.BoutonCommuniquer.SetActive(false);
                theCrewGame.NextPlayer();
                // A coder...
                break;
            case TheCrewGame.Phase.OtherPlayerPlaying:
                gameManager.BoutonCommuniquer.SetActive(false);

                // Si le joueur a sélectionné une carte, on la met dans le pli au slot correspondant
                selectedCard = gameManager.tableauCartes.GetSelectedCard();
                if (selectedCard != null)
                {
                    gameManager.pli.transform.GetChild(theCrewGame.currentPlayer).GetComponent<Card>().Activer(selectedCard, Card.ConteneurCarte.Pli);
                    selectedCard.Desactiver();
                    // On déselectionne toutes les cartes
                    gameManager.tableauCartes.DeselectAllCards();
                    theCrewGame.NextPlayer();
                }
                // Si il n'a pas sélectionné de carte on le notifie  
                else
                {
                    title = "==Erreur==";
                    msg = $"C'est toujours au joueur {theCrewGame.currentPlayer} de jouer, vous n'avez pas encore sélectionné sa carte...";
                    gameManager.ShowMessagePopup(msg, 6, title);
                }
                // A coder...
                break;
            case TheCrewGame.Phase.OtherPlayerCommunicating:
                gameManager.BoutonCommuniquer.SetActive(false);
                theCrewGame.NextPlayer();
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

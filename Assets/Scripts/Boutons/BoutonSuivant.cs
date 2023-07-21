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
                gameManager.handPanel.gameObject.SetActive(false);
                int numeroSlot=0;
                List<Card> lesCartesSelectionnees = gameManager.tableauTache.GetSelectedCard();
                if (lesCartesSelectionnees.Count!=0 || theCrewGame.capitaine!= theCrewGame.currentPlayer )//vérifier qu'une liste vide a bien une longueur de 0
                {
                    foreach(Card maCarte in lesCartesSelectionnees)
                    {
                        Card emplacementTache = GameObject.Find($"Slot{theCrewGame.currentPlayer}{(int)InfoJoueur.Tache}{numeroSlot}").GetComponent<Card>();
                        emplacementTache.Activer(maCarte, Card.ConteneurCarte.Tache);
                        theCrewGame.Joueurs[theCrewGame.currentPlayer].remainingTasks.Add(emplacementTache);//On met à jour la liste des taches pour le joueur pour qui on vient de sélectionner les cartes
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
                break;

            case TheCrewGame.Phase.UserPlaying:
                // On récupère la carte sélectionnée et on la met dans le Slot correspondant du Pli
                selectedCard = gameManager.handPanel.GetSelectedCard();
                // S'il en a sélectionné une on l'ajoute au Slot correspondant du pli
                if (selectedCard != null)
                {
                    if (theCrewGame.User.communication==false){
                        theCrewGame.User.RetirerMaxMinCouleur((int)selectedCard.Color, selectedCard.Value);//On continue à actualiser la plus haute et la plus basse carte que le joueur a en main
                    }
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
                //gameManager.BoutonCommuniquer.SetActive(false);

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
                break;
            case TheCrewGame.Phase.OtherPlayerCommunicating:
                selectedCard = gameManager.choixjetons.GetSelectedCard();
                if (selectedCard != null)//IL VA FALLOIR VERIFIER QU'ON NE PUISSE PAS CLIQUER SUR LES CARTES DANS HANDPANEL OU AUTRE
                {
                    int nb;
                    string nom = selectedCard.name.Remove(0,5);//Cela correspond à l'index du mot "Carte" dans "Carte0", "Carte1", "Carte2"..ect
                    int.TryParse(nom, out nb);//Si nb=0 jeton en haut si nb= 1 jeton au milieu et 2 jeton en bas    jn,
                    GameObject.Find($"Player{theCrewGame.currentPlayer}/Communication{theCrewGame.currentPlayer}").transform.GetChild(nb).GetComponent<SpriteRenderer>().sprite =gameManager.jetonSprite;
                    selectedCard.Desactiver();
                    gameManager.choixjetons.DeselectAllCards();//A voir si l'instruction n'a pas déjà été mise ailleurs
                    gameManager.choixjetons.gameObject.SetActive(false);
                    theCrewGame.GamePhase = TheCrewGame.Phase.OtherPlayerPlaying;
                } 
                else
                {
                    title = "==Erreur==";
                    msg = $"Vous devez sélectionner un emplacement pour le jeton communication";
                    gameManager.ShowMessagePopup(msg, 6, title);
                }
                break;
        }
        Debug.Log($"Etape suivante, phase actuelle : {theCrewGame.GamePhase}");
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

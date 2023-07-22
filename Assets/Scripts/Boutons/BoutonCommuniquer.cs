using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonCommuniquer : MonoBehaviour
{
    TheCrewGame theCrewGame;
    GameManager gameManager;
    void Start()
    {
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClick()//Est ce que ca vaut vraiment le coup de faire un switch ?
    {
        string msg;
        string title;
        Card selectedCard;
        int currentPlayer = theCrewGame.currentPlayer;
        switch (theCrewGame.GamePhase)
        {
            case TheCrewGame.Phase.UserPlayingOrCommunicating:
                selectedCard = gameManager.handPanel.GetSelectedCard();
                if (selectedCard != null)
                {
                    string communication = theCrewGame.User.CommuniquerOuPas(selectedCard);
                    if(theCrewGame.Joueurs[0].communication == false){
                        
                        switch(communication){
                            case "Rien":
                                title = "==Erreur==";
                                msg = $"Cette carte n'est ni la plus petite, ni la plus grande, ni la seule de sa couleur vous ne pouvez donc pas la communiquer";
                                gameManager.ShowMessagePopup(msg, 6, title);
                                break;
                            case "Atout":
                                title = "==Erreur==";
                                msg = $"Cette carte est un atout, vous ne pouvez donc pas la communiquer";
                                gameManager.ShowMessagePopup(msg, 6, title);
                                break;
                            default :
                                GameObject CommunicationJoueur = GameObject.Find($"Communication{0}");
                                CommunicationJoueur.GetComponent<Card>().Activer(selectedCard, Card.ConteneurCarte.CommunicationPanel);
                                gameManager.handPanel.DeselectAllCards();
                                //Est ce que c'est malin d'actualiser ici la variable communication du joueur ?
                                theCrewGame.Joueurs[0].communication =true;
                        
                                switch(communication){
                                    case "Haut":
                                        CommunicationJoueur.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite= gameManager.jetonSprite ;
                                        break;
                            
                                    case "Milieu":
                                        CommunicationJoueur.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite= gameManager.jetonSprite ;
                                        break;
                                    case "Bas":
                                        CommunicationJoueur.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite= gameManager.jetonSprite ;
                                        break;
                                }
                            break;
                        }               

                    }
                    else{
                        title = "==Erreur==";
                        msg = $"Vous avez déjà communiqué une carte.";
                        gameManager.ShowMessagePopup(msg, 6, title);
                    }   
                } 
                else
                {
                    title = "==Changement de phase==";
                    msg = $"Vous êtes dans la phase communiquer.";
                    gameManager.ShowMessagePopup(msg, 6, title);
                    theCrewGame.GamePhase = TheCrewGame.Phase.UserCommunicating;
                }
                break;
            case TheCrewGame.Phase.UserCommunicating:
                selectedCard = gameManager.handPanel.GetSelectedCard();
                if (selectedCard != null)
                {
                    string communication = theCrewGame.User.CommuniquerOuPas(selectedCard);
                        
                            
                    GameObject CommunicationJoueur = GameObject.Find($"Communication{0}");
                    CommunicationJoueur.GetComponent<Card>().Activer(selectedCard, Card.ConteneurCarte.CommunicationPanel);
                    gameManager.handPanel.DeselectAllCards();
                    //Est ce que c'est malin d'actualiser ici la variable communication du joueur ?
                    theCrewGame.Joueurs[0].communication =true;
                        
                    switch(communication){
                        case "Haut":
                            CommunicationJoueur.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite= gameManager.jetonSprite ;
                            break;
                            
                        case "Milieu":
                            CommunicationJoueur.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite= gameManager.jetonSprite ;
                            break;
                        case "Bas":
                            CommunicationJoueur.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite= gameManager.jetonSprite ;
                            break;
                    }
                }            
                theCrewGame.GamePhase = TheCrewGame.Phase.UserPlayingOrCommunicating; 
                title = "==Changement de phase==";
                msg = $"Vous êtes dans la phase jouer ou communiquer.";
                gameManager.ShowMessagePopup(msg, 6, title);
                
                break;
            
            case TheCrewGame.Phase.OtherPlayerPlaying:
                selectedCard = gameManager.tableauCartes.GetSelectedCard();
                if (selectedCard != null)
                {
                    if(theCrewGame.Joueurs[currentPlayer].communication == false){
                        if((int)selectedCard.Color != 0){
                            theCrewGame.GamePhase = TheCrewGame.Phase.TokensSelection;
                            GameObject.Find($"Communication{currentPlayer}").GetComponent<Card>().Activer(selectedCard, Card.ConteneurCarte.CommunicationPanel);
                            gameManager.choixjetons.gameObject.SetActive(true);//Partie pour activer le choix du jeton mais je ne suis pas sûre qu'on le mette aussi pour le choix du joueur lui même...puique le programme pourrait être en mesure de trouver tout seul quel jeton utiliser.
                            GameObject choixJetons = gameManager.choixjetons.gameObject;
                            
                            for (int i =0; i<3 ; i++){
                                Debug.Log($"SelectedCard : {selectedCard.Value} {selectedCard.Color}");
                                choixJetons.transform.GetChild(i).GetComponent<Card>().Activer(selectedCard, Card.ConteneurCarte.ChoixJetons); //Trouver une solution pour que la carte ne se mette pas directement sur le jeton et le cache (changer l'odre de priorité des layers)
                            }
                            //SetActive(false);
                        }
                        else{
                            title = "==Erreur==";
                            msg = $"Cette carte ne peut pas être communiquée car il s'agit d'un atout";
                            gameManager.ShowMessagePopup(msg, 6, title);
                        }
                        
                    }
                    else{
                        title = "==Erreur==";
                        msg = $"Le joueur {currentPlayer} a déjà communiqué une carte.";
                        gameManager.ShowMessagePopup(msg, 6, title);
                    }    
                } 
                else
                {
                    title = "==Erreur==";
                    msg = $"Vous n'avez selectionné aucune carte à communiquer.";
                    gameManager.ShowMessagePopup(msg, 6, title);
                }

                break;
            case TheCrewGame.Phase.OtherPlayerCommunicating:
                break;

            case TheCrewGame.Phase.TokensSelection :
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
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

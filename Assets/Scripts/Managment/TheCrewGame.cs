using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheCrewGame : MonoBehaviour
{
    GameManager gameManager;
    public List<Joueur> Joueurs { get; private set; }
    public int NbCardsInGame { get; private set; }
    public int NbPlayers { get; private set; }
    public int NbPlis { get; private set; }
    public int NbColors { get; private set; } // Compte le noir
    public int currentPlayer;
    public int capitaine;

    public enum Player
    {
        User = 0, Player1 = 1, Player2 = 2
    }
    public enum Phase
    {
        UserCardsSelection, FirstPlayerSelection, TasksSelection, UserPlaying, UserCommunicating, OtherPlayerPlaying, OtherPlayerCommunicating
    }
    private Phase gamePhase;
    public Phase GamePhase
    {
        get { return gamePhase; }
        set { gamePhase = SwitchPhase(value); }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        GamePhase = Phase.UserCardsSelection;

        // Valeurs à récupérer dans la classe Valider
        NbColors = 4;
        NbCardsInGame = 30;
        NbPlayers = 3;
        NbPlis = NbCardsInGame / NbPlayers;

        // Initialisation des autres variables
        currentPlayer = -1;
        Joueurs = new List<Joueur>(); // Initialiser la variable...
        Joueurs.Add(GameObject.Find("User").GetComponent<Joueur>());
        for (int i = 1; i < NbPlayers; i++)
        {
            Joueurs.Add(GameObject.Find($"Player{i}").GetComponent<JoueurNonUtilisateur>());
        }
    }
    /// <summary>Actualise le currentPlayer et change de phase en fonction (UserPlaying ou OtherPlayerPlaying).</summary>
    public void NextPlayer()
    {
        // On commence par vérifier si le pli est terminé
        if (gameManager.pli.GetNbOccupiedSlots() == NbPlayers)
        {
            // On récupère le vainqueur du pli et les tâches qu'il a complété
            int winner = gameManager.pli.GetStrongestCardSlotIndex();
            List<Card> completedTasks = Joueurs[winner].CheckSuccessfulTasks();
            // On reset le pli et on désigne le vainqueur comme 1er joueur
            gameManager.pli.ResetPli();
            currentPlayer = winner;

            // Puis on notifie l'utilisateur des tâches complétées
            Debug.Log((winner == 0 ? $"Vous avez" : $"Le joueur {winner} a") + $" complété {completedTasks.Count} tâches.");
        }
        // Si ce n'est pas le cas, on passe au prochain joueur.
        else
        {
            currentPlayer = (currentPlayer + 1) % NbPlayers;
        }
        switch(GamePhase)
        {
            case  Phase.TasksSelection:
                if(currentPlayer!=capitaine)//Ca veut dire que pas toutes les personnes ont selectionné leur tâches
                {
                    GamePhase = Phase.TasksSelection;
                }
                else {//La phase tache est terminée on passe au jeu
                    if (capitaine == (int)Player.User)// On passe soit au tour de l'utilisateur soit à celui d'un adversaire
                        GamePhase = Phase.UserPlaying;
                else
                    GamePhase = Phase.OtherPlayerPlaying;
                }
            break;
            
            default :   //La phase est soit UserPlaying soit OtherPlayerPlaying
                if (currentPlayer == (int)Player.User) GamePhase = Phase.UserPlaying;
                else GamePhase = Phase.OtherPlayerPlaying;
                break;

        }

        // Quoiqu'il arrive, on change la phase
        

        // Si on a fini le pli, alors on vide le pli et on vérifie si la tâche a été réalisée
    }

    /// <summary>Renvoie la nouvelle phase effective. les vérifications ont été faites quand on appelle cette méthode.</summary>
    Phase SwitchPhase(Phase phase)
    {
        // A implementer
        switch (phase)
        {
            case Phase.UserCardsSelection:
                string title = "Selection des cartes";
                string msg = "Cliquez sur les cartes que vous avez pioché. Une fois que c'est terminé, cliquez sur 'Next' en bas de la page. \nVous pouvez cliquer de nouveau sur les cartes sélectionnées pour les déselectionner.\n\n -ECHAP- pour passer...";
                gameManager.ShowMessagePopup(msg, 20, title);
                gameManager.tableauCartes.SetState(TableauCartes.State.HandCardsSelection);
                break;
            case Phase.FirstPlayerSelection:
                gameManager.tableauCartes.SetState(TableauCartes.State.Hiden); // On cache le tableau
                gameManager.BoutonSuivant.SetActive(false); // On cache le bouton suivant
                gameManager.FirstPlayerSelectionPopup.SetActive(true); // On active la popup
                break;
            case Phase.TasksSelection:
                if(currentPlayer==capitaine){//Il s'agit du premier joueur à choisir ses tâches donc c'est que la phase d'avant était FirstPlayerSelection et qu'il faut cacher le tableau
                    gameManager.tableauCartes.SetState(TableauCartes.State.Hiden); // On cache le tableau
                }
                if (currentPlayer==0){
                    msg = $"Veuillez selectionner vos tâches";
                }
                else{
                    msg = $"Veuillez selectionner les tâches du joueur {currentPlayer}";
                }
                title = "Choix des taches";
                gameManager.ShowMessagePopup(msg, 2, title, TextAlignmentOptions.Center);
                gameManager.tableauTache.SetState(TableauTache.State.TasksSelection);
                //A voir quoi mettre, probablement SetActiveLe schmilblick
                break;
            case Phase.UserPlaying:
                gameManager.tableauTache.SetState(TableauTache.State.Hiden);//Dans certains cas le tableau sera déjà caché mais flemme de rajouter une condition
                gameManager.tableauCartes.SetState(TableauCartes.State.Hiden); // On cache le tableau
                // On annonce au joueur que c'est à lui de jouer
                title = "Tour de l'utilisateur";
                msg = $"A toi de jouer !";
                gameManager.ShowMessagePopup(msg, 2, title, TextAlignmentOptions.Center);
                gameManager.BoutonCommuniquer.SetActive(true);
                // A coder...
                break;
            case Phase.UserCommunicating:
                gameManager.BoutonCommuniquer.SetActive(true);
                // A coder...
                break;
            case Phase.OtherPlayerPlaying:
                gameManager.tableauTache.SetState(TableauTache.State.Hiden);//Dans certains cas le tableau sera déjà caché mais flemme de rajouter une condition
                // On annonce au joueur c'est à qui de jouer
                title = "Tour des autres";
                msg = $"Au joueur {currentPlayer} de jouer.";
                gameManager.ShowMessagePopup(msg, 2, title, TextAlignmentOptions.Center);
                gameManager.BoutonCommuniquer.SetActive(true);
                gameManager.tableauCartes.SetState(TableauCartes.State.OtherPlayerCardSelection);
                break;

            case Phase.OtherPlayerCommunicating:
                gameManager.BoutonCommuniquer.SetActive(true);
                // A coder...
                break;
        }
        return phase;
    }
}

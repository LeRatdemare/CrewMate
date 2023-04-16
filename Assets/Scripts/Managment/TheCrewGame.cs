using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TheCrewGame : MonoBehaviour
{
    GameManager gameManager;
    public int NbCardsInGame { get; private set; }
    public int NbPlayers { get; private set; }
    public int NbPlis { get; private set; }
    public int currentPlayer;

    public enum Player
    {
        User = 0, Player1 = 1, Player2 = 2
    }
    public enum Phase
    {
        UserCardsSelection, FirstPlayerSelection, UserPlaying, UserCommunicating, OtherPlayerPlaying, OtherPlayerCommunicating
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
        NbCardsInGame = 30;
        NbPlayers = 3;
        NbPlis = NbCardsInGame / NbPlayers;

        // Initialisation des autres variables
        currentPlayer = -1;
    }

    // Renvoie la nouvelle phase effective. les vérifications ont été faites quand on appelle cette méthode.
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
            case Phase.UserPlaying:
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

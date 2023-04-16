using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCrewGame : MonoBehaviour
{
    GameManager gameManager;
    public int NbCardsInGame { get; private set; }
    public int NbPlayers { get; private set; }
    public int NbPlis { get; private set; }
    public int currentPlayer;
    public enum Phase
    {
        UserCardsSelection, FirstPlayerSelection, UserPlaying, UserCommunicating, OtherPlayerPlaying
    }
    private Phase gamePhase;
    public Phase GamePhase
    {
        get { return gamePhase; }
        set
        {
            gamePhase = SwitchPhase(value);
        }
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

    // Renvoie la nouvelle phase effective
    Phase SwitchPhase(Phase phase)
    {
        // A implementer
        switch (phase)
        {
            case Phase.UserCardsSelection:
                string title = "Selection des cartes";
                string msg = "Cliquez sur les cartes que vous avez pioché. Une fois que c'est terminé, cliquez sur 'Next' en bas de la page. \nVous pouvez cliquer de nouveau sur les cartes sélectionnées pour les déselectionner.\n\n -ECHAP- pour passer...";
                gameManager.ShowMessagePopup(msg, 12, title);
                gameManager.tableauCartes.SetState(TableauCartes.State.HandCardsSelection);
                break;
            case Phase.FirstPlayerSelection:
                break;
            case Phase.UserPlaying:
                break;
            case Phase.UserCommunicating:
                break;
            case Phase.OtherPlayerPlaying:
                break;
        }
        return phase;
    }
}

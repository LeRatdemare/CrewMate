using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCrewGame : MonoBehaviour
{
    GameManager gameManager;
    public int NbCardsInGame { get; private set; }
    public int NbPlayers { get; private set; }
    public int NbPlis { get; private set; }
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
    }

    // Renvoie la nouvelle phase effective
    Phase SwitchPhase(Phase phase)
    {
        // A implementer
        switch (phase)
        {
            case Phase.UserCardsSelection:
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

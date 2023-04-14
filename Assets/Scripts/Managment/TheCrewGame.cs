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
            switch (value)
            {
                case Phase.FirstPlayerSelection:
                    // Ouvrir la popup de sélection du 1er joueur
                    break;
            }
            gamePhase = value;
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

    void SwitchPhase(Phase phase)
    {
        this.GamePhase = phase;

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
    }
}

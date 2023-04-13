using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCrewGame : MonoBehaviour
{
    GameManager gameManager;
    public enum Phase
    {
        UserCardsSelection, FirstPlayerSelection, UserPlaying, UserCommunicating, OtherPlayerPlaying
    }
    public Phase phase;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        phase = Phase.UserCardsSelection;
    }

    void SwitchPhase(Phase phase)
    {
        this.phase = phase;

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

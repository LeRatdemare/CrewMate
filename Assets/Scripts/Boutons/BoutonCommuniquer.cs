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

    public void OnClick()
    {
        switch (theCrewGame.GamePhase)
        {
            case TheCrewGame.Phase.UserPlaying:
                break;
            case TheCrewGame.Phase.UserCommunicating:
                break;
            case TheCrewGame.Phase.OtherPlayerPlaying:
                break;
            case TheCrewGame.Phase.OtherPlayerCommunicating:
                break;
        }
    }
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

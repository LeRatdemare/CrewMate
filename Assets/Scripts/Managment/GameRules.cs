using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    //le choix de comment obtenir la couleur est à réfléchir
    public List<Card> CardsPlayed = new List<Card>();
    private int couleurDuPliActuel;
    public int CouleurDuPliActuel
    {
        get { return couleurDuPliActuel; }
        set
        {
            if (value == -1)
                couleurDuPliActuel = value;
            else
                if (CardsPlayed.Count == 0) couleurDuPliActuel = value;
        }
    }

    public Card CarteGaganteDuPli()
    {
        Card WinningCard = CardsPlayed[0];
        for (int i = 1; i < CardsPlayed.Count; i++)
        {
            WinningCard = CardChallenge(WinningCard, CardsPlayed[i]);
        }
        return WinningCard;
    }

    private Card CardChallenge(Card Champion, Card Challenger)
    {
        if (Champion.Color == Challenger.Color)   //si les couleurs sont les mêmes,
        {
            if (Champion.Number > Challenger.Number) //il suffit de comparer les nombres.
                return Champion;
            else
                return Challenger;
        }
        else                                //sinon seul la couleur est importante
        {
            if (Challenger.Color == 0) //si le challengeur est noir alors champion 
                return Challenger;        //est de la couleur de l'CouleurDuPli donc le challengeur gagne
            else                    //sinon challengeur est soit ni noir ni de la couleur de la couleur du pli donc il perd automatiquement
                return Champion;    //ou challengeur est de la couleur de l'CouleurDuPli mais puisque Champion n'est pas de la couleur de Challengeur et que il est soit noir, soit de la couleur de la couleur du pli alors Champion est forcément noir, donc Champion gagne.
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}

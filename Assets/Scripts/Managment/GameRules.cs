using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    //le choix de comment obtenir la couleur est à réfléchir
    public List<Card> CardPlayed = new List<Card>();
    private int atoutActuel;
    public int AtoutActuel
    {
        get { return atoutActuel; }
        set
        {
            if (value == -1)
                atoutActuel = value;
            else
                if (NbCarteJouer == 0) atoutActuel = value;
        }
    }

    //temporaire pour éviter les erreurs
    private int nbCarteJouer;
    public int NbCarteJouer
    {
        get
        {
            int i = 0;
            foreach (Card Carte in CardPlayed)
            {
                i++;
            }
            return i;
        }
    }

    public Card CarteGaganteDuPli()
    {
        Card WinningCard = CardPlayed[0];
        for (int i = 1; i < NbCarteJouer; i++)
        {
            WinningCard = CardChallenge(WinningCard, CardPlayed[i]);
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
                return Challenger;        //est de la couleur de l'atout donc le challengeur gagne
            else                    //sinon challengeur est soit ni noir ni de la couleur de l'atout donc il perd automatiquement
                return Champion;    //ou challengeur est de la couleur de l'atout mais puisque Champion n'est pas de la couleur de Challengeur et que il est soit noir, soit de la couleur de l'atout alors Champion est forcément noir, donc Champion gagne.
        }
    }

    //A mettre dans programme mais pour l'instant je le stocke ici



    // Update is called once per frame
    void Update()
    {

    }

}

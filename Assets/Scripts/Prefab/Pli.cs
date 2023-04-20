using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pli : MonoBehaviour
{
    public GameManager gameManager;
    public TheCrewGame theCrewGame;
    public Card.Couleur couleurDemandee;
    public Card.Couleur CouleurDemandee
    {
        get { return couleurDemandee; }
        set
        {
            if (value == Card.Couleur.Neutre)
                couleurDemandee = value;
            else
                if (cardsPlayed.Count == 0) couleurDemandee = value;
        }
    }
    public List<Card> cardsPlayed;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();
    }
    public void ResetPli()
    {
        for (int i = 0; i < theCrewGame.NbPlayers; i++)
        {
            transform.GetChild(i).GetComponent<Card>().Desactiver();
        }
        CouleurDemandee = Card.Couleur.Neutre;
    }

    // public GameObject GetRandomFreeSlot()
    // {
    //     if (GetNbOccupiedSlots() >= nbPlayableSlots) return null;

    //     int slotIndex = Random.Range(0, nbExistingSlots);
    //     GameObject slot;
    //     do
    //     {
    //         slot = transform.GetChild(slotIndex).gameObject;
    //         slotIndex = (slotIndex + 1) % nbExistingSlots;
    //     }
    //     while (slot.GetComponent<Card>().Activee);
    //     return slot;
    // }
    public int GetNbOccupiedSlots()
    {
        int nbOccupiedSlots = 0;
        for (int i = 0; i < theCrewGame.NbPlayers; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().Activee) nbOccupiedSlots++;
        }
        return nbOccupiedSlots;
    }
    public Card GetStrongestCard()
    {
        Card strongest = transform.GetChild(0).GetComponent<Card>();
        for (int i = 1; i < theCrewGame.NbPlayers; i++)
        {
            strongest = CardChallenge(strongest, cardsPlayed[i]);
        }
        return strongest;
    }
    /// Renvoie l'index de la carte la plus forte du pli. -1 si aucune carte n'est jouée
    public int GetStrongestCardSlotIndex()
    {
        // Pour cette boucle on part du principe qu'il y a autant de slots que de joueurs
        int strongestIndex = 0;
        for (int i = 1; i < theCrewGame.NbPlayers; i++)
        {
            strongestIndex = CardChallenge(strongestIndex, i);
        }
        return strongestIndex;
    }

    // public void CarteGaganteDuPli()
    // {
    //     Card WinningCard = GetStrongestCard();
    //     //à compléter
    // }

    private int CardChallenge(int championIndex, int challengerIndex)
    {
        Card champion = transform.GetChild(championIndex).GetComponent<Card>();
        Card challenger = transform.GetChild(challengerIndex).GetComponent<Card>();

        if (CardChallenge(champion, challenger) == champion) return championIndex;
        else if (CardChallenge(champion, challenger) == challenger) return challengerIndex;
        else return -1;
    }
    private Card CardChallenge(Card champion, Card challenger)//Champion est la carte qui gagne dans le pli
    {
        if ((challenger == null || !challenger.Activee) && (champion == null || !champion.Activee)) return null;
        if ((champion == null || !champion.Activee)) return challenger;
        if ((challenger == null || !challenger.Activee)) return champion;

        if (champion.Color == challenger.Color)   //si les couleurs sont les mêmes,
        {
            if (champion.Value > challenger.Value) //il suffit de comparer les nombres.
                return champion;
            else
                return challenger;
        }
        else                                //sinon seul la couleur est importante
        {
            if (challenger.Color == Card.Couleur.Noir) //si le challengeur est noir alors champion 
                return challenger;        //est de la couleur de l'CouleurDuPli donc le challengeur gagne
            else                    //sinon challengeur est soit ni noir ni de la couleur de la couleur du pli donc il perd automatiquement
                return champion;    //ou challengeur est de la couleur de l'CouleurDuPli mais puisque Champion n'est pas de la couleur de Challengeur et que il est soit noir, soit de la couleur de la couleur du pli alors Champion est forcément noir, donc Champion gagne.
        }
    }
    public bool IsInPli(Card card)
    {
        // On parcours le pli
        for (int i = 0; i < theCrewGame.NbPlayers; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().Equals(card)) return true;
        }
        return false;
    }
}

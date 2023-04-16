using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pli : MonoBehaviour
{
    public GameManager gameManager;
    public TheCrewGame theCrewGame;
    public int nbExistingSlots;
    public int nbPlayableSlots;
    public int premierJoueur;
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
        for (int i = 0; i < nbExistingSlots; i++)
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
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().Activee) nbOccupiedSlots++;
        }
        return nbOccupiedSlots;
    }
    public Card GetStrongestCard()
    {
        Card strongest = cardsPlayed[0];
        for (int i = 1; i < cardsPlayed.Count; i++)
        {
            strongest = CardChallenge(strongest, cardsPlayed[i]);
        }
        return strongest;
    }

    public void CarteGaganteDuPli()
    {
        Card WinningCard = GetStrongestCard();
        //à compléter
    }

    private Card CardChallenge(Card Champion, Card Challenger)//Champion est la carte qui gagne dans le pli
    {
        if (Champion.Color == Challenger.Color)   //si les couleurs sont les mêmes,
        {
            if (Champion.Value > Challenger.Value) //il suffit de comparer les nombres.
                return Champion;
            else
                return Challenger;
        }
        else                                //sinon seul la couleur est importante
        {
            if (Challenger.Color == Card.Couleur.Noir) //si le challengeur est noir alors champion 
                return Challenger;        //est de la couleur de l'CouleurDuPli donc le challengeur gagne
            else                    //sinon challengeur est soit ni noir ni de la couleur de la couleur du pli donc il perd automatiquement
                return Champion;    //ou challengeur est de la couleur de l'CouleurDuPli mais puisque Champion n'est pas de la couleur de Challengeur et que il est soit noir, soit de la couleur de la couleur du pli alors Champion est forcément noir, donc Champion gagne.
        }
    }
}

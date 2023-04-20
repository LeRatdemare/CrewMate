using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPanel : MonoBehaviour
{
    public int nbSlots;
    public GameObject GetFirstFreeSlot()
    {
        GameObject carte;
        for (int i = 0; i < nbSlots; i++)
        {
            carte = transform.GetChild(i).gameObject;
            if (!carte.GetComponent<Card>().Activee)
            {
                return carte;
            }
        }
        return null;
    }
    public int GetCardsCount()
    {
        int cardsCount = 0;
        for (int i = 0; i < nbSlots; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().Activee)
                cardsCount++;
        }
        return cardsCount;
    }
    public void DeselectAllCards()
    {
        for (int i = 0; i < nbSlots; i++)
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            if (card.Selected)
                card.Selected = false;
        }
    }
    public Card GetSelectedCard()
    {
        for (int i = 0; i < nbSlots; i++)
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            if (card.Selected)
                return card;
        }
        // Ne devrait pas arriver, seulement si le joueur n'a rien sélectionné
        return null;
    }
}

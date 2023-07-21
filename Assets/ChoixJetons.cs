using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixJetons : MonoBehaviour
{
    public void DeselectAllCards()
    {
        for (int i = 0; i < 3; i++)
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            if (card.Selected)
                card.Selected = false;
        }
    }
    public Card GetSelectedCard()
    {
        for (int i = 0; i < 3; i++)
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            if (card.Selected)
                return card;
        }
        return null;
    }
    
}

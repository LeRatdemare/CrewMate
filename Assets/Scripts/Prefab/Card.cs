using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool selected;
    public int Color { get; } = 1;
    public int Number { get; } = 1;

    public Card(int color, int number)
    {
        Color = color;
        Number = number;
    }

    public bool IsPlayable()
    {
        //List<Card> Hand = transform.parent.GetComponent<HandPanel>().Hand;
        List<Card> Hand = new List<Card>();
        int CouleurDuPli = GameObject.Find("Pli").GetComponent<GameRules>().CouleurDuPliActuel;
        if (CouleurDuPli == -1)   //CouleurDuPli = -1 signifie que il n'y a aucun couleur au pli actuel donc pas de carte jouée à ce pli
            return true;
        else
        {
            if (HandHoldColor(CouleurDuPli, Hand))   //Si on a des cartes dans la main qui a la couleur de la couleur du pli
            {
                if (Color == CouleurDuPli)
                    return true;
                else
                {
                    GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 0);
                    return false;
                }
            }
            else
                return true;
        }
    }

    //deuxième argument temporaire pour éviter les erreurs de compilations
    public bool HandHoldColor(int Couleur, List<Card> Hand)
    {
        foreach (Card Carte in Hand)
        {
            if (Color == Couleur)
            {
                return true;
            }
        }
        return false;
    }

    //deux premiers arguments temporaire pour éviter les erreurs de compilations
    public string AvailableCommunication(int[] MaxParCouleur, int[] MinParCouleur, Card Carte) //changer argument Carte en fonction de l'endroit où on dois placer la fonnction, si dans carte retirer totalement l'argument
    {
        //
        // Ordre des couleurs :  Bleu, Jaune, Rose
        int IndiceCouleur = Carte.Color - 1; //on réduit de 1 pour que ça passe dans le tableau
        if (IndiceCouleur == -1) //si c'est une carte noir
            return "";
        else
        {
            if (Carte.Number == MaxParCouleur[IndiceCouleur]) //si carte est la carte du haut
            {
                if (Carte.Number == MinParCouleur[IndiceCouleur]) //si carte est la carte du bas
                    return "Milieu";    //la carte est la plus haute et la plus basse donc la seul
                else
                    return "Haut";
            }
            else
            {
                if (Carte.Number == MinParCouleur[IndiceCouleur]) // si carte est la carte du bas
                    return "Bas";
                else
                    return "";
            }
        }
    }
    void OnMouseDown()
    {
        if (IsPlayable())
        {
            int slotIndex = 1;
            GameObject slot = GameObject.Find($"Slot{slotIndex}");
            GameObject pli = GameObject.Find("Pli");
            while (slotIndex < 6 && !slot.GetComponent<CardSlot>().IsFree)
            {
                slotIndex++;
                slot = GameObject.Find($"Slot{slotIndex}");
            }
            if (slotIndex < 6)
            {
                slot.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                slot.GetComponent<CardSlot>().IsFree = false;
                pli.GetComponent<GameRules>().CardPlayed.Add(this);
            }
        }
        // A changer
    }

    void OnMouseEnter()
    {
        // transform.localScale *= 1.5f;
        GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
    }
    void OnMouseExit()
    {
        // transform.localScale /= 1.5f;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

}
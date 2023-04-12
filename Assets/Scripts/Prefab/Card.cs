using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool selected;
    public int Color { get; }
    public int Number { get; }

    public bool PlayableCard(Card Carte, int Atout)
    {
        Atout = GameObject.Find("Pli").GetComponnant<GameRules>().AtoutActuel;
        if (Atout == -1)   //Atout = -1 signifie que il n'y a aucun atout actuel donc pas de carte jouée à ce pli
            return true;
        else
        {
            if (HandHoldColor(Atout))   //Si on a des cartes dans la main qui a la couleur de l'atout
            {
                if (Carte.Couleur == Atout)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }
    }

    public bool HandHoldColor(int Couleur)
    {
        foreach (Card Carte in Joueur.Main)
        {
            if (Color == Couleur)
            {
                return true;
            }
        }
        return false;
    }

    public string AvailableCommunication(int[] MaxParCouleur, int[] MinParCouleur, Card Carte) //changer argument Carte en fonction de l'endroit où on dois placer la fonnction, si dans carte retirer totalement l'argument
    {
        // Ordre des couleurs :  Bleu, Jaune, Rose
        int IndiceCouleur = Carte.Couleur - 1; //on réduit de 1 pour que ça passe dans le tableau
        if (IndiceCouleur == -1) //si c'est une carte noir
            return None;
        else
        {
            if (Carte.Nombre == MaxParCouleur[IndiceCouleur]) //si carte est la carte du haut
            {
                if (Carte.Nombre == MinParCouleur[IndiceCouleur]) //si carte est la carte du bas
                    return "Milieu";    //la carte est la plus haute et la plus basse donc la seul
                else
                    return "Haut";
            }
            else
            {
                if (Carte.Nombre == MinParCouleur[IndiceCouleur]) // si carte est la carte du bas
                    return "Bas";
                else
                    return "";
            }
        }
    }
    void OnMouseDown()
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

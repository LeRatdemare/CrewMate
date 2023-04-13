using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, IComparable
{
    public enum Couleur
    {
        Neutre = -1, Noir = 0, Bleu = 1, Jaune = 2, Rose = 3
    }

    public enum ConteneurCarte
    {
        TableauCartes, HandPanel, Pli, CommunicationPanel, Defause, AutreJoueur
    }

    public enum Communication
    {
        Rien, Seul, Haut, Bas
    }

    public int Type { get; private set; }
    public Couleur Color { get; private set; }
    public int Value { get; private set; }
    private bool selected;
    private ConteneurCarte conteneur;
    private GameManager gameManager;
    public bool Activee { get; private set; } = false;

    public Card(int type, Couleur color, int value)
    {
        Type = type;
        Color = color;
        Value = value;
    }

    public void Activer(GameManager gameManager, int type, Couleur color, int value, Sprite sprite, ConteneurCarte conteneur)
    {
        this.gameManager = gameManager;
        Type = type;
        Color = color;
        Value = value;
        GetComponent<SpriteRenderer>().sprite = sprite;
        this.conteneur = conteneur;
        Activee = true;
    }
    public void Desactiver()
    {
        Type = -1;
        Color = Couleur.Neutre;
        Value = -1;
        GetComponent<SpriteRenderer>().sprite = null;
        Activee = false;
    }

    public bool IsPlayable()
    {
        //List<Card> Hand = transform.parent.GetComponent<HandPanel>().Hand;
        List<Card> Hand = new List<Card>();
        Couleur CouleurDuPli = (Couleur)GameObject.Find("Pli").GetComponent<Pli>().CouleurDemandee;
        if (CouleurDuPli == Couleur.Neutre)   //Neutre signifie que il n'y a aucun couleur au pli actuel donc pas de carte jouée à ce pli
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

    public bool HandHoldColor(Couleur couleur, List<Card> Hand)
    {
        foreach (Card Carte in Hand)
        {
            if (Color == couleur)
            {
                return true;
            }
        }
        return false;
    }

    //deux premiers arguments temporaire pour éviter les erreurs de compilations
    public Communication AvailableCommunication(int[] MaxParCouleur, int[] MinParCouleur, Card Carte) //changer argument Carte en fonction de l'endroit où on dois placer la fonnction, si dans carte retirer totalement l'argument
    {
        //
        // Ordre des couleurs :  Bleu, Jaune, Rose
        int IndiceCouleur = Carte.Color - (Couleur)1; //on réduit de 1 pour que ça passe dans le tableau
        if (IndiceCouleur == -1) //si c'est une carte noir
            return Communication.Rien;
        else
        {
            if (Carte.Value == MaxParCouleur[IndiceCouleur]) //si carte est la carte du haut
            {
                if (Carte.Value == MinParCouleur[IndiceCouleur]) //si carte est la carte du bas
                    return Communication.Seul;    //la carte est la plus haute et la plus basse donc la seul
                else
                    return Communication.Haut;
            }
            else
            {
                if (Carte.Value == MinParCouleur[IndiceCouleur]) // si carte est la carte du bas
                    return Communication.Bas;
                else
                    return Communication.Rien;
            }
        }
    }
    void OnMouseDown()
    {
        if (Activee)
        {
            if (IsPlayable())
            {
                switch (conteneur)
                {
                    case ConteneurCarte.HandPanel:
                        Play();
                        break;
                    case ConteneurCarte.TableauCartes:
                        AjouterDansLaMain();
                        break;
                }
            }
        }
    }

    void Play()
    {
        Pli pli = gameManager.pli.GetComponent<Pli>();
        GameObject slot = pli.GetRandomFreeSlot();
        if (slot != null)
        {
            slot.GetComponent<Card>().Activer(gameManager, Type, Color, Value, GetComponent<SpriteRenderer>().sprite, ConteneurCarte.Pli);
            Desactiver();
        }
        else
        {
            // Faire apparaître une fenêtre pour le message d'erreur
            Debug.Log("Il n'y a plus de slot disponible dans le pli");
        }
    }
    void AjouterDansLaMain()
    {
        HandPanel handPanel = gameManager.handPanel.GetComponent<HandPanel>();
        GameObject slot = handPanel.GetFirstFreeSlot();
        if (slot != null)
        {
            slot.GetComponent<Card>().Activer(gameManager, Type, Color, Value, GetComponent<SpriteRenderer>().sprite, ConteneurCarte.HandPanel);
            Desactiver();
        }
        else
        {
            // Faire apparaître une dddfenêtre pour le message d'erreur
            Debug.Log("Il n'y a plus de slot disponible dans la main");
        }

    }

    void OnMouseEnter()
    {
        if (conteneur != ConteneurCarte.Pli)
            GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
    }
    void OnMouseExit()
    {
        if (conteneur != ConteneurCarte.Pli)
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }

    /* On peut passer à cette méthode soit un obj de la classe
    Card ou un GameObject avec un script Card comme compansant.
    
    Elle compare les cartes en prenant en compte le pli actuel,
    et ce même si les cartes ne sont pas dans le pli.*/
    public int CompareTo(object obj)
    {
        Card card;
        try { card = (Card)obj; }
        catch (Exception e)
        {
            try
            {
                card = ((GameObject)obj).GetComponent<Card>();
            }
            catch (Exception ex)
            {
                throw new Exception("On ne peut comparer une carte qu'avec une autre carte...");
            }
        }

        if (Color == card.Color)
            return (Value - card.Value);
        else if (Color == Couleur.Noir)
            return 1;
        else if (card.Color == Couleur.Noir)
            return -1;
        else if (Color == gameManager.pli.GetComponent<Pli>().couleurDemandee)
            return 1;
        else if (card.Color == gameManager.pli.GetComponent<Pli>().couleurDemandee)
            return -1;
        // Dans le cas où les 2 cartes ne sont ni noirs ni de la couleur demandée il y a égalité
        else
            return 0;
    }
}
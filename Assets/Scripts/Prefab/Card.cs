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

    public int Type { get; private set; }
    public Couleur Color { get; private set; }
    public int Value { get; private set; }
    private bool selected;
    private Utils.ConteneurCarte conteneur;
    private GameManager gameManager;
    public bool Activee { get; private set; } = false;

    public Card(int type, Couleur color, int value)
    {
        Type = type;
        Color = color;
        Value = value;
    }

    public void Activer(GameManager gameManager, int type, Couleur color, int value, Sprite sprite, Utils.ConteneurCarte conteneur)
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

        GetComponent<SpriteRenderer>().sprite = null;
        Activee = false;
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
            if (Activee)
        {
            switch (conteneur)
            {
                case Utils.ConteneurCarte.HandPanel:
                    Play();
                    break;
                case Utils.ConteneurCarte.TableauCartes:
                    AjouterDansLaMain();
                    break;
            }
        }
    }
    void Play()
    {
        Pli pli = gameManager.pli.GetComponent<Pli>();
            GameObject slot = pli.GetRandomFreeSlot();
        if (slot != null)
        {
            slot.GetComponent<Card>().Activer(gameManager, Type, Color, Value, GetComponent<SpriteRenderer>().sprite, Utils.ConteneurCarte.Pli);
            GameObject pli = GameObject.Find("Pli");
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
                slot.GetComponent<Card>().Activer(gameManager, Type, Color, Value, GetComponent<SpriteRenderer>().sprite, Utils.ConteneurCarte.HandPanel);
                Desactiver();
            }
            else
            {
                // Faire apparaître une dddfenêtre pour le message d'erreur
                Debug.Log("Il n'y a plus de slot disponible dans la main");
                pli.GetComponent<GameRules>().CardPlayed.Add(this);
            }
        }
    }

    void OnMouseEnter()
    {
        if (conteneur != Utils.ConteneurCarte.Pli)
            GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
    }
    void OnMouseExit()
    {
        if (conteneur != Utils.ConteneurCarte.Pli)
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
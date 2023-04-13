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

        GetComponent<SpriteRenderer>().sprite = null;
        Activee = false;
    }
    void OnMouseDown()
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

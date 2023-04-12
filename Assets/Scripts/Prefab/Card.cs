using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour, IComparable
{
    public int Type { get; private set; }
    public int Color { get; private set; }
    public int Value { get; private set; }
    private bool selected;
    private Utils.ConteneurCarte conteneur;
    private GameManager gameManager;
    public bool Activee { get; private set; } = false;

    public void Activer(GameManager gameManager, int type, int color, int value, Sprite sprite, Utils.ConteneurCarte conteneur)
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
        Color = -1;
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

    public int CompareTo(object obj)
    {
        return Color.CompareTo(obj);
    }
}

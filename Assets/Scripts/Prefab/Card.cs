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
        TableauCartes, HandPanel, Pli, CommunicationPanel, Defause, AutreJoueur,TableauTache,Tache,ChoixJetons//Enlever communicationPanel et AutreJoueur
    }

    public enum Communication
    {
        Rien, Seul, Haut, Bas
    }

    public int Type { get; private set; }
    public static int nbTacheSelectionnees =0;
    public Couleur Color { get; private set; }
    public int Value { get; private set; }
    private bool selected;
    public bool Selected
    {
        get { return selected; }
        set
        {
            switch (conteneur)
            {
                case ConteneurCarte.HandPanel:
                    if (value)
                    {
                        // On déselectionne toutes les cartes
                        gameManager.handPanel.DeselectAllCards();
                        transform.localPosition += Vector3.up * 0.3f;
                    }
                    else
                        transform.localPosition += Vector3.down * 0.3f;
                    selected = value;
                    break;
                case ConteneurCarte.TableauCartes:
                    if (value)
                    {
                        // On déselectionne toutes les cartes
                        gameManager.tableauCartes.DeselectAllCards();
                        transform.Rotate(new Vector3(0, 0, 90));
                        transform.localScale = new Vector3(transform.localScale.x * 3, transform.localScale.y / 2.5f, transform.localScale.z);
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, 0, -90));
                        transform.localScale = new Vector3(transform.localScale.x / 3, transform.localScale.y * 2.5f, transform.localScale.z);
                    }
                    selected = value;
                    break;
                
                case ConteneurCarte.TableauTache :
                    if (value){
                        if (nbTacheSelectionnees<3){//Le nombre 3 pourra potentiellement varier en fontion des tâches
                            nbTacheSelectionnees++;
                            transform.Rotate(new Vector3(0, 0, 90));
                            transform.localScale = new Vector3(transform.localScale.x * 3, transform.localScale.y / 2.5f, transform.localScale.z);
                            selected = value;
                        }
                        else{
                            string title = "Erreur";
                            string msg = $"Vous avez selectionné trop de tâches pour pouvoir en selectionner une nouvelle. Veuillez en déselectionner une.";
                            gameManager.ShowMessagePopup(msg, 2, title);
                        }                     
                    }
                    else{
                        nbTacheSelectionnees--;
                        transform.Rotate(new Vector3(0, 0, -90));
                        transform.localScale = new Vector3(transform.localScale.x / 3, transform.localScale.y * 2.5f, transform.localScale.z);
                        selected = value;
                    }
                    break;
                case ConteneurCarte.ChoixJetons://C'est un copier coller de HandPanel, voir comment améliorer ca
                if (value)
                    {
                        gameManager.choixjetons.DeselectAllCards();
                        transform.localPosition += Vector3.up * 0.3f;
                    }
                    else
                        transform.localPosition += Vector3.down * 0.3f;
                    selected = value;
                    break;
            }
        }
    }
    private ConteneurCarte conteneur;
    private GameManager gameManager;
    private TheCrewGame theCrewGame;
    public bool Activee { get; private set; } = false;//syntaxe ?

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();
    }

    public void Activer(int type, Couleur color, int value, Sprite sprite, ConteneurCarte conteneur)
    {
        Type = type;
        Color = color;
        Value = value;
        GetComponent<SpriteRenderer>().sprite = sprite;
        this.conteneur = conteneur;//Le conteneur ne change pas non?
        Activee = true;

        // On actualise la couleur demandée si la carte est activée dans le pli
        //Mais c'est que pour le premier joueur non?
        if (conteneur == ConteneurCarte.Pli)
        {
            gameManager.pli.CouleurDemandee = Color;
        }
    }
    public void Activer(Card card, ConteneurCarte conteneur)
    {
        Activer(card.Type, card.Color, card.Value, card.GetComponent<SpriteRenderer>().sprite, conteneur);
    }
    public void Desactiver()
    {
        Type = -1;
        Color = Couleur.Neutre;
        Value = -1;
        GetComponent<SpriteRenderer>().sprite = null;
        Activee = false;
    }

    public bool IsPlayable()//Mettre IsPlayable au bon endroit pour empecher de jouer une mauvaise carte
    {
        //List<Card> Hand = transform.parent.GetComponent<HandPanel>().Hand;
        List<Card> Hand = new List<Card>();
        Couleur CouleurDuPli = GameObject.Find("Pli").GetComponent<Pli>().CouleurDemandee;
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
            if (Color == couleur)//Je comprend pas comment ca marche, Color et couleur sont fixes non?
            {
                return true;
            }
        }
        return false;
    }

/*
    //deux premiers arguments temporaire pour éviter les erreurs de compilations
    public Communication AvailableCommunication(int[] maxParCouleur, int[] minParCouleur, Card carte) //changer argument Carte en fonction de l'endroit où on dois placer la fonnction, si dans carte retirer totalement l'argument
    {//Réféchir à enlever l'argument Card carte car probablement qu'il ne sert à rien

        // Ordre des couleurs :  Bleu, Jaune, Rose
        int indiceCouleur = carte.Color - (Couleur)1; //on réduit de 1 pour que ça passe dans le tableau
        if (indiceCouleur == -1) //si c'est une carte noir
            return Communication.Rien;
        else
        {
            if (carte.Value == maxParCouleur[indiceCouleur]) //si carte est la carte du haut
            {
                if (carte.Value == minParCouleur[indiceCouleur]) //si carte est la carte du bas
                    return Communication.Seul;    //la carte est la plus haute et la plus basse donc la seul
                else
                    return Communication.Haut;
            }
            else
            {
                if (carte.Value == minParCouleur[indiceCouleur]) // si carte est la carte du bas
                    return Communication.Bas;
                else
                    return Communication.Rien;
            }
        }
    }
    */

    void OnMouseDown()
    {
        if (Activee)
        {
            switch (conteneur)
            {
                // Dans le cas où la carte est dans la main, elle n'est pas toujours cliquable
                case ConteneurCarte.HandPanel:
                    switch (theCrewGame.GamePhase)
                    {
                        // Si le joueur est entrain de choisir ses cartes elle repart dans le tableau
                        case TheCrewGame.Phase.UserCardsSelection:
                            RangerDansLeTableau();
                            break;
                        case TheCrewGame.Phase.UserPlaying:
                            Selected = !Selected;
                            break;
                        case TheCrewGame.Phase.UserCommunicating:
                            break;
                    }
                    break;
                case ConteneurCarte.TableauCartes:
                    switch (theCrewGame.GamePhase)
                    {
                        // Si le joueur est entrain de choisir ses cartes elle repart dans le tableau
                        case TheCrewGame.Phase.UserCardsSelection:
                            AjouterDansLaMain();
                            break;
                        case TheCrewGame.Phase.OtherPlayerPlaying:
                            Selected = !Selected;
                            break;
                        case TheCrewGame.Phase.OtherPlayerCommunicating:
                            break;
                    }

                    break;
                case ConteneurCarte.Pli:
                    // A coder...
                    break;
                case ConteneurCarte.TableauTache://A voir s'il y a beaucoup à modif en dehors de ca
                        Selected = !Selected;
                    break;
                case ConteneurCarte.ChoixJetons:
                    Selected = !Selected;
                    break;
            }
        }
    }
    // void Play(int numPlayer)
    // {
    //     Pli pli = gameManager.pli.GetComponent<Pli>();
    //     Card slot = pli.Slots[numPlayer];
    //     if (slot != null)
    //     {
    //         slot.GetComponent<Card>().Activer(Type, Color, Value, GetComponent<SpriteRenderer>().sprite, ConteneurCarte.Pli);
    //         Desactiver();
    //     }
    //     else
    //     {
    //         // Faire apparaître une fenêtre pour le message d'erreur
    //         Debug.Log("Il n'y a plus de slot disponible dans le pli");
    //     }
    // }
    void AjouterDansLaMain()
    {
        HandPanel handPanel = gameManager.handPanel.GetComponent<HandPanel>();
        GameObject slot = handPanel.GetFirstFreeSlot();
        if (slot != null)
        {
            theCrewGame.User.AjouterMaxMinCouleur((int)Color,Value);
            slot.GetComponent<Card>().Activer(Type, Color, Value, GetComponent<SpriteRenderer>().sprite, ConteneurCarte.HandPanel);
            Desactiver();//chaque instance de carte est liée à un slot et là on est par rapport au previous slot donc on a pas besoin de le chercher car on est déjà dedans
        }
        else
        {
            // Faire apparaître une dddfenêtre pour le message d'erreur
            Debug.Log("Il n'y a plus de slot disponible dans la main");
        }

    }
    void RangerDansLeTableau()
    {
        theCrewGame.User.RetirerMaxMinCouleur((int)Color,Value);
        Card slot = gameManager.tableauCartes.cartes[(int)Color, Value - 1].GetComponent<Card>();
        slot.Activer(Type, Color, Value, GetComponent<SpriteRenderer>().sprite, ConteneurCarte.TableauCartes);
        Desactiver();
    }

    void OnMouseEnter()
    {
        if (conteneur != ConteneurCarte.Pli)
            GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);//Pour faire le petit effet lorsu'on hoover une carte
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
        else if (Color == gameManager.pli.GetComponent<Pli>().CouleurDemandee)
            return 1;
        else if (card.Color == gameManager.pli.GetComponent<Pli>().CouleurDemandee)
            return -1;
        // Dans le cas où les 2 cartes ne sont ni noirs ni de la couleur demandée il y a égalité
        else
            return 0;
    }
    /// <summary>On fait le choix de ne comparer que couleur et valeur pour pouvoir comparer avec les tâches.</summary>
    public override bool Equals(object other)
    {
        if (!(other is Card)) return false;
        Card otherCard = (Card)other;
        Debug.Log($"Value : {Value}, oherCard.Value : {otherCard.Value} Color : {Color} otherCard.Color : {otherCard.Color}");
        return Value == otherCard.Value && Color == otherCard.Color;
    }
}
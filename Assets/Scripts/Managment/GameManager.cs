using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TheCrewGame theCrewGame;
    public GameObject handPanel;
    public GameObject tableauCartes;
    public GameObject pli;
    public int nbColors;
    public int nbCardsPerColor;
    public int nbJoueurs;
    private int nbPlis;

    // Start is called before the first frame update
    void Start()
    {
        // Au départ le joueur commence par sélectionner ses cartes
        InitialiseGame();

        // Ensuite, il sélectionne le 1er joueur
        int premierJoueur = GetPremierJoueur();

        // Ensuite on rentre dans la boucle.
        for (int numPli = 0; numPli < nbPlis; numPli++)
        {
            for (int numJoueur = 0; numJoueur < nbJoueurs; numJoueur++)
            {
                int currentPlayer = numJoueur + premierJoueur;
                if (currentPlayer == 0)
                {
                    tableauCartes.GetComponent<TableauCartes>().SetState(TableauCartes.State.Hiden);
                }
            }
            // A la fin du pli on met à jour le 1er joueur
            premierJoueur = (++premierJoueur) % nbJoueurs;
        }
    }

    void InitialiseGame()
    {
        theCrewGame = GetComponent<TheCrewGame>();
        nbJoueurs = 3; // A vocation a pouvoir changer
        nbPlis = 40 / nbJoueurs;

        DrawRandomCards(10); // Le joueur devra sélectionner ses cartes à la place
        // Puis le 1er joueur...
        // Puis la tâche, et le joueur qui doit la faire
    }

    // Renvoie l'indice du 1er Joueur entre 0(Utilisateur), 1, 2, 3 et 4
    int GetPremierJoueur()
    {
        int premierJoueur = 0;
        // A coder...
        return premierJoueur;
    }

    private void DrawRandomCards(int nbCards)
    {
        for (int i = 0; i < nbCards; i++)
        {
            int type = 0;
            int couleur;
            int valeur;
            GameObject cartePiochee;
            do
            {
                couleur = Random.Range(0, 4); // On génère au hasard une couleur excepté les noirs
                valeur = Random.Range(0, 9); // Une génère la valeur de la carte
                cartePiochee = tableauCartes.GetComponent<TableauCartes>().cartes[couleur, valeur];
            } while (cartePiochee == null || !cartePiochee.GetComponent<Card>().Activee);
            Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/{type}{couleur}{valeur + 1}");

            GameObject carte = handPanel.GetComponent<HandPanel>().GetFirstFreeSlot();
            carte.GetComponent<Card>().Activer(this, type, (Card.Couleur)couleur, valeur + 1, sprite, Card.ConteneurCarte.HandPanel);
            tableauCartes.GetComponent<TableauCartes>().cartes[couleur, valeur].GetComponent<Card>().Desactiver();
        }
    }
    void Update()
    {
        // Cliquer sur 'espace' pour inverser la visibilité du tableau de cartes 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TableauCartes t = tableauCartes.GetComponent<TableauCartes>();
            t.SetState((t.currentState == TableauCartes.State.Hiden) ? TableauCartes.State.HandCardsSelection : TableauCartes.State.Hiden);
        }
        // Cliquer sur 'r' pour vider le pli afin de pouvoir rejouer d'autres cartes
        if (Input.GetKeyDown(KeyCode.R))
        {
            pli.GetComponent<Pli>().ResetPli();
        }
    }
}

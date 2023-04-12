using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject handPanel;
    public GameObject tableauCartes;
    public GameObject pli;
    public int nbColors;
    public int nbCardsPerColor;
    public int nbJoueurs;
    private int nbTours;

    // Start is called before the first frame update
    void Start()
    {
        // Au départ le joueur commence par sélectionner ses cartes
        DrawRandomCards(0);

        // Ensuite, il sélectionne le 1er joueur

        // Ensuite on rentre dans la boucle.
        for (int tour = 0; tour < nbTours; tour++)
        {

        }
    }

    void InitialiseGame()
    {
        nbJoueurs = 3; // A vocation a pouvoir changer
        nbTours = 40 / nbJoueurs;
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
            carte.GetComponent<Card>().Activer(this, type, couleur, valeur + 1, sprite, Utils.ConteneurCarte.HandPanel);
            tableauCartes.GetComponent<TableauCartes>().cartes[couleur, valeur].GetComponent<Card>().Desactiver();
        }
    }
    void Update()
    {
        // Cliquer sur 'espace' pour inverser la visibilité du tableau de cartes 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tableauCartes.GetComponent<TableauCartes>().InverserVisibilite();
        }
        // Cliquer sur 'r' pour vider le pli afin de pouvoir rejouer d'autres cartes
        if (Input.GetKeyDown(KeyCode.R))
        {
            pli.GetComponent<Pli>().ResetPli();
        }
    }
}

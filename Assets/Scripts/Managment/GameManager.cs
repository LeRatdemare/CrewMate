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

    // Start is called before the first frame update
    void Start()
    {
        DrawRandomCards(14);
    }

    private void DrawRandomCards(int nbCards)
    {
        for (int i = 0; i < nbCards; i++)
        {
            int couleur;
            int valeur;
            GameObject cartePiochee;
            do
            {
                couleur = Random.Range(0, 4); // On génère au hasard une couleur excepté les noirs
                valeur = Random.Range(0, 9); // Une génère la valeur de la carte
                cartePiochee = tableauCartes.GetComponent<TableauCartes>().cartes[couleur, valeur];
            } while (cartePiochee == null || !cartePiochee.GetComponent<Card>().Activee);
            Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/0{couleur}{valeur + 1}");

            GameObject carte = handPanel.GetComponent<HandPanel>().GetFirstFreeSlot();
            carte.GetComponent<Card>().Activer(this, sprite, Utils.ConteneurCarte.HandPanel);
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

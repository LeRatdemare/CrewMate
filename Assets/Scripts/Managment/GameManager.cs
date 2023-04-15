using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private TheCrewGame theCrewGame;
    [HideInInspector] public HandPanel handPanel;
    [HideInInspector] public TableauCartes tableauCartes;
    [HideInInspector] public Pli pli;
    public int nbColors;
    public int nbCardsPerColor;
    public int nbJoueurs;
    private int nbPlis;

    public GameObject timedMessagePopupPrefab;
    private TimedMessagePopup timedMessagePopup;
    private FirstPlayerSelectionPopup firstPlayerSelectionPopup;

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
                    tableauCartes.SetState(TableauCartes.State.Hiden);
                }
            }
            // A la fin du pli on met à jour le 1er joueur
            premierJoueur = (++premierJoueur) % nbJoueurs;
        }
    }

    void InitialiseGame()
    {
        // On prépare les données du jeu
        nbJoueurs = 3; // A vocation a pouvoir changer
        nbPlis = 40 / nbJoueurs;

        // On prépare les scripts related
        theCrewGame = GetComponent<TheCrewGame>();
        tableauCartes = GameObject.Find("TableauCartes").GetComponent<TableauCartes>();
        handPanel = GameObject.Find("HandPanel").GetComponent<HandPanel>();
        pli = GameObject.Find("Pli").GetComponent<Pli>();

        // On prépare les popups
        timedMessagePopup = Instantiate(timedMessagePopupPrefab, transform.position, Quaternion.identity).GetComponent<TimedMessagePopup>();
        timedMessagePopup.AttachToCanvas(GameObject.Find("Canvas").GetComponent<Canvas>());
        timedMessagePopup.SetVisible(false);


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
                cartePiochee = tableauCartes.cartes[couleur, valeur];
            } while (cartePiochee == null || !cartePiochee.GetComponent<Card>().Activee);
            Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/{type}{couleur}{valeur + 1}");

            GameObject carte = handPanel.GetFirstFreeSlot();
            carte.GetComponent<Card>().Activer(this, type, (Card.Couleur)couleur, valeur + 1, sprite, Card.ConteneurCarte.HandPanel);
            tableauCartes.cartes[couleur, valeur].GetComponent<Card>().Desactiver();
        }
    }
    void Update()
    {
        // Cliquer sur 'espace' pour inverser la visibilité du tableau de cartes 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tableauCartes.SetState((tableauCartes.currentState == TableauCartes.State.Hiden) ? TableauCartes.State.HandCardsSelection : TableauCartes.State.Hiden);
        }
        // Cliquer sur 'r' pour vider le pli afin de pouvoir rejouer d'autres cartes
        if (Input.GetKeyDown(KeyCode.R))
        {
            pli.GetComponent<Pli>().ResetPli();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            timedMessagePopup.SetVisible(true);
        }
    }
}

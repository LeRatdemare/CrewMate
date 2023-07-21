using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TableauCartes : MonoBehaviour
{
    public GameManager gameManager;
    public TheCrewGame theCrewGame;
    public enum State
    {
        HandCardsSelection, OtherPlayerCardSelection, Hiden //Il faudra rajouter UserCommunicating and OtherPlayerCommunicating
    }
    public State currentState;
    // Start is called before the first frame update
    public GameObject[,] cartes;
    public GameObject cardPrefab;
    public int NB_COULEURS;
    public int NB_VALEURS;
    void Start()//On met en place le tableau au démarrage du jeu
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();

        cartes = new GameObject[NB_COULEURS, NB_VALEURS];

        for (int couleur = 0; couleur < NB_COULEURS; couleur++)
        {
            for (int valeur = 0; valeur < NB_VALEURS; valeur++)
            {
                if (couleur == 0 && (valeur == 0 || valeur > 3)) continue;

                Vector3 pos = new Vector3(1.7f * valeur - 6.8f, 1.5f * couleur - 0.6f, 0);
                GameObject carte = Instantiate(cardPrefab, pos, Quaternion.identity);

                Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/0{couleur}{valeur + 1}");
                carte.GetComponent<SpriteRenderer>().sortingLayerName = "Popup";
                carte.GetComponent<SpriteRenderer>().sortingOrder = couleur;//Pour savoir si la carte va s'afficher au dessus ou en dessous des autres, en fonction de sa couleur ?
                carte.transform.localScale = new Vector3(0.66f, 0.66f, 1.2f);
                carte.transform.parent = transform;

                carte.GetComponent<Card>().Activer(0, (Card.Couleur)couleur, valeur + 1, sprite, Card.ConteneurCarte.TableauCartes);
                cartes[couleur, valeur] = carte;
            }
        }
    }

    public void SetState(State state)
    {
        switch (state)
        {
            case State.Hiden:
                gameObject.SetActive(false);
                break;
            case State.HandCardsSelection://Pour l'instant il n'y a rien de différent entre OtherPlayer et Handcard mais il faudra rajouter un message popUp
                gameObject.SetActive(true);
                break;
            case State.OtherPlayerCardSelection://Est ce que c'est ici qu'on le rajoutera ou dans SwitchPhase directement , mais dans ce cas pourquoi mettre 2 options quifont la meme chose
                gameObject.SetActive(true);
                //Je pense que ca fait plus sens de le rajouter ici
                break;
        }
        currentState = state;
    }
    public void DeselectAllCards()
    {
        for (int couleur = 0; couleur < theCrewGame.NbColors; couleur++)
        {
            for (int valeur = 0; valeur < 9; valeur++)
            {
                GameObject cardObject = cartes[couleur, valeur];
                if (cardObject != null)
                {
                    Card card = cardObject.GetComponent<Card>();
                    if (card.Selected)
                        card.Selected = false;
                }
            }
        }
    }
    public Card GetSelectedCard()
    {
        for (int couleur = 0; couleur < theCrewGame.NbColors; couleur++)
        {
            for (int valeur = 0; valeur < 9; valeur++)
            {
                GameObject cardObject = cartes[couleur, valeur];
                if (cardObject != null)
                {
                    Card card = cardObject.GetComponent<Card>();
                    if (card.Selected)
                        return card;
                }
            }
        }
        // Ne devrait pas arriver, seulement si le joueur n'a rien sélectionné
        return null;
    }
}

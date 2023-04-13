using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TableauCartes : MonoBehaviour
{
    public enum State
    {
        HandCardsSelection, OtherPlayerCardSelection, Hiden
    }
    public State currentState;
    // Start is called before the first frame update
    public GameObject[,] cartes;
    public GameObject cardPrefab;
    public GameManager gameManager;
    public int NB_COULEURS;
    public int NB_VALEURS;
    void Start()
    {
        cartes = new GameObject[NB_COULEURS, NB_VALEURS];

        for (int couleur = 0; couleur < NB_COULEURS; couleur++)
        {
            for (int valeur = 0; valeur < NB_VALEURS; valeur++)
            {
                if (couleur == 0 && (valeur == 0 || valeur > 3)) continue;

                Vector3 pos = new Vector3(1.7f * valeur - 6.8f, 1.1f * couleur - 0.6f, 0);
                GameObject carte = Instantiate(cardPrefab, pos, Quaternion.identity);

                Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/0{couleur}{valeur + 1}");
                carte.GetComponent<SpriteRenderer>().sortingLayerName = "Popup";
                carte.GetComponent<SpriteRenderer>().sortingOrder = couleur;
                carte.transform.localScale = new Vector3(0.55f, 0.55f, 1);
                carte.transform.parent = transform;

                carte.GetComponent<Card>().Activer(gameManager, 0, (Card.Couleur)couleur, valeur + 1, sprite, Card.ConteneurCarte.TableauCartes);
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
            case State.HandCardsSelection:
                gameObject.SetActive(true);
                break;
            case State.OtherPlayerCardSelection:
                gameObject.SetActive(true);
                break;
        }
        currentState = state;
    }
}

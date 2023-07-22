using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauTache : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gameManager;
    public TheCrewGame theCrewGame;
    public enum State
    {
        TasksSelection, Hiden //peut être enlever l'énum s'il n'y a qu'un seul state (je le garde pour l'instant au cas ou)
    }
    public State currentState; //Aussi peut être enlever ca pour la même raison que précedemment
    public GameObject[,] cartes;
    public GameObject cardPrefab;
    public int NB_COULEURS; //Pas en soucis car on changera le nombre de valeur en attachant le script à l'objet
    public int NB_VALEURS;//très probablement à garder sans modifier

    void Start() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();

        cartes = new GameObject[NB_COULEURS, NB_VALEURS];

        for (int couleur = 1; couleur < NB_COULEURS; couleur++)
        {
            for (int valeur = 0; valeur < NB_VALEURS; valeur++)
            {

                Vector3 pos = new Vector3(1.7f * valeur - 6.8f, 2.5f * couleur-5f, 0);
                GameObject carte = Instantiate(cardPrefab, pos, Quaternion.identity);

                Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/1{couleur}{valeur + 1}");
                carte.GetComponent<SpriteRenderer>().sortingLayerName = "Popup";
                carte.GetComponent<SpriteRenderer>().sortingOrder = couleur;
                carte.transform.localScale = new Vector3(0.66f, 0.66f, 1.2f);
                carte.transform.parent = transform;

                carte.GetComponent<Card>().Activer(0, (Card.Couleur)couleur, valeur + 1, sprite, Card.ConteneurCarte.TableauTache);
                cartes[couleur, valeur] = carte;
            }
        }
    }

    public void SetState(State state) //Est ce que j'ai vraiment besoin de ca ?
    {
        switch (state)
        {
            case State.Hiden:
                gameObject.SetActive(false);
                break;
            case State.TasksSelection:
                gameObject.SetActive(true);
                break;
        }
        currentState = state;
    }
    public void DeselectAllCards()//Au final je ne l'ai pas utilisé mais peut être le garder au cas où
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
    public List<Card> GetSelectedCard()
    {
        var cartesSelectionnees = new List<Card>();//Le même que pour tableauCarte mais on peut en renvoyer plusieurs
        for (int couleur = 0; couleur < theCrewGame.NbColors; couleur++)
        {
            for (int valeur = 0; valeur < 9; valeur++)
            {
                GameObject cardObject = cartes[couleur, valeur];
                if (cardObject != null)
                {
                    Card card = cardObject.GetComponent<Card>();
                    if (card.Selected)
                        cartesSelectionnees.Add(card);
                        
                }
            }
        }
        return cartesSelectionnees;
    }
}

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
            int color = Random.Range(1, 4); // On génère au hasard une couleur excepté les noirs
            int cardValue = Random.Range(1, 9); // Une génère la valeur de la carte
            Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/0{color}{cardValue}");

            GameObject carte = handPanel.GetComponent<HandPanel>().GetFirstFreeSlot();
            carte.GetComponent<Card>().Activer(this, sprite, Utils.ConteneurCarte.HandPanel);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tableauCartes.GetComponent<TableauCartes>().InverserVisibilite();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            pli.GetComponent<Pli>().ResetPli();
        }

    }
}

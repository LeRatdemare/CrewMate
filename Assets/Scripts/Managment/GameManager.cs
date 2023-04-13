using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    private GameObject[,] cards;
    [SerializeField] public int nbColors;
    [SerializeField] public int nbCardsPerColor;
    [SerializeField] public int spriteWidth;
    [SerializeField] public int spriteHeight;

    // Start is called before the first frame update
    void Start()
    {
        cards = new GameObject[nbColors, nbCardsPerColor];
        // Chaque ligne correspond à une couleur
        for (int i = 0; i < nbColors; i++)
        {
            int color = Random.Range(1, 4); // On génère au hasard une couleur excepté les noirs
            int cardValue = Random.Range(1, 9); // Une génère la valeur de la carte
            Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/0{color}{cardValue}");
            Vector3 pos = new Vector3(1.7f * i - 9, -3.5f, 0);
            rndCards.Add(Instantiate(cardPrefab, pos, Quaternion.identity));
            rndCards[i].GetComponent<SpriteRenderer>().sprite = sprite;
            rndCards[i].transform.localScale = new Vector3(0.55f, 0.55f, 1);
            rndCards[i].transform.parent = GameObject.Find("HandPanel").transform;
        }
    }
}

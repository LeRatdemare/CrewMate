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
            // Sur la colonne on met la carte i
            for (int j = 0; j < nbCardsPerColor; j++)
            {
                Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/0{i}{j}");
                Vector3 pos = new Vector3(2.5f * j - 9, i * 4 - 4, 0);
                cards[i, j] = Instantiate(cardPrefab, pos, Quaternion.identity);
                cards[i, j].GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }
}

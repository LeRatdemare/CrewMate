using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauCartes : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[,] cartesEnJeu;
    public GameObject cardPrefab;
    public int NB_COULEURS;
    public int NB_VALEURS;
    void Start()
    {
        cartesEnJeu = new bool[NB_COULEURS, NB_VALEURS];

        for (int couleur = 0; couleur < NB_COULEURS; couleur++)
        {
            for (int valeur = 0; valeur < NB_VALEURS; valeur++)
            {
                Vector3 pos = new Vector3(1.7f * valeur - 6.8f, 1.1f * couleur - 0.5f, 0);
                GameObject carte = Instantiate(cardPrefab, pos, Quaternion.identity);

                carte.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>($"Images/Cartes/0{couleur}{valeur + 1}");
                carte.GetComponent<SpriteRenderer>().sortingLayerName = "Popup";
                carte.GetComponent<SpriteRenderer>().sortingOrder = couleur;
                carte.transform.localScale = new Vector3(0.55f, 0.55f, 1);
                carte.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TableauCartes : MonoBehaviour
{
    // Start is called before the first frame update
    public bool[,] cartesEnJeu;
    public GameObject cardPrefab;
    public GameManager gameManager;
    public int NB_COULEURS;
    public int NB_VALEURS;
    void Start()
    {
        cartesEnJeu = new bool[NB_COULEURS, NB_VALEURS];

        for (int couleur = 0; couleur < NB_COULEURS; couleur++)
        {
            for (int valeur = 0; valeur < NB_VALEURS; valeur++)
            {
                Vector3 pos = new Vector3(1.7f * valeur - 6.8f, 1.1f * couleur - 0.6f, 0);
                GameObject carte = Instantiate(cardPrefab, pos, Quaternion.identity);

                Sprite sprite = Resources.Load<Sprite>($"Images/Cartes/0{couleur}{valeur + 1}");
                carte.GetComponent<SpriteRenderer>().sortingLayerName = "Popup";
                carte.GetComponent<SpriteRenderer>().sortingOrder = couleur;
                carte.transform.localScale = new Vector3(0.55f, 0.55f, 1);
                carte.transform.parent = transform;

                carte.GetComponent<Card>().Activer(gameManager, sprite, Utils.ConteneurCarte.TableauCartes);
            }
        }
    }

    public void InverserVisibilite()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}

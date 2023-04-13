using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    //public Carte[] Taches{get; set;}
    private bool active;
    public int numero;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<TextMesh>().text = $"Joueur {numero}";

        if (numero == 2) Activer(); // Temporaire pour le test
    }

    public void Activer()
    {
        active = true;
        GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
    }
}

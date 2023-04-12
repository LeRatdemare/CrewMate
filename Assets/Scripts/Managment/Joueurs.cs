using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueurs : MonoBehaviour
{
    private static int NB_JOUEURS;
    public Card[] Main { get; private set; }
    //public Carte[] Tache{get; set;}
    public int Numero { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Numero = NB_JOUEURS++;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

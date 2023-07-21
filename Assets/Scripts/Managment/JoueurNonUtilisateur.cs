using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurNonUtilisateur : Joueur
{

    // public enum objetDansLaScene{
    //     Tache0 =0 , Tache1 = 1, Tache2 = 2, Nom =3, CarteEnMain=4, Communication=5
    // }

    public override void Activer()
    {
        base.Activer();
        GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
    }
    protected override void Init()
    {
        base.Init();
        transform.GetChild(0).GetComponent<TextMesh>().text = $"Player {numero}";
    }
}

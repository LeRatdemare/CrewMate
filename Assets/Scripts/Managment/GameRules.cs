using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class règledejeu : MonoBehaviour
{
    // //le choix de comment obtenir la couleur est à réfléchir
    // public Card[] CardPlayed=new Card[3];
    // private int atoutActuel;
    // public int AtoutActuel{
    //     get{return atoutActuel;}
    //     set{
    //         if (value==-1)
    //             atoutActuel=value;
    //         else
    //             if (CardPlayed.Length()==0) atoutActuel=value;
    //     }
    // }

    // public Card CArteLaPlusForteDuPli(){
    //     Card WinningCard=CardPlayed[0];
    //     for (int i=1; i<Length(CardPlayed); i++)
    //     {
    //         WinningCard=CardChallenge(WinningCard,CardPlayed[i]);
    //     }
    //     return WinningCard;
    // }

    // private Card CardChallenge(Card Champion, Card Challenger){
    //     if (Champion.Couleur==Challenger.Couleur)   //si les couleurs sont les mêmes,
    //     {
    //         if(Champion.Nombre>Challenger.Nombre) //il suffit de comparer les nombres.
    //             return Champion;
    //         else
    //             return Challenger;
    //     }
    //     else                                //sinon seul la couleur est importante
    //     {
    //         if (Challenger.Couleur==Noir) //si le challengeur est noir alors champion 
    //             return Challenger;        //est de la couleur de l'atout donc le challengeur gagne
    //         else                    //sinon challengeur est soit ni noir ni de la couleur de l'atout donc il perd automatiquement
    //             return Champion;    //ou challengeur est de la couleur de l'atout mais puisque Champion n'est pas de la couleur de Challengeur et que il est soit noir, soit de la couleur de l'atout alors Champion est forcément noir, donc Champion gagne.
    //     }       
    // }

    // //A mettre dans programme mais pour l'instant je le stocke ici
    // public bool PlayableCard(Card Carte, int Atout) 
    // {
    //     //Atout=GameObjectDuJeu.GetComponnant<GameRules>();
    //     if (Atout==-1)   //Atout = -1 signifie que il n'y a aucun atout actuel donc pas de carte jouée à ce pli
    //         return true;
    //     else
    //     {
    //         if (HandHoldColor(Atout))   //Si on a des cartes dans la main qui a la couleur de l'atout
    //         {
    //             if (Carte.Couleur==Atout)
    //                 return true;
    //             else
    //                 return false;
    //         }
    //         else
    //             return true;
    //     }
    // }

    // public bool HandHoldColor(int Couleur)
    // {
    //     foreach(Card Carte in Joueur.Main)
    //     {
    //         if (Carte.Couleur==Couleur)
    //         {
    //             return true;
    //         }
    //     }
    //     return false;
    // }

    // //A mettre dans Carte
    // public string AvailableCommunication(int[] MaxParCouleur, int[] MinParCouleur, Carte Carte) //changer argument Carte en fonction de l'endroit où on dois placer la fonnction, si dans carte retirer totalement l'argument
    // {
    //     // Ordre des couleurs :  Bleu, Jaune, Rose
    //     int IndiceCouleur = Carte.Couleur-1; //on réduit de 1 pour que ça passe dans le tableau
    //     if (IndiceCouleur==-1) //si c'est une carte noir
    //         return None;
    //     else
    //     {
    //         if (Carte.Nombre==MaxParCouleur[IndiceCouleur]) //si carte est la carte du haut
    //         {
    //             if (Carte.Nombre==MinParCouleur[IndiceCouleur]) //si carte est la carte du bas
    //                 return "Milieu";    //la carte est la plus haute et la plus basse donc la seul
    //             else
    //                 return "Haut";
    //         }
    //         else
    //         {
    //             if (Carte.Nombre==MinParCouleur[IndiceCouleur]) // si carte est la carte du bas
    //                 return "Bas";
    //             else
    //                 return None;
    //         }
    //     }
    // }
    

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

}

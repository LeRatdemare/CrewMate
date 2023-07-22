using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPanel : MonoBehaviour
{
    public int nbSlots;
    public List<Card> UserHand = new List<Card>();
    public GameObject GetFirstFreeSlot()
    {
        GameObject carte;
        for (int i = 0; i < nbSlots; i++)
        {
            carte = transform.GetChild(i).gameObject;
            if (!carte.GetComponent<Card>().Activee)
            {
                return carte;
            }
        }
        return null;
    }
    public int GetCardsCount()
    {
        int cardsCount = 0;
        for (int i = 0; i < nbSlots; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().Activee)
                cardsCount++;
        }
        return cardsCount;
    }
    public void DeselectAllCards()
    {
        for (int i = 0; i < nbSlots; i++)
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            if (card.Selected)
                card.Selected = false;
        }
    }
    public Card GetSelectedCard()
    {
        for (int i = 0; i < nbSlots; i++)
        {
            Card card = transform.GetChild(i).GetComponent<Card>();
            if (card.Selected)
                return card;
        }
        // Ne devrait pas arriver, seulement si le joueur n'a rien sélectionné
        return null;
    }

    public void GetAllCards(){
        for(int i=0; i< transform.childCount;i++){
            Card carte = transform.GetChild(i).GetComponent<Card>();
            if(carte.Activee== true){
                UserHand.Add(carte);
            }
        }
    }

    public void DisplayPlayabilityInThePanel(){
        for(int i = 0; i<UserHand.Count; i++){
            if(UserHand[i].IsPlayable(UserHand)){
                UserHand[i].Interactable=true;
            }
            else{
                UserHand[i].Interactable=false;
            }
        }
    }

    public void DisplayCommunicability(){
        
    }

    public void GetAllCardsInteractable(){
        for(int i = 0; i<UserHand.Count; i++){
           if(UserHand[i].Interactable== false) {
                UserHand[i].Interactable= true;
           }
        }
    }

    //Faire une fonction qui récupère toutes les cartes du HandPanel et apelle IsPlayable dessus
    //On grise alors les cartes qui ne sont pas jouables, 
    //. on appelle la fonction dans ThecrewGame
    //quand on est à la phase UserPlaying
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool selected;
    [SerializeField]
    private Utils.ConteneurCarte conteneur;
    private GameManager gameManager;
    public bool Activee { get; private set; } = false;

    public void Activer(GameManager gameManager, Sprite sprite, Utils.ConteneurCarte conteneur)
    {
        this.gameManager = gameManager;
        GetComponent<SpriteRenderer>().sprite = sprite;
        this.conteneur = conteneur;
        Activee = true;
    }
    public void Desactiver()
    {
        GetComponent<SpriteRenderer>().sprite = null;
        Activee = false;
    }
    void OnMouseDown()
    {
        if (Activee)
        {
            switch (conteneur)
            {
                case Utils.ConteneurCarte.HandPanel:
                    PlayCard();
                    break;
                case Utils.ConteneurCarte.TableauCartes:
                    AjouterDansLaMain();
                    break;
            }
        }
    }
    void PlayCard()
    {
        // int slotIndex = 1;
        // GameObject slot = GameObject.Find($"Slot{slotIndex}");
        // while (slotIndex < 6 && slot.GetComponent<Card>().Activee)
        // {
        //     slotIndex++;
        //     slot = GameObject.Find($"Slot{slotIndex}");
        // }
        // if (slotIndex < 6)
        // {
        //     slot.GetComponent<Card>().Activer(gameManager, GetComponent<SpriteRenderer>().sprite, Utils.ConteneurCarte.Pli);
        // }
        Pli pli = gameManager.pli.GetComponent<Pli>();
        GameObject slot = pli.GetRandomFreeSlot();
        if (slot != null)
        {
            slot.GetComponent<Card>().Activer(gameManager, GetComponent<SpriteRenderer>().sprite, Utils.ConteneurCarte.Pli);
            Desactiver();
        }
        else
        {
            Debug.Log("Il n'y a plus de slot disponible dans le pli");
        }
    }
    void AjouterDansLaMain()
    {

    }
    void OnMouseEnter()
    {
        // transform.localScale *= 1.5f;
        GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 0.7f);
    }
    void OnMouseExit()
    {
        // transform.localScale /= 1.5f;
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
    }
}

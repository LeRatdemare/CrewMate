using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool selected;

    void OnMouseDown()
    {
        int slotIndex = 1;
        GameObject slot = GameObject.Find($"Slot{slotIndex}");
        while (slotIndex < 6 && !slot.GetComponent<CardSlot>().IsFree)
        {
            slotIndex++;
            slot = GameObject.Find($"Slot{slotIndex}");
        }
        if (slotIndex < 6)
        {
            slot.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            slot.GetComponent<CardSlot>().IsFree = false;
        }
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

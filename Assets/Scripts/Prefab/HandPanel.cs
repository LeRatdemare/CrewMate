using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPanel : MonoBehaviour
{
    public int nbSlots;
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
}

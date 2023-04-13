using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pli : MonoBehaviour
{
    public int nbSlots;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ResetPli()
    {
        for (int i = 0; i < nbSlots; i++)
        {
            transform.GetChild(i).GetComponent<Card>().Desactiver();
        }
    }

    public GameObject GetRandomFreeSlot()
    {
        if (GetNbOccupiedSlots() >= 5) return null;

        int slotIndex = Random.Range(0, nbSlots);
        GameObject slot;
        do
        {
            slot = transform.GetChild(slotIndex).gameObject;
            slotIndex = (slotIndex + 1) % nbSlots;
        }
        while (slot.GetComponent<Card>().Activee);
        return slot;
    }
    public int GetNbOccupiedSlots()
    {
        int nbOccupiedSlots = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Card>().Activee) nbOccupiedSlots++;
        }
        return nbOccupiedSlots;
    }
}

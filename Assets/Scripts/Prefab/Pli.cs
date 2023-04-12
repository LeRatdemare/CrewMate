using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pli : MonoBehaviour
{
    public int nbExistingSlots;
    public int nbPlayableSlots;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ResetPli()
    {
        for (int i = 0; i < nbExistingSlots; i++)
        {
            transform.GetChild(i).GetComponent<Card>().Desactiver();
        }
    }

    public GameObject GetRandomFreeSlot()
    {
        if (GetNbOccupiedSlots() >= nbPlayableSlots) return null;

        int slotIndex = Random.Range(0, nbExistingSlots);
        GameObject slot;
        do
        {
            slot = transform.GetChild(slotIndex).gameObject;
            slotIndex = (slotIndex + 1) % nbExistingSlots;
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

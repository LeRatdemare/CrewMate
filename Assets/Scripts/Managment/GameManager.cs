using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(i * 20, 0, 0);
            GameObject card = Instantiate(cardPrefab, pos, Quaternion.identity);
            card.GetComponent<Image>().sprite = Resources.Load<Sprite>("suit_life_meter_2");
        }
    }
}

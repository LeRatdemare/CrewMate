using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerSelectionPopup : MonoBehaviour
{
    FirstPlayerSelectionButton Player1Button;
    FirstPlayerSelectionButton Player2Button;
    FirstPlayerSelectionButton Player3Button;

    // Start is called before the first frame update
    void Start()
    {
        Player1Button = transform.GetChild(1).GetComponent<FirstPlayerSelectionButton>();
        Player2Button = transform.GetChild(2).GetComponent<FirstPlayerSelectionButton>();
        Player3Button = transform.GetChild(3).GetComponent<FirstPlayerSelectionButton>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

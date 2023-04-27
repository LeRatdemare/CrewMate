using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayerSelectionPopup : MonoBehaviour
{
    FirstPlayerSelectionButton UserButton;
    FirstPlayerSelectionButton Player1Button;
    FirstPlayerSelectionButton Player2Button;

    // Start is called before the first frame update
    void Start()
    {
        UserButton = transform.GetChild(0).GetComponent<FirstPlayerSelectionButton>();
        Player1Button = transform.GetChild(1).GetComponent<FirstPlayerSelectionButton>();
        Player2Button = transform.GetChild(2).GetComponent<FirstPlayerSelectionButton>();
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

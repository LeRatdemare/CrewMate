using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnMouseDown()
    {
        transform.position = Input.mousePosition;
    }
    void OnMouseEnter()
    {
        transform.localScale *= 2f;
    }
    void OnMouseExit()
    {
        transform.localScale /= 2f;
    }
}

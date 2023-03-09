using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private bool selected;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnMouseDown()
    {
        if (selected)
        {
            transform.position -= new Vector3(0, 0.5f, 0);
            selected = false;
        }
        else
        {
            transform.position += new Vector3(0, 0.5f, 0);
            selected = true;
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

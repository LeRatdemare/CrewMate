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
        transform.localScale *= 2;
    }
    void OnMouseEnter()
    {
        transform.position += Vector3.up * 3f;
    }
    void OnMouseExit()
    {
        transform.position += Vector3.down * 3f;
    }
}

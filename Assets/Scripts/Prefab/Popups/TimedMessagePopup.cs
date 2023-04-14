using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimedMessagePopup : MonoBehaviour
{
    float lifeTime = 0;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetTitle("My Popup");
    }
    public void AttachToCanvas(Canvas canvas)
    {
        transform.SetParent(canvas.transform);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        SetMessage(string.Format("{0:0.00}", lifeTime));
    }
    public void SetMessage(string message)
    {
        TextMeshProUGUI tmp = transform.GetChild(1).GetComponent<TextMeshProUGUI>(); // On récupère le message de la popup
        tmp.SetText(message);
    }
    public void SetTitle(string title)
    {
        TextMeshProUGUI tmp = transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // On récupère le titre de la popup
        tmp.SetText(title);
    }
}

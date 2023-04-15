using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimedMessagePopup : MonoBehaviour
{
    GameManager gameManager;
    public float timeBeforeDeath = 2; // Temps avant disparition en secondes
    float lifeTime = 0;
    public bool Visible { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        SetTitle("My Popup");
    }
    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        SetMessage(string.Format("{0:0.00}", lifeTime));

        if (lifeTime > timeBeforeDeath) SetVisible(false);
    }
    public void AttachToCanvas(Canvas canvas)
    {
        transform.SetParent(canvas.transform);
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
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
    public void SetVisible(bool visible)
    {
        gameObject.SetActive(visible);
        Visible = visible;

        if (!Visible) lifeTime = 0;
    }
}

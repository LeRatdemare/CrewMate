using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    protected GameManager gameManager;
    protected TheCrewGame theCrewGame;
    //public Carte[] Taches{get; set;}
    protected bool active;
    public int numero;
    public List<Card> remainingTasks;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }
    protected virtual void Init()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        theCrewGame = GameObject.Find("GameManager").GetComponent<TheCrewGame>();
        remainingTasks = new List<Card>();
        if (numero == 2) Activer(); // Temporaire pour le test
    }
    public virtual void Activer()
    {
        active = true;
    }

    /** <summary> Retire et renvoie les tâches effectuées grâce au pli actuel.
    !! Doit être appelée sur le gagnant du pli !!</summary>
    */
    public List<Card> CheckSuccessfulTasks()// Faire les vérifs aussi pour les joueurs qui n'ont pas gagné le pli
    {
        List<Card> completedTasks = new List<Card>();
        // Pour chaque tâche, on regarde si elle est dans le pli
        for (int i = 0; i < remainingTasks.Count; i++)
        {
            // Si elle est dans le pli on l'ajoute aux tâches complétées et on la supprime des tâches restantes
            if (gameManager.pli.IsInPli(remainingTasks[i]))
            {
                completedTasks.Add(remainingTasks[i]);
                remainingTasks.RemoveAt(i);
            }
        }
        return completedTasks;
    }
}

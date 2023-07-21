using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joueur : MonoBehaviour
{
    protected GameManager gameManager;
    protected TheCrewGame theCrewGame;
    
    protected bool active;
    public int numero;
    public bool communication = false;
    public List<Card> remainingTasks;
    public List<Card> remainingCards;//Mettre none pour joueurNonUtilisateur ou rien en fait

    
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
    public TheCrewGame.EtatDelaPartie CheckSuccessfulTasks()// Faire les vérifs aussi pour les joueurs qui n'ont pas gagné le pli
    {
        bool tacheTrouvee= false;
        List<Card> completedTasks = new List<Card>();
        // Pour chaque tâche, on regarde si elle est dans le pli
        for (int i = 0; i < remainingTasks.Count; i++)
        {
            // Si elle est dans le pli on l'ajoute aux tâches complétées et on la supprime des tâches restantes
            if (gameManager.pli.IsInPli(remainingTasks[i]))
            {
                if(numero == theCrewGame.currentPlayer)//On vérifie si le joueur est le gagnant du pli, et donc s'il a bien réalisé la tache ou l'a perdue
                {
                    completedTasks.Add(remainingTasks[i]);
                    remainingTasks.RemoveAt(i);
                }
                tacheTrouvee= true;
                //Ajouter quelques chose ici pour créer un effet visuel qui indiquera sur l'écran que la tache a été réalisée (ou pas si le pli n'a pas été remporté par la bonne personne)
            }
        }
        if(tacheTrouvee==true )
        {
            Debug.Log($"numéro : {numero}, currentPlayer: {theCrewGame.currentPlayer}");
            if(numero == theCrewGame.currentPlayer){//Le joueur a gagné sa tache
                Debug.Log((theCrewGame.currentPlayer == 0 ? $"Vous avez" : $"Le joueur {theCrewGame.currentPlayer} a") + $" complété {completedTasks.Count} tâches.");
            }
            else{//Le joueur a perdu sa tache
                Debug.Log("Vous avez perdue cette partie");
                return TheCrewGame.EtatDelaPartie.Perdu;
            }
        }
        
        if(remainingTasks.Count==0 ){//On regarde s'il reste des taches à accomplir au joueur
            theCrewGame.NbJoueurSansTache++;//S'il n'en reste plus, on incrémente le nombre de joueur sans tache
            if(theCrewGame.NbJoueurSansTache== theCrewGame.NbPlayers){//Si plus aucun joueurs n'a de tache
                return TheCrewGame.EtatDelaPartie.Gagne;//La partie est gagnée
            }
        }
        
        return TheCrewGame.EtatDelaPartie.EnCours;
    }

}

public class Joueur
{
    private static int NB_JOUEURS;
    //public Carte[] Main{get; set;}
    //public Carte[] Tache{get; set;}
    public int Numero { get; private set; }

    public Joueur()
    {
        Numero = NB_JOUEURS++;
    }
}

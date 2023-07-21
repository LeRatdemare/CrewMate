using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurUtilisateur : Joueur
{
    int nbCouleur = 3; //temporaire tant qu'on a pas relié le choix de setting du joueur à la partie actuelle
    List<(int max,int min)> MaxMinCouleur = new List<(int,int)>();//initialiser la liste avec des 0 comme max et 10 comme min ou des null s'ils peuvent être comparé à des nombres
    
    protected override void Init()
    {
        base.Init();
        for(int i=0; i<nbCouleur;i++){
            MaxMinCouleur.Add((0,10));
        }
    }
    
    
    public void AjouterMaxMinCouleur(int couleur, int valeur){

        if(couleur!=0){ //On vérifie que la carte qu'on vient d'ajouter n'est pas un atout

            
            if(MaxMinCouleur[couleur-1].min > valeur){//On met couleur -1 car les couleurs vont de 1 à 3 alors que les indices vont de 0 à 2
            MaxMinCouleur[couleur-1] = (MaxMinCouleur[couleur-1].max,valeur);
            }
        
            if(MaxMinCouleur[couleur-1].max < valeur){
                MaxMinCouleur[couleur-1] = (valeur, MaxMinCouleur[couleur-1].min);
            }

            Debug.Log($"Meilleur carte de la couleur: {MaxMinCouleur[couleur-1].max}");
            Debug.Log($"Pire carte de la couleur : {MaxMinCouleur[couleur-1].min}");
        }
        
    }

    public void RetirerMaxMinCouleur(int couleur, int valeur){

        if (couleur != 0){ //On vérifie que la carte qu'on vient d'ajouter n'est pas un atout
            
            if(MaxMinCouleur[couleur-1].min != MaxMinCouleur[couleur-1].max){//Il n'y a pas qu'une seule carte de cette couleur
            
                int i = 0;
                GameObject HandPanel = GameObject.Find("HandPanel");
                Card carte ;
            
                if(MaxMinCouleur[couleur-1].min == valeur){
                
                    int nouvelleValeur = 10;
                    do{
                        carte = HandPanel.transform.GetChild(i).GetComponent<Card>();
                        if (carte.Activee && (int)carte.Color == couleur && carte.Value < nouvelleValeur && carte.Value != valeur){//On est obligé de faire carte.Value != valeur car la carte n'est pas encore désactivée et donc se trouve encore dans le HandPanel
                            nouvelleValeur = carte.Value;
                        }
                        i++;
                    }while (HandPanel.transform.childCount > i);

                    MaxMinCouleur[couleur-1]= (MaxMinCouleur[couleur-1].max,nouvelleValeur);                
                
                }
                else if(MaxMinCouleur[couleur-1].max == valeur){
                    int nouvelleValeur = 0;
                
                    do{
                        carte = HandPanel.transform.GetChild(i).GetComponent<Card>();
                        if (carte.Activee  &&(int)carte.Color == couleur && carte.Value > nouvelleValeur && carte.Value != valeur){
                            nouvelleValeur= carte.Value;
                        }
                        i++;
                    }while (HandPanel.transform.childCount > i);

                    MaxMinCouleur[couleur-1]= (nouvelleValeur,MaxMinCouleur[couleur-1].min); 

                }
            }

            else if(MaxMinCouleur[couleur-1].min == valeur){ //On vient de retirer l'unique carte de cette couleur
                MaxMinCouleur[couleur-1]= (0,10);//on réinitialise les valeurs
            }   

            Debug.Log($"Meilleur carte de la couleur: {MaxMinCouleur[couleur-1].max}");
            Debug.Log($"Pire carte de la couleur : {MaxMinCouleur[couleur-1].min}"); 

        }  
    }

    public string CommuniquerOuPas(Card carte){
        
        if (carte.Color != 0){
            
            if(carte.Value == MaxMinCouleur[(int)carte.Color - 1].max){
                
                if(carte.Value == MaxMinCouleur[(int)carte.Color - 1].min){
                    return "Milieu";
                }
                
                else{
                    return "Haut";
                }
            }
        
            else if(carte.Value == MaxMinCouleur[(int)carte.Color - 1].min){
                return "Bas";
            }
        
            else{
                return "Rien";//La carte n'est pas communicable car ce n'est ni la plus petite, ni la plus grande, ni l'unique
            }
    }
    else{
        return "Atout";//La carte n'est pas communicable car il s'agit d'un atout
    }
        }

    //Faire une fonction qui récupère les cartes communicable et les entoure d'un allo bleu
    //Pour indiquer qu'elles sont communicables
        
     
}


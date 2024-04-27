using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MotsDefiles
{
    public class Joueur
    {
        private string nom;
        private int score = 0;
        private List<string> motsConnus = new List<string>();
        private int nbPartieJoue = 0;

        /// <summary>
        /// Constructeur de la classe Joueur
        /// </summary>
        /// <param name="nom"> string : nom du joueur </param>
        public Joueur(string nom)
        {
            string nomModifie = "";
            if (nom.Length > 0)
            {
                nom = nom.ToLower();
                nomModifie = Convert.ToString(nom[0]).ToUpper();
                for (int i = 1; i < nom.Length; i++)
                {
                    nomModifie += nom[i];
                }
            }
            this.nom = nomModifie;
        }

        /// <summary>
        /// Retourne le string "nom" correspondant au nom du joueur
        /// </summary>
        public string Nom
        {
            get { return nom; }
        }

        /// <summary>
        /// Retourne l'entier "score" correspondant au score total du joueur pendant une partie
        /// </summary>
        public int Score
        {
            get { return score; }
            set { this.score = value; }
        }

        /// <summary>
        /// Retourne l'entier "nbPartieJoue" correspondant au nombre de parties jouées par le joueur
        /// </summary>
        public int NbPartieJoue
        {
            get { return nbPartieJoue; }
            set { nbPartieJoue = value; }
        }

        /// <summary>
        /// Retourne la liste de string "motsConnus" correspondant à l'ensemble des mots mis en entrée par le joueur
        /// </summary>
        public List<string> MotsConnus
        {
            get { return motsConnus; }
        }

        /// <summary>
        /// Ajoute le string passé en paramètres à la liste de string "motsConnus"
        /// </summary>
        /// <param name="mot"> string : mot qu'on cherche à rajouter à la liste de string "motsConnus" </param>
        public void Add_Mot(string mot)
        {
            if (mot != null && !Contient(mot))
            {
                this.motsConnus.Add(mot);
            }
        }

        /// <summary>
        /// Retourne les différents paramètres d'une instance de Joueur sous la forme d'un string
        /// </summary>
        /// <returns> string : les différents paramètres d'une instance de Joueur </returns>
        public string toString()
        {
            string phrase = "Joueur : " + this.nom
                + "\nScore : " + this.score + " point";

            if (this.score != 0)
            {
                phrase += "s";
            }

            if (this.motsConnus.Count() == 0)
            {
                phrase += "\nAucun mot trouvé.";
            }
            else
            {
                phrase += "\nMots trouvés : " + string.Join(", ", this.motsConnus.ToArray());
            }
            return phrase;
        }

        /// <summary>
        /// Retourne le coefficient de la lettre rentré en paramètres selon le fichier "Lettre.txt"
        /// </summary>
        /// <param name="c"> char : lettre dont on cherche le coefficient </param>
        /// <returns> entier : valeur du coefficient du char c </returns>
        private int CoefLettre(char c)
        {
            int rep = -1;
            if (File.Exists("Lettre.txt"))
            {
                string[] lignesFichier = File.ReadAllLines("Lettre.txt");
                for (int i = 0; i < lignesFichier.Length; i++)
                {
                    lignesFichier = lignesFichier[i].Split(',');
                    if (c == Convert.ToChar(lignesFichier[0]))
                    {
                        return rep = Convert.ToInt32(lignesFichier[2]);
                    }
                    lignesFichier = File.ReadAllLines("Lettre.txt");
                }
            }
            return rep;
        }

        /// <summary>
        /// Retourne et ajoute à "score" l'entier correspondant à la valeur du string "mot" rentré en paramètres
        /// </summary>
        /// <param name="mot"> string : mot dont on cherche la valeur pour la rajouter au score du joueur </param>
        /// <returns> int : la valeur du string "mot" </returns>
        public int Add_Score(string mot)
        {
            int val = 0;
            double multiplicateur;
            if (mot.Length < 10)
            {
                multiplicateur = 1 + (mot.Length / 100);
            }
            else
            {
                multiplicateur = 2 + (mot.Length % 10) / 100;
            }
            foreach (char c in mot)
            {

                val += (int)(CoefLettre(c) * multiplicateur);
            }
            this.score += val;
            return val;
        }

        /// <summary>
        /// Rajoute l'entier score au score du joueur
        /// </summary>
        /// <param name="score"> int : score à ajouter </param>
        public void Add_Score_WithNb(int score)
        {
            this.score += score;
        }

        /// <summary>
        /// Vérifie si le mot est contenu dans la liste de string "motsConnus"
        /// </summary>
        /// <param name="mot"> string : mot cherché dans la liste de string "motsConnus" </param>
        /// <returns> VRAI si le mot appartient à "motsConnus", sinon FAUX </returns>
        public bool Contient(string mot)
        {
            return this.motsConnus.Contains(mot);
        }

    }
}
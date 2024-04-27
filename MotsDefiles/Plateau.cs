using System;
using System.Collections.Generic;
using System.IO;

namespace MotsDefiles
{

    /// <summary>
    /// Classe Plateau : les saisies utilisateurs ayant était faite correctement, aucune vérification des null ou vide n'est réaliser ici 
    /// </summary>
    public class Plateau
    {
        private char[,] plateau;
        private int ligne;
        private int colonne;
        private List<int[]> coordonnee = new List<int[]>();

        /// <summary>
        /// Constructeur de la classe Plateau
        /// </summary>
        /// <param name="ligne"> int : entier représentant le nombre de lignes du plateau </param>
        /// <param name="colonne"> int : entier représentant le nombre de colonnes du plateau </param>
        public Plateau(int ligne, int colonne)
        {
            this.ligne = ligne;
            this.colonne = colonne;
            this.plateau = new char[this.ligne, this.colonne];

        }

        /// <summary>
        /// Génère un plateau aléatoire en respectant le nombre maximum d'itérations selon le fichier Lettre.txt
        /// </summary>
        /// <returns> Le plateau de jeu </returns>
        public Plateau GenerePlateauAleatoire()
        {
            string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";     // Permet de référencer toutes les lettres de l'alphabet 
            Random r = new Random();
            for (int i = 0; i < this.ligne; i++)
            {
                for (int j = 0; j < this.colonne; j++)
                {
                    bool verif = false;
                    while (!verif)
                    {
                        char tst = alpha[r.Next(alpha.Length)];
                        if (this.NbLettreDansTab(tst) < NbMax(tst))     // Fonction qui compte le nb d'itérations d'une lettre dans le tableau. S'il est inferieur au nb max d'itérations de cette lettre, on ajoute la lettre au tableau
                        {
                            this.plateau[i, j] = tst;                  // On remplie le tableau en utilisant un rang choisi au hasard du string alphabet 
                            verif = true;
                        }
                    }
                }
            }
            return this;
        }

        /// <summary>
        /// Renvoie this.ligne en lecture seule
        /// </summary>
        public int Ligne
        {
            get { return this.ligne; }
        }

        /// <summary>
        /// Renvoie this.colonne en lecture seule 
        /// </summary>
        public int Colonne
        {
            get { return this.colonne; }
        }

        /// <summary>
        /// Renvoie this.plateau en lecture seule
        /// </summary>
        public char[,] Plat
        {
            get { return this.plateau; }
        }

        /// <summary>
        /// Méthode pas utile dans notre cadre mais qui permet de retourner qlq informations sur le this 
        /// </summary>
        /// <returns> string : retourne différentes informations du this créé</returns>
        public string toString()
        {
            return "Le plateau contient " + ligne * colonne + " caractères et est de taille " + ligne + " x " + colonne;
        }

        /// <summary>
        /// Permet de créer ou ajouter à un fichier déjà existant le plateau créé
        /// </summary>
        /// <param name="nomfile"> string : nom du fichier à créer et/ou à modifier </param>
        public void ToFile(string nomfile)
        {
            for (int i = 0; i < this.ligne; i++)
            {
                for (int j = 0; j < this.colonne; j++)
                {
                    if (this.plateau[i, j] == ' ')
                    {
                        this.plateau[i, j] = '#';
                    }
                }
            }
            StreamWriter sw = new StreamWriter(nomfile, true);

            for (int i = 0; i < this.colonne; i++)
            {
                sw.Write("----");
            }

            sw.WriteLine('-');
            for (int i = 0; i < this.ligne; i++)
            {
                for (int j = 0; j < this.colonne; j++)
                {
                    if (j == 0)
                    {
                        sw.Write("| " + this.plateau[i, j] + " | ");
                    }
                    else
                    {
                        sw.Write(this.plateau[i, j] + " | ");
                    }
                }
                sw.WriteLine();
            }
            for (int i = 0; i < this.colonne; i++)
            {
                sw.Write("----");
            }
            sw.WriteLine('-');
            sw.WriteLine(DateTime.Now + "\n");
            sw.Close();
            for (int i = 0; i < this.ligne; i++)
            {
                for (int j = 0; j < this.colonne; j++)
                {
                    if (this.plateau[i, j] == '#')
                    {
                        this.plateau[i, j] = ' ';
                    }
                }
            }
        }

        /// <summary>
        /// Crée un plateau après la lecture du fichier nomFile
        /// </summary>
        /// <param name="nomfile"> string : nom du fichier à lire </param>
        /// <returns> Plateau : un plateau issu du fichier passé en paramètres </returns>
        public Plateau ToRead(string nomfile)      // fichier à tableau  
        {
            if (File.Exists(nomfile))
            {
                string[] ensembleLignes = File.ReadAllLines(nomfile);
                string[][] sousEnsembleLignes = new string[ensembleLignes.Length][];

                for (int i = 0; i < ensembleLignes.Length; i++)
                {
                    sousEnsembleLignes[i] = ensembleLignes[i].Split(',', ' ', '|');
                }

                int colonne = sousEnsembleLignes[0].Length;
                this.plateau = new char[ensembleLignes.Length, colonne];
                this.ligne = this.plateau.GetLength(0);
                this.colonne = this.plateau.GetLength(1);
                Console.WriteLine(this.ligne + "et" + this.colonne);

                for (int i = 0; i < this.plateau.GetLength(0); i++)
                {
                    for (int j = 0; j < this.plateau.GetLength(1); j++)
                    {
                        Console.WriteLine(sousEnsembleLignes[i][j]);
                        this.plateau[i, j] = Convert.ToChar(sousEnsembleLignes[i][j]);
                    }
                }
                return this;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Permet de laisser aux joueurs la possibilité de recommencer une précédente partie à partir du fichier Resume_Game.txt
        /// </summary>
        /// <returns> le plateau de la précédente partie </returns>
        public Plateau ToReadPreviousParty()
        {
            string[] ensembleLignes = File.ReadAllLines("Resume_Game.txt");
            int compteur = 0;
            int PosHaut = 0;
            int PosBas = 0;
            for (int i = ensembleLignes.Length - 1; i >= 0 && compteur != 2; i--)
            {
                if (ensembleLignes[i].Contains("----"))
                {
                    compteur++;
                    if (compteur == 1)
                    {
                        PosBas = i;
                    }
                    else if (compteur == 2)
                    {
                        PosHaut = i;
                    }
                }

            }
            StreamWriter SW = new StreamWriter("Game_Continue.txt");
            for (int i = PosHaut + 1; i < PosBas; i++)
            {
                SW.WriteLine(ensembleLignes[i]);
            }
            SW.Close();
            Plateau rep = new Plateau(0, 0);

            ensembleLignes = File.ReadAllLines("Game_Continue.txt");
            int colonne = 0;
            for (int i = 0; i < ensembleLignes[0].Length; i++)
            {
                if (ensembleLignes[0][i] != ' ' && ensembleLignes[0][i] != '|')
                {
                    colonne++;
                }
            }

            this.plateau = new char[ensembleLignes.Length, colonne];
            this.ligne = this.plateau.GetLength(0);
            this.colonne = this.plateau.GetLength(1);
            for (int i = 0; i < this.plateau.GetLength(0); i++)
            {
                int column = 0;
                for (int j = 0; j < ensembleLignes[i].Length; j++)
                {
                    if (ensembleLignes[i][j] != ' ' && ensembleLignes[i][j] != '|')
                    {
                        if (ensembleLignes[i][j] == '#')
                        {
                            this.plateau[i, column] = ' ';
                        }
                        else
                        {
                            this.plateau[i, column] = ensembleLignes[i][j];
                        }
                        column++;
                    }
                }
            }
            return this;


        }

        /// <summary>
        /// Permet de rajouter des couleurs sur le thème de Noël à l'affichage des lignes du plateau
        /// </summary>
        /// <param name="nb"> int : entier correspondant au nombre de colonnes du plateau </param>
        private void AfficheLignePlateau(int nb)
        {
            for (int i = 0; i < nb; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write("-\u001b[31m---\x1b[0m");
                }
                else
                {
                    Console.Write("-\u001b[32m---\x1b[0m");
                }

            }
            Console.WriteLine('-');
        }

        /// <summary>
        /// Affiche un plateau
        /// </summary>
        public void AffichePlateau()
        {
            AfficheLignePlateau(this.colonne);
            for (int i = 0; i < this.ligne; i++)
            {
                for (int j = 0; j < this.colonne; j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j == 0)
                        {
                            Console.Write("\u001b[32m| \x1b[0m");
                        }
                        Console.Write(this.plateau[i, j]);
                        if (j % 2 == 0)
                        {
                            Console.Write("\u001b[31m | \x1b[0m");
                        }
                        else
                        {
                            Console.Write("\u001b[32m | \x1b[0m");

                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            Console.Write("\u001b[31m| \x1b[0m");
                        }
                        Console.Write(this.plateau[i, j]);
                        if (j % 2 == 0)
                        {
                            Console.Write("\u001b[32m | \x1b[0m");
                        }
                        else
                        {
                            Console.Write("\u001b[31m | \x1b[0m");

                        }
                    }
                }
                Console.WriteLine();
            }
            AfficheLignePlateau(this.colonne);
        }

        /// <summary>
        /// Permet de rechercher un mot depuis la dernière ligne d'un plateau, dans plusieurs directions
        /// </summary>
        /// <param name="mot">string : mot à rechercher </param>
        /// <returns> retourne TRUE si le mot est trouvé, sinon FALSE </returns>
        public bool Recherche_Mot(string mot)
        {
            bool rep = false;
            this.coordonnee.Clear();
            mot = mot.ToUpper();

            if (mot.Length > 0)
            {
                for (int c = 0; c < this.colonne && !rep; c++)                  // On parcourt la dernière ligne à la recherche de la première lettre du mot 
                {
                    if (this.plateau[this.ligne - 1, c] == mot[0])            // Si on trouve la première lettre alors on cherche les autres                 
                    {
                        for (int o = 1; o < mot.Length && !rep; o++)        // On répéte l'opération de recherche tant que c'est vrai et que l'on a pas vérifié tout le mot 
                        {
                            if (Recherche_Mot1(mot, this.ligne - 1, c)) rep = true;
                            else if (Recherche_Mot2(mot, this.ligne - 1, c)) rep = true;
                            else if (Recherche_Mot3(mot, this.ligne - 1, c)) rep = true;
                            else if (Recherche_Mot4(mot, this.ligne - 1, c)) rep = true;
                            else if (Recherche_Mot5(mot, this.ligne - 1, c)) rep = true;
                        }
                    }
                }
            }
            if (rep)   // Si le mot est trouvé alors on l'enlève 
            {
                this.Maj_Plateau();
            }
            return rep;
        }

        /// <summary>
        /// Modifier le plateau en enlevant le mot trouvé 
        /// </summary>
        private void Maj_Plateau()
        {
            foreach (int[] tab in this.coordonnee)
            {
                this.plateau[tab[0], tab[1]] = ' ';
            }
            for (int j = 0; j < this.colonne; j++)
            {
                int nb = NbCharColonne(j, ' ');
                for (int compt = 0; compt < nb; compt++)
                {
                    for (int k = this.ligne - 1; k > 0; k--)
                    {
                        if (this.plateau[k, j] == ' ')
                        {
                            for (int i = 0; i < k; i++)
                            {
                                this.plateau[k - i, j] = this.plateau[k - 1 - i, j];
                            }
                            this.plateau[0, j] = ' ';
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Est utile pour la recherche du mot dans un plateau, permet de vérifier que l'on a jamais regardé sur certaines positions pour éviter les bugs de recherche notamment liés au palindrome
        /// </summary>
        /// <param name="i"> int : numéro de lignes dans une matrice </param>
        /// <param name="j"> int : Numéro de colonne dans une matrice </param>
        /// <returns> retourne TRUE si on a jamais été sur cette position (si TRUE on ajoute les (i,j) à la liste) sinon FALSE </returns>
        private bool VerifNvlCase(int i, int j)
        {
            for (int compt = 0; compt < this.coordonnee.Count; compt++)
            {
                if (this.coordonnee[compt][0] == i && this.coordonnee[compt][1] == j) return false;
            }
            coordonnee.Add(new int[] { i, j });
            return true;
        }

        /// <summary>
        /// Recherche le nombre max théorique d'itérations de la lettre saisie dans le fichier Lettre.txt
        /// </summary>
        /// <param name="s"> char : lettre de l'alphabet </param>
        /// <returns> int : nombre max d'itérations de la lettre saisie </returns>
        private int NbMax(char s)
        {
            int rep = -1;
            if (File.Exists("Lettre.txt"))
            {
                string[] lignesFichier = File.ReadAllLines("Lettre.txt");
                for (int i = 0; i < lignesFichier.Length; i++)
                {
                    lignesFichier = lignesFichier[i].Split(',');
                    if (s == Convert.ToChar(lignesFichier[0][0]))
                    {
                        return rep = Convert.ToInt32(lignesFichier[1]);
                    }
                    lignesFichier = File.ReadAllLines("Lettre.txt");
                }
            }
            return rep;
        }

        /// <summary>
        /// Compte le nombre d'itérations d'un char dans une colonne de this.plateau
        /// </summary>
        /// <param name="colonne"> int : numéro de la colonne du plateau dans laquelle on va chercher le nombre d'itérations du char saisi </param>
        /// <param name="c"> char saisi à chercher </param>
        /// <returns> int : nombre d'itérations du char saisi dans une colonne du plateau </returns>
        private int NbCharColonne(int colonne, char c)
        {
            int rep = 0;
            for (int i = 0; i < this.ligne; i++)
            {
                if (this.plateau[i, colonne] == c)
                {
                    rep++;
                }
            }
            return rep;
        }

        /// <summary>
        /// Compte le nombre d'itérations d'un char dans this.plateau
        /// </summary>
        /// <param name="s"> char saisi à chercher </param>
        /// <returns> int : le nombre d'itérations du char dans this.plateau </returns>
        private int NbLettreDansTab(char s)
        {
            int rep = 0;
            for (int i = 0; i < this.colonne; i++)
            {
                rep += NbCharColonne(i, s);
            }
            return rep;
        }

        /// <summary>
        /// Recherche le mot en priorisant la direction : gauche
        /// </summary>
        /// <param name="mot"> string : mot à chercher </param>
        /// <param name="i"> int : coordonnée en ligne de la première lettre du mot </param>
        /// <param name="j"> int : coordonnée en colonnes de la premiere lettre du mot </param>
        /// <returns> TRUE si le mot est trouvé sinon FALSE </returns>
        private bool Recherche_Mot1(string mot, int i, int j)
        {
            coordonnee.Add(new int[] { i, j });
            bool verif = true;
            for (int o = 1; o < mot.Length && verif; o++)        // On répéte l'opération de recherche tant que c'est vrai et que l'on n'a pas vérifié tout le mot 
            {
                verif = false;
                if (j != 0 && this.plateau[i, j - 1] == mot[o] && VerifNvlCase(i, j - 1))       // gauche
                {
                    verif = true;
                    j--;
                }
                else if (j != this.colonne - 1 && this.plateau[i, j + 1] == mot[o] && VerifNvlCase(i, j + 1)) // droite 
                {
                    verif = true;
                    j++;
                }
                else if (i != 0 && this.plateau[i - 1, j] == mot[o] && VerifNvlCase(i - 1, j))   // en haut 
                {
                    verif = true;
                    i--;
                }
                else if (i != 0 && j != 0 && this.plateau[i - 1, j - 1] == mot[o] && VerifNvlCase(i - 1, j - 1))    // diagonale gauche
                {
                    verif = true;
                    i--;
                    j--;
                }
                else if (i != 0 && j != this.colonne - 1 && this.plateau[i - 1, j + 1] == mot[o] && VerifNvlCase(i - 1, j + 1))     // diagonale droite 
                {
                    verif = true;
                    i--;
                    j++;
                }
            }
            if (!verif) { coordonnee.Clear(); }
            return verif;
        }

        /// <summary>
        /// Recherche le mot en priorisant la direction : diagonale droite
        /// </summary>
        /// <param name="mot"> string : mot à chercher </param>
        /// <param name="i"> int : coordonnée en ligne de la première lettre du mot </param>
        /// <param name="j"> int : coordonnée en colonnes de la premiere lettre du mot </param>
        /// <returns> TRUE si le mot est trouvé sinon FALSE </returns>
        private bool Recherche_Mot2(string mot, int i, int j)
        {
            coordonnee.Add(new int[] { i, j });
            bool verif = true;
            for (int o = 1; o < mot.Length && verif; o++)        // On répéte l'opération de recherche tant que c'est vrai et que l'on n'a pas vérifié tout le mot 
            {
                verif = false;
                if (i != 0 && j != this.colonne - 1 && this.plateau[i - 1, j + 1] == mot[o] && VerifNvlCase(i - 1, j + 1))    // diagonale droite
                {
                    verif = true;
                    i--;
                    j++;
                }
                else if (j != 0 && this.plateau[i, j - 1] == mot[o] && VerifNvlCase(i, j - 1))      // gauche 
                {
                    verif = true;
                    j--;
                }
                else if (j != this.colonne - 1 && this.plateau[i, j + 1] == mot[o] && VerifNvlCase(i, j + 1))       // droite 
                {
                    verif = true;
                    j++;
                }
                else if (i != 0 && this.plateau[i - 1, j] == mot[o] && VerifNvlCase(i - 1, j)) // en haut 
                {
                    verif = true;
                    i--;
                }
                else if (i != 0 && j != 0 && this.plateau[i - 1, j - 1] == mot[o] && VerifNvlCase(i - 1, j - 1))  // diagonale gauche 
                {
                    verif = true;
                    i--;
                    j--;
                }
            }
            if (!verif) { coordonnee.Clear(); }
            return verif;
        }

        /// <summary>
        /// Recherche le mot en priorisant la direction : diagonale gauche
        /// </summary>
        /// <param name="mot"> string : mot à chercher </param>
        /// <param name="i"> int : coordonnée en ligne de la première lettre du mot </param>
        /// <param name="j"> int : coordonnée en colonnes de la premiere lettre du mot </param>
        /// <returns> TRUE si le mot est trouvé sinon FALSE </returns>
        private bool Recherche_Mot3(string mot, int i, int j)
        {
            coordonnee.Add(new int[] { i, j });
            bool verif = true;
            for (int o = 1; o < mot.Length && verif; o++)        // On répéte l'opération de recherche tant que c'est vrai et que l'on n'a pas vérifié tout le mot 
            {
                verif = false;
                if (i != 0 && j != 0 && this.plateau[i - 1, j - 1] == mot[o] && VerifNvlCase(i - 1, j - 1))    // diagonale gauche
                {
                    verif = true;
                    i--;
                    j--;
                }
                else if (i != 0 && j != this.colonne - 1 && this.plateau[i - 1, j + 1] == mot[o] && VerifNvlCase(i - 1, j + 1)) // diagonale droite 
                {
                    verif = true;
                    i--;
                    j++;
                }
                else if (j != 0 && this.plateau[i, j - 1] == mot[o] && VerifNvlCase(i, j - 1))      // gauche 
                {
                    verif = true;
                    j--;
                }
                else if (j != this.colonne - 1 && this.plateau[i, j + 1] == mot[o] && VerifNvlCase(i, j + 1))       // droite 
                {
                    verif = true;
                    j++;
                }
                else if (i != 0 && this.plateau[i - 1, j] == mot[o] && VerifNvlCase(i - 1, j))      // en haut 
                {
                    verif = true;
                    i--;
                }
            }
            if (!verif) { coordonnee.Clear(); }
            return verif;
        }

        /// <summary>
        /// Recherche le mot en priorisant la direction : en haut
        /// </summary>
        /// <param name="mot"> string : mot à chercher </param>
        /// <param name="i"> int : coordonnée en ligne de la première lettre du mot </param>
        /// <param name="j"> int : coordonnée en colonnes de la premiere lettre du mot </param>
        /// <returns> TRUE si le mot est trouvé sinon FALSE </returns>
        private bool Recherche_Mot4(string mot, int i, int j)
        {
            coordonnee.Add(new int[] { i, j });
            bool verif = true;
            for (int o = 1; o < mot.Length && verif; o++)        // On répéte l'opération de recherche tant que c'est vrai et que l'on n'a pas vérifié tout le mot 
            {
                verif = false;
                if (i != 0 && this.plateau[i - 1, j] == mot[o] && VerifNvlCase(i - 1, j))       // en haut 
                {
                    verif = true;
                    i--;
                }
                else if (i != 0 && j != 0 && this.plateau[i - 1, j - 1] == mot[o] && VerifNvlCase(i - 1, j - 1))        // diagonale gauche
                {
                    verif = true;
                    i--;
                    j--;
                }
                else if (i != 0 && j != this.colonne - 1 && this.plateau[i - 1, j + 1] == mot[o] && VerifNvlCase(i - 1, j + 1))     // diagonale droite 
                {
                    verif = true;
                    i--;
                    j++;
                }
                else if (j != 0 && this.plateau[i, j - 1] == mot[o] && VerifNvlCase(i, j - 1))      // gauche
                {
                    verif = true;
                    j--;
                }
                else if (j != this.colonne - 1 && this.plateau[i, j + 1] == mot[o] && VerifNvlCase(i, j + 1))       // droite 
                {
                    verif = true;
                    j++;
                }
            }
            if (!verif) { coordonnee.Clear(); }
            return verif;
        }

        /// <summary>
        /// Recherche le mot en priorisant la direction : droite
        /// </summary>
        /// <param name="mot"> string : mot à chercher </param>
        /// <param name="i"> int : coordonnée en ligne de la première lettre du mot </param>
        /// <param name="j"> int : coordonnée en colonnes de la premiere lettre du mot </param>
        /// <returns> TRUE si le mot est trouvé sinon FALSE </returns>
        private bool Recherche_Mot5(string mot, int i, int j)
        {
            coordonnee.Add(new int[] { i, j });
            bool verif = true;
            for (int o = 1; o < mot.Length && verif; o++)        // On répéte l'opération de recherche tant que c'est vrai et que l'on n'a pas vérifié tout le mot 
            {
                verif = false;
                if (j != this.colonne - 1 && this.plateau[i, j + 1] == mot[o] && VerifNvlCase(i, j + 1))        // droite
                {
                    verif = true;
                    j++;
                }
                else if (i != 0 && this.plateau[i - 1, j] == mot[o] && VerifNvlCase(i - 1, j)) // en haut 
                {
                    verif = true;
                    i--;
                }
                else if (i != 0 && j != 0 && this.plateau[i - 1, j - 1] == mot[o] && VerifNvlCase(i - 1, j - 1))    // diagonale gauche 
                {
                    verif = true;
                    i--;
                    j--;
                }
                else if (i != 0 && j != this.colonne - 1 && this.plateau[i - 1, j + 1] == mot[o] && VerifNvlCase(i - 1, j + 1))         // diagonale droite 
                {
                    verif = true;
                    i--;
                    j++;
                }
                else if (j != 0 && this.plateau[i, j - 1] == mot[o] && VerifNvlCase(i, j - 1))      //gauche
                {
                    verif = true;
                    j--;
                }
            }
            if (!verif) { coordonnee.Clear(); }
            return verif;
        }
    }
}
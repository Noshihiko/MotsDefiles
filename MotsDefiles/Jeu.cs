using MotsDefiles;
using System;
using System.IO;
using System.Threading;

namespace MotsDefiles
{
    internal class Jeu
    {
        private Dictionnaire dict = new Dictionnaire();
        private Plateau plateau;
        private Joueur[] tabJoueur;
        private int nbPartie = 0;
        private int temps;
        private int partieJouee;

        /// <summary>
        /// Constructeur de la classe Jeu
        /// </summary>
        /// <param name="tabJoueur"> tableau de joueurs contenant les joueurs (et donc leurs attributs) de la partie </param>
        /// <param name="temps"> int : entier représentant le temps de jeu de chaque tour </param>
        /// <param name="partieJouee"> int : entier représentant le nombre de parties jouées </param>
        /// <param name="nomFile"> string : string représenant le possible nom du fichier qui contient le plateau à lire </param>
        /// <param name="ligne"> int : entier représentant le nb de lignes du plateau, par défaut 9 </param>
        /// <param name="colonne"> int : entier représentant le nb de colonnes du plateau, par défaut 12 </param>
        public Jeu(Joueur[] tabJoueur, int temps, int partieJouee, string nomFile = "", int ligne = 9, int colonne = 12)
        {
            if (nomFile != "")
            {
                this.plateau = new Plateau(0, 0);
                if (nomFile == "Resume_Game.txt")
                {
                    this.plateau = plateau.ToReadPreviousParty();
                }
                else
                {
                    this.plateau = plateau.ToRead(nomFile);
                }
            }
            else
            {
                this.plateau = new Plateau(ligne, colonne);
                this.plateau = plateau.GenerePlateauAleatoire();

            }
            this.tabJoueur = tabJoueur;
            this.temps = temps;
            this.partieJouee = partieJouee;
            if (nomFile == "Resume_Game.txt")
            {
                this.Actu_Score();
            }
        }

        /// <summary>
        /// Prend le score des deux joueurs de la dernière partie pour les rajouter à la partie en cours si les joueurs veulent reprendre la partie dernière
        /// </summary>
        public void Actu_Score()
        {
            string[] ensembleLignes = File.ReadAllLines("Resume_Game.txt");
            int joueur = 0;
            for (int i = ensembleLignes.Length - 1; i >= 0 && joueur < tabJoueur.Length; i--)
            {
                if (ensembleLignes[i].Contains("Score total cumulé de " + (this.tabJoueur[joueur].Nom)))
                {
                    string temp = ensembleLignes[i].Remove(0, 25 + this.tabJoueur[joueur].Nom.Length);
                    this.tabJoueur[joueur].Add_Score_WithNb(Convert.ToInt32(temp));
                    joueur++;
                    i = ensembleLignes.Length - 1;
                }
            }
        }

        /// <summary>
        /// Détermine le numéro du joueur qui doit jouer
        /// </summary>
        /// <returns> int : numéro du joueur devant jouer </returns>
        private int Tour()
        {
            int rep = 0;
            int min = tabJoueur[0].NbPartieJoue;
            for (int i = 1; i < tabJoueur.Length; i++)
            {
                if (tabJoueur[i].NbPartieJoue < min)
                {
                    min = tabJoueur[i].NbPartieJoue;
                    rep = i;
                }
            }
            return rep;
        }

        /// <summary>
        /// Trie le tableau de joueurs selon leur score pour désigner le gagnant (pas besoin de plus optimiser car que des tableaux petits autour de 2 ou 3 joueurs)
        /// </summary>
        private void TriBulles()
        {
            for (int i = 0; i < this.tabJoueur.Length - 1; ++i)
            {
                for (int j = i + 1; j < this.tabJoueur.Length; ++j)
                {
                    if (this.tabJoueur[j].Score < this.tabJoueur[i].Score)
                    {
                        Joueur temp = this.tabJoueur[j];
                        this.tabJoueur[j] = this.tabJoueur[i];
                        this.tabJoueur[i] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Ajoute une ligne de tirets comme séparateur dans le fichier lié au StreamWriter SW
        /// </summary>
        /// <param name="SW"> StreamWriter : lié au fichier Resume_Game.txt </param>
        private void AjoutLigne(StreamWriter SW)
        {
            for (int i = 0; i < this.plateau.Colonne; i++)
            {
                SW.Write("____");
            }
            SW.WriteLine();
        }

        /// <summary>
        /// C'est le jeu !!!
        /// Rédige en plus dans le fichier Resume_Game.txt le déroulé de la partie
        /// </summary>
        public void Jeu_Commence()
        {
            StreamWriter SW = new StreamWriter("Resume_game.txt", true);
            if (this.partieJouee == 0)
            {
                SW.Close();
                SW = new StreamWriter("Resume_game.txt", false);  //  recherche ou création du fichier
                SW.Write("\t\tJEU : MOTS GLISSES\n\nJoueurs : "); // ajout des noms de chaque joueur au fichier
                SW.Write(this.tabJoueur[0].Nom);

                for (int i = 1; i < this.tabJoueur.Length - 1; i++)
                {
                    SW.Write(this.tabJoueur[i].Nom + ", ");
                }
                SW.WriteLine(" et " + this.tabJoueur[tabJoueur.Length - 1].Nom);
            }

            AjoutLigne(SW);
            AjoutLigne(SW);

            SW.WriteLine("Partie n° " + (this.partieJouee + 1));
            SW.Close();

            Console.Clear();
            Console.WriteLine("Très bien vous êtes prêts ? Nous allons commencé !");

            Thread.Sleep(500);
            while (this.nbPartie < this.tabJoueur.Length * 3)
            {
                this.nbPartie++;

                StreamWriter SW1 = new StreamWriter("Resume_game.txt", true);
                SW1.WriteLine("\n\n\n");
                AjoutLigne(SW1);
                SW1.WriteLine("\n\n\n\tTour n°" + this.nbPartie + " : \n"); // ajout du numéro du tour (plusieurs tours par partie)
                SW1.Close();

                int temp = this.Tour();
                Console.WriteLine("\nC'est au tour de " + this.tabJoueur[temp].Nom + "\n");

                StreamWriter SW2 = new StreamWriter("Resume_game.txt", true);
                SW2.WriteLine("Tour de " + this.tabJoueur[temp].Nom + "\n"); // spécification du tour du joueur
                SW2.Close();

                Thread.Sleep(500);
                this.tabJoueur[temp].NbPartieJoue++;

                Console.WriteLine("Prépare toi, ça commence dans : ");
                Thread.Sleep(1000);
                Console.Clear();
                Console.Write("\u001b[32m 3 !"); // vert (ANSI code)
                Thread.Sleep(1000);
                Console.Clear();
                Console.Write("\x1b[33m 2 !"); // jaune (ANSI code)
                Thread.Sleep(1000);
                Console.Clear();
                Console.Write("\x1b[38;5;208m 1 !"); // orange (ANSI code)
                Thread.Sleep(1000);
                Console.Clear();
                Console.Write("\u001b[31m Maintenant !!!\x1b[0m"); // rouge puis couleur par défaut (ANSI code)
                Thread.Sleep(400);
                Console.Clear();

                DateTime d = DateTime.Now;
                while (DateTime.Now < d + TimeSpan.FromSeconds(this.temps))
                {
                    Console.WriteLine("\nC'est au tour de " + this.tabJoueur[temp].Nom + " : ");
                    this.plateau.AffichePlateau();
                    Console.WriteLine("\nVotre score actuel : " + this.tabJoueur[temp].Score + "\n");
                    this.plateau.ToFile("Resume_game.txt");

                    Console.Write("Tu vois un mot ? Alors écris le vite ici => ");
                    string mot = Console.ReadLine().ToUpper();

                    StreamWriter SW3 = new StreamWriter("Resume_game.txt", true);
                    SW3.WriteLine("Mot : \"" + mot + "\""); // ajout des mots trouvés par le joueur
                    SW3.Close();

                    if (DateTime.Now >= d + TimeSpan.FromSeconds(this.temps))
                    {
                        Console.WriteLine("Le chrono est fini...\n");
                        Thread.Sleep(1200);

                        StreamWriter SW4 = new StreamWriter("Resume_game.txt", true);
                        SW4.WriteLine("Erreur : Mot saisi trop tard.\n\n\n");
                        SW4.Close();
                    }
                    else if (mot.Length < 2)
                    {
                        Console.WriteLine("Ce mot est trop petit, mais continue tant que le chrono n'est pas fini !\n");
                        Thread.Sleep(1200);

                        StreamWriter SW5 = new StreamWriter("Resume_game.txt", true);
                        SW5.WriteLine("Erreur : Le mot est trop petit.\n\n\n");
                        SW5.Close();
                    }
                    else if (this.tabJoueur[temp].Contient(mot))
                    {
                        Console.WriteLine("Tu as deja écrit \"" + mot + "\", mais continue tant que le chrono n'est pas fini !\n");
                        Thread.Sleep(1200);

                        StreamWriter SW6 = new StreamWriter("Resume_game.txt", true);
                        SW6.WriteLine("Erreur : Le mot a déja était saisi.\n\n\n");
                        SW6.Close();
                    }
                    else if (!dict.RechDichoRecursif(mot))
                    {
                        Console.WriteLine("Ce mot n'appartient pas au dictionnaire, mais continue tant que le chrono n'est pas fini !\n");
                        StreamWriter SW7 = new StreamWriter("Resume_game.txt", true);
                        Thread.Sleep(1200);

                        SW7.WriteLine("Erreur : Le mot n'appartient pas au dictionnaire.\n\n\n");
                        SW7.Close();
                    }
                    else if (plateau.Recherche_Mot(mot))
                    {
                        this.tabJoueur[temp].Add_Mot(mot);
                        int val = this.tabJoueur[temp].Add_Score(mot);

                        StreamWriter SW8 = new StreamWriter("Resume_game.txt", true);
                        SW8.WriteLine("Score du mot : " + val + " point(s).\n\n\n");
                        SW8.Close();
                    }
                    else
                    {
                        Console.WriteLine("Ce mot n'appartient pas au tableau, mais continue tant que le chrono n'est pas fini !\n");
                        Thread.Sleep(1200);

                        StreamWriter SW9 = new StreamWriter("Resume_game.txt", true);
                        SW9.WriteLine("Erreur : Le mot n'appartient pas au tableau.\n\n\n");
                        SW9.Close();
                    }
                    Console.Clear();
                }

                StreamWriter SW10 = new StreamWriter("Resume_game.txt", true);
                SW10.Write("Résumé du tour n°" + this.tabJoueur[temp].NbPartieJoue + " de " + this.tabJoueur[temp].Nom + " : \n\nScore total cumulé de " + this.tabJoueur[temp].Nom + " : " + this.tabJoueur[temp].Score + "\nMot trouvé : "); // ajout des mots trouvés et score dans le tour du joueur 
                if (this.tabJoueur[temp].MotsConnus.Count > 0)
                {
                    SW10.Write(this.tabJoueur[temp].MotsConnus[0]);
                    if (this.tabJoueur[temp].MotsConnus.Count > 1)
                    {
                        for (int i = 1; i < this.tabJoueur[temp].MotsConnus.Count - 1; i++)
                        {
                            SW10.Write(", " + this.tabJoueur[temp].MotsConnus[i]);
                        }
                        SW10.WriteLine(" et " + this.tabJoueur[temp].MotsConnus[this.tabJoueur[temp].MotsConnus.Count - 1] + "\n\n\n");
                    }
                }
                else
                {
                    SW10.WriteLine("\n\n\n");
                }
                SW10.Close();
            }

            Console.WriteLine("Wow vous avez bien joué !\n");
            Thread.Sleep(700);
            Console.WriteLine("Qui sera le grand gagnant ?... Roulement de tambours...\n");
            Console.Write("3 ! ");
            Thread.Sleep(1000);
            Console.Write("\t2 ! ");
            Thread.Sleep(1000);
            Console.WriteLine("\t1 !\n\n");
            Thread.Sleep(1000);

            this.TriBulles();

            StreamWriter SW11 = new StreamWriter("Resume_game.txt", true);
            SW11.WriteLine("Résultat de la partie : \n\n"); // ajout du classement des joueurs
            int k = 1;
            for (int i = this.tabJoueur.Length - 1; i >= 0; i--)
            {
                string s;
                if (i == this.tabJoueur.Length - 1)
                {
                    s = k + " er :  " + this.tabJoueur[i].Nom + ", Score : " + this.tabJoueur[i].Score + " point(s).";
                    Console.WriteLine("                     " + s);
                    SW11.WriteLine(s);
                }
                else
                {
                    s = k + " ème : " + this.tabJoueur[i].Nom + ", Score : " + this.tabJoueur[i].Score + " point(s).";
                    Console.WriteLine("                     " + s);
                    SW11.WriteLine(s);
                }
                k++;
            }
            SW11.Close();
        }
    }
}
using MotsDefiles;
using System;
using System.Collections.Generic;
using System.IO;

namespace MotsDefiles
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menu();
            Console.ReadKey();
        }

        /// <summary>
        /// Vérifie si le string passé en paramètres est un entier
        /// </summary>
        /// <param name="s"> string : chaîne de caractères dont on cherche à vérifier si elle est un entier </param>
        /// <returns> int : retourne un entier après vérification que le string correspond à un entier </returns>
        public static int Verif_Int(string s)
        {
            int rep;
            Console.WriteLine();
            while (!int.TryParse(s, out rep))
            {
                Console.Write("Veuillez saisir un nombre entier : ");
                s = Console.ReadLine();
                Console.WriteLine();
            }
            return rep;
        }

        /// <summary>
        /// Le début du jeu qui permet de récupérer tous les éléments necessaires au lancement du jeu
        /// </summary>
        public static void Menu()
        {
            Console.Write("Bonjour ! Bienvenue sur : \n\n\x1b[31m"); // rouge (ANSI code)
            Console.WriteLine(
                  "                   _                 _  _\r" +
                "\n                  | |               | |(_)\r" +
                "\n _ __ ___    ___  | |_  ___    __ _ | | _  ___  ___   ___  ___\r" +
                "\n| '_ ` _ \\  / _ \\ | __|/ __|  / _` || || |/ __|/ __| / _ \\/ __|\r" +
                "\n| | | | | || (_) || |_ \\__ \\ | (_| || || |\\__ \\\\__ \\|  __/\\__ \\\r" +
                "\n|_| |_| |_| \\___/  \\__||___/  \\__, ||_||_||___/|___/ \\___||___/\r" +
                "\n                               __/ |\r" +
                "\n                              |___/\r" +
                "\n");
            Console.Write("\x1b[0m"); // couleur par défaut (ANSI code)

            int Partiejouer = 0;
            Console.Write("\n\nVeuillez saisir un nombre de joueurs : ");           // Saisie du nombre de joueurs 
            int nbrJoueur = Verif_Int(Console.ReadLine());

            while (nbrJoueur < 2)
            {
                Console.Clear();
                Console.Write("Vous devez être au minimum deux joueurs, veuillez saisir, de nouveau, le nombre de joueurs : ");
                nbrJoueur = Verif_Int(Console.ReadLine());
            }
            Joueur[] tabJoueur = new Joueur[nbrJoueur];
            Console.Clear();

            List<string> noms = new List<string>();
            Console.WriteLine("Nous allons désormais saisir chacun de vos noms.\n");       //Saisie du nom des joueurs
            for (int i = 0; i < nbrJoueur; i++)
            {
                Console.Write("Veuillez saisir le nom du joueur numéro " + (i + 1) + " : ");
                string nomSaisi = Console.ReadLine().ToUpper();
                while (noms.Contains(nomSaisi))
                {
                    Console.Write("Veuillez saisir un nom différent des autres joueurs : ");
                    nomSaisi = Console.ReadLine().ToUpper();
                }
                noms.Add(nomSaisi);
                tabJoueur[i] = new Joueur(nomSaisi);
                Console.WriteLine("\n");
            }
            Console.Clear();

            Console.Write("Veuillez saisir le temps d'un tour de joueur en secondes (le temps recommandés est de 30 secondes) : ");     // Saisie longueur d'un tour en secondes
            int time = Verif_Int(Console.ReadLine());
            while (time <= 0)
            {
                Console.WriteLine();
                Console.Write("Veuillez saisir un nombre entier strictement positif : ");
                time = Verif_Int(Console.ReadLine());
            }

            ConsoleKeyInfo rep1 = new ConsoleKeyInfo('A', ConsoleKey.A, false, false, false);           // Menu principal du jeu 
            Jeu jeu = null;
            while (rep1.Key != ConsoleKey.NumPad1 && rep1.Key != ConsoleKey.NumPad2 && rep1.Key != ConsoleKey.NumPad3 && rep1.Key != ConsoleKey.NumPad4 && rep1.Key != ConsoleKey.D1 && rep1.Key != ConsoleKey.D2 && rep1.Key != ConsoleKey.D3 && rep1.Key != ConsoleKey.D4)
            {
                Console.Clear();
                Console.WriteLine("Vous avez maintenant le\x1b[31m pouvoir\x1b[0m !\n\n1) Pour jouer sur un tableau de taille 9x12 généré aléatoirement, tapez 1.\n2) Pour jouer sur un tableau généré aléatoirement et de taille prédéfinie par vos soins, tapez 2.\n3) Pour jouer sur un tableau que vous avez en fichier, tapez 3.\n4) Pour reprendre le jeu d'une ancienne partie, tapez 4.\n5) Pour quitter le jeu, cliquer sur \"échap\".\n");
                Console.WriteLine("Alors, verdict ? \n");
                rep1 = Console.ReadKey();
                bool verif = false;
                switch (rep1.Key)
                {
                    case ConsoleKey.NumPad1:                // 1 du pavé numérique
                        Console.Clear();
                        jeu = new Jeu(tabJoueur, time, Partiejouer);
                        verif = true;
                        break;
                    case ConsoleKey.D1:                     // 1 du clavier
                        Console.Clear();
                        jeu = new Jeu(tabJoueur, time, Partiejouer);
                        verif = true;
                        break;
                    case ConsoleKey.NumPad2:            // 2 du pavé numérique
                        Console.Clear();
                        int ligne = -1;
                        while (ligne < 2 || ligne > 9)
                        {
                            Console.Write("Veuillez saisir un nombre de lignes supérieur à 2 mais inférieur ou égale à 9 : ");
                            ligne = Verif_Int(Console.ReadLine());
                            Console.WriteLine();
                        }
                        Console.Clear();
                        int colonne = -1;
                        while (colonne < 2 || colonne > 12)
                        {
                            Console.Write("Veuillez saisir un nombre de colonnes supérieur à 2 mais inférieur ou égale à 12 : ");
                            colonne = Verif_Int(Console.ReadLine());
                            Console.WriteLine();
                        }
                        jeu = new Jeu(tabJoueur, time, Partiejouer, "", ligne, colonne);
                        verif = true;
                        break;
                    case ConsoleKey.D2:             // 2 du clavier 
                        Console.Clear();
                        int ligne2 = -1;
                        while (ligne2 < 2 || ligne2 > 9)
                        {
                            Console.Write("Veuillez saisir un nombre de lignes supérieur à 2 mais inférieur ou égale à 9 : ");
                            ligne2 = Verif_Int(Console.ReadLine());
                            Console.WriteLine();
                        }
                        Console.Clear();
                        int colonne2 = -1;
                        while (colonne2 < 2 || colonne2 > 12)
                        {
                            Console.Write("Veuillez saisir un nombre de colonnes supérieur à 2 mais inférieur ou égale à 12 : ");
                            colonne2 = Verif_Int(Console.ReadLine());
                            Console.WriteLine();
                        }
                        jeu = new Jeu(tabJoueur, time, Partiejouer, "", ligne2, colonne2);
                        verif = true;
                        break;
                    case ConsoleKey.NumPad3:            // 3 du pavé numérique
                        Console.Clear();
                        Console.Write("Veuillez saisir le nom exact du fichier texte où se trouve le tableau (sans écrire \".txt\") : ");
                        string rep = Console.ReadLine() + ".txt";
                        if (File.Exists(rep))
                        {
                            jeu = new Jeu(tabJoueur, time, Partiejouer, rep);
                        }
                        else
                        {
                            int i = 0;
                            while (!File.Exists(rep) && i < 3)
                            {
                                Console.Write("Veuillez resaisir le nom EXACT du fichier (sans écrire \".txt\"), il vous reste " + (3 - i - 1) + " tentative(s) : ");
                                rep = Console.ReadLine() + ".txt";
                                i++;
                            }
                            if (File.Exists(rep))
                            {
                                jeu = new Jeu(tabJoueur, time, Partiejouer, rep);
                            }
                            else
                            {
                                Console.WriteLine("Vous n'avez pas l'air de trouver le fichier. Appuyez sur n'importe quelle touche pour revenir au menu.");
                                rep1 = Console.ReadKey();
                            }
                        }
                        verif = true;
                        break;
                    case ConsoleKey.D3:         // 3 du clavier 
                        Console.Clear();
                        Console.Write("Veuillez saisir le nom exact du fichier texte où se trouve le tableau (sans écrire \".txt\") : ");
                        string rep2 = Console.ReadLine() + ".txt";
                        if (File.Exists(rep2))
                        {
                            jeu = new Jeu(tabJoueur, time, Partiejouer, rep2);
                        }
                        else
                        {
                            int i = 0;
                            while (!File.Exists(rep2) && i < 3)
                            {
                                Console.Write("Veuillez resaisir le nom EXACT du fichier (sans écrire \".txt\"), il vous reste " + (3 - i - 1) + " tentative(s) : ");
                                rep2 = Console.ReadLine() + ".txt";
                                i++;
                            }
                            if (File.Exists(rep2))
                            {
                                jeu = new Jeu(tabJoueur, time, Partiejouer, rep2);
                            }
                            else
                            {
                                Console.WriteLine("Vous n'avez pas l'air de trouver le fichier. Appuyez sur n'importe quelle touche pour revenir au menu.");
                                rep1 = Console.ReadKey();
                            }
                        }
                        verif = true;
                        break;
                    case ConsoleKey.NumPad4: //  4 du pavé numérique
                        Console.Clear();
                        if (File.Exists("Resume_Game.txt"))
                        {
                            jeu = new Jeu(tabJoueur, time, Partiejouer, "Resume_Game.txt");
                            verif = true;
                        }
                        else
                        {
                            Console.WriteLine("Aucune ancienne partie n'a été sauvegardée.\nPress space to return to the menu.");
                            rep1 = Console.ReadKey();
                        }
                        break;
                    case ConsoleKey.D4:   //touche 4
                        Console.Clear();
                        if (File.Exists("Resume_Game.txt"))
                        {
                            jeu = new Jeu(tabJoueur, time, Partiejouer, "Resume_Game.txt");
                            verif = true;
                        }
                        else
                        {
                            Console.WriteLine("Aucune ancienne partie n'a été sauvegardée.\nPress space to return to the menu.");
                            rep1 = Console.ReadKey();
                        }
                        break;
                    case ConsoleKey.Escape:         // touche échap
                        Environment.Exit(0);
                        StreamWriter SW = new StreamWriter("Resume_game.txt", false);
                        SW.Close();
                        break;
                }
                if (verif)
                {
                    jeu.Jeu_Commence();  // lancement du jeu
                    Partiejouer++;
                    Console.WriteLine("\n\nBravo pour cette partie ! Pour quitter le jeu ou refaire une autre partie, cliquer sur la touche espace.\n\nNe vous inquiétez pas ! Un résumé de la partie est disponible dans vos fichiers avec pour nom \"Resume_game.txt\".");
                    rep1 = Console.ReadKey(); // Permet de proposer au joueur de continuer à jouer 
                }
            }
        }

        #region QuickSort avec nombre
        //private static int[] QuickSort(int[] tab)
        //{
        //    if (tab.Length <= 1)
        //    {
        //        return tab;
        //    }

        //    List<int> tabGauche = new List<int>();
        //    List<int> tabDroit = new List<int>();

        //    int pivot = tab[tab.Length - 1];
        //    tabDroit.Add(pivot);

        //    for (int i = 0; i < tab.Length - 1; i++)
        //    {
        //        if (pivot > tab[i])
        //        {
        //            tabGauche.Add(tab[i]);
        //        }
        //        else
        //        {
        //            tabDroit.Add(tab[i]);
        //        }
        //    }
        //    int[] t1 = QuickSort(tabGauche.ToArray());
        //    int[] t2 = QuickSort(tabDroit.ToArray());

        //    Console.WriteLine(string.Join(", ", t1) + " et " + string.Join(", ", t2));

        //    return t1.Concat(t2).ToArray();
        //}
        #endregion

        #region Quicksort avec string
        //private static string[] QuickSort(string[] tab)
        //{
        //    if (tab.Length <= 1)
        //    {
        //        return tab;
        //    }

        //    List<string> tabGauche = new List<string>();
        //    List<string> tabDroit = new List<string>();

        //    string pivot = tab[tab.Length - 1];


        //    for (int i = 0; i < tab.Length - 1; i++)
        //    {
        //        if (string.Compare(pivot, tab[i]) > 0)
        //        {
        //            tabGauche.Add(tab[i]);
        //        }
        //        else
        //        {
        //            tabDroit.Add(tab[i]);
        //        }
        //    }
        //    string[] t1 = QuickSort(tabGauche.ToArray());
        //    string[] t2 = QuickSort(tabDroit.ToArray());

        //    //Console.WriteLine(string.Join(", ", t1) + " et " + string.Join(", ", t2));

        //    return t1.Concat(new string[] { pivot }).Concat(t2).ToArray();
        //}
        #endregion
    }
}
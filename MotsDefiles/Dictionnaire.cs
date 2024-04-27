using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MotsDefiles
{
    public class Dictionnaire
    {
        string[][] dictionnaire = null;
        string fileName;
        int index;

        /// <summary>
        /// Constructeur de la classe Dictionnaire créant un dictionnaire à partir du fichier "Mots_Français.txt"
        /// </summary>
        public Dictionnaire()
        {
            this.index = 0;
            this.dictionnaire = new string[26][];

            for (int i = 0; i < this.dictionnaire.Length; i++)
            {
                this.dictionnaire[i] = new string[0];
            }
            try
            {
                using (StreamReader sr = new StreamReader("Mots_Français.txt"))         //using(){} => ouvre et ferme le flux
                {
                    string ligneDoc;
                    while ((ligneDoc = sr.ReadLine()) != null)      //Lis chaque ligne du doc jusqu'à la fin
                    {
                        this.dictionnaire[index] = ligneDoc.Split(' '); //Crée un tableau pour chaque première lettre des mots
                        this.index++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu :");
                Console.WriteLine(e.Message);
            }
            Tri_XXX();
        }

        /// <summary>
        /// Constructeur de la classe Dictionnaire créant un dictionnaire à partir du fichier "fileName"
        /// </summary>
        /// <param name="fileName"> string : nom du fichier dont on va extraire les mots pour créer le dictionnaire </param>
        public Dictionnaire(string fileName)
        {
            this.index = 0;
            this.fileName = fileName;

            this.dictionnaire = new string[26][]; //Créer une fonction pour nbre de lignes exactes si besoin
            for (int i = 0; i < this.dictionnaire.Length; i++)
            {
                this.dictionnaire[i] = new string[0];
            }

            try
            {
                using (StreamReader sr = new StreamReader(this.fileName))
                {
                    string ligneDoc;
                    while ((ligneDoc = sr.ReadLine()) != null)              //Lis chaque ligne du doc jusqu'à la fin
                    {
                        this.dictionnaire[index] = ligneDoc.Split(' ');         //Crée un tableau pour chaque première lettre des mots
                        this.index++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Le fichier n'a pas pu être lu :");
                Console.WriteLine(e.Message);
            }
            Tri_XXX();
        }

        /// <summary>
        /// Retourne les différents paramètres d'une instance de Dictionnaire sous la forme d'un string
        /// </summary>
        /// <returns> string : les différents paramètres d'une instance de Dictionnaire </returns>
        public string toString()
        {
            string result = "Le dictionnaire est en français et contient :";

            int nbElementsTotal = 0;

            foreach (string[] tab in dictionnaire)
            {
                if (tab != null) { nbElementsTotal += tab.Length; }
            }

            if (nbElementsTotal == 0)
            {
                result += " 0 mots.";
            }
            else
            {
                for (int i = 0; i < this.dictionnaire.Length; i++)
                {
                    if (this.dictionnaire[i].Length > 0)
                    {
                        result += "\n" + this.dictionnaire[i].Length + " mots pour la lettre " + this.dictionnaire[i][0][0];
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Détermine si un mot appartient au dictionnaire de façon récursive
        /// </summary>
        /// <param name="mot"> string : mot recherché dans le dictionnaire </param>
        /// <param name="debut"> int : valeur par défaut à 0 représentant le début du tableau pour la recherche récursive </param>
        /// <param name="fin"> int : valeur par défaut à -2 représentant la fin du tableau pour la recherche récursive </param>
        /// <returns> VRAI si le mot appartient au dictionnaire, sinon FAUX </returns>
        public bool RechDichoRecursif(string mot, int debut = 0, int fin = -2)
        {
            mot = mot.ToUpper();
            int premiereLettre = Convert.ToChar(mot[0]) - 65; //Donne l'index de la ligne du dictionnaire concernée
            if (fin == -2)
            {
                fin = this.dictionnaire[premiereLettre].Length - 1;
            }

            if (debut <= fin)
            {
                int moitie = (debut + fin) / 2;
                string motMoitie = this.dictionnaire[premiereLettre][moitie];

                if (mot == motMoitie)
                {
                    return true;
                }
                else if (string.Compare(mot, motMoitie) <= 0)
                {
                    return RechDichoRecursif(mot, debut, moitie - 1);
                }
                else
                {
                    return RechDichoRecursif(mot, moitie + 1, fin);
                }
            }
            return false;
        }

        /// <summary>
        /// Trie l'ensemble des sous dictionnnaires contenus dans "dictionnaire" avec le tri QuickSort
        /// </summary>
        private void Tri_XXX()
        {
            for (int i = 0; i < dictionnaire.Length; i++)
            {
                dictionnaire[i] = QuickSort(dictionnaire[i]);
            }
        }

        /// <summary>
        /// Trie le tableau de façon récursive avec le tri QuickSort
        /// </summary>
        /// <param name="tab"> string [] : tableau de string à trier </param>
        /// <returns> le tableau "tab" trié par ordre alphabètique </returns>
        private string[] QuickSort(string[] tab)
        {
            if (tab.Length <= 1)
            {
                return tab;
            }

            List<string> tabGauche = new List<string>();
            List<string> tabDroit = new List<string>();

            string pivot = tab[tab.Length - 1];

            for (int i = 0; i < tab.Length - 1; i++)
            {
                if (string.Compare(pivot, tab[i]) > 0)
                {
                    tabGauche.Add(tab[i]);
                }
                else
                {
                    tabDroit.Add(tab[i]);
                }
            }
            string[] t1 = QuickSort(tabGauche.ToArray());
            string[] t2 = QuickSort(tabDroit.ToArray());

            return t1.Concat(new string[] { pivot }).Concat(t2).ToArray();
        }
    }
}

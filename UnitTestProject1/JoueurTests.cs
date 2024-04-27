using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MotsDefiles;

namespace UnitTestProject1
{
    [TestClass]
    public class JoueurTests
    {
        [TestMethod]
        public void Add_Mot_WithValidWord_UpdatesMotsConnus()
        {
            string mot = "AFFECTIONNEE";
            Joueur j = new Joueur("Camille");
            
            j.Add_Mot(mot);
            bool actual = j.Contient(mot);

            Assert.IsTrue(actual, "Mot pas ajouté à la liste de mots connus.");
        }

        [TestMethod]
        public void Add_Mot_WithValidWord_AlreadyInMotsConnus()
        {
            string mot = "AFFECTIONNEE";
            Joueur j = new Joueur("Dupont");
            j.Add_Mot(mot);
            int expected = 1;

            j.Add_Mot(mot);
            int actual = j.MotsConnus.Count;

            Assert.AreEqual(expected, actual, "Ajout d'un mot déjà contenu dans la liste des mots connus."); 
        }

        [TestMethod]
        public void toString_WithValidArguments()
        {
            string expected = "Joueur : Dupont\nScore : 0 point\nAucun mot trouvé.";
            Joueur j = new Joueur("Dupont");

            string actual = j.toString();

            Assert.AreEqual(expected, actual, "Mauvais affichage des infos du joueur.");
        }

        [TestMethod]
        public void toString_WithUpdatesParameters()
        {
            string mot1 = "AFFECTION";
            string mot2 = "ACIDULE";
            int score = 75; 
            string expected = "Joueur : Dupont\nScore : " + score + " points\nMots trouvés : " + mot1 + ", " + mot2;
            Joueur j = new Joueur("Dupont");
            
            j.Add_Mot(mot1);
            j.Add_Mot(mot2);

            Console.Write(j.Add_Score(mot1));
            Console.Write(j.Add_Score(mot2));
            string actual = j.toString();

            Assert.AreEqual(expected, actual, "Infos liées au joueur pas correctement mises à jour.");
        }

        [TestMethod]
        public void Add_Score_WithValidArguments()
        {
            string mot1 = "AFFECTION";
            string mot2 = "ACIDULE";
            int expected = 75;

            Joueur j = new Joueur("Dupont");
            j.Add_Mot(mot1);
            j.Add_Mot(mot2);

            j.Add_Score(mot1);
            j.Add_Score(mot2);
            int actual = j.Score;

            Assert.AreEqual(expected, actual, "Erreur dans le calcul des coefs des lettres du mot ajouté au score.");
        }
    }
}

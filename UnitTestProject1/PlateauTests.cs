using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotsDefiles;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class PlateauTests
    {

        [TestMethod]
        public void CreationPlateau_WithValidArguments()
        {
            Plateau p = new Plateau(5, 5);

            Assert.IsNotNull(p, "Erreur dans l'ajout des paramètres");
        }

        [TestMethod]
        public void GenerePlateauAleatoire_WithValidPLateau()
        {
            Plateau p = new Plateau(5, 5);
            p = p.GenerePlateauAleatoire();

            Assert.IsNotNull(p.Plat, "Problème dans la création aléatoire du plateau");
        }

        [TestMethod]
        public void GenerePlateauAleatoire_WithEmptyPlateau()
        {
            Plateau p = new Plateau(0,0);

            Plateau expected = p;
            Plateau actual  = p.GenerePlateauAleatoire();

            Assert.AreEqual(expected, actual, "Création d'un plateau aléatoirement alors qu'il est censé être vide");
        }

        [TestMethod]
        public void toString_WithValidPlateau()
        {
            int ligne = 5;
            int colonne = 7;
            Plateau p = new Plateau(ligne, colonne);
            string expected = "Le plateau contient 35 caractères et est de taille 5 x 7";

            string actual = p.toString();
            Assert.AreEqual(expected, actual, "Erreur de l'affiche pour toString()"); 
        }

        //Je vois pas comment tester la méthode ToFile et AffichePlateau et donc Maj_Plateau() vu qu'elle est deja appelé dans RecherchePlateau

        [TestMethod]
        public void ToRead_WithExistingFile()
        {
            Plateau p = new Plateau(0, 0);
            p = p.ToRead("testalire.txt");
            p.AffichePlateau();

            Assert.IsNotNull(p, "Erreur dans la gestion du fichier");
        }

        [TestMethod]
        public void ToRead_WithNonExistingFile()
        {
            Plateau p = new Plateau(0, 0).ToRead("adfzefzfrffedzaaddzdefezffzfzefzefzfzefzefzef.txt");

            Assert.IsNull(p, "Erreur dans la gestion du fichier");
        }

        [TestMethod]
        public void RechercheMot_WithValidWord1()
        {
            Plateau p = new Plateau(5,5) ;
            p = p.ToRead("testalire.txt");
            p.AffichePlateau();

            bool actual = p.Recherche_Mot("maison");
            Assert.IsTrue(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithValidWord2()
        {
            Plateau p = new Plateau(5, 5);
            p = p.ToRead("testalire.txt");
            p.AffichePlateau();

            bool actual = p.Recherche_Mot("sale");
            Assert.IsTrue(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithValidWord3()
        {
            Plateau p = new Plateau(5, 5);
            p = p.ToRead("testalire.txt");
            p.AffichePlateau();

            bool actual = p.Recherche_Mot("saisi");
            Assert.IsTrue(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithValidWord4()
        {
            Plateau p = new Plateau(5, 5);
            p = p.ToRead("testalire.txt");
            p.AffichePlateau();

            bool actual = p.Recherche_Mot("mais");
            Assert.IsTrue(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithNonExistingWord1()
        {
            Plateau p = new Plateau(5, 5); ;
            p = p.ToRead("testalire.txt");

            bool actual = p.Recherche_Mot("vis");
            Assert.IsFalse(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithNonExistingWord2()
        {
            Plateau p = new Plateau(5, 5); ;
            p = p.ToRead("testalire.txt");

            bool actual = p.Recherche_Mot("eo");
            Assert.IsFalse(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithNonExistingWord3()
        {
            Plateau p = new Plateau(5, 5); ;
            p = p.ToRead("testalire.txt");

            bool actual = p.Recherche_Mot("la");
            Assert.IsFalse(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithNonExistingWord4()
        {
            Plateau p = new Plateau(5, 5); ;
            p = p.ToRead("testalire.txt");

            bool actual = p.Recherche_Mot("jouer");
            Assert.IsFalse(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }

        [TestMethod]
        public void RechercheMot_WithEmptyWord()
        {
            Plateau p = new Plateau(5, 5);
            p = p.ToRead("testalire.txt");

            bool actual = p.Recherche_Mot("");
            Assert.IsFalse(actual, "Erreur dans l'algo de recherche de mot existant dans le plateau");
        }
    }
}

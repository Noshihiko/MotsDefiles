using Microsoft.VisualStudio.TestTools.UnitTesting;
using MotsDefiles;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class DictionnaireTests
    {
        [TestMethod]
        public void CreatingDictionary_WithFileName()
        {
            string fileName = "Test.txt";
            Dictionnaire d = new Dictionnaire(fileName);
           
            Assert.IsNotNull(d, "Dictionnaire sans valeur avec nom de fichier.");
        }

        [TestMethod]
        public void CreatingDictionary_WithoutFileName()
        {
            Dictionnaire d = new Dictionnaire();
            Assert.IsNotNull(d, "Dictionnaire sans valeur sans nom de fichier.");
        }

        [TestMethod]
        public void toString_WithValidDictionary()
        {
            string fileName = "Test.txt";
            Dictionnaire d = new Dictionnaire(fileName);

            string expected = "Le dictionnaire est en français et contient :" +
                "\n7 mots pour la lettre A" +
                "\n11 mots pour la lettre B" +
                "\n6 mots pour la lettre C" +
                "\n7 mots pour la lettre D" +
                "\n8 mots pour la lettre E" +
                "\n6 mots pour la lettre F" +
                "\n10 mots pour la lettre G" +
                "\n8 mots pour la lettre H";

            string actual = d.toString();

            Assert.AreEqual(expected, actual, "Erreur d'affichage du dictionnaire");
        }

        [TestMethod]
        public void toString_WithEmptyDictionary()
        {
            string fileName = "TestDictionnaireVide.txt";
            Dictionnaire d = new Dictionnaire(fileName);
            string expected = "Le dictionnaire est en français et contient : 0 mots.";

            string actual = d.toString();
            Assert.AreEqual(expected, actual, "Erreur d'affichage du dictionnaire vide");
        }

        [TestMethod]
        public void RechDichoRecursif_WithValidWordAllCaps() 
        {
            Dictionnaire d = new Dictionnaire();
            string word = "AMOVIBLE";

            bool actual = d.RechDichoRecursif(word);

            Assert.IsTrue(actual, "Le mot recherché n'est pas trouvé dans le dictionnaire alors qu'il s'y trouve");
        }

        [TestMethod]
        public void RechDichoRecursif_WithValidWordLowerCaps()
        {
            Dictionnaire d = new Dictionnaire();
            string word = "AmoVible";

            bool actual = d.RechDichoRecursif(word);

            Assert.IsTrue(actual, "Le mot recherché n'est pas trouvé dans le dictionnaire alors qu'il s'y trouve");
        }

        [TestMethod]
        public void RechDichoRecursif_WithUnvalidWord() 
        {
            Dictionnaire d = new Dictionnaire();
            string word = "HahaEuHZ";

            bool actual = d.RechDichoRecursif(word);

            Assert.IsFalse(actual, "Le mot recherché est trouvé dans le dictionnaire alors qu'il ne s'y trouve pas");
        }
    }
}

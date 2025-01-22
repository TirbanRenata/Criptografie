using System;



namespace Substitutie_Polialfabetica
{
    internal class Program
    {
        public static string Cripteaza(string textClare, string[] chei)
        {
            string textCriptat = "";
            int lungimeCheie = chei.Length;
            for (int i = 0; i < textClare.Length; i++)
            {
                char c = textClare[i];
                if (Char.IsLetter(c))
                {
                    int indexCheie = i % lungimeCheie;
                    string alfabetPermutat = chei[indexCheie];
                    int indexAlfabet = c - 'A';
                    char literaCriptata = alfabetPermutat[indexAlfabet];
                    textCriptat += literaCriptata;
                }
                else
                {
                    textCriptat += c;
                }
            }

            return textCriptat;
        }
        public static string Decripteaza(string textCriptat, string[] chei)
        {
            string textClare = "";
            int lungimeCheie = chei.Length;
            for (int i = 0; i < textCriptat.Length; i++)
            {
                char c = textCriptat[i];
                if (Char.IsLetter(c))
                {
                    int indexCheie = i % lungimeCheie;
                    string alfabetPermutat = chei[indexCheie];
                    int indexPermutare = alfabetPermutat.IndexOf(c);
                    char literaDecriptata = (char)('A' + indexPermutare);
                    textClare += literaDecriptata;
                }
                else
                {
                    textClare += c;
                }
            }
            return textClare;
        }
        static void Main()
        {
            Console.WriteLine("Introduceti textul clar (doar litere majuscule):");
            string textClare = Console.ReadLine().ToUpper();
            Console.WriteLine("Introduceti 3 permutari ale alfabetului (fiecare cheia sa fie o secventa de 26 caractere):");
            string[] chei = new string[3];
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"Introduceti cheia {i + 1} (26 caractere):");
                chei[i] = Console.ReadLine().ToUpper();
            }
            string textCriptat = Cripteaza(textClare, chei);
            Console.WriteLine($"Textul criptat: {textCriptat}");
            string textDecriptat = Decripteaza(textCriptat, chei);
            Console.WriteLine($"Textul decriptat: {textDecriptat}");

            Console.ReadLine();
        }
    }
}

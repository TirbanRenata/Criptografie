using System;


namespace Cifrul_lui_Cezar
{
    internal class Program
    {
        public static string Cripteaza(string textClare)
        {
            string textCriptat = "";
            foreach (char c in textClare)
            {
                if (Char.IsLetter(c))
                {
                    char offset = Char.IsUpper(c) ? 'A' : 'a';
                    char caracterCriptat = (char)(((c - offset + 3) % 26) + offset);
                    textCriptat += caracterCriptat;
                }
                else
                {
                    textCriptat += c;
                }
            }
            return textCriptat;
        }
        public static string Decripteaza(string textCriptat)
        {
            string textClare = "";
            foreach (char c in textCriptat)
            {
                if (Char.IsLetter(c))
                {
                    char offset = Char.IsUpper(c) ? 'A' : 'a';
                    char caracterDecriptat = (char)(((c - offset - 3 + 26) % 26) + offset);
                    textClare += caracterDecriptat;
                }
                else
                {
                    textClare += c;
                }
            }
            return textClare;
        }
        public static void Criptanalizeaza(string textCriptat)
        {
            for (int n = 0; n < 26; n++)
            {
                string textDecriptat = DecripteazaCuCheia(textCriptat, n);
                Console.WriteLine($"Cheia n={n}: {textDecriptat}");
            }
        }
        public static string DecripteazaCuCheia(string textCriptat, int n)
        {
            string textClare = "";
            foreach (char c in textCriptat)
            {
                if (Char.IsLetter(c))
                {
                    char offset = Char.IsUpper(c) ? 'A' : 'a';
                    char caracterDecriptat = (char)(((c - offset - n + 26) % 26) + offset);
                    textClare += caracterDecriptat;
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
            Console.WriteLine("Introduceti textul pentru criptare:");
            string textClare = Console.ReadLine();
            string textCriptat = Cripteaza(textClare);
            Console.WriteLine($"Textul criptat: {textCriptat}");
            string textDecriptat = Decripteaza(textCriptat);
            Console.WriteLine($"Textul decriptat: {textDecriptat}");
            Console.WriteLine("\nCriptanaliza (încercăm toate cheile):");
            Criptanalizeaza(textCriptat);
            Console.ReadLine();
        }
    }
}

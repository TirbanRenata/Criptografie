using System;


namespace Cifrul__n
{
    internal class Program
    {
        public static string Cripteaza(string textClare, int n)
        {
            string textCriptat = "";
            foreach (char c in textClare)
            {
                if (Char.IsLetter(c)) // Verificăm dacă caracterul este literă
                {
                    char offset = Char.IsUpper(c) ? 'A' : 'a';
                    char caracterCriptat = (char)(((c - offset + n) % 26) + offset);
                    textCriptat += caracterCriptat;
                }
                else
                {
                    textCriptat += c; // Caracterele care nu sunt litere rămân nemodificate
                }
            }
            return textCriptat;
        }
        public static string Decripteaza(string textCriptat, int n)
        {
            string textClare = "";
            foreach (char c in textCriptat)
            {
                if (Char.IsLetter(c)) // Verificăm dacă caracterul este literă
                {
                    char offset = Char.IsUpper(c) ? 'A' : 'a';
                    char caracterDecriptat = (char)(((c - offset - n + 26) % 26) + offset);
                    textClare += caracterDecriptat;
                }
                else
                {
                    textClare += c; // Caracterele care nu sunt litere rămân nemodificate
                }
            }
            return textClare;
        }
        public static void Criptanalizeaza(string textCriptat)
        {
            for (int n = 0; n < 26; n++) // Toate valorile posibile pentru n
            {
                string textDecriptat = Decripteaza(textCriptat, n);
                Console.WriteLine($"Cheia n={n}: {textDecriptat}");
            }
        }
        static void Main()
        {
            Console.WriteLine("Introduceti textul pentru criptare:");
            string textClare = Console.ReadLine();
            Console.WriteLine("Introduceti cheia (0-25):");
            int n = int.Parse(Console.ReadLine());
            string textCriptat = Cripteaza(textClare, n);
            Console.WriteLine($"Textul criptat: {textCriptat}");
            string textDecriptat = Decripteaza(textCriptat, n);
            Console.WriteLine($"Textul decriptat: {textDecriptat}");
            Console.WriteLine("\nCriptanaliza (încercam toate cheile):");
            Criptanalizeaza(textCriptat);
            Console.ReadLine();
        }
    }
}

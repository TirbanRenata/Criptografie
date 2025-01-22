using System;


namespace Cifrul_Playfair
{
    internal class Program
    {
        public static char[,] CreazaMatrice(string cheia)
        {
            string alf = "ABCDEFGHIKLMNOPQRSTUVWXYZ"; // Fără J
            bool[] used = new bool[26]; 
            char[,] matrice = new char[5, 5];
            int k = 0;
            // Eliminăm caracterele duplicate din cheia introdusă
            foreach (char c in cheia.ToUpper())
            {
                char modificat = (c == 'J') ? 'I' : c; // Înlocuim J cu I
                if (!used[modificat - 'A']) 
                {
                    used[modificat - 'A'] = true;
                    matrice[k / 5, k % 5] = modificat;
                    k++;
                }
            }
            // Adăugăm literele alfabetului care nu sunt deja folosite
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (c == 'J') continue; // Nu adăugăm 'J' pentru că l-am tratat ca 'I'
                if (!used[c - 'A'])
                {
                    matrice[k / 5, k % 5] = c;
                    k++;
                }
            }
            return matrice;
        }
        public static (int, int) GăseștePoziția(char c, char[,] matrice)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrice[i, j] == c)
                        return (i, j);
                }
            }
            return (-1, -1);
        }
        public static string PreprocesareText(string text)
        {
            text = text.ToUpper().Replace("J", "I"); // Înlocuim J cu I
            string rezultat = "";

            // Împărțim textul în perechi de litere
            for (int i = 0; i < text.Length; i++)
            {
                if (i + 1 < text.Length && text[i] == text[i + 1])
                {
                    rezultat += text[i] + "X"; // Adăugăm X dacă perechea are litere identice
                }
                else
                {
                    rezultat += text[i];
                }
            }
            // Dacă textul are un număr impar de caractere, adăugăm un 'X' la final
            if (rezultat.Length % 2 != 0)
            {
                rezultat += "X";
            }

            return rezultat;
        }
        public static string Cripteaza(string text, string cheia)
        {
            char[,] matrice = CreazaMatrice(cheia);
            string textPreprocesat = PreprocesareText(text);
            string textCriptat = "";
            for (int i = 0; i < textPreprocesat.Length; i += 2)
            {
                char a = textPreprocesat[i];
                char b = textPreprocesat[i + 1];

                var poz1 = GăseștePoziția(a, matrice);
                var poz2 = GăseștePoziția(b, matrice);

                if (poz1.Item1 == poz2.Item1) 
                {
                    textCriptat += matrice[poz1.Item1, (poz1.Item2 + 1) % 5];
                    textCriptat += matrice[poz2.Item1, (poz2.Item2 + 1) % 5];
                }
                else if (poz1.Item2 == poz2.Item2) 
                {
                    textCriptat += matrice[(poz1.Item1 + 1) % 5, poz1.Item2];
                    textCriptat += matrice[(poz2.Item1 + 1) % 5, poz2.Item2];
                }
                else 
                {
                    textCriptat += matrice[poz1.Item1, poz2.Item2];
                    textCriptat += matrice[poz2.Item1, poz1.Item2];
                }
            }

            return textCriptat;
        }
        public static string Decripteaza(string text, string cheia)
        {
            char[,] matrice = CreazaMatrice(cheia);
            string textPreprocesat = PreprocesareText(text);
            string textDecriptat = "";
            // Procesăm fiecare pereche de litere
            for (int i = 0; i < textPreprocesat.Length; i += 2)
            {
                char a = textPreprocesat[i];
                char b = textPreprocesat[i + 1];

                var poz1 = GăseștePoziția(a, matrice);
                var poz2 = GăseștePoziția(b, matrice);

                if (poz1.Item1 == poz2.Item1) // Dacă literele sunt pe aceeași linie
                {
                    textDecriptat += matrice[poz1.Item1, (poz1.Item2 - 1 + 5) % 5];
                    textDecriptat += matrice[poz2.Item1, (poz2.Item2 - 1 + 5) % 5];
                }
                else if (poz1.Item2 == poz2.Item2) // Dacă literele sunt pe aceeași coloană
                {
                    textDecriptat += matrice[(poz1.Item1 - 1 + 5) % 5, poz1.Item2];
                    textDecriptat += matrice[(poz2.Item1 - 1 + 5) % 5, poz2.Item2];
                }
                else // Dacă literele sunt în colțuri opuse ale unui dreptunghi
                {
                    textDecriptat += matrice[poz1.Item1, poz2.Item2];
                    textDecriptat += matrice[poz2.Item1, poz1.Item2];
                }
            }
            return textDecriptat;
        }
        static void Main()
        {
            Console.WriteLine("Introduceti cheia:");
            string cheia = Console.ReadLine();
            Console.WriteLine("Introduceti textul pentru criptare:");
            string text = Console.ReadLine();
            string textCriptat = Cripteaza(text, cheia);
            Console.WriteLine($"Textul criptat: {textCriptat}");
            string textDecriptat = Decripteaza(textCriptat, cheia);
            Console.WriteLine($"Textul decriptat: {textDecriptat}");
            Console.ReadLine();
        }
    }
}

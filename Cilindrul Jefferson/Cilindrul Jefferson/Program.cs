using System;
using System.Collections.Generic;


namespace Cilindrul_Jefferson
{
    class CilindruJefferson
    {
        private List<string> discuri;
        private List<int> cheie;
        public CilindruJefferson(int n)
        {
            discuri = new List<string>();
            Random rand = new Random();
            // Generăm n discuri cu permutări aleatorii ale literelor A-Z
            for (int i = 0; i < n; i++)
            {
                char[] litere = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                Amesteca(litere, rand);
                discuri.Add(new string(litere));
            }
            // Generăm cheia de criptare (permutare a valorilor de la 1 la n)
            cheie = new List<int>();
            for (int i = 1; i <= n; i++)
            {
                cheie.Add(i);
            }
            Amesteca(cheie.ToArray(), rand);
        }
        private void Amesteca<T>(T[] array, Random rand)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
        public string Cripteaza(string textClar)
        {
            string textCriptat = "";
            int lungimeText = textClar.Length;
            int lungimeDisc = discuri[0].Length;
            for (int i = 0; i < lungimeText; i++)
            {
                char c = textClar[i];
                if (c < 'A' || c > 'Z') continue;
                int index = c - 'A';
                int indexDisc = i % cheie.Count;
                textCriptat += discuri[cheie[indexDisc] - 1][index % lungimeDisc];
            }
            return textCriptat;
        }
        public string Decripteaza(string textCriptat)
        {
            string textClar = "";
            int lungimeText = textCriptat.Length;
            int lungimeDisc = discuri[0].Length;
            for (int i = 0; i < lungimeText; i++)
            {
                char c = textCriptat[i];
                int indexDisc = i % cheie.Count;
                int indexCaracter = discuri[cheie[indexDisc] - 1].IndexOf(c);
                if (indexCaracter != -1)
                {
                    textClar += (char)('A' + indexCaracter);
                }
            }
            return textClar;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduceti numarul de discuri:");
            int n = int.Parse(Console.ReadLine());
            CilindruJefferson cilindruJefferson = new CilindruJefferson(n);
            Console.WriteLine("Introduceti mesajul de criptat:");
            string mesaj = Console.ReadLine();
            string mesajCriptat = cilindruJefferson.Cripteaza(mesaj);
            Console.WriteLine($"Mesaj criptat: {mesajCriptat}");
            string mesajDecriptat = cilindruJefferson.Decripteaza(mesajCriptat);
            Console.WriteLine($"Mesaj decriptat: {mesajDecriptat}");
            Console.ReadLine();
        }
    }
}

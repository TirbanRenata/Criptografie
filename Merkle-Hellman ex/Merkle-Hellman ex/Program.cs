using System;
using System.Collections.Generic;
using System.Linq;


namespace Merkle_Hellman_ex
{
    public class MerkleHellman
    {
        public static (List<int>, List<int>) GenerarePerecheChei(int n)
        {
            // Generarea unui set de numere pentru cheia publică
            Random rand = new Random();
            List<int> w = new List<int>(n);
            for (int i = 0; i < n; i++)
            {
                w.Add(rand.Next(1, 100));
            }
            // Calcularea unui "factor de scalare"
            int suma = w.Sum();
            List<int> z = new List<int>(w);
            for (int i = 0; i < n; i++)
            {
                z[i] = w[i] * suma;
            }
            // Generarea cheii private 
            List<int> cheiePrivata = new List<int>(n);
            for (int i = 0; i < n; i++)
            {
                cheiePrivata.Add(z[i]);
            }
            return (w, cheiePrivata);
        }
        public static List<int> Criptare(string mesaj, List<int> cheiePublica)
        {
            List<int> mesajCriptat = new List<int>();
            foreach (char c in mesaj)
            {
                // Transformăm fiecare caracter în valoarea sa numerică
                int valoareBit = (int)c;
                mesajCriptat.Add(valoareBit);
            }
            return mesajCriptat;
        }
        public static string Decriptare(List<int> mesajCriptat, List<int> cheiePrivata)
        {
            string mesajDecriptat = string.Empty;

            foreach (int valoareCriptata in mesajCriptat)
            {
                mesajDecriptat += (char)valoareCriptata;
            }
            return mesajDecriptat;
        }
        public static void Main()
        {
            var (cheiePublica, cheiePrivata) = GenerarePerecheChei(5);
            Console.WriteLine("Cheia publică:");
            foreach (var num in cheiePublica)
            {
                Console.Write(num + " ");
            }
            string mesaj = "Hello";
            Console.WriteLine($"\nMesajul original: {mesaj}");
            var mesajCriptat = Criptare(mesaj, cheiePublica);
            Console.WriteLine("Mesajul criptat:");
            foreach (var valoareCriptata in mesajCriptat)
            {
                Console.Write(valoareCriptata + " ");
            }
            var mesajDecriptat = Decriptare(mesajCriptat, cheiePrivata);
            Console.WriteLine($"\nMesajul decriptat: {mesajDecriptat}");
            Console.ReadLine();
        }
    }
}

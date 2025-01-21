using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubstitutieMonoalfabetica
{
    internal class Program
    {
        private static readonly string alfabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string cheia = "MIPODSQRTUVWXYZABCEFGHJKL";

        public static string Cripteaza(string textClare)
        {
            string textCriptat = "";
            foreach (char c in textClare)
            {
                if (Char.IsLetter(c))
                {
                    char cUpper = Char.ToUpper(c);
                    int index = alfabet.IndexOf(cUpper);
                    textCriptat += Char.IsUpper(c) ? cheia[index] : Char.ToLower(cheia[index]);
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
                    char cUpper = Char.ToUpper(c);
                    int index = cheia.IndexOf(cUpper);
                    textClare += Char.IsUpper(c) ? alfabet[index] : Char.ToLower(alfabet[index]);
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
            Dictionary<char, int> frecventa = new Dictionary<char, int>();
            foreach (char c in textCriptat.ToUpper())
            {
                if (Char.IsLetter(c))
                {
                    if (frecventa.ContainsKey(c))
                        frecventa[c]++;
                    else
                        frecventa[c] = 1;
                }
            }
            var litereSortate = frecventa.OrderByDescending(x => x.Value).ToList();
            Console.WriteLine("Frecvența literelor din textul criptat:");
            foreach (var item in litereSortate)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
            string frecventaLimbaRomana = "AEIOUETNRSLCPMDGBHVFJKXYZ";
            string presupusaCheie = "";
            foreach (var item in litereSortate)
            {
                if (presupusaCheie.Length < 26)
                {
                    char literaCriptata = item.Key;
                    char literaDesencriptata = frecventaLimbaRomana[presupusaCheie.Length];
                    presupusaCheie += literaCriptata + "->" + literaDesencriptata + " ";
                }
            }
            Console.WriteLine("\nPresupunerea cheii pe baza criptanalizei:");
            Console.WriteLine(presupusaCheie);
        }
        static void Main()
        {
            Console.WriteLine("Introduceti textul pentru criptare:");
            string textClare = Console.ReadLine();
            string textCriptat = Cripteaza(textClare);
            Console.WriteLine($"Textul criptat: {textCriptat}");
            string textDecriptat = Decripteaza(textCriptat);
            Console.WriteLine($"Textul decriptat: {textDecriptat}");
            Console.WriteLine("\nCriptanaliza (folosind frecventa literelor):");
            Criptanalizeaza(textCriptat);
            Console.ReadLine();
        }
    }
}

using System;
using System.Numerics;

namespace Criptare_RSA
{
    public class RSA
    {
        private BigInteger p, q, n, phi, e, d;
        public RSA()
        {
            GenerateKeys();
        }
        private void GenerateKeys()
        {
            // Generăm două numere prime mari
            p = GenerateLargePrime();
            q = GenerateLargePrime();
            n = p * q;
            phi = (p - 1) * (q - 1);
            // Alegem e, 1 < e < φ(n) și gcd(e, φ(n)) = 1
            e = 65537; // Un număr prim frecvent folosit
            // Calculăm d, inversul lui e modulo φ(n)
            d = ModInverse(e, phi);
        }
        // Criptarea mesajului m: c = m^e mod n
        public BigInteger Encrypt(BigInteger message)
        {
            return ModularExponentiation(message, e, n);
        }

        // Decriptarea mesajului criptat c: m = c^d mod n
        public BigInteger Decrypt(BigInteger cipher)
        {
            return ModularExponentiation(cipher, d, n);
        }
        private BigInteger GenerateLargePrime()
        {
            Random rand = new Random();
            BigInteger prime;
            do
            {
                prime = GenerateRandomOddNumber(rand, 512); // 512 biți
            } while (!IsProbablePrime(prime));
            return prime;
        }
        private BigInteger GenerateRandomOddNumber(Random rand, int bits)
        {
            byte[] bytes = new byte[bits / 8];
            rand.NextBytes(bytes);
            bytes[bytes.Length - 1] |= 0x01;
            return new BigInteger(bytes);
        }
        // Testul probabilistic de primalitate Miller-Rabin
        private bool IsProbablePrime(BigInteger n, int k = 10)
        {
            if (n < 2) return false;
            if (n != 2 && n % 2 == 0) return false;
            BigInteger d = n - 1;
            int r = 0;
            while (d % 2 == 0)
            {
                d /= 2;
                r++;
            }
            Random rand = new Random();
            for (int i = 0; i < k; i++)
            {
                BigInteger a = 2 + (BigInteger)(rand.NextDouble() * (double)(n - 4));
                BigInteger x = ModularExponentiation(a, d, n);
                if (x == 1 || x == n - 1)
                    continue;
                bool isComposite = true;
                for (int j = 0; j < r - 1; j++)
                {
                    x = ModularExponentiation(x, 2, n);
                    if (x == n - 1)
                    {
                        isComposite = false;
                        break;
                    }
                }
                if (isComposite)
                    return false;
            }
            return true;
        }
        // Algoritmul lui Euclid pentru GCD
        private BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                BigInteger temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Algoritmul Euclid extins pentru a calcula inversul modular
        private BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m, t, q;
            BigInteger x0 = 0, x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                q = a / m;
                t = m;

                m = a % m;
                a = t;
                t = x0;

                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0) x1 += m0;

            return x1;
        }

        // Exponentiere modulară rapidă
        private BigInteger ModularExponentiation(BigInteger baseValue, BigInteger exponent, BigInteger modulus)
        {
            BigInteger result = 1;
            baseValue = baseValue % modulus;
            while (exponent > 0)
            {
                if (exponent % 2 == 1)
                    result = (result * baseValue) % modulus;
                exponent = exponent >> 1;
                baseValue = (baseValue * baseValue) % modulus;
            }
            return result;
        }
        public static void Main()
        {
            RSA rsa = new RSA();
            Console.WriteLine("Cheia publică (n, e):");
            Console.WriteLine($"n: {rsa.n}");
            Console.WriteLine($"e: {rsa.e}");
            Console.WriteLine("Cheia privată (d):");
            Console.WriteLine($"d: {rsa.d}");
            BigInteger message = 12345;
            Console.WriteLine($"\nMesajul original: {message}");
            BigInteger cipher = rsa.Encrypt(message);
            Console.WriteLine($"Mesajul criptat: {cipher}");
            BigInteger decryptedMessage = rsa.Decrypt(cipher);
            Console.WriteLine($"Mesajul decriptat: {decryptedMessage}");
            Console.ReadLine();
        }
    }
}

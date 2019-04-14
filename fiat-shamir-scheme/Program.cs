using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Math;

namespace fiat_shamir_scheme
{
    class Program
    {
        static BigInteger getRandomBit()
        {
            return new Random().Next(0, 2) == 1 ? BigInteger.One : BigInteger.Zero;
        }
        static void Main(string[] args)
        {
           
            Console.WriteLine("---Центр доверия---");
            BigInteger p = BigInteger.ProbablePrime(256, new Random());
            BigInteger q = BigInteger.ProbablePrime(256, new Random());
            BigInteger n = p.Multiply(q);
            Console.WriteLine("n: " + n);


            Console.WriteLine("---Абонент---");
            BigInteger s = BigInteger.ProbablePrime(256, new Random()); // private key 
            while (s.CompareTo(n) != -1 && s.Gcd(n) != BigInteger.One)
            {
                s = BigInteger.ProbablePrime(256, new Random());
            }
            BigInteger v = s.ModPow(BigInteger.Two,n);// public key
            Console.WriteLine("V: " + v);
            Console.Write("Введите кол-во аккредитаций: ");
            int t = Int32.Parse(Console.ReadLine());

            for(int i = 0; i < t; i++)
            {
                Console.WriteLine("---Аккредитация---"+(i+1));

                BigInteger r = BigInteger.ProbablePrime(256, new Random());

                while (r.CompareTo(n) != -1 && r.Gcd(n) != BigInteger.One)
                {
                    Console.WriteLine(n);
                    Console.WriteLine(r);

                    r = BigInteger.ProbablePrime(256, new Random());
                }


                BigInteger x = r.ModPow(BigInteger.Two, n);

                Console.WriteLine("P->V :" + x);

                BigInteger e = getRandomBit();
                Console.WriteLine("V->P :" + e);

                BigInteger y = r.Multiply(s.Pow(Int32.Parse(e.ToString())).Mod(n));


                Console.WriteLine("P->V :" + y);

                BigInteger a = y.Pow(2).Mod(n);



                BigInteger b = x.Multiply(v.Pow(Int32.Parse(e.ToString()))).Mod(n);



                    if (a.ToString() != b.ToString())
                    {
                        Console.WriteLine("V: Проверка не пройдена.");
                        Console.ReadKey();
                        break;
                    }
                else
                {
                    Console.WriteLine("V: Проверка пройдена!!");
                }
                

            }

           
            Console.ReadKey();

        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FANNCSharp;
using FANN;

namespace FANN
{
    class Program
    {
        private static int charindex(char c)
        {
            return (int)c % 32;
        }

        static double[] calc_frequencies(string file)
        {
            string text = File.ReadAllText(file);
            double[] letter_frequ = new double[27];

            int num_chars = 0;
            foreach (char c in text)
            {
                if (char.ToLower(c) >= 97 && char.ToLower(c) < 123) num_chars++;
            }
            Console.WriteLine(num_chars);
            foreach (char c in text)
            {
                Console.Write(charindex(char.ToLower(c)) + " ");
                letter_frequ[charindex(char.ToLower(c))] += (1f/num_chars);
            }
            //Console.WriteLine("\n" + (double)1/5);

            foreach(double f in letter_frequ)
            {
                Console.Write(f+ " ");
            }
            Console.WriteLine();


            return letter_frequ;

        }

        static void fill_train_file(string file_in, string file_out)
        {
            double[] frequencies = calc_frequencies(file_in);

            if (File.Exists(file_out))
            {
                foreach (double freq in frequencies)
                {
                    Console.Write(freq.ToString() + " ");
                }


                using (StreamWriter sw = File.AppendText(file_out))
                {

                    sw.Write("\n1 0 0\n");
                    foreach (float freq in frequencies)
                    {
                        sw.Write(freq.ToString() + " ");
                    }

                }

            }
            else
            {
                Console.WriteLine("no file");
            }

        }
        public static void Main()
        {

            string file_in = Directory.GetCurrentDirectory() + "\\text.txt";
            string file_out = Directory.GetCurrentDirectory() + "\\training.txt";
            //Console.WriteLine((int)'z');

            fill_train_file(file_in, file_out);
            //if (File.Exists(file))
            //string body = File.ReadAllText(file);

            Console.Read();
        }
    }
}

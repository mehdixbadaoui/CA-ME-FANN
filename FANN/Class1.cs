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

        public static void Main()
        {
            float[] letter_frequ = new float[27];

            string file = "C:\\Users\\mebadaoui\\Documents\\AI\\CA-ME-FANN\\FANN\\text.txt";
            if (File.Exists(file))
            {
                int num_chars = 0;
                string body = File.ReadAllText(file);
                foreach (char c in body)
                {
                    if(c != ' ') num_chars++;
                }

                foreach (char c in body)
                {
                    //Console.Write(charindex(c) + " ");
                    letter_frequ[charindex(c)] = (letter_frequ[charindex(c)] + 1) / num_chars;
                }

                Console.WriteLine(letter_frequ[1]);
            }

            Console.Read();
        }
    }
}

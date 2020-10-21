using System;
using System.IO;
using System.Collections.Generic;
using FANNCSharp.Double;

namespace UnderstandFANN
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
            double[] letter_frequ = new double[32];

            int num_chars = 0;
            foreach (char c in text)
            {
                if (char.ToLower(c) >= 97 && char.ToLower(c) < 123) num_chars++;
            }

            foreach (char c in text)
            {
                letter_frequ[charindex(char.ToLower(c))] += (1f / num_chars);
            }



            return letter_frequ;

        }

        static void Main()
        {
            double[][] inputs =
            {
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\english1.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\english2.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\english3.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\french1.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\french2.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\french3.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\swedish1.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\swedish2.txt"),
               calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\swedish3.txt")
            };

            double[][] outputs =
            {
                new double[]{1,0,0},
                new double[]{1,0,0},
                new double[]{1,0,0},
                new double[]{0,1,0},
                new double[]{0,1,0},
                new double[]{0,1,0},
                new double[]{0,0,1},
                new double[]{0,0,1},
                new double[]{0,0,1}
            };

            List<uint> layers = new List<uint>();
            layers.Add(32);
            layers.Add(4);
            layers.Add(3);

            NeuralNet network = new NeuralNet(FANNCSharp.NetworkType.LAYER, layers);

            TrainingData data = new TrainingData();
            data.SetTrainData(inputs, outputs);

            network.TrainOnData(data, 3000, 100, 0.001f);

            Console.WriteLine("Final Error :" + network.MSE);

            double[] test = calc_frequencies(Directory.GetCurrentDirectory() + "\\texts\\test.txt");
            double[] result = network.Run(test);


            Console.WriteLine();
            Console.WriteLine("EN: " + result[0]);
            Console.WriteLine("FR: " + result[1]);
            Console.WriteLine("SE: " + result[2]);

            Console.Read();



        }

    }
}

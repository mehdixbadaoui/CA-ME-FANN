using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FANNCSharp;
using FANN;
//using FANNCSharp.Double;
using FANNCSharp.Float;

namespace FANN
{
    class Program
    {
        private int charindex(char c)
        {
            return (int)c % 32;
        }

        double[] calc_frequencies(string file)
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
                letter_frequ[charindex(char.ToLower(c))] += (1f/num_chars);
            }



            return letter_frequ;

        }

        void fill_train_file(string file_in, string file_out)
        {
            double[] frequencies = calc_frequencies(file_in);

            if (File.Exists(file_out))
            {

                using (StreamWriter sw = File.AppendText(file_out))
                {

                    sw.Write("\n0 0 1\n");
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

            //fill_train_file(file_in, file_out);
            List<uint> layers = new List<uint>();
            layers.Add(12);
            layers.Add(26);
            layers.Add(3);

            NeuralNet network = new NeuralNet(FANNCSharp.NetworkType.LAYER, layers);

            TrainingData data = new TrainingData();
            network.TrainOnFile("training.data", 200, 10, 0.001f);
            network.Save("trained.net");

            Console.WriteLine("error: ", +network.MSE);
            
            //Console.Read();
        }
    }
}

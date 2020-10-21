using FANNCSharp.Double;
using System.IO;
using System.Collections.Generic;
using System;

namespace NeuralNetwork {
    class Program2 {
        static void Main2(string[] args) {

            double[][] inputs = {
                Generate_frequencies("texts/fr1.txt"),
                Generate_frequencies("texts/fr2.txt"),
                Generate_frequencies("texts/fr3.txt"),
                Generate_frequencies("texts/en1.txt"),
                Generate_frequencies("texts/en2.txt"),
                Generate_frequencies("texts/en3.txt"),
                Generate_frequencies("texts/lt1.txt"),
                Generate_frequencies("texts/lt2.txt"),
                Generate_frequencies("texts/lt3.txt")
            };
            double[][] outputs = {
                new double[] { 1.0, 0, 0 },
                new double[] { 1.0, 0, 0 },
                new double[] { 1.0, 0, 0 },
                new double[] { 0, 1.0, 0 },
                new double[] { 0, 1.0, 0 },
                new double[] { 0, 1.0, 0 },
                new double[] { 0, 0, 1.0 },
                new double[] { 0, 0, 1.0 },
                new double[] { 0, 0, 1.0 }
            };

            // Create network
            List<uint> layers = new List<uint>();
            layers.Add(26);
            layers.Add(12);
            layers.Add(6);
            layers.Add(3);

            NeuralNet net = new NeuralNet(FANNCSharp.NetworkType.LAYER, layers);

            // Training
            TrainingData data = new TrainingData();
            data.SetTrainData(inputs, outputs);

            net.TrainOnData(data, 3000, 100, 0.001f);

            Console.WriteLine(net.MSE);


        }

        static double[] Generate_frequencies(string filename) {
            uint[] letters = new uint[26];
            double[] freq = new double[26];
            string txt = File.ReadAllText(filename);
            int totalLetters = 0;
            foreach (char c in txt) {
                if (c >= 'a' && c <= 'z') {
                    Char.ToLower(c);
                    letters[c - 'a']++;
                    totalLetters++;
                }
            }
            for (int i = 0; i < letters.Length; i++) {
                freq[i] = (float)letters[i] / totalLetters;
            }
            return freq;
        }
    }
}

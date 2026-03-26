using Microsoft.ML;
using MLModel2_ConsoleApp2;
using System;
using System.IO;
using System.Linq;

namespace MLModel2_ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TESTIRANJE MODELA ZA DETEKCIJU NAPADA\n");

            try
            {
                TestModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Greska: " + ex.Message);
            }

            Console.WriteLine("\nPritisnite Enter za izlaz.");
            Console.ReadLine();
        }

        static void TestModel()
        {
            string testDataPath = @"C:\Users\Korisnik\source\repos\DetekcijaMreznogPrometa\DetekcijaMreznogPrometa\Datasets\UNSW_NB15_training-set.csv";
            string modelPath = @"C:\Users\Korisnik\source\repos\DetekcijaMreznogPrometa\MLModel2_ConsoleApp2\MLModel2.mlnet";


            if (!File.Exists(modelPath))
            {
                modelPath = Path.Combine(Directory.GetCurrentDirectory(), "MLModel2.mlnet");
            }

            if (!File.Exists(modelPath))
            {
                Console.WriteLine("Model nije pronadjen!");
                return;
            }

            var mlContext = new MLContext();

            ITransformer model;
            using (var stream = File.OpenRead(modelPath))
            {
                //ucitavanje modela
                model = mlContext.Model.Load(stream, out _);
            }

            if (!File.Exists(testDataPath))
            {
                Console.WriteLine("Dataset nije pronadjen!");
                return;
            }

            //ucitavanje csv u ML.NET format
            var testData = mlContext.Data.LoadFromTextFile<MLModel2.ModelInput>(
                testDataPath, separatorChar: ',', hasHeader: true);

            var predictions = model.Transform(testData);

            //pretvaranje u C# objekte
            var predictedResults = mlContext.Data.CreateEnumerable<MLModel2.ModelOutput>(predictions, false);
            var actualData = mlContext.Data.CreateEnumerable<MLModel2.ModelInput>(testData, false);

            var predictedArray = predictedResults.ToArray();
            var actualArray = actualData.ToArray();

            int total = predictedArray.Length;
            Console.WriteLine($"Ukupno primjera: {total:N0}\n");

            int TP = 0, TN = 0, FP = 0, FN = 0;

            for (int i = 0; i < total; i++)
            {
                float actual = actualArray[i].Label;
                float predicted = predictedArray[i].PredictedLabel;

                if (actual == 1 && predicted == 1) TP++;
                else if (actual == 0 && predicted == 0) TN++;
                else if (actual == 0 && predicted == 1) FP++;
                else if (actual == 1 && predicted == 0) FN++;
            }

            double accuracy = (double)(TP + TN) / total;
            double precision = (TP + FP) > 0 ? (double)TP / (TP + FP) : 0;
            double recall = (TP + FN) > 0 ? (double)TP / (TP + FN) : 0;

            Console.WriteLine("REZULTATI:");

            Console.WriteLine($"\nUkupno predvidjeno: {total:N0} primjera");
            Console.WriteLine($"Tocnost (Accuracy): {accuracy:P2}");
            Console.WriteLine($"Preciznost (Precision): {precision:P2}");
            Console.WriteLine($"Odziv (Recall): {recall:P2}");

            Console.WriteLine("\nMATRICA KONFUZIJE:");
            Console.WriteLine($"|                 | Pred Normal | Pred Attack |");
            Console.WriteLine($"|-----------------|-------------|-------------|");
            Console.WriteLine($"| Actual Normal   |{TN,6}       |{FP,6}       |");
            Console.WriteLine($"|-----------------|-------------|-------------|");
            Console.WriteLine($"| Actual Attack   |{FN,6}       |{TP,6}       |");
            Console.WriteLine("-------------------------------------------");

            Console.WriteLine("\nDETALJI:");
            Console.WriteLine($"True Negative (TN): {TN:N0} - Normal pravilno");
            Console.WriteLine($"False Positive (FP): {FP:N0} - Normal, model predvidio kao Attack");
            Console.WriteLine($"False Negative (FN): {FN:N0} - Attack, model predvidio kao Normal");
            Console.WriteLine($"True Positive (TP): {TP:N0} - Attack pravilno");


            Console.WriteLine("\nZAKLJUCAK:");

            if (accuracy > 0.95)
                Console.WriteLine($"ODLICNO! Model ima {accuracy:P2} tocnosti.");
            else if (accuracy > 0.85)
                Console.WriteLine($"DOBRO! Model ima {accuracy:P2} tocnosti.");
            else if (accuracy > 0.70)
                Console.WriteLine($"Model ima {accuracy:P2} tocnosti.");
            else
                Console.WriteLine($"POTREBNA OPTIMIZACIJA! Model ima samo {accuracy:P2} tocnosti.");

        }
    }
}
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Projekt: Detekcija zlonamjernog mrežnog prometa korištenjem strojnog učenja");
        Console.WriteLine("-----------------------------------------------------------------------");

        Console.WriteLine("Korišteni skup podataka:");
        Console.WriteLine("- UNSW-NB15 (javno dostupni dataset za mrežnu sigurnost)");

        Console.WriteLine("\nKorištene tehnologije:");
        Console.WriteLine("- .NET");
        Console.WriteLine("- ML.NET");
        Console.WriteLine("- ML.NET Model Builder");

        Console.WriteLine("\nOpis rješenja:");
        Console.WriteLine("U projektu su razvijena dva klasifikacijska modela strojnog učenja:");

        Console.WriteLine("\n1) Binarni klasifikacijski model:");
        Console.WriteLine("   - Cilj: razlikovanje normalnog i zlonamjernog mrežnog prometa");
        Console.WriteLine("   - Labela: 'label' (0 = normal, 1 = attack)");
        Console.WriteLine("   - Evaluacija: Accuracy, Precision, Recall");


        Console.WriteLine("\nImplementacija:");
        Console.WriteLine("- Treniranje modela provedeno je pomoću ML.NET Model Builder alata");
        Console.WriteLine("- Evaluacija i testiranje modela implementirani su u zasebnim Console aplikacijama:");
        Console.WriteLine("  * MLModel1_ConsoleApp1 – binarna klasifikacija");

        Console.WriteLine("\nZaključak:");
        Console.WriteLine("Projekt demonstrira primjenu strojnog učenja u području detekcije mrežnih napada,");
        Console.WriteLine("uz uspješnu detekciju zlonamjernog prometa.");

        Console.WriteLine("\nProjekt je uspješno implementiran i spreman za evaluaciju.");
    }
}

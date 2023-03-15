using System;
using System.Collections.Generic;
using System.IO;


/// //////.........Seyed Erfan Noorbakhsh......///////////
/// //////..............Seyed Erfan Noorbakhsh..........///////////


public class Program
{
    static void Main(string[] args)
    {
        // Get resource path from user 
        Console.WriteLine("Choose your Dataset file path.");
        Console.WriteLine("If you don't have the file, you can download it from bigdataset-ir.com website");
        Console.WriteLine("Enter the dataset file path here: ");

        string dataSetFilePath = Console.ReadLine();

        // define collection of orders as objects in c#
        List<string[]> orders = new List<string[]>();

        // Fill orders Collection
        using (StreamReader reader = new StreamReader(dataSetFilePath))
        {
            string line = reader.ReadLine();
            while ((line = reader.ReadLine()) != null)
            {
                orders.Add(line.Split('\u002C')/* Comma unicode character ( , ) */); // Add new values to orders collections as object
            }
        }

        // Classification based on cities in a key & value pair
        Dictionary<string, List<string[]>> cityRecords = new Dictionary<string, List<string[]>>();

        foreach (string[] order in orders)
        {
            string cityName = order[5]; // city_name_fa Column in .csv file

            if (!cityRecords.ContainsKey(cityName))
            {
                cityRecords.Add(cityName, new List<string[]>());
            }

            cityRecords[cityName].Add(order);
        }

        // write files
        Console.WriteLine("Start Generating Files from data");
        foreach (string cityName in cityRecords.Keys)
        {
            string directoryPath = new FileInfo(dataSetFilePath).Directory.FullName;
            using (StreamWriter writer = new StreamWriter(directoryPath + "/" + cityName + ".csv"))
            {
                Console.WriteLine($"writing data for city {cityName}");
                string text = "ID_Order,ID_Customer,ID_Item,DateTime_CartFinalize,Amount_Gross_Order,city_name_fa,Quantity_item";
                writer.WriteLine(text);
                foreach (string[] order in cityRecords[cityName])
                {
                    text = $"{string.Join("\u002C", order)}";
                    writer.WriteLine(text);
                }
            }
        }

    }
}


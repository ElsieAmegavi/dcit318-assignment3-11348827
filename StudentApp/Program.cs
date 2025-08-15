using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    public static void Main()
    {
        var processor = new StudentResultProcessor();
        string inputFile = "students.txt";
        string outputFile = "report.txt";
        try
        {
            var students = processor.ReadStudentsFromFile(inputFile);
            processor.WriteReportToFile(students, outputFile);
            Console.WriteLine($"Report written to {outputFile}");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
        }
        catch (InvalidScoreFormatException ex)
        {
            Console.WriteLine($"Invalid score format: {ex.Message}");
        }
        catch (MissingFieldException ex)
        {
            Console.WriteLine($"Missing field: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}

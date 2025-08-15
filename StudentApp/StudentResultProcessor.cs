using System;
using System.Collections.Generic;
using System.IO;

public class StudentResultProcessor
{
    public List<Student> ReadStudentsFromFile(string inputFilePath)
    {
        var students = new List<Student>();
        using (var reader = new StreamReader(inputFilePath))
        {
            string? line;
            int lineNumber = 0;
            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                var parts = line.Split(',');
                if (parts.Length != 3)
                    throw new MissingFieldException($"Line {lineNumber}: Missing fields. Expected 3, got {parts.Length}.");
                if (!int.TryParse(parts[0].Trim(), out int id))
                    throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid ID format.");
                string name = parts[1].Trim();
                if (!int.TryParse(parts[2].Trim(), out int score))
                    throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format.");
                students.Add(new Student(id, name, score));
            }
        }
        return students;
    }

    public void WriteReportToFile(List<Student> students, string outputFilePath)
    {
        using (var writer = new StreamWriter(outputFilePath))
        {
            foreach (var student in students)
            {
                writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
            }
        }
    }
}

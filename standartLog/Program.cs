using System;
using System.IO;
using System.Globalization;
using System.Text;

class Program
{
    static void Main()
    {
        string inputPath = @"C:\Users\filip\source\repos\test\standartLog\input.txt";
        string outputPath = @"C:\Users\filip\source\repos\test\standartLog\output.txt";
        string errorPath = @"C:\Users\filip\source\repos\test\standartLog\problems.txt";
        if (!File.Exists(inputPath))
        {
            Console.WriteLine("Ошибка: Входной файл input.txt не найден!");
            return;
        }
        using (StreamReader reader = new StreamReader(inputPath, Encoding.Default))
        using (StreamWriter swSuccess = new StreamWriter(outputPath, false, Encoding.UTF8))
        using (StreamWriter swErrors = new StreamWriter(errorPath, false, Encoding.UTF8))
        {

            foreach (string line in File.ReadLines(inputPath))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string processedLine = ProcessLogLine(line);

                if (processedLine != null)
                {
                    swSuccess.WriteLine(processedLine);
                }
                else
                {
                    swErrors.WriteLine(line);
                }
            }
        }
        Console.WriteLine("Обработка завершена. Результаты в output.txt, ошибки в problems.txt");
    }

    static string ProcessLogLine(string line)
    {
        try
        {
            string dateStr, timeStr, level, method, message;
            DateTime dt;

            if (line.Contains("|"))
            {
                string[] parts = line.Split('|');
                if (parts.Length < 5)
                {
                    return null;
                } 
                    
                string[] dateTimeParts = parts[0].Trim().Split(' ');
                dateStr = dateTimeParts[0];
                timeStr = dateTimeParts[1].Length > 12 ? dateTimeParts[1].Substring(0, 12) : dateTimeParts[1];

                level = parts[1].Trim();
                method = parts[3].Trim();
                message = parts[4].Trim();

                dt = DateTime.ParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                string[] parts = line.Split(' ', 4);
                if (parts.Length < 4)
                {
                    return null;
                }
                dateStr = parts[0];
                timeStr = parts[1];
                level = parts[2];
                method = "DEFAULT";
                message = parts[3];

                dt = DateTime.ParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            if (level == "INFORMATION")
            {
                level = "INFO";
            } 
                
            else if (level == "WARNING")
            {
                level = "WARN";
            }
                

            return $"{dt:dd-MM-yyyy}\t{timeStr}\t{level}\t{method}\t{message}";
        }
        catch
        {
            return null;
        }
    }
}
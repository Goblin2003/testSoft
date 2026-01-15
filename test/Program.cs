using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string stroke = "";
            if (String.IsNullOrEmpty(stroke))
            {
                Console.WriteLine("Строка пустая введите строку");
                stroke = Console.ReadLine();
                Console.WriteLine($"Исходная строка: {stroke}");
                Console.WriteLine($"Сжатая строка: {Compress(stroke)}"); 
                Console.WriteLine($"Строка после декомпресии: {deCompress(Compress(stroke))}");            
            }
            else
            {
                Console.WriteLine($"Исходная строка: {stroke}");
                Console.WriteLine($"Сжатая строка: {Compress(stroke)}");
                Console.WriteLine($"Строка после декомпресии: {deCompress(Compress(stroke))}");
            }
            


            Console.ReadLine();
        }
        static string Compress(string stroke)
        {
            
            string result = "";
            int count = 1;
            for (int i = 0; i < stroke.Length; i++)
            {
                if (i+1 < stroke.Length && stroke[i] == stroke[i + 1])
                {
                    count++;
                }
                else
                {
                    result = result + stroke[i];
                    if (count > 1)
                    {
                        result = result + count;
                    }
                    count = 1;
                }
            }
            return result;
        }
        static string deCompress(string stroke)  // Метод Декомпресии
        {
            
            string result = "";
            for (int i = 0; i < stroke.Length; i++)
            {
                char letter = stroke[i];
                string numberStr = "";
                while (i + 1 < stroke.Length && char.IsDigit(stroke[i + 1]))
                {
                    numberStr = numberStr + stroke[i + 1];
                    i++;
                }
                if (numberStr == "")
                {
                    result = result + letter;

                }
                else
                {
                    int count = int.Parse(numberStr);
                    for (int j = 0; j < count; j++)
                    {
                        result = result + letter;
                    }
                }   
            }
            return result;
        }
    }
}

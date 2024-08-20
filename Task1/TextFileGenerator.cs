namespace Task1;

public class TextFileGenerator
{
    public static void GenerateFile(int numberOfLines, string nameOfFile="textFile.txt") 
    {
        try
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), nameOfFile)))
            {
                Console.WriteLine($"Генерация файла {nameOfFile}...");
                string finalString = "";

                Random gen = new Random();
                DateTime start = new DateTime(DateTime.Today.Year - 5, DateTime.Today.Month, DateTime.Today.Day);

                string latinLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                string russianLetters = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";

                for (int i = 0; i < numberOfLines; i++)
                {
                    finalString = "";
                    // Генерация рандомной даты
                    int range = (DateTime.Today - start).Days;           
                    DateTime date = start.AddDays(gen.Next(range));   

                    finalString += date.ToString("dd.MM.yyyy") + "||";
                    
                    // // Генерация 10 латинских символов
                    // for (int j = 0; j < 10; j++)
                    // {
                    //     finalString += (char)(101 + gen.Next(1) * 40 + gen.Next(31));
                    // }
                    // finalString += "||";

                    // Генерация 10 латинских символов
                    for (int j = 0; j < 10; j++)
                    {
                        finalString += latinLetters[gen.Next(latinLetters.Length)];
                    }
                    finalString += "||";

                    // Генерация 10 русских символов
                    for (int j = 0; j < 10; j++)
                    {
                        finalString += russianLetters[gen.Next(russianLetters.Length)];
                    }
                    finalString += "||";

                    // Генерация случайного числа от 1 до 100 000 000
                    finalString += gen.Next(100000000) + 1;
                    finalString += "||";

                    // Генерация случайного дробного числа с 8 знаками после запятой в диапазоне от 1 до 20
                    finalString += string.Format("{0:F8}", gen.NextDouble() * 20);
                    finalString += "||";

                    outputFile.WriteLine(finalString);
                }
                Console.WriteLine("Генерация файла успешно закончилась");
            }
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}

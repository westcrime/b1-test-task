namespace Task1
{
    public class TextFileGenerator
    {
        // Метод для генерации текстового файла с данными
        // Параметры:
        // - numberOfLines: количество строк, которые нужно сгенерировать
        // - nameOfFile: имя файла, в который будут записаны данные (по умолчанию "textFile.txt")
        public static void GenerateFile(int numberOfLines, string nameOfFile = "textFile.txt") 
        {
            try
            {
                // Создание StreamWriter для записи в файл
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), nameOfFile)))
                {
                    Console.WriteLine($"Генерация файла {nameOfFile}...");
                    string finalString = ""; // Строка для хранения данных одной строки файла

                    Random gen = new Random(); // Генератор случайных чисел
                    DateTime start = new DateTime(DateTime.Today.Year - 5, DateTime.Today.Month, DateTime.Today.Day); // Дата начала диапазона

                    // Строки для генерации случайных латинских и русских символов
                    string latinLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    string russianLetters = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";

                    // Генерация заданного количества строк
                    for (int i = 0; i < numberOfLines; i++)
                    {
                        finalString = "";
                        // Генерация случайной даты в пределах указанного диапазона
                        int range = (DateTime.Today - start).Days;           
                        DateTime date = start.AddDays(gen.Next(range));   
                        finalString += date.ToString("dd.MM.yyyy") + "||";

                        // Генерация 10 случайных латинских символов
                        for (int j = 0; j < 10; j++)
                        {
                            finalString += latinLetters[gen.Next(latinLetters.Length)];
                        }
                        finalString += "||";

                        // Генерация 10 случайных русских символов
                        for (int j = 0; j < 10; j++)
                        {
                            finalString += russianLetters[gen.Next(russianLetters.Length)];
                        }
                        finalString += "||";

                        // Генерация случайного целого числа от 1 до 100 000 000
                        finalString += gen.Next(100000000) + 1;
                        finalString += "||";

                        // Генерация случайного дробного числа с 8 знаками после запятой в диапазоне от 1 до 20
                        finalString += string.Format("{0:F8}", gen.NextDouble() * 20);
                        finalString += "||";

                        // Запись строки в файл
                        outputFile.WriteLine(finalString);
                    }
                    Console.WriteLine("Генерация файла успешно закончилась");
                }
            }
            catch (System.Exception ex)
            {
                // Обработка исключений и вывод сообщения об ошибке
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}

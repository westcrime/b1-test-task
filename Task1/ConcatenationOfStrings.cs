namespace Task1
{
    public class ConcatenationOfStrings
    {
        // Метод для объединения содержимого нескольких файлов в один файл
        // Параметры:
        // - fileNames: список имен файлов, которые нужно объединить
        // - outputFileName: имя выходного файла, куда будет записано объединенное содержимое
        // - removePattern: строка, определяющая шаблон, по которому будут удалены строки из файлов
        public static void ConcatenateStrings(List<string> fileNames, string outputFileName, string removePattern) 
        {
            try
            {
                Console.WriteLine($"Начало объединения файлов...");

                int removedLinesCount = 0; // Счетчик удаленных строк

                // Создание StreamWriter для записи в выходной файл
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), outputFileName)))
                {
                    // Цикл по каждому файлу в списке fileNames
                    foreach (string fileName in fileNames)
                    {
                        // Создание StreamReader для чтения текущего файла
                        using (StreamReader inputFile = new StreamReader(fileName))
                        {
                            string line;
                            // Чтение файла построчно
                            while ((line = inputFile.ReadLine()) != null)
                            {
                                // Если строка содержит шаблон removePattern, она не записывается в выходной файл
                                if (line.Contains(removePattern))
                                {
                                    removedLinesCount++;
                                }
                                else
                                {
                                    outputFile.WriteLine(line); // Запись строки в выходной файл
                                }
                            }
                        }
                    }
                }

                // Сообщение об успешном завершении объединения и количестве удаленных строк
                Console.WriteLine($"Объединение завершено. Количество удалённых строк: {removedLinesCount}");
            }
            catch (Exception ex)
            {
                // Обработка исключений и вывод сообщения об ошибке
                Console.WriteLine(ex.Message);
            }
        }
    }
}

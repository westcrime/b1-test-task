namespace Task1
{
    using Task1.Data;
    using Task1.Models;

    public class ImportToDatabaseService
    {
        // Метод для импорта данных из файлов в базу данных
        // Параметры:
        // - fileNames: массив имен файлов, из которых будет производиться импорт данных
        public static void ImportToDatabase(string[] fileNames)
        {
            try
            {
                // Цикл по каждому файлу в списке fileNames
                foreach (var fileName in fileNames)
                {
                    Console.WriteLine($"Импортируем строки из файла {fileName} в базу данных...");

                    // Создание контекста базы данных и StreamReader для чтения файла
                    using (var db = new LineDbContext())
                    using (var reader = new StreamReader(fileName))
                    {
                        int lineCount = 0; // Счетчик строк
                        List<Line> batch = new List<Line>(); // Пакет для хранения строк перед вставкой в базу данных

                        string line;
                        // Чтение файла построчно
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Разделение строки на свойства
                            var properties = line.Split("||");
                            var dbLine = new Line
                            {
                                Date = DateTime.Parse(properties[0]),          // Парсинг даты
                                LatinLetters = properties[1],                  // Латинские буквы
                                RussianLetters = properties[2],                // Русские буквы
                                IntNumber = Int32.Parse(properties[3]),        // Целое число
                                DoubleNumber = Double.Parse(properties[4])     // Число с плавающей точкой
                            };

                            batch.Add(dbLine); // Добавление строки в пакет
                            lineCount++;

                            // Пакетное сохранение каждые 1000 строк
                            if (lineCount % 1000 == 0)
                            {
                                db.Lines.AddRange(batch);  // Добавление строк в базу данных
                                db.SaveChanges();          // Сохранение изменений
                                batch.Clear();             // Очистка пакета
                                Console.WriteLine($"Импортировано: {lineCount} строк");
                            }
                        }

                        // Сохранение оставшихся строк, если они есть
                        if (batch.Count > 0)
                        {
                            db.Lines.AddRange(batch);
                            db.SaveChanges();
                        }
                        Console.WriteLine($"Импортирование строк из файла {fileName} в базу данных завершено. Всего импортировано: {lineCount} строк.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                // Обработка исключений и вывод сообщения об ошибке
                Console.WriteLine(ex.Message);
            }
        }
    }
}

namespace Task1;

using Task1.Data;
using Task1.Models;

public class ImportToDatabaseService
{
    public static void ImportToDatabase(string[] fileNames)
    {
        try
        {
            foreach (var fileName in fileNames)
            {
                Console.WriteLine($"Импортируем строки из файла {fileName} в базу данных...");

                using (var db = new LineDbContext())
                using (var reader = new StreamReader(fileName))
                {
                    int lineCount = 0;
                    List<Line> batch = new List<Line>();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var properties = line.Split("||");
                        var dbLine = new Line
                        {
                            Date = DateTime.Parse(properties[0]),
                            LatinLetters = properties[1],
                            RussianLetters = properties[2],
                            IntNumber = Int32.Parse(properties[3]),
                            DoubleNumber = Double.Parse(properties[4])
                        };

                        batch.Add(dbLine);
                        lineCount++;

                        // Пакетное сохранение каждые 1000 строк
                        if (lineCount % 1000 == 0)
                        {
                            db.Lines.AddRange(batch);
                            db.SaveChanges();
                            batch.Clear();
                            Console.WriteLine($"Импортировано: {lineCount} строк");
                        }
                    }

                    // Сохранение оставшихся строк
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
            Console.WriteLine(ex.Message);
        }
    }
}

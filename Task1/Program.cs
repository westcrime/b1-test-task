using Task1;

// Создание списка для хранения путей к файлам
List<string> files = new List<string>();

// Генерация файлов с данными и добавление их путей в список
for (int i = 1; i < 101; i++)
{
    // Генерация файла с данными. Комментарий означает, что этот код временно закомментирован.
    TextFileGenerator.GenerateFile(100000, Path.Combine("TextFilesFolder", "File" + i.ToString() + ".txt"));
    
    // Добавление пути к сгенерированному файлу в список файлов
    files.Add(Path.Combine("TextFilesFolder", "File" + i.ToString() + ".txt"));
}

// Объединение строк из файлов в один файл и удаление строк, содержащих символ "a"
ConcatenationOfStrings.ConcatenateStrings(files, "GeneralFile.txt", "a");

// Импорт данных из указанных файлов в базу данных
// Здесь передается массив путей к файлам, например, только один файл для примера
ImportToDatabaseService.ImportToDatabase(new string[] { Path.Combine("TextFilesFolder", "File1.txt") });

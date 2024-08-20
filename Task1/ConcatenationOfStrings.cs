namespace Task1;

public class ConcatenationOfStrings
{
    public static void ConcatenateStrings(List<string> fileNames, string outputFileName, string removePattern) 
    {
        try
        {
            Console.WriteLine($"Начало объеденения файлов...");

            int removedLinesCount = 0;

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), outputFileName)))
            {
                foreach (string fileName in fileNames)
                {
                    using (StreamReader inputFile = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = inputFile.ReadLine()) != null)
                        {
                            if (line.Contains(removePattern))
                            {
                                removedLinesCount++;
                            }
                            else
                            {
                                outputFile.WriteLine(line);
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Объединение завершено. Количество удалённых строк: {removedLinesCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
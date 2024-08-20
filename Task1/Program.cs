using Task1;

for (int i = 1; i < 101; i++)
{
    TextFileGenerator.GenerateFile(100000, Path.Combine("TextFilesFolder", "File" + i.ToString() + ".txt"));
}
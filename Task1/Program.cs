using Task1;

List<string> files = new List<string>();
for (int i = 1; i < 101; i++)
{
    //TextFileGenerator.GenerateFile(100000, Path.Combine("TextFilesFolder", "File" + i.ToString() + ".txt"));
    //files.Add(Path.Combine("TextFilesFolder", "File" + i.ToString() + ".txt"));
}
//ConcatenationOfStrings.ConcatenateStrings(files, "GeneralFile.txt", "a");
ImportToDatabaseService.ImportToDatabase([Path.Combine("TextFilesFolder", "File1.txt"),]);
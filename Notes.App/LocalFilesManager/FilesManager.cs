using System.Text;

namespace LocalFilesManager
{
    public class FilesManager
    {
        public static string Read(string filePath)
        {
            try
            {
                StringBuilder content = new StringBuilder();

                // Open the file for reading
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the contents of the file line by line
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // Process the line as needed
                        content.AppendLine(line);
                    }
                }

                return content.ToString();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file reading
                return "An error occurred while reading the file: " + ex.Message;
            }
        }

        public static List<FileEntry> ReadFilesFromDirectory(string directoryPath)
        {
            List<FileEntry> fileEntries = new List<FileEntry>();

            try
            {
                // Get all text files in directory
                string[] textFiles = Directory.GetFiles(directoryPath, "*.txt");

                foreach (string filePath in textFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(filePath);
                    string fileContent = Read(filePath);
                    DateTime fileCreationDate = File.GetCreationTime(filePath);

                    FileEntry file = new FileEntry
                    {
                        FileName = fileName,
                        Content = fileContent,
                        CreationDate = fileCreationDate
                    };

                    fileEntries.Add(file);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during file reading
                FileEntry errorEntry = new FileEntry
                {
                    FileName = "Error",
                    Content = "An error occurred while reading the files: " + ex.Message
                };

                fileEntries.Add(errorEntry);
            }

            return fileEntries;
        }

        public static void SaveFilesToDirectory(string directoryPath, List<FileEntry> notes)
        {
            var currentFilesInDirectory = ReadFilesFromDirectory(directoryPath);

            foreach (var note in notes)
            {
                var existingFile = currentFilesInDirectory.FirstOrDefault(f => f.CreationDate == note.CreationDate);

                if (existingFile == null)
                {
                    string filePath = Path.Combine(directoryPath, note.FileName + ".txt");
                    using (StreamWriter writer = File.CreateText(filePath))
                    {
                        writer.Write(note.Content);
                    }
                }
                else if (existingFile.FileName != note.FileName && existingFile.Content != note.Content)
                {
                    string currentFilePath = Path.Combine(directoryPath, existingFile.FileName + ".txt");
                    string newFilePath = Path.Combine(directoryPath, note.FileName + ".txt");

                    if (File.Exists(currentFilePath))
                    {
                        File.Move(currentFilePath, newFilePath);
                    }

                    File.WriteAllText(newFilePath, note.Content);
                }
                else 
                {
                    if (existingFile.FileName != note.FileName)
                    {
                        string currentFilePath = Path.Combine(directoryPath, existingFile.FileName + ".txt");
                        string newFilePath = Path.Combine(directoryPath, note.FileName + ".txt");

                        if (File.Exists(currentFilePath))
                        {
                            File.Move(currentFilePath, newFilePath);
                        }
                    }
                    else
                    {
                        string filePath = Path.Combine(directoryPath, existingFile.FileName + ".txt");
                        File.WriteAllText(filePath, note.Content);
                    }
                }
            }
        }
    }
}
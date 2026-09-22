namespace FindTop20Words
{
    internal class FileValidatior
    {
        internal bool ValidateArguments(int argsLength)
        {
            switch(argsLength)
            {
                case < 1:
                    Console.Error.WriteLine("no file provided to read");
                    return false;
                case > 1:
                    Console.Error.WriteLine("can't process more than 1 file");
                    return false;
            }
            return true;
        }

        internal bool ValidateFile(string filePath)
        {
            // Directory.Exists is checked seperately so its clear message
            // when the path points at a folder instead of a file.
            if (Directory.Exists(filePath))
            {
                Console.Error.WriteLine($"Path is a directory, not a file: {filePath}");
                return false;
            }

            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                return false;
            }

            // An empty file has no words, so there's no point reading it. We can
            // tell from the file size without opening or reading the contents.
            if (new FileInfo(filePath).Length == 0)
            {
                Console.Error.WriteLine($"File is empty: {filePath}");
                return false;
            }
            return true;
        }
    }
}

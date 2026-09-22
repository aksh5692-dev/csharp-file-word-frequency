using FindTop20Words;

// Exit codes: 0 = success, 1 = something went wrong (bad args or file error).

// Validate arguments and the file first
var validator = new FileValidatior();

if (!validator.ValidateArguments(args.Length))
{
    return 1;
}

string filePath = args[0];

if (!validator.ValidateFile(filePath))
{
    return 1;
}

try
{
    var counter = new WordCounter();
    var counts = counter.CountWords(filePath);

    // The printer decides what to show, including the "no words found" case.
    var printer = new WordPrinter();
    printer.PrintTopWords(counts, 20);

    return 0;
}
catch (UnauthorizedAccessException)
{
    Console.Error.WriteLine($"Access denied. Check you have permision to read: {filePath}");
    return 1;
}
catch (System.Security.SecurityException)
{
    Console.Error.WriteLine($"Not allowed to read file: {filePath}");
    return 1;
}
catch (IOException ex)
{
    // Covers the file being locked etc
    Console.Error.WriteLine($"Could not read file '{filePath}': {ex.Message}");
    return 1;
}
catch (Exception ex)
{
    // Covers the all types of excpetion which were not handled by above types
    Console.Error.WriteLine($"something wnt wrong while readin file '{filePath}': {ex.Message}");
    return 1;
}

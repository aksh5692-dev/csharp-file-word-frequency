using System.Text.RegularExpressions;

namespace FindTop20Words;

// Reads a file line by line and counts how often each word appears.
// considering words based on ASCII letters (a-z / A-Z), everything else is a word seperator.
public class WordCounter
{
    public Dictionary<string, int> CountWords(string filePath)
    {
        // OrdinalIgnoreCase makes counting case-insensitive, so "The" and "the"
        // are treated as the same word.
        var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // StreamReader reads the file one line at a time, so the whole file is
        // not putt in memry at once.
        using var reader = new StreamReader(filePath);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            foreach (var word in ExtractWords(line))
            {
                counts.TryGetValue(word, out int count);
                counts[word] = count + 1;
            }
        }

        return counts;
    }

    // Finds every run of ASCII letters in the line. Any other character
    // (digits, punctuation, spaces) acts as a separator.
    private static IEnumerable<string> ExtractWords(string line)
    {
        return Regex.Matches(line, "[a-zA-Z]+").Select(match => match.Value);
    }
}

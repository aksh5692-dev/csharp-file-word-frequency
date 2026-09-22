namespace FindTop20Words;

// Prints the most frequent words, ordered by count with hihest counts first
public class WordPrinter
{
    public void PrintTopWords(Dictionary<string, int> counts, int top)
    {
        // The file had content but no actual words in it (e.g. only numbers or
        // punctuation). We can only know this after counting, so it's handled here
        // rather than up front. goes to stderr so it never mixes with output.
        if (counts.Count == 0)
        {
            Console.Error.WriteLine("No words found in file.");
            return;
        }

        var topWords = counts
            .OrderByDescending(pair => pair.Value)
            .Take(top);

        foreach (var pair in topWords)
        {
            Console.WriteLine($"{pair.Value} {pair.Key}");
        }
    }
}

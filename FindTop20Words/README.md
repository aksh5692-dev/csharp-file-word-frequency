# FindTop20Words

A small C# console program that prints the 20 most frequently occurring words
in a file, with their frequencies, ordered by frequency (highest first).

It reproduces the behaviour of this reference pipeline:

`bash
cat $1 | tr -cs 'a-zA-Z' '[\n\*]' | grep -v "^$" | tr '[:upper:]' '[:lower:]' | sort | uniq -c | sort -nr | head -20
`

A word is a run of ASCII letters (`A-Z` / `a-z`); every other character is a
separator. Matching is case-insensitive.

---

## Build

Requires the .NET 8 SDK (reference platform: 64-bit Windows).

`powershell
dotnet build -c Release
`

## Run

`powershell
dotnet run -c Release -- <path-to-file>
`

A copy of Moby Dick is included as sample input:

`powershell
.\bin\Release\net8.0\FindTop20Words.exe mobydick.txt
`

---

## Design

The code is split into a thin entry point and three small classes:

| File | Responsibility |
|------|----------------|
| `Program.cs` | Wire things together and handle read errors with exit codes. |
| `FileValidatior.cs` | Validate the arguments and the file before any reading. |
| `WordCounter.cs` | Read the file line by line and count each word. |
| `WordPrinter.cs` | Print the top 20 words, ordered by count. |

Key points:

* `StreamReader` reads the file one line at a time, so the whole file is
  never loaded into memory.
* `Regex "[a-zA-Z]+"` extracts the ASCII-letter words from each line; any
  other character separates words.
* `Dictionary<string,int>` counts occurrences, using`StringComparer.OrdinalIgnoreCase` so matching is case-insensitive.
* `OrderByDescending(count).Take(20)` selects the most frequent words.

Case-insensitive counting keeps the first casing it sees for each word (e.g.
`The`), so the printed word may be capitalised while its count still includes
`the`, `THE`, etc.

Errors and notices go to stderr (so they never mix with the word output on
stdout), and the program returns exit code 0 on success or 1 on failure.

Two "empty" cases are handled in different places:

* A physically empty (0-byte) file is caught up front in `FileValidatior`, so
  it is never read.
* A file that has content but no ASCII letters (e.g. only numbers or
  punctuation) can only be detected after counting, so `WordPrinter` reports
  "No words found" in that case.

---

## Time spent

Approximately 2 hours, including implementation, testing against the
sample and a some edge cases

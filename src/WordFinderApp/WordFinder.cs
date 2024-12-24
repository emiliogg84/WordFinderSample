using System.Text;
using WordFinderApp.Helpers;

namespace WordFinderApp;

public class WordFinder
{
    private readonly ICollection<string> _matrixWords; // Resulting matrix could have repeated words so, hashset is not used here.
    private const int MATRIX_MAX_ROWS = 64;

    public WordFinder(IEnumerable<string> matrix)
    {
        ExceptionHelper.ThrowIfNull(matrix);

        // Directly process rows and columns without extra list allocation
        var matrixArray = matrix.ToArray(); // Avoid multiple enumerations
        int rows = matrixArray.Length;

        _matrixWords = new List<string>(rows * 2); // Allocate capacity for both horizontal and vertical words.

        if (rows == 0) return; // Handle empty matrix edge case

        // Validate matrix rows count
        ExceptionHelper.ThrowIfTrue(rows > MATRIX_MAX_ROWS, $"The rows of the matrix should not exceed: {MATRIX_MAX_ROWS}");
        // Validate matrix rows length
        ExceptionHelper.ThrowIfTrue(matrixArray.Any(row => row.Length != rows), "All rows in the matrix must have the same length.");

        // Add horizontal words
        foreach (var row in matrixArray)
        {
            _matrixWords.Add(row);
        }

        int cols = matrixArray[0].Length;

        // Add vertical words
        for (int col = 0; col < cols; col++)
        {
            var verticalWord = new StringBuilder(rows); // Use StringBuilder for efficiency
            for (int row = 0; row < rows; row++)
            {
                verticalWord.Append(matrixArray[row][col]);
            }
            _matrixWords.Add(verticalWord.ToString());
        }
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        ExceptionHelper.ThrowIfNull(wordstream);

        // Dictionary to count occurrences of words found in the matrix
        var wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

        // Deduplicate wordstream using a HashSet
        var uniqueWords = new HashSet<string>(wordstream, StringComparer.OrdinalIgnoreCase);

        foreach (var word in uniqueWords)
        {
            int occurrences = _matrixWords.Count(e => e.Contains(word));

            if (occurrences > 0)
            {
                wordFrequency[word] = occurrences;
            }
        }

        // Return the top 10 most repeated words, breaking ties alphabetically
        return wordFrequency
            .OrderByDescending(kv => kv.Value) // Sort by frequency (descending)
            .ThenBy(kv => kv.Key)             // Break ties alphabetically
            .Take(10)
            .Select(kv => kv.Key);            // Return only the words
    }
}
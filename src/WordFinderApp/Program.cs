using WordFinderApp;

Console.WriteLine("WordFinder APP");

Console.WriteLine("Matrix:");
Console.WriteLine("__________");

var matrix = new List<string>
        {
            "chill",
            "hello",
            "world",
            "windy",
            "cloud"
        };

foreach (var m in matrix)
{
    Console.WriteLine(m);
}

Console.WriteLine();
Console.WriteLine("Wordstream:");
Console.WriteLine("__________");

var wordstream = new List<string>
        {
            "chill", "cold", "wind", "hot", "cold", "chill", "wind"
        };

foreach (var ws in wordstream)
{
    Console.WriteLine(ws);
}

Console.WriteLine();

var wordFinder = new WordFinder(matrix);
var result = wordFinder.Find(wordstream);

Console.WriteLine("Top Words Found:");
Console.WriteLine("__________");

foreach (var word in result)
{
    Console.WriteLine(word);
}
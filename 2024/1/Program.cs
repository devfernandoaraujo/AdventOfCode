using _1;
FileManager fileManager = new FileManager();

(List<long> Left, List<long> Right) lists = fileManager.getLists("2024/1/input.txt");

var total = lists.Left.Select((v, index) => Math.Abs(v - lists.Right[index]))
    .Sum();


Console.WriteLine($"Total elements in left list: {total}");

List<long> scores = fileManager.similarityScore("2024/1/input.txt");

Console.WriteLine($"Similarity score: {scores.Sum()}");
 
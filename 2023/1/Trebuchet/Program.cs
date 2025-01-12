// See https://aka.ms/new-console-template for more information

List<string> records = Data.GetPart1();

int sum = 0;
foreach (string record in records)
{
   sum += int.Parse(record.First(c => char.IsDigit(c)) + "" + record.Last(c => char.IsDigit(c)));
}
Console.WriteLine($"Part one value is {sum}");

sum = 0;
// Part 2 can have string representations of numbers such as one two three up to nine
// So we need to convert the string to a number
records = Data.GetPart2();

var numberMap = new Dictionary<string, int>
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 }
        };


sum += records.Sum(record =>
        {
            // Get the first and last characters or words
            int firstValue = GetFirstValue(record.Trim(), numberMap);
            int lastValue = GetLastValue(record.Trim(), numberMap);
            Console.WriteLine($"First value: {firstValue}, Last value: {lastValue}");

            return Int32.Parse(firstValue + "" +  lastValue);
        });

Console.WriteLine($"Part two value is {sum}");

static int GetFirstValue(string record, Dictionary<string, int> numberMap)
{
    // Find the first number word or digit in the record using LINQ
    var wordMatch = numberMap
        .Where(kvp => record.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) != -1)
        .Select(kvp => new
        {
            Value = kvp.Value,
            Index = record.IndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase)
        })
        .OrderBy(match => match.Index)
        .FirstOrDefault();

    // Find the first digit in the record
    var digitMatch = record
        .Select((c, i) => new { Character = c, Index = i })
        .Where(x => char.IsDigit(x.Character))
        .OrderBy(x => x.Index)
        .FirstOrDefault();

    // Compare the indexes and return the earliest value
    if (wordMatch != null && (digitMatch == null || wordMatch.Index < digitMatch.Index))
    {
        return wordMatch.Value;
    }

    return digitMatch != null ? int.Parse(digitMatch.Character.ToString()) : 0;
}

static int GetLastValue(string record, Dictionary<string, int> numberMap)
{
    // Check for the last digit in the record
    // Find the first number word or digit in the record using LINQ
    var wordMatch = numberMap
        .Where(kvp => record.LastIndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase) != -1)
        .Select(kvp => new
        {
            Value = kvp.Value,
            Index = record.LastIndexOf(kvp.Key, StringComparison.OrdinalIgnoreCase)
        })
        .OrderByDescending(match => match.Index)
        .FirstOrDefault();

    // Find the first digit in the record
    var digitMatch = record
        .Select((c, i) => new { Character = c, Index = i })
        .Where(x => char.IsDigit(x.Character))
        .OrderByDescending(x => x.Index)
        .FirstOrDefault();

    // Compare the indexes and return the earliest value
    if (wordMatch != null && (digitMatch == null || wordMatch.Index > digitMatch.Index))
    {
        return wordMatch.Value;
    }

    return digitMatch != null ? int.Parse(digitMatch.Character.ToString()) : 0;
}



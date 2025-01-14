using RedNosedReport;

FileManager fileManger = new FileManager("input.txt");


Console.WriteLine(fileManger.getSafeReports());
Console.WriteLine(fileManger.getSafeReports(true));

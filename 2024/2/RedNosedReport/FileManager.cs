using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace RedNosedReport;

public class FileManager
{

    private string fileName;
    public FileManager(string fileName)
    {
        this.fileName = fileName;
    }

    private List<List<int>> getReports()
    {
        List<List<int>> reports = File.ReadAllLines(fileName)
             .Select(line => line.Split(' ')) 
             .Select(parts => parts.Select(int.Parse).ToList()) 
             .ToList();

        return reports;

    }

    public int getSafeReports(bool isProblemDampener = false)
    {
        List<List<int>> reports = getReports();
        int safeReports = 0;
        
        reports.ForEach(report => {
            
            var isOrderedAsc = this.isOrderedAsc(report);
            var isOrderedDesc = this.isOrderedDesc(report);

            if(!isOrderedAsc && !isOrderedDesc && isProblemDampener)
            {
                //Try to remove the sequence that is preventing the order
                
                var tempReport = removeSequence(report);

                if(tempReport != null)
                {
                    report = tempReport;
                    isOrderedAsc = this.isOrderedAsc(report);
                    isOrderedDesc = this.isOrderedDesc(report);
                }
            }

            if (isOrderedAsc || isOrderedDesc)
            {
                var isValidAdjacent = report.Zip(report.Skip(1), (a, b) => Math.Abs(a - b))
                          .All(differ => differ >=1 && differ <= 3);

                if (isValidAdjacent)
                    safeReports++;
            }
            
        });

        return safeReports;
    }

    private bool isOrderedAsc(List<int> report)
    {
        return report.OrderBy(report => report).SequenceEqual(report);
    }

    private bool isOrderedDesc(List<int> report)
    {
        return report.OrderByDescending(report => report).SequenceEqual(report);
    }

    private List<int> removeSequence(List<int> report)
    {
        List<int> tempReport = report;

        bool isReportValid = false;

        isReportValid = report.Any( v => {
           
            tempReport = new List<int>(report);

            tempReport.Remove(v);

            if (isOrderedAsc(tempReport) || isOrderedDesc(tempReport))
            {
                return true;
            }

            return false;

        });

        if(isReportValid)
            return tempReport;
        
        return null;
    }
}

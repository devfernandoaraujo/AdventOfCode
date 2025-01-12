using System.IO;
using System.Collections.Generic;
namespace _1;

public class FileManager
{
    public (List<long> LeftList, List<long> RightList) getLists(string fileName)
    {
        (List<long> leftList, List<long> rightList)  = mountLists(fileName);

         leftList = leftList.OrderBy(x => x).ToList();
         rightList = rightList.OrderBy(x => x).ToList();

        return (leftList, rightList);

    }

    public List<long> similarityScore(string fileName)
    {
        (List<long> leftList, List<long> rightList) = mountLists(fileName);

        return leftList.Select((v) => v * rightList.Where(r => r==v).Count()).ToList();
    }

    private (List<long> LeftList, List<long> RightList) mountLists(string fileName){
        List<string> lines = new List<string>(File.ReadAllLines(fileName));
        List<long> leftList = new List<long>(), rightList= new List<long>();   

        foreach (string line in lines)
        {
            string[] parts = line.Split(' ');
            leftList.Add(long.Parse(parts.First().Trim())); 
            rightList.Add(long.Parse(parts.Last().Trim()));
        }

        return (leftList, rightList);
    }
}

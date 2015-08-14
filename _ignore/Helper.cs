
using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Helper
{
    private static HashSet<string> _excluded = new HashSet<string>(new[] { "obj", "bin", "Properties", "_ignore" });

    public static string CreateSingleModule(string rootPath)
    {
        var dirList = GetDirectories(
            new Stack<string>(),
            new Queue<string>(new List<string>() { { rootPath } }));

        var classFiles = new List<string>();

        foreach (var dir in dirList) {
            Directory.GetFiles(dir, "*.cs").ToList().ForEach(classFiles.Add);
        }

        return ParseFiles(classFiles);
    }

    private static IEnumerable<string> GetDirectories(Stack<string> output, Queue<string> input)
    {
        if (!input.Any()) {
            return output;
        }

        var dir = input.Dequeue();

        new DirectoryInfo(dir).EnumerateDirectories()
            .Where(x => !_excluded.Contains(x.Name))
            .Where(x => !x.Name.StartsWith("."))
            .Select(x => x.FullName)
            .ToList()
            .ForEach(input.Enqueue);

        output.Push(dir);

        return GetDirectories(output, input);
    }

    private static string ParseFiles(List<string> filePaths)
    {
        var classLines = new List<string>();
        var usingsSet = new HashSet<string>();

        Func<string, bool> isUsingStmt = (x) => x.StartsWith("using");

        var lines = filePaths.Select(x => File.ReadAllLines(x)).SelectMany(x => x).ToList();

        foreach (var line in filePaths.Select(x => File.ReadAllLines(x)).SelectMany(x => x)) {

            if (isUsingStmt(line)) {
                usingsSet.Add(line);

            } else {

                classLines.Add(line);
            }

        }

        var result = usingsSet.ToList();

        result.AddRange(classLines);

        var builder = new StringBuilder();

        result.ForEach(x => builder.AppendLine(x));

        return builder.ToString();
    }
}

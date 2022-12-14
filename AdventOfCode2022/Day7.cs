namespace AdventOfCode2022;

internal class Day7
{
    private readonly int _totalSpace = 70000000;
    private readonly int _neededSpace = 30000000;

    public Queue<string> Lines { get; set; } = new();
    public Directory CurrentDirectory { get; set; }

    public Day7()
    {
        CurrentDirectory = new Directory("/");
    }

    public void Run()
    {
        var input = Helper.ReadTextFile("7.txt");
        input.ForEach(Lines.Enqueue);

        while (Lines.Count > 0)
        {
            ExecuteCommand(Lines.Dequeue());
        }

        Console.WriteLine("\nCalculating all the sizes...");
        var rootDirectory = CurrentDirectory.GetRootDirectory();
        rootDirectory.CalculateSize();

        var sumOfSmallDirecoriesSize = FileSystemItem.AllDirectories
            .Where(d => d.Size <= 100000)
            .Select(d => d.Size)
            .Sum();
        Console.WriteLine($"\nSum of small directories size: {sumOfSmallDirecoriesSize}");

        int freeSpace = _totalSpace - (int)rootDirectory.Size!;
        int spaceToFreeUp = _neededSpace - freeSpace;
        var deletionCandidate = FileSystemItem.AllDirectories
            .Where(d => d.Size >= spaceToFreeUp)
            .OrderBy(d => d.Size)
            .ToList()[0];
        Console.WriteLine($"Space to free up: {spaceToFreeUp}, best candidate directory size: {deletionCandidate.Size}");
    }

    private void ExecuteCommand(string line)
    {
        if (!IsCommand(line))
            throw new Exception("Line is not a command.");

        var lineParts = line.Split(' ');
        string command = lineParts[1];
        switch (command)
        {
            case "ls":
                ListContents();
                break;
            case "cd":
                string target = lineParts[2];
                ChangeDirectory(target);
                Console.WriteLine($"{Lines.Count} Changed to directory: {CurrentDirectory.Name}");
                break;
        }
    }

    private bool IsCommand(string line) => line[..2] == "$ ";

    private void ListContents()
    {
        Console.WriteLine($"{Lines.Count} Listing content:");
        while ((Lines.Count > 0) && (!IsCommand(Lines.Peek())))
        {
            var lineParts = Lines.Dequeue().Split(' ');
            string part1 = lineParts[0];
            string name = lineParts[1];

            if (part1 == "dir")
            {
                CurrentDirectory.Directories.Add(new Directory(name));
                Console.WriteLine($"  {Lines.Count} Added directory: {name}");
            }
            else if (int.TryParse(part1, out int size))
            {
                CurrentDirectory.Files.Add(new File(name, size));
                Console.WriteLine($"  {Lines.Count} Added file: {name}");
            }
            else
                throw new Exception("Fail.");
        }
    }

    private void ChangeDirectory(string target)
    {
        switch (target)
        {
            case "/":
                GoToRootDirectory();
                break;
            case "..":
                UpOneLevel();
                break;
            default:
                GoToDirectory();
                break;
        }

        void GoToRootDirectory() => CurrentDirectory = CurrentDirectory.GetRootDirectory();

        void UpOneLevel()
        {
            var parentDirectory = CurrentDirectory.GetParentDirectory();
            if (parentDirectory == null)
                throw new Exception("Can't go up from root directory.");
            CurrentDirectory = parentDirectory;
        }

        void GoToDirectory()
        {
            var directory = CurrentDirectory.Directories.FirstOrDefault(d => d.Name == target);
            if (directory == null)
                throw new ArgumentException($"Can't find directory \"{target}\".");
            CurrentDirectory = directory;
        }
    }
}

internal abstract class FileSystemItem
{
    protected static List<FileSystemItem> _allItems = new();

    public static List<Directory> AllDirectories => _allItems.Where(fsi => fsi is Directory).Cast<Directory>().ToList();
    public static List<File> AllFiles => _allItems.Where(fsi => fsi is File).Cast<File>().ToList();
    public string Name { get; set; }
    public int? Size { get; set; } = null;

    public FileSystemItem(string name)
    {
        Name = name;
        _allItems.Add(this);
    }

    public Directory GetRootDirectory()
    {
        var rootDirectory = _allItems.FirstOrDefault(fsi => fsi.Name == "/") as Directory;
        if (rootDirectory == null)
            throw new Exception("Can't find root directory.");
        return rootDirectory;
    }

    public Directory? GetParentDirectory()
    {
        return AllDirectories.FirstOrDefault(d => d.Directories.Contains(this) || d.Files.Contains(this));
    }
}

internal class Directory : FileSystemItem
{
    public List<Directory> Directories { get; set; } = new();
    public List<File> Files { get; set; } = new();

    public Directory(string name)
        : base(name)
    {
    }

    public void CalculateSize()
    {
        var badFile = Files.FirstOrDefault(f => f.Size == null);
        if (badFile != null)
            throw new Exception($"File \"{badFile.Name}\" has no size.");

        var uncalculatedDirectories = Directories.Where(d => d.Size == null).ToList();
        foreach (var directory in uncalculatedDirectories)
            directory.CalculateSize();

        Size = Files.Select(f => f.Size).Sum() + Directories.Select(d => d.Size).Sum();
    }
}

internal class File : FileSystemItem
{

    public File(string name)
        : base(name)
    {
    }

    public File(string name, int size)
        : base(name)
    {
        Size = size;
    }
}
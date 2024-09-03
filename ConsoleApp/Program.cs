namespace ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var fileReader = new FileReader();
            var contents = fileReader.ReadFileAsStrings("C:\\Users\\carlo\\Downloads\\sv.po");

            await foreach (var line in contents)
            {
                Console.WriteLine(line);
            }
        }
    }

    public interface IFileReader
    {
        IAsyncEnumerable<string?> ReadFileAsStrings(string filePath);
    }

    public class FileReader : IFileReader
    {
        public async IAsyncEnumerable<string?> ReadFileAsStrings(string filePath)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(filePath);

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File is not a present or is accessible", nameof(filePath));
            }

            await using var fileStream = File.OpenRead(filePath);
            using var streamReader = new StreamReader(fileStream);

            while (!streamReader.EndOfStream)
            {
                yield return await streamReader.ReadLineAsync();
            }
        }
    }
}

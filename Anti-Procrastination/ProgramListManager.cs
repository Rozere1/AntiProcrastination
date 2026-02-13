

namespace Anti_Procrastination
{
    public class ProgramListManager : IService
    {
        private readonly string  path = @$"{Directory.GetCurrentDirectory()}\Lists";
        public List<string> ReadAList(string file)
        {
            var programList = new List<string>();
            var pathToFile = @$"{path}\{file}";
            if (!File.Exists(pathToFile))
            {
                var w = File.Create(pathToFile);
                w.Dispose();
            }
            using var streamReader = new StreamReader(pathToFile);

            try
            {
                programList = streamReader.ReadToEnd().Split('\n', StringSplitOptions.TrimEntries).ToList();
            }
            catch
            {
                Console.WriteLine("Ошибка при чтении");
            }
            return programList;


        }
    }
}

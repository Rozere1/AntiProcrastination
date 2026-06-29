namespace Anti_Procrastination.Services
{
    public static class ProgramListManager
    {
        private static readonly string path = @$"{Directory.GetCurrentDirectory()}\Lists";
        public static List<string> ReadAList(string file)
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
                programList = streamReader.ReadToEnd()
                    .ToLower()
                    .Split('\n', StringSplitOptions.TrimEntries)
                    .ToList();
            }
            catch
            {

            }
            return programList;


        }
    }
}

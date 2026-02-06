

namespace Anti_Procrastination
{
    public class ProgramListManager
    {
        private string path = @$"{Directory.GetCurrentDirectory()}\Lists";
        public List<string> ReadAList(string file)
        {
            var programList = new List<string>();
            var pathToFile = @$"{path}\{file}";
            using (StreamReader streamReader = new StreamReader(pathToFile))
            {

                List<string> rawJobList = streamReader.ReadToEnd().Split('\n').ToList();
                for (int i = 0; i < rawJobList.Count; i++)
                {
                    {
                        if (rawJobList[i].StartsWith('#'))
                            rawJobList.Remove(rawJobList[i]);
                    }
                    programList = rawJobList;
                    Logger.Write($"BlackList Programs Count: {programList.Count}\nPrograms:\n[");
                    foreach (var program in programList)
                    {
                        Logger.Write($"{program}");
                    }
                    Logger.Write("]");
                }
                return programList;
            }


        }
    }
}

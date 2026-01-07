using System;

namespace Anti_Procrastination
{
    public class ProgramListManager
    {
        private string path = @$"{Directory.GetCurrentDirectory()}\Lists";
        public List<string> ReadAList(string file)
        {
            var programList = new List<string>();
            var pathToFile = @$"{path}\{file}";
            if (!Directory.Exists(pathToFile))
            {
                var fileProcess = File.Create(pathToFile);
                fileProcess.Close();
            }
            using (StreamReader streamReader = new StreamReader(pathToFile))
            {
                string rawJobList = streamReader.ReadToEnd();
                programList = rawJobList.Split("\n").ToList();
                Logger.Write("JobList Count: " + programList.Count.ToString());
                foreach (var program in programList)
                {
                    Logger.Write(program);
                }
            }
            return programList;
        }
        public void WriteToText(List<string> programList, string file)
        {
            var pathToFile = @$"{path}\{file}";
            using(StreamWriter sWriter = new StreamWriter(pathToFile, true))
            {
                foreach(var program in programList)
                {
                    sWriter.WriteLine(program);
                }
            }
            Logger.Write($"Writing in the {file} was done");
        }
    }
}

namespace Anti_Procrastination
{
    public class DirectoriesDictionary
    {
        private Dictionary<int, string> directories = new Dictionary<int, string>();
        private int indexGetting;
        private bool isFirst = true;
        public int Count { get { return directories.Count; } }
        public void SetValue(int index, string value)
        {
            directories.Add(index, value);
            if (isFirst)
            {
                indexGetting = index;
                isFirst = false;
            }
        }
        public string GetValueWithIndex(int index)
        {
            string fileWithIndex;
            fileWithIndex = $"{index} {directories[index]}";
            return fileWithIndex;
        }

        public string GetValue(int index)
        {
            return directories[index];
        }

        public void Clear()
        {
            directories.Clear();
            isFirst = true;
        }
    }
}

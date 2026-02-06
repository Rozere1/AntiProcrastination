namespace Anti_Procrastination
{
    public class FilesDictionary
    {
        private Dictionary<int, string> files = new Dictionary<int, string>();
        private int indexGetting;
        private bool isFirst = true;
        public int Count { get { return files.Count; } }
        public void SetValue(int index, string value)
        {
            files.Add(index, value);
            if (isFirst)
            {
                indexGetting = index;
                isFirst = false;
            }
        }
        public string GetValueWithIndex(int index)
        {
            string fileWithIndex;
            if (index >= indexGetting)
            {
                fileWithIndex = $"{index} {files[index]}";
                return fileWithIndex;
            }
            fileWithIndex = $"{index + indexGetting} {files[indexGetting + index]}";
            return fileWithIndex;
        }
        public string GetValue(int index)
        {
            if (index >= indexGetting)
                return files[index];
            return files[indexGetting + index];
        }

        public void Clear()
        {
            files.Clear();
            isFirst = true;
        }

    }
}

namespace PanDataCollector.NpInput
{
    public interface INPInput
    {
        ReadData GetRead();
        void CloseConnections();
    }
    
    public struct ReadData
    {
        public int id;
        public string quality;
        public string data;
        public int[] signals; //mabye empty
    }
}
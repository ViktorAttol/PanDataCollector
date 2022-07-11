using System;

namespace PanDataCollector.NpInput
{
    public class NPInput: INPInput
    {
        private int currId = 0;
        public ReadData GetRead()
        {
            ReadData rd = new ReadData();
            rd.id = currId;
            currId++;
            rd.data = "aasdasdasdasdasdasd";
            rd.quality = "12331231aydyxcyx";
            rd.signals = new int[] {115, 112, 113, 224, 114};
            return rd;
        }

        public void CloseConnections()
        {
            Console.WriteLine("NPInput::ConnectionsClosed");
        }
    }
}
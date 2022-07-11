using System;
using System.Collections.Generic;
using PanDataCollector.NpInput;

namespace PanDataCollector.PhenotypeConnector
{
    public class PhenotypeConnector: IPhenotypeConnector
    {
        public void CalculatePhenotypes(List<ReadData> reads)
        {
            throw new NotImplementedException();
        }

        public void SubscribeForPhenotypeResults(Action<List<PhenotypeData>> cbReceivePhenotypeFunc)
        {
            throw new NotImplementedException();
        }

        public void UnSubscribeForPhenotypeResults(Action<List<PhenotypeData>> cbReceivePhenotypeFunc)
        {
            throw new NotImplementedException();
        }
    }
}
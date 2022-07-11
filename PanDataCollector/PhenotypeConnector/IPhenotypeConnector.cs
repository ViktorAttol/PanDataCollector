using System;
using System.Collections.Generic;
using PanDataCollector.NpInput;

namespace PanDataCollector.PhenotypeConnector
{
    public interface IPhenotypeConnector
    {
        /// <summary>
        /// Calculates all phenotypes with the given reads
        /// </summary>
        /// <param name="reads"></param>
        void CalculatePhenotypes(List<ReadData> reads);
        
        /// <summary>
        /// Subscribe to get phenotype data when found
        /// </summary>
        /// <param name="receiver"></param>
        void SubscribeForPhenotypeResults(Action<List<PhenotypeData>> cbReceivePhenotypeFunc);
        void UnSubscribeForPhenotypeResults(Action<List<PhenotypeData>> cbReceivePhenotypeFunc);

    }
}
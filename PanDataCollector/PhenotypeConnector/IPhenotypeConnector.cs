using System;
using System.Collections.Generic;

namespace PanDataCollector.PhenotypeConnector
{
    public interface IPhenotypeConnector
    {
        /// <summary>
        /// Calculates all phenotypes with the given reads
        /// </summary>
        /// <param name="reads"></param>
        void CalculatePhenotypes(List<string> reads);
        
        /// <summary>
        /// Subscribe to get phenotype data when found
        /// </summary>
        /// <param name="receiver"></param>
        void SubscribeForPhenotypeResults(IPhenotypeResultReceiver receiver);
        void UnSubscribeForPhenotypeResults(IPhenotypeResultReceiver receiver);

    }
}
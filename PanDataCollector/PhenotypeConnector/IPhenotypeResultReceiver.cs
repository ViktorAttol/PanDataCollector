using System.Collections.Generic;

namespace PanDataCollector.PhenotypeConnector
{
    public enum Phenotype
    {
        Eye, Hair, Skin
    }
    
    public interface IPhenotypeResultReceiver
    {
        /// <summary>
        /// Receives the data of a single found phenotype
        /// </summary>
        /// <param name="data"></param>
        void ReceivePhenotypeResult(List<PhenotypeData> data);
    }

    public struct PhenotypeData
    {
        public Phenotype phenotype;
        public string color;
        public float probability;
    }
}
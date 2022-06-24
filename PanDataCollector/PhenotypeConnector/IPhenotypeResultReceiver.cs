namespace PanDataCollector.PhenotypeConnector
{
    public enum Phenotype
    {
        Haircolor, Eyecolor, Skincolor
    }
    
    public interface IPhenotypeResultReceiver
    {
        /// <summary>
        /// Receives the data of a single found phenotype
        /// </summary>
        /// <param name="result"></param>
        void ReceivePhenotypeResult(PhenotypeResult result);
    }

    public struct PhenotypeResult
    {
        public Phenotype phenotype;
        public string color;
        public float percentage;
    }
}
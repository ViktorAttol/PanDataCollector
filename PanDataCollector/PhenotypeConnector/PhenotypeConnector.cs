using System;
using System.Collections.Generic;
using PanDataCollector.NpInput;

namespace PanDataCollector.PhenotypeConnector
{
    public class PhenotypeConnector: IPhenotypeConnector
    {
        private string[] colors = new[] {"white, red, green, brown, black, yellow, blue"};

        private Action<List<PhenotypeData>> cbPhenotypeData;
        private List<PhenotypeData> phenotypeData;

        public PhenotypeConnector()
        {
            phenotypeData = new List<PhenotypeData>();
            FillWithDummyData();
        }
        
        public void CalculatePhenotypes(List<ReadData> reads)
        {
            Console.WriteLine("PhenotypeConnector::CalculatePhenotypes--Called");
            OnPhenotypesCalculated();
        }

        private void OnPhenotypesCalculated()
        {
            cbPhenotypeData?.Invoke(phenotypeData);
        }

        public void SubscribeForPhenotypeResults(Action<List<PhenotypeData>> cbReceivePhenotypeFunc)
        {
            cbPhenotypeData += cbReceivePhenotypeFunc;
        }

        public void UnSubscribeForPhenotypeResults(Action<List<PhenotypeData>> cbReceivePhenotypeFunc)
        {
            cbPhenotypeData -= cbReceivePhenotypeFunc;
        }

        private void FillWithDummyData()
        {
            Random rnd = new Random();
            for (int i = 0; i < 9; i++)
            {
                PhenotypeData pd = new PhenotypeData();
                pd.phenotype = (Phenotype)rnd.Next(0, 3);
                pd.color = colors[rnd.Next(0, colors.Length)];
                pd.probability = 1.0f;
                phenotypeData.Add(pd);
            }
        }
    }
}
using System;
using PanDataCollector.DataCollectorController;
using PanDataCollector.NpInput;
using PanDataCollector.PhenotypeConnector;
using PanDataCollector.VisuConnector;

namespace PanDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            StateMachine stateMachine = StateMachine.Instance;
            stateMachine.SetNPInput(new NPInput());
            IPhenotypeConnector phenotypeConnector = new PhenotypeConnector.PhenotypeConnector();
            stateMachine.SetPhenotypeConnector(phenotypeConnector);
            phenotypeConnector.SubscribeForPhenotypeResults(stateMachine.ReceivePhenotypeResult);
            IVisuConnector visuConnector = new VisuConnector.VisuConnector();
            stateMachine.SetVisuConnector(visuConnector);
            visuConnector.SubscribeForConnectionChangeStatus(stateMachine.ConnectionStatusChanged);
            stateMachine.RunStateMachine();
        }
    }
}
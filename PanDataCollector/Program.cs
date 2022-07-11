using System;
using PanDataCollector.DataCollectorController;
using PanDataCollector.NpInput;
using PanDataCollector.VisuConnector;

namespace PanDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            StateMachine stateMachine = StateMachine.Instance;
            stateMachine.SetNPInput(new NPInput());
            stateMachine.SetPhenotypeConnector(new PhenotypeConnector.PhenotypeConnector());
            IVisuConnector visuConnector = new VisuConnector.VisuConnector();
            stateMachine.SetVisuConnector(visuConnector);
            visuConnector.SubscribeForConnectionChangeStatus(stateMachine.ConnectionStatusChanged);
            stateMachine.RunStateMachine();
        }
    }
}
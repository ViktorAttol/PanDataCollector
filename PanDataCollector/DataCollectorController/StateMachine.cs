using System.Collections.Generic;
using PanDataCollector.NpInput;
using PanDataCollector.PhenotypeConnector;
using PanDataCollector.VisuConnector;

namespace PanDataCollector.DataCollectorController
{
    public enum CollectorState
    {
        Init, Connect, Reconnect, WaitForConnection, Start, Read, WaitForPhenotypeCalculation, End, Exit
    }
    
    public class StateMachine: IConnectionStatusChangeReceiver, IPhenotypeResultReceiver
    {
        private static StateMachine instance = null;
        
        private StateMachine(){}
        public static StateMachine Instance => instance ??= new StateMachine();

        private IVisuConnector visuConnector;

        public void SetVisuConnector(IVisuConnector visuConnector)
        {
            this.visuConnector = visuConnector;
        }

        private INPInput npInput;
        
        public void SetNPInput(INPInput npInput)
        {
            this.npInput = npInput;
        }

        private IPhenotypeConnector phenotypeConnector;
        
        public void SetPhenotypeConnector(IPhenotypeConnector phenotypeConnector)
        {
            this.phenotypeConnector = phenotypeConnector;
        }

        
        private CollectorState state = CollectorState.Init;
        private ConnectionStatus connectionStatus = ConnectionStatus.NotConnected;

        private bool shouledRunning = true;

        private List<ReadData> reads = new List<ReadData>();

        private int requiredReads = 41;

        private List<PhenotypeData> phenotypeDatas;

        public void RunStateMachine()
        {
            while (shouledRunning)
            {
                SwitchState();
            }
        }
        
        public void SwitchState()
        {
            switch (state)
            {
                case CollectorState.Init:
                    CaseInit();
                    break;
                case CollectorState.WaitForConnection:
                    CaseWaitForConnection();
                    break;
                case CollectorState.Connect:
                    CaseConnect();
                    break;
                case CollectorState.WaitForPhenotypeCalculation:
                    WaitForPhenotypeCalculation();
                    break;
                case CollectorState.End:
                    CaseEnd();
                    break;
                case CollectorState.Exit:
                    CaseExit();
                    break;
            }
        }

        // initialise Connection
        private void CaseInit()
        {
            visuConnector.ConnectWithVisu();
            state = CollectorState.WaitForConnection;
        }

        private void CaseWaitForConnection()
        {
            if (state == CollectorState.WaitForConnection && connectionStatus == ConnectionStatus.Connected)
            {
                state = CollectorState.Connect;
            }
            else visuConnector.ConnectWithVisu();
        }
        
        private void CaseConnect()
        {
            ReadData read = npInput.GetRead();
            reads.Add(read);
            visuConnector.SendRead(read);
            if (reads.Count == requiredReads)
            {
                phenotypeConnector.CalculatePhenotypes(reads);
                state = CollectorState.WaitForPhenotypeCalculation;
            }
        }

        private void WaitForPhenotypeCalculation()
        {
            if (phenotypeDatas.Count > 0)
            {
                visuConnector.SendPhenotypes(phenotypeDatas);
                state = CollectorState.End;
            }
        }
        
        private void CaseEnd()
        {
            visuConnector.DisconnectFromVisu();
            npInput.CloseConnections();
            state = CollectorState.Exit;
        }
        
        private void CaseExit()
        {
            shouledRunning = false;
        }

        public void ConnectionStatusChanged(ConnectionStatus status)
        {
            connectionStatus = status;
        }

        public void ReceivePhenotypeResult(List<PhenotypeData> data)
        {
            phenotypeDatas = data;
        }
    }
}
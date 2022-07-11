using System;
using System.Collections.Generic;
using PanDataCollector.DataCollectorController;
using PanDataCollector.NpInput;
using PanDataCollector.PhenotypeConnector;

namespace PanDataCollector.VisuConnector
{
    public interface IVisuConnector
    {
        /// <summary>
        /// Sends the current state of the stateMachine to connected software
        /// </summary>
        /// <param name="state"></param>
        void SendState(CollectorState state);
        
        /// <summary>
        /// Sends single read to connected software
        /// </summary>
        /// <param name="read"></param>
        void SendRead(ReadData read);
        
        /// <summary>
        /// Sends date of a single found phenotype 
        /// </summary>
        /// <param name="phenotype"></param>
        void SendPhenotypes(List<PhenotypeData> Data);
        
        /// <summary>
        /// Tries to connect with visualisation software via sockets
        /// </summary>
        void ConnectWithVisu();

        /// <summary>
        /// Disconnects from visualisation software
        /// </summary>
        void DisconnectFromVisu();

        /// <summary>
        /// Subscribe to be notified about connection status changes
        /// </summary>
        /// <param name="receiver"></param>
        void SubscribeForConnectionChangeStatus(Action<ConnectionStatus> cbConnectionChangedFunc);
        void UnSubscribeForConnectionChangeStatus(Action<ConnectionStatus> cbConnectionChangedFunc);

    }
}
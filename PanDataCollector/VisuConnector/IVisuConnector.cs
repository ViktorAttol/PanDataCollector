using PanDataCollector.DataCollectorController;
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
        void SendRead(string read);
        
        /// <summary>
        /// Sends date of a single found phenotype 
        /// </summary>
        /// <param name="phenotype"></param>
        void SendPhenotype(Phenotype phenotype);
        
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
        void SubscribeForConnectionChangeStatus(IConnectionChangeReceiver receiver);
        void UnSubscribeForConnectionChangeStatus(IConnectionChangeReceiver receiver);

    }
}
namespace PanDataCollector.VisuConnector
{
    public enum ConnectionStatus
    {
        Connected, ConnectionFailed, ConnectionLost, ConnectionClosed
    }
    
    public interface IConnectionChangeReceiver
    {
        /// <summary>
        /// Is called by subscriber to notify about connection status changes
        /// </summary>
        /// <param name="status"></param>
        void ReceiveConnectionStatus(ConnectionStatus status);
    }
}
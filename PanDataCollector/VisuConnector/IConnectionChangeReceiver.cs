namespace PanDataCollector.VisuConnector
{
    public enum ConnectionStatus
    {
        Connected, ConnectionFailed, ConnectionLost, ConnectionClosed
    }
    
    public interface IConnectionChangeReceiver
    {
        /// <summary>
        /// Is called by subscriber to get notified about connection status changes
        /// </summary>
        /// <param name="status"></param>
        void ReceiveConnectionStatus(ConnectionStatus status);

        /// <summary>
        /// Changes the connection status
        /// </summary>
        /// <param name="status"></param>
        void ChangeConnectionStatus(ConnectionStatus status);
    }
}
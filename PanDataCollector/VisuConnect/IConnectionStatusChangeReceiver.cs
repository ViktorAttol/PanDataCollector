namespace PanDataCollector.VisuConnector
{
    public enum ConnectionStatus
    {
        NotConnected, Connected, ConnectionFailed, ConnectionLost, ConnectionClosed
    }
    
    public interface IConnectionStatusChangeReceiver
    {
        /// <summary>
        /// Changes the connection status
        /// </summary>
        /// <param name="status"></param>
        void ConnectionStatusChanged(ConnectionStatus status);
    }
}
using System;

namespace PanDataCollector.VisuConnector
{
    // Reminder: dotnet build / dotnet run
    class ConnectionChangeReceiver : IConnectionChangeReceiver
    {
        private ConnectionStatus connectionStatus;

        public void ReceiveConnectionStatus(ConnectionStatus status)
        {
            Console.WriteLine("Visu connector got the status: " + connectionStatus);
        }

        public void ChangeConnectionStatus(ConnectionStatus status)
        {
            connectionStatus = status;
        }
    }
}

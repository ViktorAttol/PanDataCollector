using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using PanDataCollector.DataCollectorController;
using PanDataCollector.NpInput;
using PanDataCollector.PhenotypeConnector;

namespace PanDataCollector.VisuConnector
{
    // Reminder: dotnet build / dotnet run
    class VisuConnector : IVisuConnector
    {
        private TcpClient client;
        private Socket sender = null;
        private NetworkStream networkStream = null;
        private StreamWriter streamWriter = null;
        private StreamReader streamReader = null;

        private Action<ConnectionStatus> cbConnectionChanged;

        private int counter = 0;

        public void SendState(CollectorState state)
        {
            if (streamWriter != null)
            {
                counter++;
                streamWriter.WriteLine(counter);
                streamWriter.WriteLine("state");
                streamWriter.WriteLine(state);
                streamWriter.Flush();

            }
        }

        public void SendRead(ReadData read)
        {
            if (streamWriter != null)
            {
                counter++;
                streamWriter.WriteLine(counter);
                streamWriter.WriteLine("read");
                
                streamWriter.WriteLine(read.id);
                streamWriter.WriteLine(read.quality);
                streamWriter.WriteLine(read.data);
                streamWriter.WriteLine(read.signals.Length);
                foreach (var signal in read.signals)
                {
                    streamWriter.WriteLine(signal);
                }
                
                streamWriter.Flush();

            }
        }

        public void SendPhenotypes(List<PhenotypeData> phenotypeData)
        {
            if (streamWriter != null)
            {
                counter++;
                streamWriter.WriteLine(counter);
                streamWriter.WriteLine("phenotype");
                
                streamWriter.WriteLine(phenotypeData.Count);
                foreach (var data in phenotypeData)
                {
                    streamWriter.WriteLine((int)data.phenotype);
                    streamWriter.WriteLine(data.color);
                    streamWriter.WriteLine(data.probability.ToString());
                }
                
                streamWriter.Flush();

            }
        }

        public void ConnectWithVisu()
        {
            try
            {
                client = new TcpClient();
                client.Connect("localhost", 8080);
                networkStream = client.GetStream();
                /*
                IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11100);
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                */
                try
                {
                    //sender.Connect(localEndPoint);
                    OnConnectionChanged(ConnectionStatus.Connected);
                    //Console.WriteLine("Socket connected to {0} ", sender.RemoteEndPoint.ToString());

                    //networkStream = new NetworkStream(sender);
                    streamWriter = new StreamWriter(networkStream);
                    streamReader = new StreamReader(networkStream);
                }

                catch (ArgumentNullException ane)
                {
                    OnConnectionChanged(ConnectionStatus.ConnectionFailed);
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {
                    OnConnectionChanged(ConnectionStatus.ConnectionFailed);
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    OnConnectionChanged(ConnectionStatus.ConnectionFailed);
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void DisconnectFromVisu()
        {
            try
            {
                Console.WriteLine("Client disconnected!");
                SendState(CollectorState.End);
                streamWriter.Close();
                networkStream.Close();
                //sender.Shutdown(SocketShutdown.Both);
                //sender.Close();
                client.Close();
                OnConnectionChanged(ConnectionStatus.ConnectionClosed);
            }

            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }

            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        private void OnConnectionChanged(ConnectionStatus status)
        {
            cbConnectionChanged?.Invoke(status);
        }
        
        public void SubscribeForConnectionChangeStatus(Action<ConnectionStatus> cbConnectionChangedFunc)
        {
            cbConnectionChanged += cbConnectionChangedFunc;
        }

        public void UnSubscribeForConnectionChangeStatus(Action<ConnectionStatus> cbConnectionChangedFunc)
        {
            cbConnectionChanged -= cbConnectionChangedFunc;
        }
    }

}

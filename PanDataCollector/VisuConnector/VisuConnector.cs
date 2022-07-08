using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

using PanDataCollector.DataCollectorController;
using PanDataCollector.PhenotypeConnector;

namespace PanDataCollector.VisuConnector
{
    // Reminder: dotnet build / dotnet run
    class ExporterProgram : IVisuConnector
    {
        private Socket sender = null;
        private NetworkStream networkStream = null;
        private StreamWriter streamWriter = null;
        private StreamReader streamReader = null;

        private List<IConnectionChangeReceiver> receiverList = new List<IConnectionChangeReceiver>();

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

        public void SendRead(string read)
        {
            if (streamWriter != null)
            {
                counter++;
                streamWriter.WriteLine(counter);
                streamWriter.WriteLine("read");
                streamWriter.WriteLine(read);
                streamWriter.Flush();

            }
        }

        public void SendPhenotype(Phenotype phenotype)
        {
            if (streamWriter != null)
            {
                counter++;
                streamWriter.WriteLine(counter);
                streamWriter.WriteLine("phenotype");
                streamWriter.WriteLine(phenotype);
                streamWriter.Flush();

            }
        }

        public void ConnectWithVisu()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry("127.0.0.1");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11100);

                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(localEndPoint);

                    Console.WriteLine("Socket connected to {0} ", sender.RemoteEndPoint.ToString());

                    networkStream = new NetworkStream(sender);
                    streamWriter = new StreamWriter(networkStream);
                    streamReader = new StreamReader(networkStream);
                }

                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
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
                streamWriter.Close();
                networkStream.Close();
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
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

        public void SubscribeForConnectionChangeStatus(IConnectionChangeReceiver receiver)
        {
            receiverList.Add(receiver);
            Console.WriteLine("Receiver has been added!");
        }

        public void UnSubscribeForConnectionChangeStatus(IConnectionChangeReceiver receiver)
        {
            receiverList.Remove(receiver);
            Console.WriteLine("Receiver has been removed!");
        }
    }

}

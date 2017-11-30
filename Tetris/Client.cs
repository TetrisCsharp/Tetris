using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_Like
{
    class Client
    {
        public void StartClient()
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 80);
                byte[] bytes = new byte[1024];
                string message = "";

                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());


                    byte[] msg = Encoding.ASCII.GetBytes("This is a test : Hello<EOF>");
                    int byteSent = sender.Send(msg);

                    Console.WriteLine("------------Message envoyé---------------");
                    int bytesRec = sender.Receive(bytes);

                    Console.WriteLine("Reçu test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    while (message != "quit")
                    {
                        message = Console.ReadLine();
                        msg = Encoding.ASCII.GetBytes(message);
                        sender.Send(msg);

                        bytesRec = sender.Receive(bytes);
                        Console.WriteLine("Reçu test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                        if (message == "quit")
                        {
                            msg = Encoding.ASCII.GetBytes("A+ ");
                            sender.Send(msg);
                            sender.Shutdown(SocketShutdown.Both);
                            sender.Close();
                        }
                    }
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
    }
}

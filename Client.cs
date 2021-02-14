using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class Client
{
    public int Start(IPAddress remoteIPAddress) 
    {
        if (remoteIPAddress == null) return 1;
        byte[] buffer = new byte[1024];
        try
        {
            IPEndPoint remoteEP = new IPEndPoint(remoteIPAddress, 33333);
            Socket socket = new Socket(remoteIPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        }
        catch (System.Exception)
        {
            
            throw;
        }
    }
}
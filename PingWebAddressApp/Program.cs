using System.Net.NetworkInformation;
using System;
using System.Net.Sockets;

namespace PingWebAddressApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //This Code Using For Ping Address Without Port - For Using this return Main Method to void or Modified this code

            //Enter Address Without http:// or https://
            //Console.Write("Enter the website address (e.g., www.example.com): ");
            //string? address = Console.ReadLine();

            //try
            //{
            //    using (Ping pingSender = new Ping())
            //    {
            //        PingReply reply = pingSender.Send(address);

            //        if (reply.Status == IPStatus.Success)
            //        {
            //            Console.WriteLine($"Ping to {address} successful!");
            //            Console.WriteLine($"Roundtrip time: {reply.RoundtripTime}ms");
            //            Console.WriteLine($"Time to live: {reply.Options.Ttl}");
            //            Console.WriteLine($"Don't fragment: {reply.Options.DontFragment}");
            //            Console.WriteLine($"Buffer size: {reply.Buffer.Length} bytes");
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Ping to {address} failed: {reply.Status}");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error: {ex.Message}");
            //}

            //////////////////////////////////////////////////////////////////////////////////////////////////////

            //Enter Address Without http:// or https://
            //Console.Write("Enter the website address (e.g., www.example.com): ");
            //string? address = Console.ReadLine();

            //Console.Write("Enter the starting port number: ");
            //if (!int.TryParse(Console.ReadLine(), out int startPort))
            //{
            //    Console.WriteLine("Invalid starting port number.");
            //    return;
            //}

            //Console.Write("Enter the ending port number: ");
            //if (!int.TryParse(Console.ReadLine(), out int endPort) || endPort < startPort)
            //{
            //    Console.WriteLine("Invalid ending port number. It must be greater than or equal to the starting port.");
            //    return;
            //}

            //for (int i = startPort; i <= endPort; i++)
            //{
            //    port = i;
            //    using (TcpClient tcpClient = new TcpClient())
            //    {
            //        try
            //        {
            //            Console.WriteLine($"{address} on port {i}");
            //            tcpClient.Connect(address, i);
            //            if (tcpClient.Connected)
            //                Console.WriteLine($"Successfully connected to {address} on port {i}.");
            //        }
            //        catch (Exception)
            //        {


            //        }
            //    }
            //}

            //Console.WriteLine("Finished...");
            //Console.WriteLine();

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////

            //Enter Address Without http:// or https://
            Console.Write("Enter the website address (e.g., www.example.com): ");
            string? address = Console.ReadLine();

            Console.Write("Enter the starting port number: ");
            if (!int.TryParse(Console.ReadLine(), out int startPort))
            {
                Console.WriteLine("Invalid starting port number.");
                return;
            }

            Console.Write("Enter the ending port number: ");
            if (!int.TryParse(Console.ReadLine(), out int endPort) || endPort < startPort)
            {
                Console.WriteLine("Invalid ending port number. It must be greater than or equal to the starting port.");
                return;
            }

            for (int i = startPort; i <= endPort; i++)
            {
                using (TcpClient tcpClient = new TcpClient())
                {
                    Console.WriteLine($"{address} on port {i}");
                    bool isConnected = await IsPortOpen(address, i, TimeSpan.FromSeconds(1));
                    if (isConnected)
                    {
                        Console.WriteLine($"Successfully connected to {address} on port {i}.");
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Finished...");
            Console.WriteLine();
        }

        private static async Task<bool> IsPortOpen(string address, int port, TimeSpan timeout)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    var connectTask = tcpClient.ConnectAsync(address, port);
                    if (await Task.WhenAny(connectTask, Task.Delay(timeout)) == connectTask)
                    {
                        // Successfully connected
                        return tcpClient.Connected;
                    }
                    else
                    {
                        // Timed out
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}

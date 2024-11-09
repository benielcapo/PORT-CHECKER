using System.Net.Sockets;

class YEAH
{
    public async static Task Main()
    {
        Console.WriteLine("Write the server you want to check for open ports");
        string SERVER = Console.ReadLine() ?? "invalid";
        Console.WriteLine("Write the starting port (integers only)");
        string PORT_NOINT = Console.ReadLine() ?? "1";
        if (int.TryParse(PORT_NOINT, out var PORT))
        {
            Console.WriteLine("Write the end port");
            if (int.TryParse(Console.ReadLine(), out var END_PORT))
            {
                for (int i = PORT; i < END_PORT; i++)
                {
                    Console.WriteLine("Trying " + i.ToString() + ". Tries left: " + (END_PORT - i).ToString());
                    try
                    {
                        var CANCEL = new CancellationTokenSource(2000);
                        using TcpClient CLIENT = new();
                        await CLIENT.ConnectAsync(SERVER, i, CANCEL.Token);
                        if (CLIENT.Connected)
                        {
                            Console.WriteLine($"Connection succeeded with port {i}");
                        }
                        else
                        {
                            Console.WriteLine($"Connection failed with port {i}");
                        }
                        CLIENT.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: {ex.Message}");
                    }
                }
                Console.WriteLine("Restarting");
                await Main();
            } else
            {
                await Main();
            }
        } else
        {
            await Main();
        }
    }
}
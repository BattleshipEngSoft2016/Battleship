using System;
using System.Collections.Generic;
using System.Text;
using Servidor;
using Servidor.manager;
using Servidor.netGameManager;
using Servidor.netGamaManager;
using System.Threading;

namespace Servidor
{

    /* CLASSE: Program
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERS�O: beta 1.1
     * 
     * Classe para a cria��o de um servidor de jogos. As mensagens de log aparecera��o na tela 
     *   e o servidor ser� encerrado quando o usu�rio digitar 'quit', 'stop', 'exit' ou 'halt'.
     * O servidor � criado nos endere�o e porta passados pelo usu�rio, ou nos endere�o e porta
     *   padr�o, caso o programa seja executado sem nenhum par�metro de entrada.
     */

    class Program
    {
        static void Main(string[] args)
        {

            String host = "172.16.36.85";
            int port = 8623;
            if (args.Length == 2)
            {
                try
                {
                    host = args[0];
                    port = int.Parse(args[1]);
                }
                catch (Exception e)
                {
                    host = "172.16.36.85";
                    port = 8623;
                    Console.Error.WriteLine("Unable to parse input parameters. Using default parameters. " + e.Message);
                }
            }
            GameServer server = new GameServer(host,port);
            server.startServer();
            Console.WriteLine("Press 'quit', 'exit', 'stop' or 'halt' to stop the server.");

            while (true)
            {
                string line = Console.ReadLine();
                line = line.ToLower();
                if (line.Contains("stop") || line.Contains("halt") || line.Contains("exit") || line.Contains("quit"))
                {
                    server.stopServer();
                    break;
                }
            }
            
        }

    }
}
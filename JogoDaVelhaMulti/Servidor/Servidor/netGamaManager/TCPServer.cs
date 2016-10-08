using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Servidor.netGameManager;

namespace Servidor.netGamaManager
{

    /* CLASSE: TCPServer
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERSÃO: beta 1.1
     * 
     * Classe para a criação e gerenciamento da escuta em uma porta utilizando
     *   o protocolo TCP. É recomendável que a execução do método 
     *   startTCPServer ocorra em uma thread exclusiva para ele.
     */

    class TCPServer
    {
        public string ip = "172.16.36.85";
        public int port = 6666;
        public bool ended = false;
        TcpListener listener = null;
        GameServer server = null;


        /* Construtor padrão da classe UPDServer. Recebe o ip do servidor,
         *   a porta de comunicação e a instância do servidor de jogos que 
         *   está sendo gerenciada.
         * Parâmetros:
         *   pIp: IP (ou endereço html) do servidor
         *   pPort: número da porta de comunicação;
         *   pServer: instância do servidor de jogos que está relacionada
         *            ao IP e à porta passadas.
         */
        public TCPServer(String pIp, int pPort, GameServer pServer)
        {
            ip = pIp;
            port = pPort;
            server = pServer;
        }

        /* Método que gerencia o processo de monitoramento da porta de 
         *   comunicação. É recomendado que este método seja executado em uma
         *   thread exclusiva.
         * Além de iniciar o processo de escuta de uma porta, este método
         *   recebe as requisições das aplicações clientes, encaminha ao
         *   servidor de jogos (referenciado pela variável server) e envia
         *   a resposta das mensagens aos clientes.
         */
        public void startTCPServer()
        {

            Console.WriteLine("Starting TCP echo at ip: " + ip + " port: " + port);

            listener = new TcpListener(IPAddress.Parse(ip), port);
            listener.Start();

            TcpClient ourTCP_Client = null;
            NetworkStream ourStream = null;

            while (!ended)
            {
                string message = "";
                string answer_message = "";
                byte[] response = new byte[65530];
                try
                {

                    ourTCP_Client = listener.AcceptTcpClient();
                    ourStream = ourTCP_Client.GetStream();
                    byte[] data = new byte[ourTCP_Client.ReceiveBufferSize];
                    int bytesRead = ourStream.Read(data, 0, System.Convert.ToInt32(ourTCP_Client.ReceiveBufferSize));
                    // echo the data we got to the console until the newline, and delay closing our window.
                    message = Encoding.ASCII.GetString(data, 0, bytesRead);

                    Console.WriteLine("Received: '" + message + "'");

                    AddressPort ClientAddress = new AddressPort(IPAddress.Parse("172.16.36.85"), 8623);
                    RequestMessage req_message = new RequestMessage(message, ClientAddress);
                    AnswerMessage am = GameServer.executeRequest(req_message);
                    if (am.GID >= 0)
                    {
                        am.GAME_GENERAL_STATUS = GameServer.gameGeneralStatus(am.GID, req_message.PLAYER);
                    }
                    else
                    {
                        am.GAME_GENERAL_STATUS = AnswerMessage.GGS_NOTHING_TO_BE_DONE;
                    }
                    answer_message = am.message2string();

                    response = Encoding.ASCII.GetBytes(answer_message);
                }
                catch (Exception e)
                {

                    if (System.Type.Equals(new SocketException().GetType(),e.GetType()))
                    {
                        ended = true;

                        if (ourTCP_Client != null)
                        {
                            ourStream.Flush();
                            ourStream.Close();
                            ourTCP_Client.Close();
                            
                        }
                        listener.Stop();

                        return;
                    }
                    else
                    {
                        Console.Error.WriteLine("ERROR: " + message + "  " + e.Message);
                        answer_message = new AnswerMessage(false, AnswerMessage.UNKNOWN_ERROR, "", -1).message2string();
                        response = Encoding.ASCII.GetBytes(answer_message);
                    }

                }
                if (!ended)
                {
                    server.insertLogMessage("INPUT", message);
                    server.insertLogMessage("OUTPUT", answer_message);
                    Console.WriteLine("Sending: " + answer_message);
                    ourStream.Write(response, 0, response.Length);
                    ourStream.Flush();
                    ourTCP_Client.Close();
                }
            }
            return;
        }

        /* 
         * Método que encerra a escuda de uma dada porta de comunicação.
         */
        public void stopTCPServer()
        {
            ended = true;
            Console.WriteLine("Stopping TCP echo at ip: " + ip + " port: " + port);
            if (listener != null)
            {
             
                listener.Stop();        
            }
            return;
        }

    }
}
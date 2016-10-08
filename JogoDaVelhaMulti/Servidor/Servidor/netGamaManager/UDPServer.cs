using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Servidor.netGameManager;
namespace Servidor.netGamaManager
{

    /* CLASSE: UDPServer
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERS�O: beta 1.1
     * 
     * Classe para a cria��o e gerenciamento da escuta em uma porta utilizando
     *   o protocolo UDP. � recomend�vel que a execu��o do m�todo 
     *   startUDPServer ocorra em uma thread exclusiva para ele.
     */

    class UDPServer
    {
        public string ip = "172.16.36.85";
        public int port = 8623;
        public Socket socket = null;
        public bool ended = false;
        public GameServer server = null;

        /* Construtor padr�o da classe UPDServer. Recebe o ip do servidor,
         *   a porta de comunica��o e a inst�ncia do servidor de jogos que 
         *   est� sendo gerenciada.
         * Par�metros:
         *   pIp: IP (ou endere�o html) do servidor
         *   pPort: n�mero da porta de comunica��o;
         *   pServer: inst�ncia do servidor de jogos que est� relacionada
         *            ao IP e � porta passadas.
         */
        public UDPServer(String pIp, int pPort, GameServer pServer)
        {
            ip = pIp;
            port = pPort;
            server = pServer;
        }
        
        /* M�todo que gerencia o processo de monitoramento da porta de 
         *   comunica��o. � recomendado que este m�todo seja executado em uma
         *   thread exclusiva.
         * Al�m de iniciar o processo de escuta de uma porta, este m�todo
         *   recebe as requisi��es das aplica��es clientes, encaminha ao
         *   servidor de jogos (referenciado pela vari�vel server) e envia
         *   a resposta das mensagens aos clientes.
         */
        public void startUDPServer()
        {
            Console.WriteLine("Starting UDP echo at ip: " + ip + " port: " + port);

            int receivedDataLength;
            byte[] data = new byte[65530];

            IPEndPoint ipEP = new IPEndPoint(IPAddress.Parse(ip), port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint tmpRemote = (EndPoint)(sender);

            socket.Bind(ipEP);
            while (!ended)
            {
                string message = "";
                string answer_message = "";
                byte[] response = new byte[65530];
                try
                {
                    sender = new IPEndPoint(IPAddress.Any, 0);
                    tmpRemote = (EndPoint)(sender);


                    data = new byte[65530];
                    receivedDataLength = socket.ReceiveFrom(data, ref tmpRemote);
                    message = Encoding.ASCII.GetString(data, 0, receivedDataLength);
                    Console.WriteLine("Received: '" + message + "'");

                    AddressPort ClientAddress = new AddressPort(sender.Address, sender.Port);
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
                    if (System.Type.Equals(new SocketException().GetType(), e.GetType()))
                    {
                        ended = true;
                        return;
                    }
                    else
                    {

                        Console.Error.WriteLine("ERROR: " + message + "  " + e.ToString());
                        answer_message = new AnswerMessage(false, AnswerMessage.UNKNOWN_ERROR, "", -1).message2string();
                        response = Encoding.ASCII.GetBytes(answer_message);
                    }
                }
                if (!ended)
                {
                    Console.WriteLine("Sending: " + answer_message);
                    socket.SendTo(response, response.Length, SocketFlags.None, tmpRemote);
                    server.insertLogMessage("INPUT", message);
                    server.insertLogMessage("OUTPUT", answer_message);
                }
            }
        }

        /* 
         * M�todo que encerra a escuda de uma dada porta de comunica��o.
         */
        public void stopUDPServer()
        {
            ended = true;
            Console.WriteLine("Stopping UDP echo at ip: " + ip + " port: " + port);
            if (socket != null)
            {
                 socket.Close(1);
            }
        }
    }
}
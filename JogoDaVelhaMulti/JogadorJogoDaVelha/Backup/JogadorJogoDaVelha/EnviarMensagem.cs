using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace JogadorJogoDaVelha
{
    /* CLASSE: EnviarMensagem
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERS�O: beta 1.1
     * 
     * Esta classe envia uma mensagem ao servidor e recebe a resposta.
     * Para evitar que os programas que precisam se comunicar com o servidor 
     *   travem (nem que seja momentaneamente), � recomendado que o m�todo
     *   executarEnvio seja executado em uma nova thread.
     */
    class EnviarMensagem
    {
        private string mensagem = "";
        private string host = "";
        private int port = 0;
        private string[] resposta = null;

        /* Construtor padr�o para a classe Enviar Mensagem. Recebe todos
         *   os par�metros que ser�o utilizados no envio de mensagens:
         * pMensagem: mensagem que ser� enviada (produzida pela classe MensagemDeRequisicao
         * pHost: endere�o IP (ou nome) do servidor de jogos
         * pPort: n�mero da porta de comuni��o do servidor
         * pResposta: vari�vel onde a sa�da ser� retornada
         */
        public EnviarMensagem(string pMensagem, string pHost, int pPort, string[] pResposta)
        {
            mensagem = pMensagem;
            host = pHost;
            port = pPort;
            resposta = pResposta;
        }

        /* Envia a mensagem xml para o servidor utilizando o protocolo UDP.
         * Este m�todo deve ser executado numa nova thread a fim de evitar que
         *   seu programa principal trave enquanto aguarda a resposta do 
         *   servidor (que pode estar desconectado).
         * A resposta do servidor � colocada na primeira posi��o do arranjo 
         *   resposta[0]
         */
        public void executarEnvio()
        {
            System.Net.IPAddress addr = null;

            try
            {
                System.Net.IPAddress.TryParse(host, out addr);
            }
            catch (Exception uhe)
            {
                MessageBox.Show("Endere�o do servidor inv�lido: " + host + ". Mensagem de erro:  '" + uhe.Message + "'.");
            }

            try
            {
                System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(host);
                System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, port);
                Socket m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                try
                {
                    m_socClient.Connect(remoteEP);
                }
                catch (Exception exc)
                {
                    MessageBox.Show("N�o foi poss�vel se conectar com o servidor (" + host.ToString() + ":" + port.ToString() + "). " + exc.Message);
                    resposta[0] = null;
                }
                try
                {
                    String szData = mensagem;
                    byte[] byData = System.Text.Encoding.ASCII.GetBytes(szData);
                    int res = m_socClient.Send(byData);
                }
                catch (SocketException se)
                {
                    MessageBox.Show(se.Message);
                }

                byte[] buffer = new byte[65535];
                int iRx = m_socClient.Receive(buffer);
                string resp = "";
                for (int i = 0; i < 65535; i++)
                {
                    if (buffer[i] == 0) break;
                    resp += (char)buffer[i];
                }
                resposta[0] = resp;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                resposta[0] = null;
            }
            
        }
    }
}
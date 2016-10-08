using Servidor.netGameManager;
using System.Collections;
using System;

namespace Servidor.netGameManager
{

    /* CLASSE: AddressPort
     * AUTOR: Luciano Antonio Digiampietri
     * DATA: 03/11/2008
     * VERS�O: beta 1.1
     * 
     * Esta classe serve para juntar dois atributos: address e port.
     * address: cont�m o endere�o IP (por exemplo, do servidor de jogos 
     *          ou da aplica��o cliente)
     * port: n�mero da porta de comunica��o (relacionada ao endere�o)
     */

    public class AddressPort
    {
        public String getClass2()
        {
            return "AddressPort";
        }
        public static String getClass()
        {
            return "AddressPort";
        }
 
        public System.Net.IPAddress address;
        public int port;
        public AddressPort(System.Net.IPAddress a, int p)
        {
            address = a;
            port = p;
        }
    }
}